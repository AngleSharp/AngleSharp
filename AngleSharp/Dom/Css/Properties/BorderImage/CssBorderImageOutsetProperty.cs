namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CssBorderImageOutsetProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter TheConverter = Converters.LengthOrPercentConverter.Periodic();
        static readonly IValueConverter StyleConverter = TheConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssBorderImageOutsetProperty()
            : base(PropertyNames.BorderImageOutset)
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
