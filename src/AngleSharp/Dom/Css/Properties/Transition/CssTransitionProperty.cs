namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition
    /// </summary>
    sealed class CssTransitionProperty : CssShorthandProperty
    {
        #region Fields

        internal static readonly IValueConverter ListConverter = WithAny(
            AnimatableConverter.Option().For(PropertyNames.TransitionProperty),
            TimeConverter.Option().For(PropertyNames.TransitionDuration),
            TransitionConverter.Option().For(PropertyNames.TransitionTimingFunction),
            TimeConverter.Option().For(PropertyNames.TransitionDelay)).FromList().OrDefault();

        #endregion

        #region ctor

        internal CssTransitionProperty()
            : base(PropertyNames.Transition)
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
