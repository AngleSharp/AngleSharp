namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CssBackgroundProperty : CssShorthandProperty
    {
        #region Fields

        //[ <bg-layer> , ]* <final-bg-layer> where: 
        //  <bg-layer> = 
        //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> 
        //  <final-bg-layer> = 
        //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> || <background-color>

        static readonly IValueConverter NormalLayerConverter = Converters.WithAny(
            Converters.OptionalImageSourceConverter.Option().For(PropertyNames.BackgroundImage),
            Converters.WithOrder(
                Converters.PointConverter.Option().For(PropertyNames.BackgroundPosition),
                Converters.BackgroundSizeConverter.StartsWithDelimiter().Option().For(PropertyNames.BackgroundSize)),
            Converters.BackgroundRepeatsConverter.Option().For(PropertyNames.BackgroundRepeat),
            Converters.BackgroundAttachmentConverter.Option().For(PropertyNames.BackgroundAttachment),
            Converters.BoxModelConverter.Option().For(PropertyNames.BackgroundOrigin),
            Converters.BoxModelConverter.Option().For(PropertyNames.BackgroundClip));

        static readonly IValueConverter FinalLayerConverter = Converters.WithAny(
            Converters.OptionalImageSourceConverter.Option().For(PropertyNames.BackgroundImage),
            Converters.WithOrder(
                Converters.PointConverter.Option().For(PropertyNames.BackgroundPosition),
                Converters.BackgroundSizeConverter.StartsWithDelimiter().Option().For(PropertyNames.BackgroundSize)),
            Converters.BackgroundRepeatsConverter.Option().For(PropertyNames.BackgroundRepeat),
            Converters.BackgroundAttachmentConverter.Option().For(PropertyNames.BackgroundAttachment),
            Converters.BoxModelConverter.Option().For(PropertyNames.BackgroundOrigin),
            Converters.BoxModelConverter.Option().For(PropertyNames.BackgroundClip),
            Converters.CurrentColorConverter.Option().For(PropertyNames.BackgroundColor));

        static readonly IValueConverter StyleConverter = NormalLayerConverter.RequiresEnd(FinalLayerConverter).OrDefault();

        #endregion

        #region ctor

        internal CssBackgroundProperty()
            : base(PropertyNames.Background, PropertyFlags.Animatable)
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
