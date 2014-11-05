namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// </summary>
    sealed class CSSOutlineColorProperty : CSSProperty, ICssOutlineColorProperty
    {
        #region Fields

        ICssObject _mode;

        #endregion

        #region ctor

        internal CSSOutlineColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.OutlineColor, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        public Color Color
        {
            get { return _mode is Color ? (Color)_mode : Color.Transparent; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = Colors.Invert;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _mode = color.Value;
            else if (value.Is(Keywords.Invert))
                _mode = Colors.Invert;
            else
                return false;

            return true;
        }

        #endregion
    }
}
