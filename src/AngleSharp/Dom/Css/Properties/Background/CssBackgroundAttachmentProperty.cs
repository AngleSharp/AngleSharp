namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// Gets an enumeration with the desired attachment settings.
    /// </summary>
    sealed class CssBackgroundAttachmentProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter AttachmentConverter = Converters.BackgroundAttachmentConverter.FromList().OrDefault(BackgroundAttachment.Scroll);

        #endregion

        #region ctor

        internal CssBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return AttachmentConverter; }
        }

        #endregion
    }
}
