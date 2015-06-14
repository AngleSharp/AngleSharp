namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
    /// Gets the selected color for the foreground.
    /// </summary>
    sealed class CssColorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Black);

        #endregion

        #region ctor

        internal CssColorProperty()
            : base(PropertyNames.Color, PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable)
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
