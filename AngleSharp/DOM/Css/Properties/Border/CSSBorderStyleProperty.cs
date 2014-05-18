namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    public sealed class CSSBorderStyleProperty : CSSProperty
    {
        #region Fields

        CSSBorderTopStyleProperty _top;
        CSSBorderRightStyleProperty _right;
        CSSBorderBottomStyleProperty _bottom;
        CSSBorderLeftStyleProperty _left;

        #endregion

        #region ctor

        internal CSSBorderStyleProperty()
            : base(PropertyNames.BorderStyle)
        {
            _inherited = false;
            _left = new CSSBorderLeftStyleProperty();
            _right = new CSSBorderRightStyleProperty();
            _bottom = new CSSBorderBottomStyleProperty();
            _top = new CSSBorderTopStyleProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the style of the top border.
        /// </summary>
        public CSSBorderTopStyleProperty Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the style of the right border.
        /// </summary>
        public CSSBorderRightStyleProperty Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the style of the bottom border.
        /// </summary>
        public CSSBorderBottomStyleProperty Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the style of the left border.
        /// </summary>
        public CSSBorderLeftStyleProperty Left
        {
            get { return _left; }
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

            var values = value as CSSValueList ?? new CSSValueList(value);
            var top = new CSSBorderTopStyleProperty();
            var bottom = new CSSBorderBottomStyleProperty();
            var right = new CSSBorderRightStyleProperty();
            var left = new CSSBorderLeftStyleProperty();

            if (values.Length == 1)
            {
                if (!CheckSingleProperty(top, 0, values))
                    return false;

                right.Value = left.Value = bottom.Value = top.Value;
            }
            else if (values.Length == 2)
            {
                if (!CheckSingleProperty(top, 0, values) || !CheckSingleProperty(right, 1, values))
                    return false;

                bottom.Value = top.Value;
                left.Value = right.Value;
            }
            else if (values.Length == 3)
            {
                if (!CheckSingleProperty(top, 0, values) || !CheckSingleProperty(right, 1, values) || !CheckSingleProperty(bottom, 2, values))
                    return false;

                left.Value = right.Value;
            }
            else if (values.Length == 4)
            {
                if (!CheckSingleProperty(top, 0, values) || !CheckSingleProperty(right, 1, values) || !CheckSingleProperty(bottom, 2, values) || !CheckSingleProperty(left, 3, values))
                    return false;
            }
            else
                return false;

            _top = top;
            _bottom = bottom;
            _right = right;
            _left = left;
            return true;
        }

        #endregion
    }
}
