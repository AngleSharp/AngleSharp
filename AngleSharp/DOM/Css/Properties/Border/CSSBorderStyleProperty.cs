namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    sealed class CSSBorderStyleProperty : CSSShorthandProperty, ICssBorderStylesProperty
    {
        #region Fields

        readonly CSSBorderTopStyleProperty _top;
        readonly CSSBorderRightStyleProperty _right;
        readonly CSSBorderBottomStyleProperty _bottom;
        readonly CSSBorderLeftStyleProperty _left;

        #endregion

        #region ctor

        internal CSSBorderStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderStyle, rule)
        {
            _top = Get<CSSBorderTopStyleProperty>();
            _right = Get<CSSBorderRightStyleProperty>();
            _bottom = Get<CSSBorderBottomStyleProperty>();
            _left = Get<CSSBorderLeftStyleProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the style of the top border.
        /// </summary>
        public LineStyle Top
        {
            get { return _top.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the right border.
        /// </summary>
        public LineStyle Right
        {
            get { return _right.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the bottom border.
        /// </summary>
        public LineStyle Bottom
        {
            get { return _bottom.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the left border.
        /// </summary>
        public LineStyle Left
        {
            get { return _left.Style; }
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
            return ValidatePeriodic(value, _top, _right, _bottom, _left);
        }

        #endregion
    }
}
