namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// Gets the selected font style.
    /// </summary>
    sealed class CssFontStyleProperty : CssProperty
    {
        #region ctor

        internal CssFontStyleProperty()
            : base(PropertyNames.FontStyle, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: FontStyle.Normal
            get { return Converters.FontStyleConverter; }
        }

        #endregion
    }
}
