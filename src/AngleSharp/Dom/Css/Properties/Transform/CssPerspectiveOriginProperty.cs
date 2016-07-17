namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// Gets the position of the abscissa of the vanishing point.
    /// Gets the position of the ordinate of the vanishing point.
    /// </summary>
    sealed class CssPerspectiveOriginProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter PerspectiveConverter = LengthOrPercentConverter.Or(
            Keywords.Left, new Point(Length.Zero, Length.Half)).Or(
            Keywords.Center, new Point(Length.Half, Length.Half)).Or(
            Keywords.Right, new Point(Length.Full, Length.Half)).Or(
            Keywords.Top, new Point(Length.Half, Length.Zero)).Or(
            Keywords.Bottom, new Point(Length.Half, Length.Full)).Or(
            WithAny(
                LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half))).
            OrDefault(Point.Center);

        #endregion

        #region ctor

        internal CssPerspectiveOriginProperty()
            : base(PropertyNames.PerspectiveOrigin, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return PerspectiveConverter; }
        }

        #endregion
    }
}
