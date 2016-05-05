namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// Gets the selected decoration style.
    /// </summary>
    sealed class CssTextDecorationStyleProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.TextDecorationStyleConverter.OrDefault(TextDecorationStyle.Solid);

        #endregion

        #region ctor

        internal CssTextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
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
