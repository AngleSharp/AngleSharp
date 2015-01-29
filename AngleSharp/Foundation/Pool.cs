namespace AngleSharp
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Provides a pool of used / recycled resources.
    /// </summary>
    [DebuggerStepThrough]
    static class Pool
    {
        #region Fields

		static readonly Stack<StringBuilder> _builder;
        static readonly Stack<CssSelectorConstructor> _selector;
        static readonly Object _lock;

        #endregion

        #region ctor

        static Pool()
        {
            _builder = new Stack<StringBuilder>();
			_selector = new Stack<CssSelectorConstructor>();
            _lock = new Object();
        }

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
                    return new StringBuilder(1024);

                return _builder.Pop().Clear();
            }
        }

		/// <summary>
		/// Either creates a fresh selector constructor or gets a (cleaned) used one.
		/// </summary>
		/// <returns>A selector constructor to use.</returns>
		public static CssSelectorConstructor NewSelectorConstructor()
		{
			lock (_lock)
			{
				if (_selector.Count == 0)
					return new CssSelectorConstructor();

				return _selector.Pop().Reset();
			}
		}

        /// <summary>
        /// Returns the given stringbuilder to the pool and gets the current
        /// string content.
        /// </summary>
        /// <param name="sb">The stringbuilder to recycle.</param>
        /// <returns>The string that is contained in the stringbuilder.</returns>
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
        /// <returns>The Selector that is contained in the constructor.</returns>
		public static ISelector ToPool(this CssSelectorConstructor ctor)
        {
            var result = ctor.Result;

			lock (_lock)
			{
				_selector.Push(ctor);
            }

            return result;
		}

        #endregion
    }
}
