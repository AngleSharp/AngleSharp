namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                CssBackgroundSizeProperty.SingleConverter.StartsWithDelimiter().Option()),
            CssBackgroundRepeatProperty.SingleConverter.Option(),
            Converters.BackgroundAttachmentConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.BoxModelConverter.Option());

        //TODO Convert instead of validate
        /*
            Get<CssBackgroundImageProperty>().TrySetValue(Transform(m, n => n.Item1));
            Get<CssBackgroundPositionProperty>().TrySetValue(Transform(m, n => n.Item2.Item1));
            Get<CssBackgroundSizeProperty>().TrySetValue(Transform(m, n => n.Item2.Item2));
            Get<CssBackgroundRepeatProperty>().TrySetValue(Transform(m, n => n.Item3));
            Get<CssBackgroundAttachmentProperty>().TrySetValue(Transform(m, n => n.Item4));
            Get<CssBackgroundOriginProperty>().TrySetValue(Transform(m, n => n.Item5));
            Get<CssBackgroundClipProperty>().TrySetValue(Transform(m, n => n.Item6));
            Get<CssBackgroundColorProperty>().TrySetValue(m.Item2.Item7);
         */
        static readonly IValueConverter FinalLayerConverter = Converters.WithAny(
            Converters.ImageSourceConverter.Option(),
            Converters.WithOrder(
                Converters.PointConverter.Option(),
                CssBackgroundSizeProperty.SingleConverter.StartsWithDelimiter().Option()),
            CssBackgroundRepeatProperty.SingleConverter.Option(),
            Converters.BackgroundAttachmentConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.BoxModelConverter.Option(),
            Converters.CurrentColorConverter.Option());

        static readonly IValueConverter StyleConverter = NormalLayerConverter.FromList().RequiresEnd(FinalLayerConverter);

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

        #region Methods

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var image = properties.OfType<CssBackgroundImageProperty>().FirstOrDefault();
            var position = properties.OfType<CssBackgroundPositionProperty>().FirstOrDefault();
            var size = properties.OfType<CssBackgroundSizeProperty>().FirstOrDefault();
            var repeat = properties.OfType<CssBackgroundRepeatProperty>().FirstOrDefault();
            var attachment = properties.OfType<CssBackgroundAttachmentProperty>().FirstOrDefault();
            var origin = properties.OfType<CssBackgroundOriginProperty>().FirstOrDefault();
            var clip = properties.OfType<CssBackgroundClipProperty>().FirstOrDefault();
            var color = properties.OfType<CssBackgroundColorProperty>().FirstOrDefault();

            if (image == null || position == null || size == null || repeat == null || attachment == null || origin == null || clip == null || color == null)
                return String.Empty;

            var values = new List<String>();

            if (image.HasValue)
                values.Add(image.SerializeValue());

            if (position.HasValue || size.HasValue)
            {
                values.Add(position.SerializeValue());

                if (size.HasValue)
                {
                    values.Add("/");
                    values.Add(size.SerializeValue());
                }
            }

            if (repeat.HasValue)
                values.Add(repeat.SerializeValue());

            if (attachment.HasValue)
                values.Add(attachment.SerializeValue());

            if (clip.HasValue)
                values.Add(clip.SerializeValue());

            if (origin.HasValue)
                values.Add(origin.SerializeValue());

            if (color.HasValue)
                values.Add(color.SerializeValue());

            return String.Join(" ", values);
        }

        #endregion
    }
}
