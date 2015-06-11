namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CssTopProperty : CssProperty
    {
        #region ctor

        internal CssTopProperty()
            : base(PropertyNames.Top, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
