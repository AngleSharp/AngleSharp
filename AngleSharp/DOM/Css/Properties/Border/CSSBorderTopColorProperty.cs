namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    sealed class CssBorderTopColorProperty : CssBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CssBorderTopColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopColor, rule)
        { 
        }

        #endregion
    }
}
