namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// </summary>
    sealed class CSSFontVariantProperty : CSSProperty, ICssFontVariantProperty
    {
        #region Fields

        FontVariant _variant;

        #endregion

        #region ctor

        internal CSSFontVariantProperty()
            : base(PropertyNames.FontVariant, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font variant transformation, if any.
        /// </summary>
        public FontVariant Variant
        {
            get { return _variant; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _variant = FontVariant.Normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var variant = value.ToFontVariant();

            if (variant.HasValue)
            {
                _variant = variant.Value;
                return true;
            }

            return false;
        }

        #endregion
    }
}
