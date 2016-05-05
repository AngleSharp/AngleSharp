namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// Gets the list of all given positions.
    /// </summary>
    sealed class CssBackgroundPositionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter ListConverter = Converters.PointConverter.FromList().OrDefault(Point.Center);

        #endregion

        #region ctor

        internal CssBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
