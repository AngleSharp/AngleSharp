namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CssBorderCollapseProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.BorderCollapseConverter.OrDefault(true);

        #endregion

        #region ctor

        internal CssBorderCollapseProperty()
            : base(PropertyNames.BorderCollapse, PropertyFlags.Inherited)
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
