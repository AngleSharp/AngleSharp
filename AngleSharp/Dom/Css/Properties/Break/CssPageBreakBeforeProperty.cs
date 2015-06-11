namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakBeforeProperty : CssProperty
    {
        #region ctor

        internal CssPageBreakBeforeProperty()
            : base(PropertyNames.PageBreakBefore)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: BreakMode.Auto
            get { return Converters.PageBreakModeConverter; }
        }

        #endregion
    }
}
