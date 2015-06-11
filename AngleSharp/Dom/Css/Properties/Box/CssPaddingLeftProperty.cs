namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// Gets the padding relative to the width of the containing block or a
    /// fixed width.
    /// </summary>
    sealed class CssPaddingLeftProperty : CssProperty
    {
        #region ctor

        internal CssPaddingLeftProperty()
            : base(PropertyNames.PaddingLeft, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
