namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// Gets the selected text direction.
    /// </summary>
    sealed class CssDirectionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.DirectionModeConverter.OrDefault(DirectionMode.Ltr);

        #endregion

        #region ctor

        internal CssDirectionProperty()
            : base(PropertyNames.Direction, PropertyFlags.Inherited)
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
