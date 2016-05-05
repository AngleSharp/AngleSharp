namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// Gets the enumeration of all images.
    /// </summary>
    sealed class CssBackgroundImageProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.MultipleImageSourceConverter.OrDefault();

        #endregion

        #region ctor

        internal CssBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
