namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakAfterProperty : CssProperty
    {
        #region ctor

        internal CssBreakAfterProperty()
            : base(PropertyNames.BreakAfter)
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
