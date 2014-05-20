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
        /// Gets the outset property of the border-image.
        /// </summary>
        public CSSBorderImageOutsetProperty Outset
        {
            get { return _outset; }
        }

        /// <summary>
        /// Gets the repeat property of the border-image.
        /// </summary>
        public CSSBorderImageRepeatProperty Repeat
        {
            get { return _repeat; }
        }

        /// <summary>
        /// Gets the slice property of the border-image.
        /// </summary>
        public CSSBorderImageSliceProperty Slice
        {
            get { return _slice; }
        }

        /// <summary>
        /// Gets the source property of the border-image.
        /// </summary>
        public CSSBorderImageSourceProperty Source
        {
            get { return _source; }
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
