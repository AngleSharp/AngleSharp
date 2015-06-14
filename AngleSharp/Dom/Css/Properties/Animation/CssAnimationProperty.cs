namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation
    /// </summary>
    sealed class CssAnimationProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.WithAny(
            Converters.TimeConverter.Option(),
            Converters.TransitionConverter.Option(),
            Converters.TimeConverter.Option(),
            Converters.PositiveOrInfiniteNumberConverter.Option(),
            Converters.AnimationDirectionConverter.Option(),
            Converters.AnimationFillStyleConverter.Option(),
            Converters.PlayStateConverter.Option(),
            Converters.IdentifierConverter.Option()).FromList().OrDefault();

        #endregion

        #region ctor

        internal CssAnimationProperty()
            : base(PropertyNames.Animation)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
