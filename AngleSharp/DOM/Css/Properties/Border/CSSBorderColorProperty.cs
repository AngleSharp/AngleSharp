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

        Color _top;
        Color _right;
        Color _bottom;
        Color _left;

        #endregion

        #region ctor

        internal CSSBorderColorProperty()
            : base(PropertyNames.BorderColor, PropertyFlags.Hashless)
        {
            _top = Color.Transparent;
            _right = Color.Transparent;
            _bottom = Color.Transparent;
            _left = Color.Transparent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the color of the top border.
        /// </summary>
        public Color Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the color of the right border.
        /// </summary>
        public Color Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the color of the bottom border.
        /// </summary>
        public Color Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the color of the left border.
        /// </summary>
        public Color Left
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
            Color? top;
            Color? bottom;
            Color? right;
            Color? left;

            if (values.Length == 1)
            {
                right = left = bottom = top = values[0].ToColor();
            }
            else if (values.Length == 2)
            {
                bottom = top = values[0].ToColor();
                left = right = values[1].ToColor();
            }
            else if (values.Length == 3)
            {
                top = values[0].ToColor();
                left = right = values[1].ToColor();
                bottom = values[2].ToColor();
            }
            else if (values.Length == 4)
            {
                top = values[0].ToColor();
                right = values[1].ToColor();
                bottom = values[2].ToColor();
                left = values[3].ToColor();
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
