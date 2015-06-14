namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.OptionalLengthConverter.OrDefault();

        #endregion

        #region ctor

        internal CssLetterSpacingProperty()
            : base(PropertyNames.LetterSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
