namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// Gets if the caption box will be above the table.
    /// Otherwise the caption box will be below the table.
    /// </summary>
    sealed class CssCaptionSideProperty : CssProperty
    {
        #region ctor

        internal CssCaptionSideProperty()
            : base(PropertyNames.CaptionSide)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: true
            get { return Converters.CaptionSideConverter; }
        }

        #endregion
    }
}
