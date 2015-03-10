namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// Gets the position of the abscissa of the vanishing point.
    /// Gets the position of the ordinate of the vanishing point.
    /// </summary>
    sealed class CssPerspectiveOriginProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Point> Converter = 
            Converters.LengthOrPercentConverter.To(m => new Point(m, m)).Or(
                Keywords.Left, new Point(Length.Zero, Length.Half)).Or(
                Keywords.Center, new Point(Length.Half, Length.Half)).Or(
                Keywords.Right, new Point(Length.Full, Length.Half)).Or(
                Keywords.Top, new Point(Length.Half, Length.Zero)).Or(
                Keywords.Bottom, new Point(Length.Half, Length.Full)).Or(
                Converters.WithAny(
                    Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(
                        Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                    Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(
                        Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half)).To(
                m => new Point(m.Item1, m.Item2)));

        #endregion

        #region ctor

        internal CssPerspectiveOriginProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PerspectiveOrigin, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Point.Center;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
