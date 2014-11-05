namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CSSColumnsProperty : CSSShorthandProperty, ICssColumnsProperty
    {
        #region Fields

        readonly CSSColumnCountProperty _count;
        readonly CSSColumnWidthProperty _width;

        #endregion

        #region ctor

        internal CSSColumnsProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Columns, rule, PropertyFlags.Animatable)
        {
            _count = Get<CSSColumnCountProperty>();
            _width = Get<CSSColumnWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width for the columns, if set.
        /// </summary>
        public Length? Width
        {
            get { return _width.Width; }
        }

        /// <summary>
        /// Gets the count for the columns, if set.
        /// </summary>
        public Int32? Count
        {
            get { return _count.Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList ?? new CSSValueList(value);
            CSSValue width = null, count = null;

            if (list.Length > 2)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_width.CanStore(list[i], ref width) && !_count.CanStore(list[i], ref count))
                    return false;
            }

            return _width.TrySetValue(width) && _count.TrySetValue(count);
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Concat(_width.SerializeValue(), " ", _count.SerializeValue());
        }

        #endregion
    }
}
