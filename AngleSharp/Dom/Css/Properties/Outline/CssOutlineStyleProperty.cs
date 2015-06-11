namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// Gets the selected outline style.
    /// </summary>
    sealed class CssOutlineStyleProperty : CssProperty
    {
        #region ctor

        internal CssOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
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
