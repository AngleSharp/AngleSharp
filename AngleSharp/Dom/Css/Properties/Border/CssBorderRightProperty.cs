namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CssBorderRightProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option().For(PropertyNames.BorderRightWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.BorderRightStyle),
            Converters.CurrentColorConverter.Option().For(PropertyNames.BorderRightColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderRightProperty()
            : base(PropertyNames.BorderRight, PropertyFlags.Animatable)
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
