namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-style
    /// </summary>
    sealed class CssBorderBottomStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderBottomStyleProperty()
            : base(PropertyNames.BorderBottomStyle)
        { 
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: LineStyle.None
            get { return Converters.LineStyleConverter; }
        }

        #endregion
    }
}
