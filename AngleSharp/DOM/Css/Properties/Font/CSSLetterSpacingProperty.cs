namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/letter-spacing
    /// </summary>
    sealed class CSSLetterSpacingProperty : CSSProperty, ICssLetterSpacingProperty
    {
        #region Fields

        internal static readonly Length? Default = null;
        internal static readonly IValueConverter<Length?> Converter = Converters.LengthConverter.ToNullable().Or(Keywords.Normal, Default);
        Length? _spacing;

        #endregion

        #region ctor

        internal CSSLetterSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.LetterSpacing, rule, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
            Reset();
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

        void SetSpacing(Length? spacing)
        {
            _spacing = spacing;
        }

        internal override void Reset()
        {
            _spacing = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetSpacing);
        }

        #endregion
    }
}
