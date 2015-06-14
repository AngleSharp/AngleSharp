namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// Gets the iteration count of the covered animations.
    /// </summary>
    sealed class CssAnimationIterationCountProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.PositiveOrInfiniteNumberConverter.FromList().OrDefault(1f);

        #endregion

        #region ctor

        internal CssAnimationIterationCountProperty()
            : base(PropertyNames.AnimationIterationCount)
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
