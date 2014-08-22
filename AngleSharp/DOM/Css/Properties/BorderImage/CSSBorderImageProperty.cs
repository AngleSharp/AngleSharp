namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    public sealed class CSSBorderImageProperty : CSSProperty
    {
        #region Fields

        CSSBorderImageOutsetProperty _outset;
        CSSBorderImageRepeatProperty _repeat;
        CSSBorderImageSliceProperty _slice;
        CSSBorderImageSourceProperty _source;
        CSSBorderImageWidthProperty _width;

        #endregion

        #region ctor

        internal CSSBorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
            _inherited = false;
            _outset = new CSSBorderImageOutsetProperty();
            _repeat = new CSSBorderImageRepeatProperty();
            _slice = new CSSBorderImageSliceProperty();
            _source = new CSSBorderImageSourceProperty();
            _width = new CSSBorderImageWidthProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the outset bottom width of the border-image.
        /// </summary>
        internal CSSCalcValue OutsetBottom
        {
            get { return _outset.Bottom; }
        }

        /// <summary>
        /// Gets the outset left width of the border-image.
        /// </summary>
        internal CSSCalcValue OutsetLeft
        {
            get { return _outset.Left; }
        }

        /// <summary>
        /// Gets the outset right width of the border-image.
        /// </summary>
        internal CSSCalcValue OutsetRight
        {
            get { return _outset.Right; }
        }

        /// <summary>
        /// Gets the outset top width of the border-image.
        /// </summary>
        internal CSSCalcValue OutsetTop
        {
            get { return _outset.Top; }
        }

        /// <summary>
        /// Gets the horizontal repeat value of the border-image.
        /// </summary>
        public BorderRepeat HorizontalRepeat
        {
            get { return _repeat.Horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value of the border-image.
        /// </summary>
        public BorderRepeat VerticalRepeat
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
        internal CSSCalcValue SliceBottom
        {
            get { return _slice.Bottom; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        internal CSSCalcValue SliceRight
        {
            get { return _slice.Right; }
        }

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        internal CSSCalcValue SliceTop
        {
            get { return _slice.Top; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        internal CSSCalcValue SliceLeft
        {
            get { return _slice.Left; }
        }

        /// <summary>
        /// Gets the image source of the border-image.
        /// </summary>
        internal CSSImageValue Source
        {
            get { return _source.Image; }
        }

        /// <summary>
        /// Gets the width property of the border-image.
        /// </summary>
        public CSSBorderImageWidthProperty Width
        {
            get { return _width; }
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
            if (value == CSSValue.Inherit)
                return true;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);

            return Evaluate(new CSSValueList(value));
        }

        Boolean Evaluate(CSSValueList values)
        {
            var outset = new CSSBorderImageOutsetProperty();
            var repeat = new CSSBorderImageRepeatProperty();
            var slice = new CSSBorderImageSliceProperty();
            var source = new CSSBorderImageSourceProperty();
            var width = new CSSBorderImageWidthProperty();
            var foundSource = false;

            //TODO
            for (int i = 0; i < values.Length; i++)
            {
                if (!foundSource && CheckSingleProperty(source, i, values))
                    foundSource = true;
                else
                    return false;
            }

            _outset = outset;
            _repeat = repeat;
            _slice = slice;
            _source = source;
            _width = width;
            return true;
        }

        #endregion
    }
}
