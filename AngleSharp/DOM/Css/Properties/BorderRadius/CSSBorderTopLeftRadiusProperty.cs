namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-left-radius
    /// </summary>
    sealed class CSSBorderTopLeftRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderTopLeftRadiusProperty
    {
        #region ctor

        internal CSSBorderTopLeftRadiusProperty()
            : base(PropertyNames.BorderTopLeftRadius)
        {
        }

        #endregion
    }
}
