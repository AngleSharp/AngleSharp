namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CSSBorderImageProperty : CSSProperty, ICssBorderImageProperty
    {
        #region Fields

        IDistance _topOutset;
        IDistance _rightOutset;
        IDistance _bottomOutset;
        IDistance _leftOutset;
        BorderRepeat _horizontal;
        BorderRepeat _vertical;
        IDistance _topSlice;
        IDistance _rightSlice;
        IDistance _bottomSlice;
        IDistance _leftSlice;
        Boolean _fillSlice;
        IBitmap _image;
        IDistance _topWidth;
        IDistance _leftWidth;
        IDistance _rightWidth;
        IDistance _bottomWidth;

        #endregion

        #region ctor

        internal CSSBorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outset bottom width of the border-image.
        /// </summary>
        public IDistance OutsetBottom
        {
            get { return _bottomOutset; }
        }

        /// <summary>
        /// Gets the outset left width of the border-image.
        /// </summary>
        public IDistance OutsetLeft
        {
            get { return _leftOutset; }
        }

        /// <summary>
        /// Gets the outset right width of the border-image.
        /// </summary>
        public IDistance OutsetRight
        {
            get { return _rightOutset; }
        }

        /// <summary>
        /// Gets the outset top width of the border-image.
        /// </summary>
        public IDistance OutsetTop
        {
            get { return _topOutset; }
        }

        /// <summary>
        /// Gets the horizontal repeat value of the border-image.
        /// </summary>
        public BorderRepeat Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value of the border-image.
        /// </summary>
        public BorderRepeat Vertical
        {
            get { return _vertical; }
        }

        /// <summary>
        /// Gets if the slice should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _fillSlice; }
        }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        public IDistance SliceBottom
        {
            get { return _bottomSlice; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        public IDistance SliceRight
        {
            get { return _rightSlice; }
        }

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        public IDistance SliceTop
        {
            get { return _topSlice; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        public IDistance SliceLeft
        {
            get { return _leftSlice; }
        }

        /// <summary>
        /// Gets the image source of the border-image.
        /// </summary>
        public IBitmap Image
        {
            get { return _image; }
        }

        public IDistance WidthTop
        {
            get { throw new NotImplementedException(); }
        }

        public IDistance WidthBottom
        {
            get { throw new NotImplementedException(); }
        }

        public IDistance WidthLeft
        {
            get { throw new NotImplementedException(); }
        }

        public IDistance WidthRight
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _topOutset = Percent.Zero;
            _rightOutset = Percent.Zero;
            _bottomOutset = Percent.Zero;
            _leftOutset = Percent.Zero;
            _horizontal = BorderRepeat.Stretch;
            _vertical = BorderRepeat.Stretch;
            _topSlice = Percent.Hundred;
            _rightSlice = Percent.Hundred;
            _bottomSlice = Percent.Hundred;
            _leftSlice = Percent.Hundred;
            _fillSlice = false;
            _image = Color.Transparent;
            _topWidth = Percent.Hundred;
            _rightWidth = Percent.Hundred;
            _bottomWidth = Percent.Hundred;
            _leftWidth = Percent.Hundred;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSValueList)
                return Evaluate((CSSValueList)value);

            return Evaluate(new CSSValueList(value));
        }

        Boolean Evaluate(CSSValueList values)
        {
            var fillSlice = false;
            IDistance topOutset = null;
            IDistance rightOutset = null;
            IDistance bottomOutset = null;
            IDistance leftOutset = null;
            BorderRepeat? horizontal = null;
            BorderRepeat? vertical = null;
            IDistance topSlice = null;
            IDistance rightSlice = null;
            IDistance bottomSlice = null;
            IDistance leftSlice = null;
            IBitmap image = null;
            IDistance topWidth = null;
            IDistance rightWidth = null;
            IDistance bottomWidth = null;
            IDistance leftWidth = null;

            //TODO
            //<'border-image-source'> || <'border-image-slice'> [ / <'border-image-width'> | / <'border-image-outset'> ]? || <'border-image-repeat'>
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];

                if (image == null && (image = value.ToImage()) != null)
                    continue;
                else if (horizontal == null && (horizontal = value.ToBorderRepeat()).HasValue)
                    continue;
                else if (vertical == null && (vertical = value.ToBorderRepeat()).HasValue)
                    continue;
                else if (topSlice == null && (topSlice = value.ToBorderSlice()) != null)
                    continue;
                else if (rightSlice == null && (rightSlice = value.ToBorderSlice()) != null)
                    continue;
                else if (bottomSlice == null && (bottomSlice = value.ToBorderSlice()) != null)
                    continue;
                else if (leftSlice == null && (leftSlice = value.ToBorderSlice()) != null)
                    continue;
                else if (!fillSlice && (fillSlice = value.Is(Keywords.Fill)))
                    continue;
                else if (value == CSSValue.Delimiter)
                {
                    while (++i < values.Length)
                    {
                        value = values[i];

                        if (topOutset == null && (topOutset = value.ToDistance()) != null)
                            continue;
                        else if (rightOutset == null && (rightOutset = value.ToDistance()) != null)
                            continue;
                        else if (bottomOutset == null && (bottomOutset = value.ToDistance()) != null)
                            continue;
                        else if (leftOutset == null && (leftOutset = value.ToDistance()) != null)
                            continue;

                        break;
                    }

                    continue;
                }

                return false;
            }

            _fillSlice = fillSlice;

            _topOutset = topOutset;
            _rightOutset = rightOutset ?? _topOutset;
            _bottomOutset = bottomOutset ?? _topOutset;
            _leftOutset = leftOutset ?? _rightOutset;

            _horizontal = horizontal ?? BorderRepeat.Stretch;
            _vertical = vertical ?? (horizontal ?? BorderRepeat.Stretch);

            _topSlice = topSlice;
            _rightSlice = rightSlice ?? _topSlice;
            _bottomSlice = bottomSlice ?? _topSlice;
            _leftSlice = leftSlice ?? _rightSlice;

            _topWidth = topWidth;
            _rightWidth = rightWidth ?? _topWidth;
            _bottomWidth = bottomWidth ?? _topWidth;
            _leftWidth = leftWidth ?? _rightWidth;

            _image = image;
            return true;
        }

        #endregion
    }
}
