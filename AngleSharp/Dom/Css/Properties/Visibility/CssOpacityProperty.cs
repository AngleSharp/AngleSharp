namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// Gets the value that should be used for the opacity.
    /// </summary>
    sealed class CssOpacityProperty : CssProperty
    {
        #region ctor

        internal CssOpacityProperty()
            : base(PropertyNames.Opacity, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: 1f
            get { return Converters.NumberConverter; }
        }

        #endregion
    }
}
