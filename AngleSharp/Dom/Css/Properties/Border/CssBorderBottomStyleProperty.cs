namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    sealed class CssBorderBottomStyleProperty : CssBorderPartStyleProperty
    {
        #region ctor

        internal CssBorderBottomStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomStyle, rule)
        { 
        }

        #endregion
    }
}
