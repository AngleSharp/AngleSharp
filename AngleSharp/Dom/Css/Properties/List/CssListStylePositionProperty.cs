namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// Gets the selected position.
    /// </summary>
    sealed class CssListStylePositionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ListPositionConverter.OrDefault(ListPosition.Outside);

        #endregion

        #region ctor

        internal CssListStylePositionProperty()
            : base(PropertyNames.ListStylePosition, PropertyFlags.Inherited)
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
