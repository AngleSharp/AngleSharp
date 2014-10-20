namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CSSBorderRightProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderRightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderRight, rule)
        {
        }

        #endregion
    }
}
