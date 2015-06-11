namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-width
    /// </summary>
    sealed class CssBorderLeftWidthProperty : CssProperty
    {
        #region ctor

        internal CssBorderLeftWidthProperty()
            : base(PropertyNames.BorderLeftWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
