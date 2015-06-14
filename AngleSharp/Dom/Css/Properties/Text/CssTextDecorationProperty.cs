namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CssTextDecorationProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.ColorConverter.Option().For(PropertyNames.TextDecorationColor),
            Converters.TextDecorationStyleConverter.Option().For(PropertyNames.TextDecorationStyle),
            Converters.TextDecorationLinesConverter.Option().For(PropertyNames.TextDecorationLine)).OrDefault();

        #endregion

        #region ctor

        internal CssTextDecorationProperty()
            : base(PropertyNames.TextDecoration, PropertyFlags.Animatable)
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
