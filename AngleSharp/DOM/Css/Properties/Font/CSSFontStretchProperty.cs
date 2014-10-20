namespace AngleSharp.DOM.Css
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

        internal override void Reset()
        {
            _stretch = FontStretch.Normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var stretch = value.ToFontStretch();

            if (stretch.HasValue)
            {
                _stretch = stretch.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
