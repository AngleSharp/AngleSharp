namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-position
    /// </summary>
    sealed class CssObjectPositionProperty : CssProperty
    {
        #region ctor

        internal CssObjectPositionProperty()
            : base(PropertyNames.ObjectPosition, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Point.Center
            get { return Converters.PointConverter; }
        }

        #endregion
    }
}
