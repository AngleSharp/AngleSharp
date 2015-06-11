namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clear
    /// Gets the value of the clear mode.
    /// </summary>
    sealed class CssClearProperty : CssProperty
    {
        #region ctor

        internal CssClearProperty()
            : base(PropertyNames.Clear)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: ClearMode.None
            get { return Converters.ClearModeConverter; }
        }

        #endregion
    }
}
