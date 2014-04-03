namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// </summary>
    sealed class CSSFontStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStyle> _styles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly CSSFontStyleProperty _default = new CSSFontStyleProperty();
        FontStyle _style;

        #endregion

        #region ctor

        static CSSFontStyleProperty()
        {
            _styles.Add("normal", FontStyle.Normal);
            _styles.Add("italic", FontStyle.Italic);
            _styles.Add("oblique", FontStyle.Oblique);
        }

        public CSSFontStyleProperty()
            : base(PropertyNames.FONT_STYLE)
        {
            _inherited = true;
            _style = FontStyle.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default font style.
        /// </summary>
        public static CSSFontStyleProperty Default
        {
            get { return _default; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Style Enumeration

        enum FontStyle
        {
            /// <summary>
            /// Selects a font that is classified as normal within a font-family.
            /// </summary>
            Normal,
            /// <summary>
            /// Selects a font that is labeled italic, if that is not available, one labeled oblique.
            /// </summary>
            Italic,
            /// <summary>
            /// Selects a font that is labeled oblique.
            /// </summary>
            Oblique
        }

        #endregion
    }
}
