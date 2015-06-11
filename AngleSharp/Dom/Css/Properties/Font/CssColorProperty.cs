namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
    /// Gets the selected color for the foreground.
    /// </summary>
    sealed class CssColorProperty : CssProperty
    {
        #region ctor

        internal CssColorProperty()
            : base(PropertyNames.Color, PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Black
            get { return Converters.ColorConverter; }
        }

        #endregion
    }
}
