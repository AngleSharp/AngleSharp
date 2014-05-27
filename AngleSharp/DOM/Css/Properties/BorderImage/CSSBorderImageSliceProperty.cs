namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// or even better:
    /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
    /// </summary>
    public sealed class CSSBorderImageSliceProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _top;
        CSSCalcValue _right;
        CSSCalcValue _bottom;
        CSSCalcValue _left;
        Boolean _fill;

        #endregion

        #region ctor

        internal CSSBorderImageSliceProperty()
            : base(PropertyNames.BorderImageSlice)
        {
            _inherited = false;
            _top = CSSCalcValue.Full;
            _right = CSSCalcValue.Full;
            _bottom = CSSCalcValue.Full;
            _left = CSSCalcValue.Full;
            _fill = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        public CSSCalcValue Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        public CSSCalcValue Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        public CSSCalcValue Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        public CSSCalcValue Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets if the center patch should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _fill; }
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
            var mode = ToMode(value);

            if (mode != null)
                _top = _left = _bottom = _right = mode;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        static CSSCalcValue ToMode(CSSValue value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return CSSCalcValue.FromPercent(percent.Value);

            var number = value.ToNumber();

            if (number.HasValue)
                return CSSCalcValue.FromLength(new Length(number.Value, Length.Unit.Px));

            return null;
        }

        Boolean Evaluate(CSSValueList values)
        {
            if (values.Length > 5)
                return false;

            var fill = false;
            var modes = new List<CSSCalcValue>(values.Length);

            foreach (var value in values)
            {
                if (!fill && value.Is("fill"))
                    fill = true;
                else if (ToMode(value) == null)
                    return false;
                else
                    modes.Add(ToMode(value));
            }

            if (modes.Count == 5 || modes.Count == 0)
                return false;

            _fill = fill;
            _bottom = _left = _right = _top = modes[0];

            if (modes.Count > 1)
            {
                _left = _right = modes[1];

                if (modes.Count > 2)
                {
                    _bottom = modes[2];

                    if (modes.Count > 3)
                        _left = modes[3];
                }
            }

            return true;
        }

        #endregion
    }
}
