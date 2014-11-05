namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CSSPaddingProperty : CSSShorthandProperty, ICssPaddingProperty
    {
        #region Fields

        readonly CSSPaddingTopProperty _top;
        readonly CSSPaddingRightProperty _right;
        readonly CSSPaddingBottomProperty _bottom;
        readonly CSSPaddingLeftProperty _left;

        #endregion

        #region ctor

        internal CSSPaddingProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Padding, rule)
        {
            _top = Get<CSSPaddingTopProperty>();
            _right = Get<CSSPaddingRightProperty>();
            _bottom = Get<CSSPaddingBottomProperty>();
            _left = Get<CSSPaddingLeftProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top padding.
        /// </summary>
        public IDistance Top
        {
            get { return _top.Top; }
        }

        /// <summary>
        /// Gets the value for the right padding.
        /// </summary>
        public IDistance Right
        {
            get { return _right.Right; }
        }

        /// <summary>
        /// Gets the value for the bottom padding.
        /// </summary>
        public IDistance Bottom
        {
            get { return _bottom.Bottom; }
        }

        /// <summary>
        /// Gets the value for the left padding.
        /// </summary>
        public IDistance Left
        {
            get { return _left.Left; }
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

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return SerializePeriodic(_top, _right, _bottom, _left);
        }

        #endregion
    }
}
