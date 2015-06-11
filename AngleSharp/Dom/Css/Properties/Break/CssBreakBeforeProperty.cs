namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakBeforeProperty : CssProperty
    {
        #region ctor

        internal CssBreakBeforeProperty()
            : base(PropertyNames.BreakBefore)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: BreakMode.Auto
            get { return Converters.BreakModeConverter; }
        }

        #endregion
    }
}
