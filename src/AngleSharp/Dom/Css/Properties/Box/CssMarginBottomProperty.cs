namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-bottom
    /// Gets the margin relative to the height of the containing block or a
    /// fixed height, if any.
    /// Gets if the margin is automatically determined.
    /// </summary>
    sealed class CssMarginBottomProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssMarginBottomProperty()
            : base(PropertyNames.MarginBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
