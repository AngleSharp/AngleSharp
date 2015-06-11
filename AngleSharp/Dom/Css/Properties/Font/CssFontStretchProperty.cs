namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// Gets the selected font stretch setting.
    /// </summary>
    sealed class CssFontStretchProperty : CssProperty
    {
        #region ctor

        internal CssFontStretchProperty()
            : base(PropertyNames.FontStretch, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: FontStretch.Normal
            get { return Converters.FontStretchConverter; }
        }

        #endregion
    }
}
