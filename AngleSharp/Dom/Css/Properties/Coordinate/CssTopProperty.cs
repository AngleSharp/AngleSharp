namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CssTopProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);

        #endregion

        #region ctor

        internal CssTopProperty()
            : base(PropertyNames.Top, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
