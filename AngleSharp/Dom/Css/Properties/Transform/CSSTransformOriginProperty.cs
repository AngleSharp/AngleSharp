namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// </summary>
    sealed class CssTransformOriginProperty : CssProperty, ICssTransformOriginProperty
    {
        #region Fields

        internal static readonly Point Default = Point.Center;
        internal static IValueConverter<Tuple<Point, Length>> Converter = Converters.WithOrder(
            Converters.LengthOrPercentConverter.To(m => new Point(m, m)).Or(Keywords.Center, Point.Center).Or(Converters.WithAny(
                Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half)).To(
            m => new Point(m.Item1, m.Item2))).Or(Converters.WithAny(
                Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half),
                Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half).Option(Length.Half)).To(
            m => new Point(m.Item1, m.Item2))).Required(),
            Converters.LengthConverter.Option(Length.Zero));

        Length _x;
        Length _y;
        Length _z;

        #endregion

        #region ctor

        internal CssTransformOriginProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TransformOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        public Length X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        public Length Y
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

        public void SetPosition(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
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
            return Converter.TryConvert(value, m => SetPosition(m.Item1.X, m.Item1.Y, m.Item2));
        }

        #endregion
    }
}
