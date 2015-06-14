namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// Gets the delays for the transitions.
    /// </summary>
    sealed class CssTransitionDelayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssTransitionDelayProperty()
            : base(PropertyNames.TransitionDelay)
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
