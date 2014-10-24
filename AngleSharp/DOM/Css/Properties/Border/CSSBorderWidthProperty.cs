namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CSSBorderWidthProperty : CSSShorthandProperty, ICssBorderWidthsProperty
    {
        #region Fields

        Length _top;
        Length _right;
        Length _bottom;
        Length _left;

        #endregion

        #region ctor

        internal CSSBorderWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderWidth, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the width of the top border.
        /// </summary>
        public Length Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the width of the right border.
        /// </summary>
        public Length Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the width of the bottom border.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the width of the left border.
        /// </summary>
        public Length Left
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
            var values = value as CSSValueList ?? new CSSValueList(value);

            if (values.Length > 4)
                return false;

            var widths = new Length?[4];

            for (int i = 0; i < values.Length; i++)
            {
                var width = values[i].ToBorderWidth();

                if (!width.HasValue)
                    return false;

                widths[i] = width;
            }

            if (!widths[1].HasValue)
                widths[1] = widths[0];

            if (!widths[2].HasValue)
                widths[2] = widths[0];

            if (!widths[3].HasValue)
                widths[3] = widths[1];

            _top = widths[0].Value;
            _right = widths[1].Value;
            _bottom = widths[2].Value;
            _left = widths[3].Value;
            return true;
        }

        #endregion
    }
}
