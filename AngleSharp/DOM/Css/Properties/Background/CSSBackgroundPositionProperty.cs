namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    sealed class CSSBackgroundPositionProperty : CSSProperty, ICssBackgroundPositionProperty
    {
        #region Fields

        List<Point> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition, PropertyFlags.Animatable)
        {
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

        internal override void Reset()
        {
            if (_positions == null)
                _positions = new List<Point>();
            else
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
            var values = value as CSSValueList ?? new CSSValueList(value);
            var list = values.ToList();
            var positions = new List<Point>();

            foreach (var entry in list)
            {
                if (entry.Length == 0 || entry.Length > 4)
                    return false;

                var position = entry.ToPoint();

                if (position == null)
                    return false;

                positions.Add(position);
            }

            _positions = positions;
            return true;
        }

        #endregion
    }
}
