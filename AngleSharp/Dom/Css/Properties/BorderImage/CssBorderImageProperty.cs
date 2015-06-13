namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter ImageConverter = Converters.WithAny(
            Converters.OptionalImageSourceConverter.Option(),
            Converters.WithOrder(
                CssBorderImageSliceProperty.StyleConverter.Option(),
                CssBorderImageWidthProperty.StyleConverter.StartsWithDelimiter().Option(),
                CssBorderImageOutsetProperty.StyleConverter.StartsWithDelimiter().Option()),
            CssBorderImageRepeatProperty.StyleConverter.Option());

        #endregion

        #region ctor

        internal CssBorderImageProperty()
            : base(PropertyNames.BorderImage)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ImageConverter; }
        }

        #endregion
    }
}
