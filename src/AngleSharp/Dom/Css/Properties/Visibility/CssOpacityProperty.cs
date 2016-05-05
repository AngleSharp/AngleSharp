namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// Gets the value that should be used for the opacity.
    /// </summary>
    sealed class CssOpacityProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.NumberConverter.OrDefault(1f);

        #endregion

        #region ctor

        internal CssOpacityProperty()
            : base(PropertyNames.Opacity, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
