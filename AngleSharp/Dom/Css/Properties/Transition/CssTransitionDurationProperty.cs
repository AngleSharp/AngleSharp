namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-duration
    /// Gets the durations for the transitions.
    /// </summary>
    sealed class CssTransitionDurationProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssTransitionDurationProperty()
            : base(PropertyNames.TransitionDuration)
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
