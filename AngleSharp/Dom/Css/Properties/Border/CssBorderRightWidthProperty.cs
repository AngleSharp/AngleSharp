namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-width
    /// </summary>
    sealed class CssBorderRightWidthProperty : CssProperty
    {
        #region ctor

        internal CssBorderRightWidthProperty()
            : base(PropertyNames.BorderRightWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
