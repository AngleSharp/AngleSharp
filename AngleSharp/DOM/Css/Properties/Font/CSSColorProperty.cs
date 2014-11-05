namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
    /// </summary>
    sealed class CSSColorProperty : CSSProperty, ICssColorProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        internal CSSColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Color, rule, PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected color for the foreground.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _color = Color.Black;
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
            {
                _color = color.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
