namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-style
    /// </summary>
    sealed class CssBorderTopStyleProperty : CssBorderPartStyleProperty, ICssBorderStyleProperty
    {
        #region ctor

        internal CssBorderTopStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopStyle, rule)
        { 
        }

        #endregion
    }
}
