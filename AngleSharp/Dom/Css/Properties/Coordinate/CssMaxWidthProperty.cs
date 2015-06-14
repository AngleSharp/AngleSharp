namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// Gets the specified max-width of the element. A percentage is
    /// calculated with respect to the width of the containing block.
    /// </summary>
    sealed class CssMaxWidthProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.OptionalLengthOrPercentConverter.OrDefault();

        #endregion

        #region ctor

        internal CssMaxWidthProperty()
            : base(PropertyNames.MaxWidth, PropertyFlags.Animatable)
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
