namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CSSBorderImageWidthProperty : CSSProperty, ICssBorderImageWidthProperty
    {
        #region Fields

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the height of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the bottom length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the height of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the width of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthLeft
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the right length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the width of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthRight
        {
            get { return _right; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _top = Percent.Hundred;
            _right = Percent.Hundred;
            _bottom = Percent.Hundred;
            _left = Percent.Hundred;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var mode = ToMode(value);

            if (mode != null)
                _top = _right = _left = _bottom = mode;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else
                return false;

            return true;
        }

        static IDistance ToMode(CSSValue value)
        {
            if (value.Is(Keywords.Auto))
                return null;

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new Percent(multiple.Value * 100f);

            return value.ToDistance();
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 4)
                return false;

            var top = ToMode(values[0]);
            var right = ToMode(values[1]);
            var bottom = top;
            var left = right;

            if (top == null || right == null)
                return false;

            if (values.Length > 2)
            {
                bottom = ToMode(values[2]);

                if (bottom == null)
                    return false;

                if (values.Length > 3)
                {
                    left = ToMode(values[3]);

                    if (left == null)
                        return false;
                }
            }

            _left = left;
            _right = right;
            _bottom = bottom;
            _top = top;
            return true;
        }

        #endregion
    }
}
