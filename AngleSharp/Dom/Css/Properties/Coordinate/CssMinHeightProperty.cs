namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinHeightProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssMinHeightProperty()
            : base(PropertyNames.MinHeight, PropertyFlags.Animatable)
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
