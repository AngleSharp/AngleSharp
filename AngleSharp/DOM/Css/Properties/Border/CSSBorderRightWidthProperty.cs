namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-width
    /// </summary>
    sealed class CSSBorderRightWidthProperty : CSSBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CSSBorderRightWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderRightWidth, rule)
        {
        }

        #endregion
    }
}
