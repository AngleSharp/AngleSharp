namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, Tuple<ICssValue, ICssValue, ICssValue>, ICssValue>> Converter = Converters.WithAny(
            CssBorderImageSourceProperty.Converter.Val().Option(),
            Converters.WithOrder(
                CssBorderImageSliceProperty.Converter.Val().Option(),
                CssBorderImageWidthProperty.Converter.Val().StartsWithDelimiter().Option(),
                CssBorderImageOutsetProperty.Converter.Val().StartsWithDelimiter().Option()),
            CssBorderImageRepeatProperty.Converter.Val().Option()
        );

        readonly CssBorderImageOutsetProperty _outset;
        readonly CssBorderImageRepeatProperty _repeat;
        readonly CssBorderImageSliceProperty _slice;
        readonly CssBorderImageWidthProperty _width;
        readonly CssBorderImageSourceProperty _source;

        #endregion

        #region ctor

        internal CssBorderImageProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImage, rule)
        {
            _outset = Get<CssBorderImageOutsetProperty>();
            _repeat = Get<CssBorderImageRepeatProperty>();
            _slice = Get<CssBorderImageSliceProperty>();
            _source = Get<CssBorderImageSourceProperty>();
            _width = Get<CssBorderImageWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outset bottom width of the border-image.
        /// </summary>
        public Length OutsetBottom
        {
            get { return _outset.OutsetBottom; }
        }

        /// <summary>
        /// Gets the outset left width of the border-image.
        /// </summary>
        public Length OutsetLeft
        {
            get { return _outset.OutsetLeft; }
        }

        /// <summary>
        /// Gets the outset right width of the border-image.
        /// </summary>
        public Length OutsetRight
        {
            get { return _outset.OutsetRight; }
        }

        /// <summary>
        /// Gets the outset top width of the border-image.
        /// </summary>
        public Length OutsetTop
        {
            get { return _outset.OutsetTop; }
        }

        /// <summary>
        /// Gets the horizontal repeat value of the border-image.
        /// </summary>
        public BorderRepeat Horizontal
        {
            get { return _repeat.Horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value of the border-image.
        /// </summary>
        public BorderRepeat Vertical
        {
            get { return _repeat.Vertical; }
        }

        /// <summary>
        /// Gets if the slice should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _slice.IsFilled; }
        }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        public Length SliceBottom
        {
            get { return _slice.SliceBottom; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        public Length SliceRight
        {
            get { return _slice.SliceRight; }
        }

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        public Length SliceTop
        {
            get { return _slice.SliceTop; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        public Length SliceLeft
        {
            get { return _slice.SliceLeft; }
        }

        /// <summary>
        /// Gets the image source of the border-image.
        /// </summary>
        public IImageSource Image
        {
            get { return _source.Image; }
        }

        public Length WidthTop
        {
            get { return _width.WidthTop; }
        }

        public Length WidthBottom
        {
            get { return _width.WidthBottom; }
        }

        public Length WidthLeft
        {
            get { return _width.WidthLeft; }
        }

        public Length WidthRight
        {
            get { return _width.WidthRight; }
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
            return Converter.TryConvert(value, m =>
            {
                _source.TrySetValue(m.Item1);
                _slice.TrySetValue(m.Item2.Item1);
                _width.TrySetValue(m.Item2.Item2);
                _outset.TrySetValue(m.Item2.Item3);
                _repeat.TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!properties.Contains(_source))
                return String.Empty;

            var values = new List<String>();
            values.Add(_source.SerializeValue());

            if (_slice.HasValue && properties.Contains(_slice))
                values.Add(_slice.SerializeValue());

            var hasWidth = (_width.HasValue && properties.Contains(_width));
            var hasOutset = (_outset.HasValue && properties.Contains(_outset));

            if (hasWidth || hasOutset)
            {
                values.Add("/");

                if (hasWidth)
                    values.Add(_width.SerializeValue());

                if (hasOutset)
                {
                    values.Add("/");
                    values.Add(_outset.SerializeValue());
                }
            }

            if (_repeat.HasValue && properties.Contains(_repeat))
                values.Add(_repeat.SerializeValue());

            return String.Format(" ", values);
        }

        #endregion
    }
}
