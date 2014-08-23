namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing
    /// </summary>
    public sealed class CSSLetterSpacingProperty : CSSProperty
    {
        #region Fields

        Length? _spacing;

        #endregion

        #region ctor

        internal CSSLetterSpacingProperty()
            : base(PropertyNames.LetterSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
            _spacing = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the spacing is the normal spacing for the current font. This value
        /// allows the user agent to alter the space between characters in order to
        /// justify text. That's the difference to the length value 0.
        /// </summary>
        public Boolean IsNormal
        {
            get { return _spacing.HasValue == false; }
        }

        /// <summary>
        /// Gets the defined custom spacing, if any. Indicates inter-character space in
        /// addition to the default space between characters. Values may be negative,
        /// but there may be implementation-specific limits. User agents may not further
        /// increase or decrease the inter-character space in order to justify text.
        /// </summary>
        public Length? Spacing
        {
            get { return _spacing; }
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
            if (value.Is("normal"))
                _spacing = null;
            else if (value.ToLength().HasValue)
                _spacing = value.ToLength();
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
