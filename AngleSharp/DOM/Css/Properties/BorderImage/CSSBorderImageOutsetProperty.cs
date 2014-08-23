namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    public sealed class CSSBorderImageOutsetProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _top;
        CSSCalcValue _right;
        CSSCalcValue _bottom;
        CSSCalcValue _left;

        #endregion

        #region ctor

        internal CSSBorderImageOutsetProperty()
            : base(PropertyNames.BorderImageOutset)
        {
            _top = CSSCalcValue.Zero;
            _right = CSSCalcValue.Zero;
            _bottom = CSSCalcValue.Zero;
            _left = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length or percentage for the outset of the top border.
        /// </summary>
        internal CSSCalcValue Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the right border.
        /// </summary>
        internal CSSCalcValue Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the bottom border.
        /// </summary>
        internal CSSCalcValue Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the left border.
        /// </summary>
        internal CSSCalcValue Left
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
            var calc = value.AsCalc();

            if (calc != null)
                _top = _bottom = _right = _left = calc;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 4)
                return false;

            var top = values[0].AsCalc();
            var right = values[1].AsCalc();
            var bottom = top;
            var left = right;

            if (top == null || right == null)
                return false;

            if (values.Length > 2)
            {
                bottom = values[2].AsCalc();

                if (bottom == null)
                    return false;

                if (values.Length > 3)
                {
                    left = values[3].AsCalc();

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
