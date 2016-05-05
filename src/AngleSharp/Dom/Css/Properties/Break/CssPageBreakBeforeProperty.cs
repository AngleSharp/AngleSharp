namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakBeforeProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.PageBreakModeConverter.OrDefault(BreakMode.Auto);

        #endregion

        #region ctor

        internal CssPageBreakBeforeProperty()
            : base(PropertyNames.PageBreakBefore)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
