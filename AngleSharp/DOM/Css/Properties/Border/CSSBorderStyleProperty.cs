namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    sealed class CSSBorderStyleProperty : CSSProperty, ICssBorderStylesProperty
    {
        #region Fields

        LineStyle _top;
        LineStyle _right;
        LineStyle _bottom;
        LineStyle _left;

        #endregion

        #region ctor

        internal CSSBorderStyleProperty()
            : base(PropertyNames.BorderStyle)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the style of the top border.
        /// </summary>
        public LineStyle Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the style of the right border.
        /// </summary>
        public LineStyle Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the style of the bottom border.
        /// </summary>
        public LineStyle Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the style of the left border.
        /// </summary>
        public LineStyle Left
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _left = LineStyle.None;
            _right = LineStyle.None;
            _bottom = LineStyle.None;
            _top = LineStyle.None;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value as CSSValueList ?? new CSSValueList(value);
            LineStyle? top;
            LineStyle? bottom;
            LineStyle? right;
            LineStyle? left;

            if (values.Length == 1)
            {
                right = left = bottom = top = values[0].ToLineStyle();
            }
            else if (values.Length == 2)
            {
                bottom = top = values[0].ToLineStyle();
                left = right = values[1].ToLineStyle();
            }
            else if (values.Length == 3)
            {
                top = values[0].ToLineStyle();
                left = right = values[1].ToLineStyle();
                bottom = values[2].ToLineStyle();
            }
            else if (values.Length == 4)
            {
                top = values[0].ToLineStyle();
                right = values[1].ToLineStyle();
                bottom = values[2].ToLineStyle();
                left = values[3].ToLineStyle();
            }
            else
                return false;

            if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return false;

            _top = top.Value;
            _bottom = bottom.Value;
            _right = right.Value;
            _left = left.Value;
            return true;
        }

        #endregion
    }
}
