namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CssBorderLeftProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option().For(PropertyNames.BorderLeftWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.BorderLeftStyle),
            Converters.CurrentColorConverter.Option().For(PropertyNames.BorderLeftColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderLeftProperty()
            : base(PropertyNames.BorderLeft, PropertyFlags.Animatable)
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
