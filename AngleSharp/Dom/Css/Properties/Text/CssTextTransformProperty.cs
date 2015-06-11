namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// Gets the selected text transformation mode.
    /// </summary>
    sealed class CssTextTransformProperty : CssProperty
    {
        #region ctor

        internal CssTextTransformProperty()
            : base(PropertyNames.TextTransform, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: TextTransform.None
            get { return Converters.TextTransformConverter; }
        }

        #endregion
    }
}
