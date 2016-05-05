namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    sealed class CssFontWeightProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontWeightConverter.Or(
            Converters.WeightIntegerConverter).OrDefault(FontWeight.Normal);

        #endregion

        #region ctor
        
        internal CssFontWeightProperty()
            : base(PropertyNames.FontWeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
