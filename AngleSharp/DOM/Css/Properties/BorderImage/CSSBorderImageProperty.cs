namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Distances = System.Tuple<IDistance, IDistance, IDistance, IDistance>;
    using Slices = System.Tuple<IDistance, IDistance, IDistance, IDistance, System.Boolean>;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CSSBorderImageProperty : CSSShorthandProperty, ICssBorderImageProperty
    {
        #region Fields

        static readonly IDistance nil = null;

        public static readonly IValueConverter<Tuple<IImageSource, Tuple<Slices, Distances, Distances>, BorderRepeat[]>> Converter = WithAny(
            CSSBorderImageSourceProperty.Converter.Option(CSSBorderImageSourceProperty.Default),
            WithOrder(
                CSSBorderImageSliceProperty.Converter.Option(Tuple.Create(CSSBorderImageSliceProperty.Default, nil, nil, nil, false)),
                CSSBorderImageWidthProperty.Converter.StartsWithDelimiter().Option(Tuple.Create(CSSBorderImageWidthProperty.Default, CSSBorderImageWidthProperty.Default, CSSBorderImageWidthProperty.Default, CSSBorderImageWidthProperty.Default)),
                CSSBorderImageOutsetProperty.Converter.StartsWithDelimiter().Option(Tuple.Create(CSSBorderImageOutsetProperty.Default, CSSBorderImageOutsetProperty.Default, CSSBorderImageOutsetProperty.Default, CSSBorderImageOutsetProperty.Default))
            ),
            CSSBorderImageRepeatProperty.Converter.Option(new[] { CSSBorderImageRepeatProperty.Default, CSSBorderImageRepeatProperty.Default })
        );

        readonly CSSBorderImageOutsetProperty _outset;
        readonly CSSBorderImageRepeatProperty _repeat;
        readonly CSSBorderImageSliceProperty _slice;
        readonly CSSBorderImageWidthProperty _width;
        readonly CSSBorderImageSourceProperty _source;

        #endregion

        #region ctor

        internal CSSBorderImageProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImage, rule)
        {
            _outset = Get<CSSBorderImageOutsetProperty>();
            _repeat = Get<CSSBorderImageRepeatProperty>();
            _slice = Get<CSSBorderImageSliceProperty>();
            _source = Get<CSSBorderImageSourceProperty>();
            _width = Get<CSSBorderImageWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outset bottom width of the border-image.
        /// </summary>
        public IDistance OutsetBottom
        {
            get { return _outset.OutsetBottom; }
        }

        /// <summary>
        /// Gets the outset left width of the border-image.
        /// </summary>
        public IDistance OutsetLeft
        {
            get { return _outset.OutsetLeft; }
        }

        /// <summary>
        /// Gets the outset right width of the border-image.
        /// </summary>
        public IDistance OutsetRight
        {
            get { return _outset.OutsetRight; }
        }

        /// <summary>
        /// Gets the outset top width of the border-image.
        /// </summary>
        public IDistance OutsetTop
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
        public IDistance SliceBottom
        {
            get { return _slice.SliceBottom; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        public IDistance SliceRight
        {
            get { return _slice.SliceRight; }
        }

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        public IDistance SliceTop
        {
            get { return _slice.SliceTop; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        public IDistance SliceLeft
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

        public IDistance WidthTop
        {
            get { return _width.WidthTop; }
        }

        public IDistance WidthBottom
        {
            get { return _width.WidthBottom; }
        }

        public IDistance WidthLeft
        {
            get { return _width.WidthLeft; }
        }

        public IDistance WidthRight
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
                var slices = m.Item2.Item1;
                var widths = m.Item2.Item2;
                var outsets = m.Item2.Item3;
                var repeats = m.Item3;

                _source.SetImages(m.Item1);
                _slice.SetSlice(slices.Item1, slices.Item2, slices.Item3, slices.Item4, slices.Item5);
                _width.SetWidth(widths.Item1, widths.Item2, widths.Item3, widths.Item4);
                _outset.SetOutset(outsets.Item1, outsets.Item2, outsets.Item3, outsets.Item4);
                _repeat.SetRepeat(repeats[0], repeats.Length == 2 ? repeats[1] : repeats[0]);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
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
