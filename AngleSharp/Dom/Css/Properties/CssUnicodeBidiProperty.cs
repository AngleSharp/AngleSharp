namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CssUnicodeBidiProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.UnicodeModeConverter.OrDefault(UnicodeMode.Normal);

        #endregion

        #region ctor

        internal CssUnicodeBidiProperty()
            : base(PropertyNames.UnicodeBidi)
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
