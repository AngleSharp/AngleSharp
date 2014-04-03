namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// </summary>
    sealed class CSSFontStretchProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStretch> _styles = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase);
        FontStretch _stretch;

        #endregion

        #region ctor

        static CSSFontStretchProperty()
        {
            _styles.Add("normal", FontStretch.Normal);
            _styles.Add("ultra-condensed", FontStretch.UltraCondensed);
            _styles.Add("extra-condensed", FontStretch.ExtraCondensed);
            _styles.Add("condensed", FontStretch.Condensed);
            _styles.Add("semi-condensed", FontStretch.SemiCondensed);
            _styles.Add("semi-expanded", FontStretch.SemiExpanded);
            _styles.Add("expanded", FontStretch.Expanded);
            _styles.Add("extra-expanded", FontStretch.ExtraExpanded);
            _styles.Add("ultra-expanded", FontStretch.UltraExpanded);
        }

        public CSSFontStretchProperty()
            : base(PropertyNames.FONT_STRETCH)
        {
            _inherited = true;
            _stretch = FontStretch.Normal;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontStretch style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _stretch = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Style Enumeration

        enum FontStretch
        {
            /// <summary>
            /// Specifies a normal font face.
            /// </summary>
            Normal,
            /// <summary>
            /// Specifies a font face most condensed.
            /// </summary>
            UltraCondensed,
            /// <summary>
            /// Specifies a font face a lot more condensed than normal.
            /// </summary>
            ExtraCondensed, 
            /// <summary>
            /// Specifies a font face more condensed than normal.
            /// </summary>
            Condensed,
            /// <summary>
            /// Specifies a font face a little more condensed than normal.
            /// </summary>
            SemiCondensed,
            /// <summary>
            /// Specifies a font face a little more expanded than normal.
            /// </summary>
            SemiExpanded,
            /// <summary>
            /// Specifies a font face more expanded than normal.
            /// </summary>
            Expanded,
            /// <summary>
            /// Specifies a font face more expanded than normal.
            /// </summary>
            ExtraExpanded,
            /// <summary>
            /// Specifies a font face most expanded.
            /// </summary>
            UltraExpanded
        }

        #endregion
    }
}
