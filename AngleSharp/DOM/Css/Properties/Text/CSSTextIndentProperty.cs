namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent
    /// </summary>
    sealed class CSSTextIndentProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Indentation is a percentage of the containing block width.
        /// OR Indentation is specified as fixed length.
        /// Negative values are allowed.
        /// </summary>
        CSSCalcValue _indent;

        #endregion

        #region ctor

        public CSSTextIndentProperty()
            : base(PropertyNames.TextIndent)
        {
            _inherited = true;
            _indent = CSSCalcValue.Zero;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var indent = value.ToCalc();

            if (indent != null)
                _indent = indent;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
