namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// Gets the margin relative to the width of the containing block or a
    /// fixed width, if any.
    /// Gets if the margin is automatically determined.
    /// </summary>
    sealed class CssMarginRightProperty : CssProperty
    {
        #region ctor

        internal CssMarginRightProperty()
            : base(PropertyNames.MarginRight, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Zero
            get { return Converters.AutoLengthOrPercentConverter; }
        }

        #endregion
    }
}
