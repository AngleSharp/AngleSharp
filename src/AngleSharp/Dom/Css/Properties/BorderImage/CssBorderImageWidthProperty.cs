namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CssBorderImageWidthProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter TheConverter = Converters.ImageBorderWidthConverter.Periodic();
        static readonly IValueConverter StyleConverter = TheConverter.OrDefault(Length.Full);

        #endregion

        #region ctor

        internal CssBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
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
