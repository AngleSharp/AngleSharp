namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// Gets the enumeration of all images.
    /// </summary>
    sealed class CssBackgroundImageProperty : CssProperty
    {
        #region ctor

        internal CssBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return Converters.MultipleImageSourceConverter; }
        }

        #endregion
    }
}
