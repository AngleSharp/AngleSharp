namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// </summary>
    sealed class CSSColumnWidthProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-count) should be considered.
        /// </summary>
        Length? _width;

        #endregion

        #region ctor

        public CSSColumnWidthProperty()
            : base(PropertyNames.ColumnWidth)
        {
            _width = null;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var width = value.ToLength();

            if (width.HasValue)
                _width = width.Value;
            else if (value.Is("auto"))
                _width = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
