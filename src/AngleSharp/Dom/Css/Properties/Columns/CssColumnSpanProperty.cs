namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// Gets if the element should span across all columns.
    /// </summary>
    sealed class CssColumnSpanProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ColumnSpanConverter.OrDefault(false);

        #endregion

        #region ctor

        internal CssColumnSpanProperty()
            : base(PropertyNames.ColumnSpan)
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
