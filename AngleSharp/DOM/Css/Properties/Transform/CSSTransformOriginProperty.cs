namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// </summary>
    sealed class CSSTransformOriginProperty : CSSProperty, ICssTransformOriginProperty
    {
        #region Fields

        internal static readonly Point Default = Point.Centered;
        internal static IValueConverter<Tuple<Point, Length>> Converter = Converters.WithOrder(
            Converters.DistanceConverter.To(m => new Point(m, m)).Or(Keywords.Center, new Point(Percent.Fifty, Percent.Fifty)).Or(Converters.WithAny(
                Converters.DistanceConverter.Or(Keywords.Left, Percent.Zero).Or(Keywords.Right, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty),
                Converters.DistanceConverter.Or(Keywords.Top, Percent.Zero).Or(Keywords.Bottom, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty)).To(
            m => new Point(m.Item1, m.Item2))).Or(Converters.WithAny(
                Converters.DistanceConverter.Or(Keywords.Top, Percent.Zero).Or(Keywords.Bottom, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty),
                Converters.DistanceConverter.Or(Keywords.Left, Percent.Zero).Or(Keywords.Right, Percent.Hundred).Or(Keywords.Center, Percent.Fifty).Option(Percent.Fifty)).To(
            m => new Point(m.Item1, m.Item2))).Required(),
            Converters.LengthConverter.Option(Length.Zero));

        IDistance _x;
        IDistance _y;
        Length _z;

        #endregion

        #region ctor

        internal CSSTransformOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TransformOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets how far from the user eye the z = 0 origin is set.
        /// </summary>
        public Length Z
        {
            get { return _z; }
        }

        #endregion

        #region Methods

        public void SetPosition(Point pt, Length z)
        {
            _x = pt.X;
            _y = pt.Y;
            _z = z;
        }

        internal override void Reset()
        {
            _x = Default.X;
            _y = Default.Y;
            _z = Length.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetPosition(m.Item1, m.Item2));
        }

        #endregion
    }
}
