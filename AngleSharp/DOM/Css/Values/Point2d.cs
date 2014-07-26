namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a point value consisting of two lengths.
    /// </summary>
    public sealed class Point2d 
    {
        #region Fields

        readonly CSSCalcValue _x;
        readonly CSSCalcValue _y;

        /// <summary>
        /// Gets the center, center point.
        /// </summary>
        public static readonly Point2d Centered = new Point2d();

        #endregion

        #region ctor

        internal Point2d(CSSCalcValue x = null, CSSCalcValue y = null)
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
    }
}
