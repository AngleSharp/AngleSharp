namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssTextShadowProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.MultipleShadowConverter.OrDefault();

        #endregion

        #region ctor

        internal CssTextShadowProperty()
            : base(PropertyNames.TextShadow, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
