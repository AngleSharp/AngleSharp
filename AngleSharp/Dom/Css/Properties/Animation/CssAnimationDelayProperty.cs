namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// Gets the delays for the animations.
    /// </summary>
    sealed class CssAnimationDelayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);

        #endregion

        #region ctor

        internal CssAnimationDelayProperty()
            : base(PropertyNames.AnimationDelay)
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
