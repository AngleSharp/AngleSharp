namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CssHeightProperty : CssProperty
    {
        #region ctor

        internal CssHeightProperty()
            : base(PropertyNames.Height, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: auto
            get { return Converters.AutoLengthOrPercentConverter; }
        }

        #endregion
    }
}
