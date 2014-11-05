namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CSSBorderWidthProperty : CSSShorthandProperty, ICssBorderWidthsProperty
    {
        #region Fields

        readonly CSSBorderTopWidthProperty _top;
        readonly CSSBorderRightWidthProperty _right;
        readonly CSSBorderBottomWidthProperty _bottom;
        readonly CSSBorderLeftWidthProperty _left;

        #endregion

        #region ctor

        internal CSSBorderWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderWidth, rule, PropertyFlags.Animatable)
        {
            _top = Get<CSSBorderTopWidthProperty>();
            _right = Get<CSSBorderRightWidthProperty>();
            _bottom = Get<CSSBorderBottomWidthProperty>();
            _left = Get<CSSBorderLeftWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the width of the top border.
        /// </summary>
        public Length Top
        {
            get { return _top.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the right border.
        /// </summary>
        public Length Right
        {
            get { return _right.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the bottom border.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom.Width; }
        }

        /// <summary>
        /// Gets the value for the width of the left border.
        /// </summary>
        public Length Left
        {
            get { return _left.Width; }
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
