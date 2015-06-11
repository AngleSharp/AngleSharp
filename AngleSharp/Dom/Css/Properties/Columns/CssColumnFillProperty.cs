namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// Gets if the columns should be filled uniformly.
    /// </summary>
    sealed class CssColumnFillProperty : CssProperty
    {
        #region ctor

        internal CssColumnFillProperty()
            : base(PropertyNames.ColumnFill)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: true
            get { return Converters.ColumnFillConverter; }
        }

        #endregion
    }
}
