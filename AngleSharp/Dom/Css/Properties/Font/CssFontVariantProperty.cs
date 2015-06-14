namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// Gets the selected font variant transformation, if any.
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontVariantConverter.OrDefault(FontVariant.Normal);

        #endregion

        #region ctor

        internal CssFontVariantProperty()
            : base(PropertyNames.FontVariant, PropertyFlags.Inherited)
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
