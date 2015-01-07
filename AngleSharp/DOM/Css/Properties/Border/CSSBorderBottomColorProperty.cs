namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-color
    /// </summary>
    sealed class CssBorderBottomColorProperty : CssBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CssBorderBottomColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomColor, rule)
        {
        }

        #endregion
    }
}
