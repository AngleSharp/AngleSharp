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

        Length _top;
        Length _right;
        Length _bottom;
        Length _left;

        #endregion

        #region ctor

        internal CSSBorderWidthProperty()
            : base(PropertyNames.BorderWidth)
        {
            _inherited = false;
            _top = Length.Medium;
            _right = Length.Medium;
            _bottom = Length.Medium;
            _left = Length.Medium;
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
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            Length? top;
            Length? bottom;
            Length? right;
            Length? left;

            if (values.Length == 1)
            {
                right = left = bottom = top = values[0].ToLength();
            }
            else if (values.Length == 2)
            {
                bottom = top = values[0].ToLength();
                left = right = values[1].ToLength();
            }
            else if (values.Length == 3)
            {
                top = values[0].ToLength();
                left = right = values[1].ToLength();
                bottom = values[2].ToLength();
            }
            else if (values.Length == 4)
            {
                top = values[0].ToLength();
                right = values[1].ToLength();
                bottom = values[2].ToLength();
                left = values[3].ToLength();
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
