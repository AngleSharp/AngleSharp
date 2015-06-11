namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-image
    /// Gets the selected image.
    /// </summary>
    sealed class CssListStyleImageProperty : CssProperty
    {
        #region ctor

        internal CssListStyleImageProperty()
            : base(PropertyNames.ListStyleImage, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return Converters.OptionalImageSourceConverter; }
        }

        #endregion
    }
}
