namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    sealed class CssBorderRightStyleProperty : CssProperty
    {
        #region ctor

        internal CssBorderRightStyleProperty()
            : base(PropertyNames.BorderRightStyle)
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
