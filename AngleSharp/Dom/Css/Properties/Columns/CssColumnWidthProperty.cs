namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// Gets the width of a single columns.
    /// </summary>
    sealed class CssColumnWidthProperty : CssProperty
    {
        #region ctor

        internal CssColumnWidthProperty()
            : base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: auto
            get { return Converters.AutoLengthConverter; }
        }

        #endregion
    }
}
