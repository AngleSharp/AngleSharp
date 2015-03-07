namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-width
    /// </summary>
    sealed class CssBorderTopWidthProperty : CssBorderPartWidthProperty
    {
        #region ctor

        internal CssBorderTopWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopWidth, rule)
        {
        }

        #endregion
    }
}
