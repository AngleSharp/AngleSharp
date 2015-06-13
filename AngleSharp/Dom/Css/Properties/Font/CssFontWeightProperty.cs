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

        internal static readonly IValueConverter StyleConverter = Converters.FontWeightConverter.Or(Converters.WeightIntegerConverter);

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
            // Default: new FontWeight { IsRelative = false, Value = 400 }
            get { return StyleConverter; }
        }

        #endregion
    }
}
