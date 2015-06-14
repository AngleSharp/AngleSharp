namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// Gets how far from the left edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the top edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the user eye the z = 0 origin is set.
    /// </summary>
    sealed class CssTransformOriginProperty : CssProperty
    {
        #region Fields

        static IValueConverter StyleConverter = Converters.WithOrder(
            Converters.LengthOrPercentConverter.Or(Keywords.Center, Point.Center).Or(Converters.WithAny(
                Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half))).Or(
            Converters.WithAny(
                Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half))).Required(),
            Converters.LengthConverter.Option(Length.Zero)).OrDefault(Point.Center);

        #endregion

        #region ctor

        internal CssTransformOriginProperty()
            : base(PropertyNames.TransformOrigin, PropertyFlags.Animatable)
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
