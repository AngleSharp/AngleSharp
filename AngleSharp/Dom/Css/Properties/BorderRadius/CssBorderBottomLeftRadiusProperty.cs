namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-left-radius
    /// </summary>
    sealed class CssBorderBottomLeftRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderBottomLeftRadiusProperty()
            : base(PropertyNames.BorderBottomLeftRadius, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Zero
            get { return Converters.BorderRadiusConverter; }
        }

        #endregion
    }
}
