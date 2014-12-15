namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// </summary>
    sealed class CSSPerspectiveOriginProperty : CSSProperty, ICssPerspectiveOriginProperty
    {
        #region Fields

        internal static readonly Point Default = Point.Center;
        internal static readonly IValueConverter<Point> Converter = Converters.LengthOrPercentConverter.To(m => new Point(m, m)).Or(
                Keywords.Left, new Point(Length.Zero, Length.Half)).Or(
                Keywords.Center, new Point(Length.Half, Length.Half)).Or(
                Keywords.Right, new Point(Length.Full, Length.Half)).Or(
                Keywords.Top, new Point(Length.Half, Length.Zero)).Or(
                Keywords.Bottom, new Point(Length.Half, Length.Full)).Or(
                Converters.WithAny(
                    Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                    Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half)).To(
                m => new Point(m.Item1, m.Item2)));

        Point _pt;

        #endregion

        #region ctor

        internal CSSPerspectiveOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.PerspectiveOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the abscissa of the vanishing point.
        /// </summary>
        public IDistance X
        {
            get { return _pt.X; }
        }

        /// <summary>
        /// Gets the position of the ordinate of the vanishing point.
        /// </summary>
        public IDistance Y
        {
            get { return _pt.Y; }
        }

        #endregion

        #region Methods

        public void SetPosition(Point pt)
        {
            _pt = pt;
        }

        internal override void Reset()
        {
            _pt = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetPosition);
        }

        #endregion
    }
}
