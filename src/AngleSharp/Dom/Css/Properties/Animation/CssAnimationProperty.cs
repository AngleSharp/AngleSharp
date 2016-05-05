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
            Converters.TimeConverter.Option().For(PropertyNames.AnimationDuration),
            Converters.TransitionConverter.Option().For(PropertyNames.AnimationTimingFunction),
            Converters.TimeConverter.Option().For(PropertyNames.AnimationDelay),
            Converters.PositiveOrInfiniteNumberConverter.Option().For(PropertyNames.AnimationIterationCount),
            Converters.AnimationDirectionConverter.Option().For(PropertyNames.AnimationDirection),
            Converters.AnimationFillStyleConverter.Option().For(PropertyNames.AnimationFillMode),
            Converters.PlayStateConverter.Option().For(PropertyNames.AnimationPlayState),
            Converters.IdentifierConverter.Option().For(PropertyNames.AnimationName)).FromList().OrDefault();

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
