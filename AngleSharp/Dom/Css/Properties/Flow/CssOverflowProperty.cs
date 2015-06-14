namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CssOverflowProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.OverflowModeConverter.OrDefault(OverflowMode.Visible);

        #endregion

        #region ctor

        internal CssOverflowProperty()
            : base(PropertyNames.Overflow)
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
