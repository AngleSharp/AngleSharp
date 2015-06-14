namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// Gets the selected horizontal alignment mode.
    /// </summary>
    sealed class CssTextAlignProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.HorizontalAlignmentConverter.OrDefault(HorizontalAlignment.Left);

        #endregion

        #region ctor

        internal CssTextAlignProperty()
            : base(PropertyNames.TextAlign, PropertyFlags.Inherited)
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
