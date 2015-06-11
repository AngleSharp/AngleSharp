namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// Gets the visibility mode.
    /// </summary>
    sealed class CssVisibilityProperty : CssProperty
    {
        #region ctor

        internal CssVisibilityProperty()
            : base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Visibility.Visible
            get { return Converters.VisibilityConverter; }
        }

        #endregion
    }
}
