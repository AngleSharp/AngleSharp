namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// </summary>
    sealed class CSSFontStyleProperty : CSSProperty, ICssFontStyleProperty
    {
        #region Fields

        FontStyle _style;

        #endregion

        #region ctor

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

        internal override void Reset()
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
            var style = value.ToFontStyle();

            if (style.HasValue)
            {
                _style = style.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
