namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// Gets the color of the outline.
    /// Gets if the color is inverted.
    /// </summary>
    sealed class CssOutlineColorProperty : CssProperty
    {
        #region ctor

        internal CssOutlineColorProperty()
            : base(PropertyNames.OutlineColor, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Transparent
            get { return Converters.InvertedColorConverter; }
        }

        #endregion
    }
}
