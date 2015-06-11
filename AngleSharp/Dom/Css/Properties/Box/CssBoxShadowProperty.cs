namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssBoxShadowProperty : CssProperty
    {
        #region ctor

        internal CssBoxShadowProperty()
            : base(PropertyNames.BoxShadow, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return Converters.MultipleShadowConverter; }
        }

        #endregion
    }
}
