namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-clip
    /// Gets an enumeration with the desired clip settings.
    /// </summary>
    sealed class CssBackgroundClipProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<BoxModel[]> ListConverter = 
            Converters.BoxModelConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundClipProperty()
            : base(PropertyNames.BackgroundClip)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BoxModel.BorderBox;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return ListConverter.Validate(value);
        }

        #endregion
    }
}
