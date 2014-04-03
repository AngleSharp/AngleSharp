namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// </summary>
    sealed class CSSFontVariantProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontVariant> _styles = new Dictionary<String, FontVariant>(StringComparer.OrdinalIgnoreCase);
        static readonly CSSFontVariantProperty _default = new CSSFontVariantProperty();
        FontVariant _style;

        #endregion

        #region ctor

        static CSSFontVariantProperty()
        {
            _styles.Add("normal", FontVariant.Normal);
            _styles.Add("small-caps", FontVariant.SmallCaps);
        }

        public CSSFontVariantProperty()
            : base(PropertyNames.FONT_VARIANT)
        {
            _inherited = true;
            _style = FontVariant.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default font variant.
        /// </summary>
        public static CSSFontVariantProperty Default
        {
            get { return _default; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontVariant style;
                
            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Variant Enumeration

        enum FontVariant
        {
            /// <summary>
            /// Specifies a normal font face.
            /// </summary>
            Normal,
            /// <summary>
            /// Specifies a font that is labeled as a small-caps font. 
            /// </summary>
            SmallCaps
        }

        #endregion
    }
}
