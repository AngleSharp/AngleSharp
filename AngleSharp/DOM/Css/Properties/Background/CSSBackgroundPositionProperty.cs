namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    public sealed class CSSBackgroundPositionProperty : CSSProperty
    {
        #region Fields

        List<Point2d> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition)
        {
            _positions = new List<Point2d>();
            _positions.Add(Point2d.Centered);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of all given positions.
        /// </summary>
        internal IEnumerable<Point2d> Positions
        {
            get { return _positions; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var list = values.ToList();
            var positions = new List<Point2d>();

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
