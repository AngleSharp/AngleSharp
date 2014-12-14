namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Specifies a cubic-bezier curve. 
    /// </summary>
    public sealed class CubicBezierTimingFunction : ITimingFunction
    {
        #region Fields

        readonly Single _x1;
        readonly Single _y1;
        readonly Single _x2;
        readonly Single _y2;

        #endregion

        #region ctor

        /// <summary>
        /// The four values specify points P1 and P2 of the curve as (x1, y1, x2, y2). Both
        /// x values must be in the range [0, 1] or the definition is invalid. The y values
        /// can exceed this range.
        /// </summary>
        /// <param name="x1">The x-coordinate of P1.</param>
        /// <param name="y1">The y-coordinate of P1.</param>
        /// <param name="x2">The x-coordinate of P2.</param>
        /// <param name="y2">The y-coordinate of P2.</param>
        public CubicBezierTimingFunction(Single x1, Single y1, Single x2, Single y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x-coordinate of the p1.
        /// </summary>
        public Single X1
        {
            get { return _x1; }
        }

        /// <summary>
        /// Gets the y-coordinate of the p1.
        /// </summary>
        public Single Y1
        {
            get { return _y1; }
        }

        /// <summary>
        /// Gets the x-coordinate of the p2.
        /// </summary>
        public Single X2
        {
            get { return _x2; }
        }

        /// <summary>
        /// Gets the y-coordinate of the p2.
        /// </summary>
        public Single Y2
        {
            get { return _y2; }
        }

        #endregion
    }
}
