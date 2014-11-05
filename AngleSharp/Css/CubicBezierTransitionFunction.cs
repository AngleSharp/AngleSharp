namespace AngleSharp.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Specifies a cubic-bezier curve. 
    /// </summary>
    public sealed class CubicBezierTransitionFunction : TransitionFunction
    {
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
        public CubicBezierTransitionFunction(Single x1, Single y1, Single x2, Single y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x-coordinate of the p1.
        /// </summary>
        public Single X1
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the y-coordinate of the p1.
        /// </summary>
        public Single Y1
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the x-coordinate of the p2.
        /// </summary>
        public Single X2
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the y-coordinate of the p2.
        /// </summary>
        public Single Y2
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the CSS representation of the cubic bezier timing function.
        /// </summary>
        /// <returns>A string that resembles CSS code.</returns>
        public override String ToCss()
        {
            return FunctionNames.Build(FunctionNames.CubicBezier, 
                X1.ToString(CultureInfo.InvariantCulture), Y1.ToString(CultureInfo.InvariantCulture), 
                X2.ToString(CultureInfo.InvariantCulture), Y2.ToString(CultureInfo.InvariantCulture));
        }

        #endregion
    }
}
