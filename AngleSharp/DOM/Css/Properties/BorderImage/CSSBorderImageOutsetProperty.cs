namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CSSBorderImageOutsetProperty : CSSProperty, ICssBorderImageOutsetProperty
    {
        #region Fields

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSBorderImageOutsetProperty()
            : base(PropertyNames.BorderImageOutset)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length or percentage for the outset of the top border.
        /// </summary>
        public IDistance OutsetTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the right border.
        /// </summary>
        public IDistance OutsetRight
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the bottom border.
        /// </summary>
        public IDistance OutsetBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the left border.
        /// </summary>
        public IDistance OutsetLeft
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _top = Percent.Zero;
            _right = Percent.Zero;
            _bottom = Percent.Zero;
            _left = Percent.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var calc = value.ToDistance();

            if (calc != null)
                _top = _bottom = _right = _left = calc;
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
                var width = value.ToDistance();

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
