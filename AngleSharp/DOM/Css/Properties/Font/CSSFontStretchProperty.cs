namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// </summary>
    sealed class CSSFontStretchProperty : CSSProperty, ICssFontStretchProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStretch> _styles = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase);
        FontStretch _stretch;

        #endregion

        #region ctor

        static CSSFontStretchProperty()
        {
            _styles.Add(Keywords.Normal, FontStretch.Normal);
            _styles.Add(Keywords.UltraCondensed, FontStretch.UltraCondensed);
            _styles.Add(Keywords.ExtraCondensed, FontStretch.ExtraCondensed);
            _styles.Add(Keywords.Condensed, FontStretch.Condensed);
            _styles.Add(Keywords.SemiCondensed, FontStretch.SemiCondensed);
            _styles.Add(Keywords.SemiExpanded, FontStretch.SemiExpanded);
            _styles.Add(Keywords.Expanded, FontStretch.Expanded);
            _styles.Add(Keywords.ExtraExpanded, FontStretch.ExtraExpanded);
            _styles.Add(Keywords.UltraExpanded, FontStretch.UltraExpanded);
        }

        internal CSSFontStretchProperty()
            : base(PropertyNames.FontStretch, PropertyFlags.Inherited)
        {
            _stretch = FontStretch.Normal;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            FontStretch style;

            if (_styles.TryGetValue(value, out style))
                _stretch = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
