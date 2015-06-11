namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-left-radius
    /// </summary>
    sealed class CssBorderTopLeftRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopLeftRadiusProperty()
            : base(PropertyNames.BorderTopLeftRadius, PropertyFlags.Animatable)
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
