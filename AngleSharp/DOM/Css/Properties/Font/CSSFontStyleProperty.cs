namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// </summary>
    sealed class CSSFontStyleProperty : CSSProperty, ICssFontStyleProperty
    {
        #region Fields

        internal static readonly FontStyle Default = FontStyle.Normal;
        internal static readonly IValueConverter<FontStyle> Converter = From(Map.FontStyles);
        FontStyle _style;

        #endregion

        #region ctor

        internal CSSFontStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontStyle, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font style.
        /// </summary>
        public FontStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        public void SetStyle(FontStyle style)
        {
            _style = style;
        }

        internal override void Reset()
        {
            _style = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetStyle);
        }

        #endregion
    }
}
