namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// Gets the value of the display mode.
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region ctor

        internal CssDisplayProperty()
            : base(PropertyNames.Display)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: DisplayMode.Inline
            get { return Converters.DisplayModeConverter; }
        }

        #endregion
    }
}
