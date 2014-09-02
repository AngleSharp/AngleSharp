namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-left-radius
    /// </summary>
    sealed class CSSBorderBottomLeftRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderBottomLeftRadiusProperty
    {
        #region ctor

        internal CSSBorderBottomLeftRadiusProperty()
            : base(PropertyNames.BorderBottomLeftRadius)
        {
        }

        #endregion
    }
}
