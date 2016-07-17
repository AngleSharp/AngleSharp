namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top
    /// </summary>
    sealed class CssBorderTopProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = WithAny(
            LineWidthConverter.Option().For(PropertyNames.BorderTopWidth),
            LineStyleConverter.Option().For(PropertyNames.BorderTopStyle),
            CurrentColorConverter.Option().For(PropertyNames.BorderTopColor)
        ).OrDefault();

        #endregion

        #region ctor

        internal CssBorderTopProperty()
            : base(PropertyNames.BorderTop, PropertyFlags.Animatable)
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
