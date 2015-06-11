namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// Gets if the element should span across all columns.
    /// </summary>
    sealed class CssColumnSpanProperty : CssProperty
    {
        #region ctor

        internal CssColumnSpanProperty()
            : base(PropertyNames.ColumnSpan)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: false
            get { return Converters.ColumnSpanConverter; }
        }

        #endregion
    }
}
