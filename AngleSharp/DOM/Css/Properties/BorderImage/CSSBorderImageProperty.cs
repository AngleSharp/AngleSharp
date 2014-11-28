namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CSSBorderImageProperty : CSSShorthandProperty, ICssBorderImageProperty
    {
        #region Fields

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
        public IEnumerable<Url> Images
        {
            get { return _source.Images; }
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
        protected override Boolean IsValid(CSSValue value)
        {
            //Required: SOurce, Slice and Repeat
            if (value is CSSValueList)
                return Evaluate((CSSValueList)value);

            return Evaluate(new CSSValueList(value));
        }

        Boolean Evaluate(CSSValueList values)
        {
            CSSValue source = null;
            var slice = new CSSValueList();
            var outset = new CSSValueList();
            var repeat = new CSSValueList();
            var width = new CSSValueList();

            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];

                if (_source.CanStore(value, ref source))
                    continue;
                else if (repeat.Length == 0 && _repeat.CanTake(value))
                {
                    repeat.Add(value);

                    if (i + 1 < values.Length)
                    {
                        repeat.Add(values[++i]);

                        if (!_repeat.CanTake(repeat))
                            repeat.Remove(values[i--]);
                    }

                    continue;
                }
                else if (slice.Length == 0 && _slice.CanTake(value))
                {
                    slice.Add(value);

                    while (i + 1 < values.Length)
                    {
                        slice.Add(values[++i]);

                        if (!_slice.CanTake(slice))
                        {
                            slice.Remove(values[i--]);
                            break;
                        }
                    }

                    continue;
                }
                else if (value == CSSValue.Delimiter)
                {
                    if (++i == values.Length)
                        return false;

                    while (i + 1 < values.Length)
                    {
                        value = values[++i];

                        if (value == CSSValueList.Delimiter)
                        {
                            if (++i == values.Length)
                                return false;

                            while (i + 1 < values.Length)
                            {
                                outset.Add(values[++i]);

                                if (!_outset.CanTake(outset))
                                {
                                    outset.Remove(values[i--]);
                                    break;
                                }
                            }

                            if (outset.Length == 0)
                                return false;

                            break;
                        }

                        width.Add(value);

                        if (!_width.CanTake(width))
                        {
                            width.Remove(values[i--]);
                            break;
                        }
                    }

                    continue;
                }

                return false;
            }

            return _width.TrySetValue(width.Reduce()) && _source.TrySetValue(source) &&
                   _outset.TrySetValue(outset.Reduce()) && _repeat.TrySetValue(repeat.Reduce()) &&
                   _slice.TrySetValue(slice.Reduce());
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
