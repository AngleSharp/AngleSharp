namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// Gets the selected font variant transformation, if any.
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty
    {
        #region ctor

        internal CssFontVariantProperty()
            : base(PropertyNames.FontVariant, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: FontVariant.Normal
            get { return Converters.FontVariantConverter; }
        }

        #endregion
    }
}
