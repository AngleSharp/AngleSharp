namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-color
    /// Gets the selected text-decoration color.
    /// </summary>
    sealed class CssTextDecorationColorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Black);

        #endregion

        #region ctor

        internal CssTextDecorationColorProperty()
            : base(PropertyNames.TextDecorationColor, PropertyFlags.Animatable)
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
