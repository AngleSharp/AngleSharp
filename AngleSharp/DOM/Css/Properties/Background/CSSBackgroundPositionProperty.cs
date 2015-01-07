namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    sealed class CSSBackgroundPositionProperty : CssProperty, ICssBackgroundPositionProperty
    {
        #region Fields

        internal static readonly Point Default = Point.Center;
        internal static readonly IValueConverter<Point> SingleConverter = Converters.PointConverter;
        internal static readonly IValueConverter<Point[]> Converter = SingleConverter.FromList();
        readonly List<Point> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundPosition, rule, PropertyFlags.Animatable)
        {
            _positions = new List<Point>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of all given positions.
        /// </summary>
        public IEnumerable<Point> Positions
        {
            get { return _positions; }
        }

        #endregion

        #region Methods

        public void SetPositions(IEnumerable<Point> positions)
        {
            _positions.Clear();
            _positions.AddRange(positions);
        }

        internal override void Reset()
        {
            _positions.Clear();
            _positions.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetPositions);
        }

        #endregion
    }
}
