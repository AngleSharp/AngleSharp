namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top
    /// </summary>
    sealed class CSSBorderTopProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderTopProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderTop, rule)
        {
        }

        #endregion
    }
}
