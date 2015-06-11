namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/left
    /// </summary>
    sealed class CssLeftProperty : CssProperty
    {
        #region ctor

        internal CssLeftProperty()
            : base(PropertyNames.Left, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
