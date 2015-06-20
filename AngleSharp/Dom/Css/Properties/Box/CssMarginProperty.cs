namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CssMarginProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.Periodic(
            PropertyNames.MarginTop, PropertyNames.MarginRight, PropertyNames.MarginBottom, PropertyNames.MarginLeft).OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssMarginProperty()
            : base(PropertyNames.Margin)
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
