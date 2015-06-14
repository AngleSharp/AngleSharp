namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// Gets the selected font style.
    /// </summary>
    sealed class CssFontStyleProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontStyleConverter.OrDefault(FontStyle.Normal);

        #endregion

        #region ctor

        internal CssFontStyleProperty()
            : base(PropertyNames.FontStyle, PropertyFlags.Inherited)
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
