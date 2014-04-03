namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing
    /// </summary>
    sealed class CSSLetterSpacingProperty : CSSProperty
    {
        #region Fields

        static readonly NormalLetterSpacingMode _normal = new NormalLetterSpacingMode();
        LetterSpacingMode _mode;

        #endregion

        #region ctor

        public CSSLetterSpacingProperty()
            : base(PropertyNames.LetterSpacing)
        {
            _inherited = true;
            _mode = _normal;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("normal", StringComparison.OrdinalIgnoreCase))
                _mode = _normal;
            else if (value.ToLength().HasValue)
                _mode = new CustomLetterSpacingMode(value.ToLength().Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class LetterSpacingMode
        {
            //TODO add members
        }

        /// <summary>
        /// The spacing is the normal spacing for the current font. This value
        /// allows the user agent to alter the space between characters in order
        /// to justify text. That's the difference to the length value 0.
        /// </summary>
        sealed class NormalLetterSpacingMode : LetterSpacingMode
        {

        }

        /// <summary>
        /// Indicates inter-character space in addition to the default space between
        /// characters. Values may be negative, but there may be implementation-specific
        /// limits. User agents may not further increase or decrease the inter-character
        /// space in order to justify text.
        /// </summary>
        sealed class CustomLetterSpacingMode : LetterSpacingMode
        {
            Length _spacing;

            public CustomLetterSpacingMode(Length spacing)
            {
                _spacing = spacing;
            }
        }

        #endregion
    }
}
