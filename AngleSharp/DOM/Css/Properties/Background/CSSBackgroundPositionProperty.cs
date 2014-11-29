namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    sealed class CSSBackgroundPositionProperty : CSSProperty, ICssBackgroundPositionProperty
    {
        #region Fields

        readonly List<Point> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty(CSSStyleDeclaration rule)
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
            _positions.Add(Point.Centered);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return TakeList(WithPoint()).TryConvert(value, SetPositions);
        }

        #endregion
    }
}
