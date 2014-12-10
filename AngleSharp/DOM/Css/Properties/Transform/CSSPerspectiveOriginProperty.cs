namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// </summary>
    sealed class CSSPerspectiveOriginProperty : CSSProperty, ICssPerspectiveOriginProperty
    {
        #region Fields

        internal static readonly Point Default = Point.Centered;
        internal static readonly IValueConverter<Point> Converter = Converters.DistanceConverter.To(m => new Point(m, m)).Or(
                Keywords.Left, new Point(Percent.Zero, Percent.Fifty)).Or(
                Keywords.Center, new Point(Percent.Fifty, Percent.Fifty)).Or(
                Keywords.Right, new Point(Percent.Hundred, Percent.Fifty)).Or(
                Keywords.Top, new Point(Percent.Fifty, Percent.Zero)).Or(
                Keywords.Bottom, new Point(Percent.Fifty, Percent.Hundred)).Or(
                Converters.WithAny(
                    Converters.DistanceConverter.Or(Keywords.Left, Percent.Zero).Or(Keywords.Right, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty),
                    Converters.DistanceConverter.Or(Keywords.Top, Percent.Zero).Or(Keywords.Bottom, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty)).To(
                m => new Point(m.Item1, m.Item2)));

        IDistance _x;
        IDistance _y;

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
            get { return _x; }
        }

        /// <summary>
        /// Gets the position of the ordinate of the vanishing point.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        #endregion

        #region Methods

        public void SetPosition(Point pt)
        {
            _x = pt.X;
            _y = pt.Y;
        }

        internal override void Reset()
        {
            _x = Default.X;
            _y = Default.Y;
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
