namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-left-radius
    /// </summary>
    sealed class CssBorderTopLeftRadiusProperty : CssBorderRadiusPartProperty, ICssBorderTopLeftRadiusProperty
    {
        #region ctor

        internal CssBorderTopLeftRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopLeftRadius, rule)
        {
        }

        #endregion

        #region Properties

        public Length HorizontalTopLeft
        {
            get { return HorizontalRadius; }
        }

        public Length VerticalTopLeft
        {
            get { return VerticalRadius; }
        }

        #endregion
    }
}
