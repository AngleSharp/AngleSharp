namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-width
    /// </summary>
    sealed class CssBorderRightWidthProperty : CssBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CssBorderRightWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRightWidth, rule)
        {
        }

        #endregion
    }
}
