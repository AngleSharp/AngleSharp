namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssAnimationTimingFunctionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TransitionConverter.FromList().OrDefault(Map.TimingFunctions[Keywords.Ease]);

        #endregion

        #region ctor

        internal CssAnimationTimingFunctionProperty()
            : base(PropertyNames.AnimationTimingFunction)
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
