namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-color
    /// </summary>
    sealed class CssBorderBottomColorProperty : CssProperty
    {
        #region ctor

        internal CssBorderBottomColorProperty()
            : base(PropertyNames.BorderBottomColor)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Transparent
            get { return Converters.CurrentColorConverter; }
        }

        #endregion
    }
}
