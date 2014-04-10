namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// </summary>
    sealed class CSSColumnCountProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-width) should be considered.
        /// </summary>
        Int32? _count;

        #endregion

        #region ctor

        public CSSColumnCountProperty()
            : base(PropertyNames.ColumnCount)
        {
            _count = null;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var count = value.ToInteger();

            if (count.HasValue)
                _count = count.Value;
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("auto", StringComparison.OrdinalIgnoreCase))
                _count = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
