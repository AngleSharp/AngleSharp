namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// Gets if the columns should be filled uniformly.
    /// </summary>
    sealed class CssColumnFillProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ColumnFillConverter.OrDefault(true);

        #endregion

        #region ctor

        internal CssColumnFillProperty()
            : base(PropertyNames.ColumnFill)
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
