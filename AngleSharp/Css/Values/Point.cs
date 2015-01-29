namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    public struct Point
    {
        #region Basic values

        /// <summary>
        /// Gets the (50%, 50%) point.
        /// </summary>
        public static readonly Point Center = new Point(Length.Half, Length.Half);

        /// <summary>
        /// Gets the (0, 0) point.
        /// </summary>
        public static readonly Point LeftTop = new Point(Length.Zero, Length.Zero);

        /// <summary>
        /// Gets the (100%, 0) point.
        /// </summary>
        public static readonly Point RightTop = new Point(Length.Full, Length.Zero);

        /// <summary>
        /// Gets the (100%, 100%) point.
        /// </summary>
        public static readonly Point RightBottom = new Point(Length.Full, Length.Full);

        /// <summary>
        /// Gets the (0, 100%) point.
        /// </summary>
        public static readonly Point LeftBottom = new Point(Length.Zero, Length.Full);

        #endregion

        #region Fields

        readonly Length _x;
        readonly Length _y;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Point.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public Point(Length x, Length y)
        {
            _x = x;
            _y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the x-coordinate.
        /// </summary>
        public Length X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value for the y-coordinate.
        /// </summary>
        public Length Y
        {
            get { return _y; }
        }

        #endregion
    }
}
