namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-width
    /// </summary>
    sealed class CssBorderLeftWidthProperty : CssBorderPartWidthProperty
    {
        #region ctor

        internal CssBorderLeftWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderLeftWidth, rule)
        {
        }

        #endregion
    }
}
