namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-right-radius
    /// </summary>
    sealed class CSSBorderBottomRightRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderBottomRightRadiusProperty
    {
        #region ctor

        internal CSSBorderBottomRightRadiusProperty()
            : base(PropertyNames.BorderBottomRightRadius)
        {
        }

        #endregion

        #region Properties

        public IDistance HorizontalBottomRight
        {
            get { return HorizontalRadius; }
        }

        public IDistance VerticalBottomRight
        {
            get { return VerticalRadius; }
        }

        #endregion
    }
}
