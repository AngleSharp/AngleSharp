namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// Gets the selected text transformation mode.
    /// </summary>
    sealed class CssTextTransformProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.TextTransformConverter.OrDefault(TextTransform.None);

        #endregion

        #region ctor

        internal CssTextTransformProperty()
            : base(PropertyNames.TextTransform, PropertyFlags.Inherited)
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
