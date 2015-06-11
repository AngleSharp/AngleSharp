namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// Gets the selected decoration style.
    /// </summary>
    sealed class CssTextDecorationStyleProperty : CssProperty
    {
        #region ctor

        internal CssTextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: TextDecorationStyle.Solid
            get { return Converters.TextDecorationStyleConverter; }
        }

        #endregion
    }
}
