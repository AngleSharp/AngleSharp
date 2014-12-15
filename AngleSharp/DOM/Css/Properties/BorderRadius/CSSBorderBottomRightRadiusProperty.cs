namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-right-radius
    /// </summary>
    sealed class CSSBorderBottomRightRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderBottomRightRadiusProperty
    {
        #region ctor

        internal CSSBorderBottomRightRadiusProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderBottomRightRadius, rule)
        {
        }

        #endregion

        #region Properties

        public Length HorizontalBottomRight
        {
            get { return HorizontalRadius; }
        }

        public Length VerticalBottomRight
        {
            get { return VerticalRadius; }
        }

        #endregion
    }
}
