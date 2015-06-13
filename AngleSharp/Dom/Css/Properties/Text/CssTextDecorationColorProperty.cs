namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-color
    /// Gets the selected text-decoration color.
    /// </summary>
    sealed class CssTextDecorationColorProperty : CssProperty
    {
        #region ctor

        internal CssTextDecorationColorProperty()
            : base(PropertyNames.TextDecorationColor, PropertyFlags.Animatable)
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
