namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-left-radius
    /// </summary>
    sealed class CssBorderBottomLeftRadiusProperty : CssBorderRadiusPartProperty, ICssBorderBottomLeftRadiusProperty
    {
        #region ctor

        internal CssBorderBottomLeftRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomLeftRadius, rule)
        {
        }

        #endregion

        #region Properties

        public Length HorizontalBottomLeft
        {
            get { return HorizontalRadius; }
        }

        public Length VerticalBottomLeft
        {
            get { return VerticalRadius; }
        }

        #endregion
    }
}
