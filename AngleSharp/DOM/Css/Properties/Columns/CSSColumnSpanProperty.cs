namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// </summary>
    sealed class CSSColumnSpanProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Content in the normal flow that appears before the element is automatically
        /// balanced across all columns before the element appears. The element
        /// establishes a new block formatting context.
        /// </summary>
        Boolean _span;

        #endregion

        #region ctor

        public CSSColumnSpanProperty()
            : base(PropertyNames.ColumnSpan)
        {
            _span = false;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                //The element does not span multiple columns.
                if (ident.Equals("none", StringComparison.OrdinalIgnoreCase))
                    _span = false;
                //The element spans across all columns.
                else if (ident.Equals("all", StringComparison.OrdinalIgnoreCase))
                    _span = true;
                else
                    return false;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
