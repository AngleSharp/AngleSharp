namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
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

        internal CSSBorderImageWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageWidth, rule)
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

        internal override void Reset()
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
            var mode = value.ToImageBorderWidth();

            if (mode != null)
                _top = _right = _left = _bottom = mode;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else
                return false;

            return true;
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 4)
                return false;

            IDistance top = null;
            IDistance right = null;
            IDistance bottom = null;
            IDistance left = null;

            foreach (var value in values)
            {
                var width = value.ToImageBorderWidth();

                if (width == null)
                    return false;

                if (top == null)
                    top = width;
                else if (right == null)
                    right = width;
                else if (bottom == null)
                    bottom = width;
                else if (left == null)
                    left = width;
            }

            _top = top;
            _right = right ?? _top;
            _bottom = bottom ?? _top;
            _left = left ?? _right;
            return true;
        }

        #endregion
    }
}
