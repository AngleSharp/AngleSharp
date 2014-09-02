namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-right-radius
    /// </summary>
    sealed class CSSBorderTopRightRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderTopRightRadiusProperty
    {
        #region ctor

        internal CSSBorderTopRightRadiusProperty()
            : base(PropertyNames.BorderTopRightRadius)
        {
        }

        #endregion
    }
}
