namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-color
    /// Gets the color of the background.
    /// </summary>
    sealed class CssBackgroundColorProperty : CssProperty
    {
        #region ctor

        internal CssBackgroundColorProperty()
            : base(PropertyNames.BackgroundColor, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Transparent
            get { return Converters.CurrentColorConverter; }
        }

        #endregion
    }
}
