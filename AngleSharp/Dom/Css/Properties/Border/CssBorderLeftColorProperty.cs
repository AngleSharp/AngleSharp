namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-color
    /// </summary>
    sealed class CssBorderLeftColorProperty : CssBorderPartColorProperty
    {
        #region ctor

        internal CssBorderLeftColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderLeftColor, rule)
        { 
        }

        #endregion
    }
}
