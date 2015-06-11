namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CssOverflowProperty : CssProperty
    {
        #region ctor

        internal CssOverflowProperty()
            : base(PropertyNames.Overflow)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: OverflowMode.Visible
            get { return Converters.OverflowModeConverter; }
        }

        #endregion
    }
}
