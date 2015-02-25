namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty, ICssFontVariantProperty
    {
        #region Fields

        internal static readonly FontVariant Default = FontVariant.Normal;
        internal static readonly IValueConverter<FontVariant> Converter = Converters.Assign(Keywords.Normal, FontVariant.Normal).Or(Keywords.SmallCaps, FontVariant.SmallCaps);
        FontVariant _variant;

        #endregion

        #region ctor

        internal CssFontVariantProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontVariant, rule, PropertyFlags.Inherited)
        {
            Reset();
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

        void SetVariant(FontVariant variant)
        {
            _variant = variant;
        }

        internal override void Reset()
        {
            _variant = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetVariant);
        }

        #endregion
    }
}
