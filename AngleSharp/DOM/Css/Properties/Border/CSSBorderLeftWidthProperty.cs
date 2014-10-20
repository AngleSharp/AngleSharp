namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-width
    /// </summary>
    sealed class CSSBorderLeftWidthProperty : CSSBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CSSBorderLeftWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderLeftWidth, rule)
        {
        }

        #endregion
    }
}
