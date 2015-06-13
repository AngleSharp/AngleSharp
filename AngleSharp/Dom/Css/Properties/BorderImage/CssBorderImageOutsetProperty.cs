namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CssBorderImageOutsetProperty : CssProperty
    {
        #region Fields

        // Default: Tuple.Create(Length.Zero, Length.Zero, Length.Zero, Length.Zero)
        internal static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.Periodic();

        #endregion

        #region ctor

        internal CssBorderImageOutsetProperty()
            : base(PropertyNames.BorderImageOutset)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
