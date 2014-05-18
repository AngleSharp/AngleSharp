namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    public sealed class CSSBorderWidthProperty : CSSProperty
    {
        #region Fields

        CSSBorderTopWidthProperty _top;
        CSSBorderRightWidthProperty _right;
        CSSBorderBottomWidthProperty _bottom;
        CSSBorderLeftWidthProperty _left;

        #endregion

        #region ctor

        internal CSSBorderWidthProperty()
            : base(PropertyNames.BorderWidth)
        {
            _inherited = false;
            _top = new CSSBorderTopWidthProperty();
            _right = new CSSBorderRightWidthProperty();
            _bottom = new CSSBorderBottomWidthProperty();
            _left = new CSSBorderLeftWidthProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the width of the top border.
        /// </summary>
        public CSSBorderTopWidthProperty Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the width of the right border.
        /// </summary>
        public CSSBorderRightWidthProperty Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the width of the bottom border.
        /// </summary>
        public CSSBorderBottomWidthProperty Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the width of the left border.
        /// </summary>
        public CSSBorderLeftWidthProperty Left
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
            var top = new CSSBorderTopWidthProperty();
            var bottom = new CSSBorderBottomWidthProperty();
            var right = new CSSBorderRightWidthProperty();
            var left = new CSSBorderLeftWidthProperty();

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
