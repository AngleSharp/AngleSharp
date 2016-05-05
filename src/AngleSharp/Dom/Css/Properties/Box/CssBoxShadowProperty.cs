namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssBoxShadowProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.MultipleShadowConverter.OrDefault();

        #endregion

        #region ctor

        internal CssBoxShadowProperty()
            : base(PropertyNames.BoxShadow, PropertyFlags.Animatable)
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
