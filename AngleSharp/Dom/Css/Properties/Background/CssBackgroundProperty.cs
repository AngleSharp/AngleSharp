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
            Converters.ImageSourceConverter.Option(),
            Converters.WithOrder(
                Converters.PointConverter.Option(),
                Converters.BackgroundSizeConverter.StartsWithDelimiter().Option()),
            Converters.BackgroundRepeatsConverter.Option(),
            Converters.BackgroundAttachmentConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.BoxModelConverter.Option());

        static readonly IValueConverter FinalLayerConverter = Converters.WithAny(
            Converters.ImageSourceConverter.Option(),
            Converters.WithOrder(
                Converters.PointConverter.Option(),
                Converters.BackgroundSizeConverter.StartsWithDelimiter().Option()),
            Converters.BackgroundRepeatsConverter.Option(),
            Converters.BackgroundAttachmentConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.CurrentColorConverter.Option());

        static readonly IValueConverter StyleConverter = NormalLayerConverter.FromList().RequiresEnd(FinalLayerConverter).OrDefault();

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
