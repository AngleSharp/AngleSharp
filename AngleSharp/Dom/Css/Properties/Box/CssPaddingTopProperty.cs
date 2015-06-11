namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-top
    /// Gets the padding relative to the height of the containing block or a
    /// fixed height.
    /// </summary>
    sealed class CssPaddingTopProperty : CssProperty
    {
        #region ctor

        internal CssPaddingTopProperty()
            : base(PropertyNames.PaddingTop, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Zero
            get { return Converters.LengthOrPercentConverter; }
        }

        #endregion
    }
}
