namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CssOutlineProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option().For(PropertyNames.OutlineWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.OutlineStyle),
            Converters.InvertedColorConverter.Option().For(PropertyNames.OutlineColor)).OrDefault();

        #endregion

        #region ctor

        internal CssOutlineProperty()
            : base(PropertyNames.Outline, PropertyFlags.Animatable)
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
