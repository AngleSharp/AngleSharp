namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter ImageConverter = WithAny(
            OptionalImageSourceConverter.Option().For(PropertyNames.BorderImageSource),
            WithOrder(
                CssBorderImageSliceProperty.TheConverter.Option().For(PropertyNames.BorderImageSlice),
                CssBorderImageWidthProperty.TheConverter.StartsWithDelimiter().Option().For(PropertyNames.BorderImageWidth),
                CssBorderImageOutsetProperty.TheConverter.StartsWithDelimiter().Option().For(PropertyNames.BorderImageOutset)),
            CssBorderImageRepeatProperty.TheConverter.Option().For(PropertyNames.BorderImageRepeat)).OrDefault();

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
