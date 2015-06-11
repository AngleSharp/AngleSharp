namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-right
    /// Gets the padding relative to the width of the containing block or a
    /// fixed width.
    /// </summary>
    sealed class CssPaddingRightProperty : CssProperty
    {
        #region ctor

        internal CssPaddingRightProperty()
            : base(PropertyNames.PaddingRight, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
