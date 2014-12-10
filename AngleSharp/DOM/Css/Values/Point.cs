namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a point value consisting of two distances.
    /// </summary>
    public sealed class Point : ICssValue
    {
        #region Fields

        readonly IDistance _x;
        readonly IDistance _y;

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
        public Point(IDistance x = null, IDistance y = null)
        {
            _x = x ?? Percent.Fifty;
            _y = y ?? Percent.Fifty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the x-coordinate.
        /// </summary>
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the value for the y-coordinate.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.List; }
        }

        String ICssValue.CssText
        {
            get { return String.Concat(_x.CssText, " ", _y.CssText); }
        }

        #endregion
    }
}
