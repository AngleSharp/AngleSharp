namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// Gets the selected font stretch setting.
    /// </summary>
    sealed class CssFontStretchProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontStretchConverter.OrDefault(FontStretch.Normal);

        #endregion

        #region ctor

        internal CssFontStretchProperty()
            : base(PropertyNames.FontStretch, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
