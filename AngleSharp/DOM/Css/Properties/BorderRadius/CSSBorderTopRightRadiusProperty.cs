namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-right-radius
    /// </summary>
    sealed class CSSBorderTopRightRadiusProperty : CSSBorderRadiusPartProperty, ICssBorderTopRightRadiusProperty
    {
        #region ctor

        internal CSSBorderTopRightRadiusProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderTopRightRadius, rule)
        {
        }

        #endregion

        #region Properties

        public Length HorizontalTopRight
        {
            get { return HorizontalRadius; }
        }

        public Length VerticalTopRight
        {
            get { return VerticalRadius; }
        }

        #endregion
    }
}
