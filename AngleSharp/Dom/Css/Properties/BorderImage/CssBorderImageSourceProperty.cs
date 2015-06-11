namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-source
    /// </summary>
    sealed class CssBorderImageSourceProperty : CssProperty
    {
        #region ctor

        internal CssBorderImageSourceProperty()
            : base(PropertyNames.BorderImageSource)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing
            get { return Converters.OptionalImageSourceConverter; }
        }

        #endregion
    }
}
