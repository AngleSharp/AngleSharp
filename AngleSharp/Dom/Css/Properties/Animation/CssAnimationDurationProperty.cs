namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-duration
    /// Gets the durations for the animations.
    /// </summary>
    sealed class CssAnimationDurationProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssAnimationDurationProperty()
            : base(PropertyNames.AnimationDuration)
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
