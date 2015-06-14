namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/bottom
    /// </summary>
    sealed class CssBottomProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);

        #endregion

        #region ctor

        internal CssBottomProperty()
            : base(PropertyNames.Bottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
