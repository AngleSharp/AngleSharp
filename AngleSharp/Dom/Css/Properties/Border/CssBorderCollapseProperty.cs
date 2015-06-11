namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CssBorderCollapseProperty : CssProperty
    {
        #region ctor

        internal CssBorderCollapseProperty()
            : base(PropertyNames.BorderCollapse, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: true
            get { return Converters.BorderCollapseConverter; }
        }

        #endregion
    }
}
