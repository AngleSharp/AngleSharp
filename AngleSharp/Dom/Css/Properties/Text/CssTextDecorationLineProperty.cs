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

        static readonly TextDecorationLine[] Default = new TextDecorationLine[0];
        static readonly IValueConverter<TextDecorationLine[]> SingleConverter = 
            Map.TextDecorationLines.ToConverter().Many();
        internal static readonly IValueConverter<TextDecorationLine[]> ListConverter = 
            SingleConverter.Or(Keywords.None, Default);

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
            // Default: Nothing
            get { return ListConverter; }
        }

        #endregion
    }
}
