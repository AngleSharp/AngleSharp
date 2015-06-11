namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// Gets the selected font-size.
    /// </summary>
    sealed class CssFontSizeProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: FontSize.Medium.ToLength()
            get { return Converters.FontSizeConverter; }
        }

        #endregion
    }
}
