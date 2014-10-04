namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-color
    /// </summary>
    sealed class CSSTextDecorationColorProperty : CSSProperty, ICssTextDecorationColorProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        internal CSSTextDecorationColorProperty()
            : base(PropertyNames.TextDecorationColor)
        {
            _color = Color.Black;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected text-decoration color.
        /// </summary>
        public Color Color
        {
            get { return _color; }
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
            var color = value.ToColor();

            if (color.HasValue)
                _color = color.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
