namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// Gets the enumeration over all selected styles for text decoration
    /// lines.
    /// </summary>
    sealed class CssTextDecorationLineProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.TextDecorationLinesConverter.OrDefault();

        #endregion

        #region ctor

        internal CssTextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
