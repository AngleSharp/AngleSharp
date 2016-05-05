namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom
    /// </summary>
    sealed class CssBorderBottomProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option().For(PropertyNames.BorderBottomWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.BorderBottomStyle),
            Converters.CurrentColorConverter.Option().For(PropertyNames.BorderBottomColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderBottomProperty()
            : base(PropertyNames.BorderBottom, PropertyFlags.Animatable)
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
