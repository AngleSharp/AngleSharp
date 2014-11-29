namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// </summary>
    sealed class CSSFontStretchProperty : CSSProperty, ICssFontStretchProperty
    {
        #region Fields

        internal static readonly FontStretch Default = FontStretch.Normal;
        internal static readonly IValueConverter<FontStretch> Converter = From(Map.FontStretches);
        FontStretch _stretch;

        #endregion

        #region ctor

        internal CSSFontStretchProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontStretch, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font stretch setting.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch; }
        }

        #endregion

        #region Methods

        public void SetStretch(FontStretch stretch)
        {
            _stretch = stretch;
        }

        internal override void Reset()
        {
            _stretch = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetStretch);
        }

        #endregion
    }
}
