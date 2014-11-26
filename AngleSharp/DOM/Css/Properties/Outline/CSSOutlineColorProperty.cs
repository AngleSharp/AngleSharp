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

        Color _color;
        Boolean _inverted;

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
            get { return _color; }
        }

        /// <summary>
        /// Gets if the color is inverted.
        /// </summary>
        public Boolean IsInverted
        {
            get { return _inverted; }
        }

        #endregion

        #region Methods

        public void SetColor(Color color)
        {
            _color = color;
            _inverted = false;
        }

        public void SetInverted(Boolean active)
        {
            _inverted = active;
        }

        internal override void Reset()
        {
            _color = Color.Transparent;
            _inverted = true;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithColor().TryConvert(value, SetColor) || 
                   this.TakeOne(Keywords.Invert, true).TryConvert(value, SetInverted);
        }

        #endregion
    }
}
