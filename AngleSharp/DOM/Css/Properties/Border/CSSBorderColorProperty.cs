namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-color
    /// </summary>
    public sealed class CSSBorderColorProperty : CSSProperty
    {
        #region Fields

        CSSBorderTopColorProperty _top;
        CSSBorderRightColorProperty _right;
        CSSBorderBottomColorProperty _bottom;
        CSSBorderLeftColorProperty _left;

        #endregion

        #region ctor

        internal CSSBorderColorProperty()
            : base(PropertyNames.BorderColor)
        {
            _inherited = false;
            _top = new CSSBorderTopColorProperty();
            _right = new CSSBorderRightColorProperty();
            _bottom = new CSSBorderBottomColorProperty();
            _left = new CSSBorderLeftColorProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the color of the top border.
        /// </summary>
        public CSSBorderTopColorProperty Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the color of the right border.
        /// </summary>
        public CSSBorderRightColorProperty Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the color of the bottom border.
        /// </summary>
        public CSSBorderBottomColorProperty Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the color of the left border.
        /// </summary>
        public CSSBorderLeftColorProperty Left
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var top = new CSSBorderTopColorProperty();
            var bottom = new CSSBorderBottomColorProperty();
            var right = new CSSBorderRightColorProperty();
            var left = new CSSBorderLeftColorProperty();

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
