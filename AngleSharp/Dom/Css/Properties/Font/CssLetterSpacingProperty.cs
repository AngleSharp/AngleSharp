namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing
    /// Gets if the spacing is the normal spacing for the current font.
    /// This value allows the user agent to alter the space between
    /// characters in order to justify text. That's the difference to the
    /// length value 0.
    /// Gets the defined custom spacing, if any. Indicates inter-character
    /// space in addition to the default space between characters. Values
    /// may be negative, but there may be implementation-specific limits.
    /// User agents may not further increase or decrease the
    /// inter-character space in order to justify text.
    /// </summary>
    sealed class CssLetterSpacingProperty : CssProperty
    {
        #region ctor

        internal CssLetterSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.LetterSpacing, rule, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.OptionalLengthConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalLengthConverter.Validate(value);
        }

        #endregion
    }
}
