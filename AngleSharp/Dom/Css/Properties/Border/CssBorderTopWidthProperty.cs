namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-width
    /// </summary>
    sealed class CssBorderTopWidthProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopWidthProperty()
            : base(PropertyNames.BorderTopWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Medium
            get { return Converters.LineWidthConverter; }
        }

        #endregion
    }
}
