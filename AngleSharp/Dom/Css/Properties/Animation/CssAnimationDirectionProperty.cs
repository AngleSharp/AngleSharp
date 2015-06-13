namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// Gets an iteration over all defined directions.
    /// </summary>
    sealed class CssAnimationDirectionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.AnimationDirectionConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: AnimationDirection.Normal
            get { return ListConverter; }
        }

        #endregion
    }
}
