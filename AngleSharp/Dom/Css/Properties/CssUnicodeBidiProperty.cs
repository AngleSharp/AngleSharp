namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CssUnicodeBidiProperty : CssProperty
    {
        #region ctor

        internal CssUnicodeBidiProperty()
            : base(PropertyNames.UnicodeBidi)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: UnicodeMode.Normal
            get { return Converters.UnicodeModeConverter; }
        }

        #endregion
    }
}
