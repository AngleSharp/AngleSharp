namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CssCursorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ImageSourceConverter.Or(
            Converters.WithOrder(
                Converters.ImageSourceConverter.Required(),
                Converters.NumberConverter.Required(),
                Converters.NumberConverter.Required())).RequiresEnd(
            Map.Cursors.ToConverter()).OrDefault(SystemCursor.Auto);

        #endregion

        #region ctor

        internal CssCursorProperty()
            : base(PropertyNames.Cursor, PropertyFlags.Inherited)
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
