namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakAfterProperty : CssProperty
    {
        #region ctor

        internal CssPageBreakAfterProperty()
            : base(PropertyNames.PageBreakAfter)
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
