namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation
    /// </summary>
    sealed class CssAnimationProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = WithAny(
            TimeConverter.Option().For(PropertyNames.AnimationDuration),
            TransitionConverter.Option().For(PropertyNames.AnimationTimingFunction),
            TimeConverter.Option().For(PropertyNames.AnimationDelay),
            PositiveOrInfiniteNumberConverter.Option().For(PropertyNames.AnimationIterationCount),
            AnimationDirectionConverter.Option().For(PropertyNames.AnimationDirection),
            AnimationFillStyleConverter.Option().For(PropertyNames.AnimationFillMode),
            PlayStateConverter.Option().For(PropertyNames.AnimationPlayState),
            IdentifierConverter.Option().For(PropertyNames.AnimationName)).FromList().OrDefault();

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
