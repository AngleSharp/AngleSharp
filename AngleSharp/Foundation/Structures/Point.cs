namespace AngleSharp
{
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    sealed class Point : ICssObject
    {
        #region Fields

        readonly CSSCalcValue _x;
        readonly CSSCalcValue _y;

        /// <summary>
        /// Gets the center, center point.
        /// </summary>
        public static readonly Point Centered = new Point();

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Point.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public Point(CSSCalcValue x = null, CSSCalcValue y = null)
        {
            _x = x ?? CSSCalcValue.Center;
            _y = y ?? CSSCalcValue.Center;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the x-coordinate.
        /// </summary>
        public CSSCalcValue X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value for the y-coordinate.
        /// </summary>
        public CSSCalcValue Y
        {
            get { return _y; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS representation of the Point.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return String.Format("{0} {1}", _x.ToCss(), _y.ToCss());
        }

        #endregion
    }
}
