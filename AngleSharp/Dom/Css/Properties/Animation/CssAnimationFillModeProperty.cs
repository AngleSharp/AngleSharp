namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// Gets an iteration over all defined fill modes.
    /// </summary>
    sealed class CssAnimationFillModeProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.AnimationFillStyleConverter.FromList().OrDefault(AnimationFillStyle.None);

        #endregion

        #region ctor

        internal CssAnimationFillModeProperty()
            : base(PropertyNames.AnimationFillMode)
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
