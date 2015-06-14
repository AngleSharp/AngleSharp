namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-position
    /// </summary>
    sealed class CssObjectPositionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.PointConverter.OrDefault(Point.Center);

        #endregion

        #region ctor

        internal CssObjectPositionProperty()
            : base(PropertyNames.ObjectPosition, PropertyFlags.Animatable)
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
