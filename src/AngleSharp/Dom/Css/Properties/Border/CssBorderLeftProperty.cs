namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CssBorderLeftProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = WithAny(
            LineWidthConverter.Option().For(PropertyNames.BorderLeftWidth),
            LineStyleConverter.Option().For(PropertyNames.BorderLeftStyle),
            CurrentColorConverter.Option().For(PropertyNames.BorderLeftColor)
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
