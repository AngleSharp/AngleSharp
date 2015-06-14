namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// Gets the selected font-size.
    /// </summary>
    sealed class CssFontSizeProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontSizeConverter.OrDefault(FontSize.Medium.ToLength());

        #endregion

        #region ctor

        internal CssFontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
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
