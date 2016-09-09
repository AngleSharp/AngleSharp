namespace AngleSharp
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides a pool of used / recycled resources.
    /// </summary>
    static class Pool
    {
        #region Fields

        private static readonly Stack<StringBuilder> _builder = new Stack<StringBuilder>();
        private static readonly Stack<CssSelectorConstructor> _selector = new Stack<CssSelectorConstructor>();
        private static readonly Stack<CssValueBuilder> _value = new Stack<CssValueBuilder>();
        private static readonly Object _lock = new Object();

        #endregion

        #region Methods

        /// <summary>
        /// Either creates a fresh stringbuilder or gets a (cleaned) used one.
        /// </summary>
        /// <returns>A stringbuilder to use.</returns>
        public static StringBuilder NewStringBuilder()
        {
            lock (_lock)
            {
                if (_builder.Count == 0)
                {
                    return new StringBuilder(1024);
                }

                return _builder.Pop().Clear();
            }
        }

		/// <summary>
		/// Either creates a fresh selector constructor or gets a (cleaned)
        /// used one.
		/// </summary>
		/// <returns>A selector constructor to use.</returns>
		public static CssSelectorConstructor NewSelectorConstructor(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
		{
			lock (_lock)
			{
                if (_selector.Count == 0)
                {
                    return new CssSelectorConstructor(attributeSelector, pseudoClassSelector, pseudoElementSelector);
                }

				return _selector.Pop().Reset(attributeSelector, pseudoClassSelector, pseudoElementSelector);
			}
		}

        /// <summary>
        /// Either creates a fresh value builder or gets a (cleaned) used one.
        /// </summary>
        /// <returns>A value builder to use.</returns>
        public static CssValueBuilder NewValueBuilder()
        {
            lock (_lock)
            {
                if (_value.Count == 0)
                {
                    return new CssValueBuilder();
                }

                return _value.Pop().Reset();
            }
        }

        /// <summary>
        /// Returns the given stringbuilder to the pool and gets the current
        /// string content.
        /// </summary>
        /// <param name="sb">The stringbuilder to recycle.</param>
        /// <returns>The string that is created in the stringbuilder.</returns>
        public static String ToPool(this StringBuilder sb)
        {
            var result = sb.ToString();

            lock (_lock)
            {
                _builder.Push(sb);
            }

            return result;
        }

		/// <summary>
		/// Returns the given selector constructor to the pool and gets the
        /// constructed selector.
		/// </summary>
        /// <param name="ctor">The constructor to recycle.</param>
        /// <returns>The Selector that is created in the constructor.</returns>
		public static ISelector ToPool(this CssSelectorConstructor ctor)
        {
            var result = ctor.GetResult();

			lock (_lock)
			{
				_selector.Push(ctor);
            }

            return result;
		}

        /// <summary>
        /// Returns the given value builder to the pool and gets the
        /// constructed value.
        /// </summary>
        /// <param name="vb">The builder to recycle.</param>
        /// <returns>The value that is contained in the builder.</returns>
        public static CssValue ToPool(this CssValueBuilder vb)
        {
            var result = vb.GetResult();

            lock (_lock)
            {
                _value.Push(vb);
            }

            return result;
        }

        #endregion
    }
}
