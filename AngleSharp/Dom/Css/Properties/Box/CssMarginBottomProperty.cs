namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-bottom
    /// Gets the margin relative to the height of the containing block or a
    /// fixed height, if any.
    /// Gets if the margin is automatically determined.
    /// </summary>
    sealed class CssMarginBottomProperty : CssProperty
    {
        #region ctor

        internal CssMarginBottomProperty()
            : base(PropertyNames.MarginBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
