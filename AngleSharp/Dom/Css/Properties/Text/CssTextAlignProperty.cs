namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// Gets the selected horizontal alignment mode.
    /// </summary>
    sealed class CssTextAlignProperty : CssProperty
    {
        #region ctor

        internal CssTextAlignProperty()
            : base(PropertyNames.TextAlign, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: HorizontalAlignment.Left
            get { return Converters.HorizontalAlignmentConverter; }
        }

        #endregion
    }
}
