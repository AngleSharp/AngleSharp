namespace AngleSharp.Dom.Css
{
    using System;
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

        static readonly IValueConverter<BackgroundAttachment[]> AttachmentConverter =
            Converters.BackgroundAttachmentConverter.FromList();

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

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BackgroundAttachment.Scroll;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return AttachmentConverter.Validate(value);
        }

        #endregion
    }
}
