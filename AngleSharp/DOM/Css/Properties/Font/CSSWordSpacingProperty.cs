namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// </summary>
    sealed class CssWordSpacingProperty : CssProperty, ICssWordSpacingProperty
    {
        #region Fields

        internal static readonly Length? Default = null;
        internal static readonly IValueConverter<Length?> Converter = Converters.LengthConverter.ToNullable().Or(Keywords.Normal, Default);
        Length? _spacing;

        #endregion

        #region ctor

        internal CssWordSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.WordSpacing, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if normal inter-word space, as defined by the current
        /// font and/or the browser, is active.
        /// </summary>
        public Boolean IsNormal
        {
            get { return _spacing.HasValue == false; }
        }

        /// <summary>
        /// Gets the defined custom spacing, if any.
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
