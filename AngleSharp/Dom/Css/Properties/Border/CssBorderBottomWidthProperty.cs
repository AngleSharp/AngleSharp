namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-width
    /// </summary>
    sealed class CssBorderBottomWidthProperty : CssBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CssBorderBottomWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomWidth, rule)
        {
        }

        #endregion
    }
}
