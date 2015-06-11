namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// Gets the aspect value specified by the property, if any.
    /// </summary>
    sealed class CssFontSizeAdjustProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeAdjustProperty()
            : base(PropertyNames.FontSizeAdjust, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return Converters.OptionalNumberConverter; }
        }

        #endregion
    }
}
