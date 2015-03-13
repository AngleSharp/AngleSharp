namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BgLayer = System.Tuple<ICssValue, System.Tuple<ICssValue, ICssValue>, ICssValue, ICssValue, ICssValue, ICssValue>;
    using FinalBgLayer = System.Tuple<ICssValue, System.Tuple<ICssValue, ICssValue>, ICssValue, ICssValue, ICssValue, ICssValue, ICssValue>;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CssBackgroundProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<BgLayer> NormalLayerConverter = Converters.WithAny(
            Converters.ImageSourceConverter.Val().Option(),
            Converters.WithOrder(
                Converters.PointConverter.Val().Option(),
                CssBackgroundSizeProperty.SingleConverter.StartsWithDelimiter().Val().Option()),
            CssBackgroundRepeatProperty.SingleConverter.Val().Option(),
            Converters.BackgroundAttachmentConverter.Val().Option(),
            Converters.BoxModelConverter.Val().Option(),
            Converters.BoxModelConverter.Val().Option());

        static readonly IValueConverter<FinalBgLayer> FinalLayerConverter = Converters.WithAny(
            Converters.ImageSourceConverter.Val().Option(),
            Converters.WithOrder(
                Converters.PointConverter.Val().Option(),
                CssBackgroundSizeProperty.SingleConverter.StartsWithDelimiter().Val().Option()),
            CssBackgroundRepeatProperty.SingleConverter.Val().Option(),
            Converters.BackgroundAttachmentConverter.Val().Option(),
            Converters.BoxModelConverter.Val().Option(),
            Converters.BoxModelConverter.Val().Option(),
            Converters.CurrentColorConverter.Val().Option());

        static readonly IValueConverter<Tuple<BgLayer[], FinalBgLayer>> Converter = NormalLayerConverter.FromList().RequiresEnd(FinalLayerConverter);

        #endregion

        #region ctor

        internal CssBackgroundProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Background, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            //[ <bg-layer> , ]* <final-bg-layer> where: 
            //  <bg-layer> = 
            //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> 
            //  <final-bg-layer> = 
            //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> || <background-color>

            return Converter.TryConvert(value, m =>
            {
                Get<CssBackgroundImageProperty>().TrySetValue(Transform(m, n => n.Item1));
                Get<CssBackgroundPositionProperty>().TrySetValue(Transform(m, n => n.Item2.Item1));
                Get<CssBackgroundSizeProperty>().TrySetValue(Transform(m, n => n.Item2.Item2));
                Get<CssBackgroundRepeatProperty>().TrySetValue(Transform(m, n => n.Item3));
                Get<CssBackgroundAttachmentProperty>().TrySetValue(Transform(m, n => n.Item4));
                Get<CssBackgroundOriginProperty>().TrySetValue(Transform(m, n => n.Item5));
                Get<CssBackgroundClipProperty>().TrySetValue(Transform(m, n => n.Item6));
                Get<CssBackgroundColorProperty>().TrySetValue(m.Item2.Item7);
            });
        }

        static ICssValue Transform(Tuple<BgLayer[], FinalBgLayer> data, Func<BgLayer, ICssValue> selector)
        {
            var final = new BgLayer(data.Item2.Item1, data.Item2.Item2, data.Item2.Item3, data.Item2.Item4, data.Item2.Item5, data.Item2.Item6);

            if (data.Item1.Length == 0)
                return selector(final);

            var list = new CssValueList();

            foreach (var item in data.Item1)
                list.Add(selector(item));

            list.Add(selector(final));
            return list;
        }

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
