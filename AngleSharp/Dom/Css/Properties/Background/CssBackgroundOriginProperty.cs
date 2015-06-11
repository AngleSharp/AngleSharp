namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// Gets an enumeration with the desired origin settings.
    /// </summary>
    sealed class CssBackgroundOriginProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<BoxModel[]> ListConverter = 
            Converters.BoxModelConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundOriginProperty()
            : base(PropertyNames.BackgroundOrigin)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: BoxModel.PaddingBox
            get { return ListConverter; }
        }

        #endregion
    }
}
