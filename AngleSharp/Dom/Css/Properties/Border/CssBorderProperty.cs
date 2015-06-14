namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border
    /// </summary>
    sealed class CssBorderProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option().For(PropertyNames.BorderWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.BorderStyle),
            Converters.CurrentColorConverter.Option().For(PropertyNames.BorderColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderProperty()
            : base(PropertyNames.Border, PropertyFlags.Animatable)
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
