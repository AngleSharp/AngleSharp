namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using BgLayer = System.Tuple<ICssValue, System.Tuple<ICssValue, ICssValue>, ICssValue, ICssValue, ICssValue, ICssValue>;
    using FinalBgLayer = System.Tuple<ICssValue, System.Tuple<ICssValue, ICssValue>, ICssValue, ICssValue, ICssValue, ICssValue, ICssValue>;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CSSBackgroundProperty : CSSShorthandProperty, ICssBackgroundProperty
    {
        #region Fields

        static readonly IValueConverter<BgLayer> NormalLayerConverter = WithAny(
            CSSBackgroundImageProperty.Converter.Val().Option(),
            WithOrder(
                CSSBackgroundPositionProperty.Converter.Val().Option(),
                CSSBackgroundSizeProperty.Converter.StartsWithDelimiter().Val().Option()),
            CSSBackgroundRepeatProperty.Converter.Val().Option(),
            CSSBackgroundAttachmentProperty.Converter.Val().Option(),
            CSSBackgroundOriginProperty.Converter.Val().Option(),
            CSSBackgroundClipProperty.Converter.Val().Option()
        );

        static readonly IValueConverter<FinalBgLayer> FinalLayerConverter = WithAny(
            CSSBackgroundImageProperty.Converter.Val().Option(),
            WithOrder(
                CSSBackgroundPositionProperty.Converter.Val().Option(),
                CSSBackgroundSizeProperty.Converter.StartsWithDelimiter().Val().Option()),
            CSSBackgroundRepeatProperty.Converter.Val().Option(),
            CSSBackgroundAttachmentProperty.Converter.Val().Option(),
            CSSBackgroundOriginProperty.Converter.Val().Option(),
            CSSBackgroundClipProperty.Converter.Val().Option(),
            CSSBackgroundColorProperty.Converter.Val().Option()
        );

        static readonly IValueConverter<Tuple<BgLayer[], FinalBgLayer>> Converter = NormalLayerConverter.FromList().RequiresEnd(FinalLayerConverter);

        readonly CSSBackgroundImageProperty _image;
        readonly CSSBackgroundPositionProperty _position;
        readonly CSSBackgroundSizeProperty _size;
        readonly CSSBackgroundRepeatProperty _repeat;
        readonly CSSBackgroundAttachmentProperty _attachment;
        readonly CSSBackgroundOriginProperty _origin;
        readonly CSSBackgroundClipProperty _clip;
        readonly CSSBackgroundColorProperty _color;

        #endregion

        #region ctor

        internal CSSBackgroundProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Background, rule, PropertyFlags.Animatable)
        {
            _image = Get<CSSBackgroundImageProperty>();
            _position = Get<CSSBackgroundPositionProperty>();
            _size = Get<CSSBackgroundSizeProperty>();
            _repeat = Get<CSSBackgroundRepeatProperty>();
            _attachment = Get<CSSBackgroundAttachmentProperty>();
            _origin = Get<CSSBackgroundOriginProperty>();
            _clip = Get<CSSBackgroundClipProperty>();
            _color = Get<CSSBackgroundColorProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the background image property.
        /// </summary>
        public IEnumerable<IImageSource> Images
        {
            get { return _image.Images; }
        }

        /// <summary>
        /// Gets the value of the background position property.
        /// </summary>
        public IEnumerable<Point> Positions
        {
            get { return _position.Positions; }
        }

        /// <summary>
        /// Gets the value of the horizontal repeat property.
        /// </summary>
        public IEnumerable<BackgroundRepeat> HorizontalRepeats
        {
            get { return _repeat.HorizontalRepeats; }
        }

        /// <summary>
        /// Gets the value of the vertical repeat property.
        /// </summary>
        public IEnumerable<BackgroundRepeat> VerticalRepeats
        {
            get { return _repeat.VerticalRepeats; }
        }

        /// <summary>
        /// Gets the value of the background attachment property.
        /// </summary>
        public IEnumerable<BackgroundAttachment> Attachments
        {
            get { return _attachment.Attachments; }
        }

        /// <summary>
        /// Gets the value of the background origin property.
        /// </summary>
        public IEnumerable<BoxModel> Origins
        {
            get { return _origin.Origins; }
        }

        /// <summary>
        /// Gets the value of the background clip property.
        /// </summary>
        public IEnumerable<BoxModel> Clips
        {
            get { return _clip.Clips; }
        }

        /// <summary>
        /// Gets the value of the background color property.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        /// <summary>
        /// Gets if the background image should be covered, i.e. min(100%).
        /// </summary>
        public IEnumerable<Boolean> IsCovered
        {
            get { return _size.IsCovered; }
        }

        /// <summary>
        /// Gets if the background image should be contained, i.e. max(100%).
        /// </summary>
        public IEnumerable<Boolean> IsContained
        {
            get { return _size.IsContained; }
        }

        /// <summary>
        /// Gets the widths and heights of the background image, if specified.
        /// </summary>
        public IEnumerable<Point> Sizes
        {
            get { return _size.Sizes; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            //[ <bg-layer> , ]* <final-bg-layer> where: 
            //  <bg-layer> = 
            //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> 
            //  <final-bg-layer> = 
            //      <bg-image> || <position> [ / <bg-size> ]? || <repeat-style> || <attachment> || <box> || <box> || <background-color>

            return Converter.TryConvert(value, m =>
            {
                _image.TrySetValue(Transform(m, n => n.Item1));
                _position.TrySetValue(Transform(m, n => n.Item2.Item1));
                _size.TrySetValue(Transform(m, n => n.Item2.Item2));
                _repeat.TrySetValue(Transform(m, n => n.Item3));
                _attachment.TrySetValue(Transform(m, n => n.Item4));
                _origin.TrySetValue(Transform(m, n => n.Item5));
                _clip.TrySetValue(Transform(m, n => n.Item6));
                _color.TrySetValue(m.Item2.Item7);
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

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var values = new List<String>();
            values.Add(_image.SerializeValue());
            values.Add(_position.SerializeValue());

            if (_size.HasValue)
            {
                values.Add("/");
                values.Add(_size.SerializeValue());
            }

            values.Add(_repeat.SerializeValue());
            values.Add(_attachment.SerializeValue());
            values.Add(_clip.SerializeValue());
            values.Add(_origin.SerializeValue());
            values.Add(_color.SerializeValue());
            values.RemoveAll(m => String.IsNullOrEmpty(m));

            return String.Join(" ", values);
        }

        #endregion
    }
}
