namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// </summary>
    sealed class CSSFontStyleProperty : CSSProperty, ICssFontStyleProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStyle> _styles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
        FontStyle _style;

        #endregion

        #region ctor

        static CSSFontStyleProperty()
        {
            _styles.Add(Keywords.Normal, FontStyle.Normal);
            _styles.Add(Keywords.Italic, FontStyle.Italic);
            _styles.Add(Keywords.Oblique, FontStyle.Oblique);
        }

        internal CSSFontStyleProperty()
            : base(PropertyNames.FontStyle, PropertyFlags.Inherited)
        {
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

        protected override void Reset()
        {
            _style = FontStyle.Normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            FontStyle style;

            if (_styles.TryGetValue(value, out style))
            {
                _style = style;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
