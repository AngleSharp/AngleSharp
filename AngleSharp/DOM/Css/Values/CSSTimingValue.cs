namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a timing-function in CSS.
    /// http://dev.w3.org/csswg/css-transitions/#transition-timing-function
    /// </summary>
    abstract class CSSTimingValue : CSSValue
    {
        #region Fields

        /// <summary>
        /// Gets the pre-defined ease function.
        /// </summary>
        public static readonly CubicBezier Ease = new CubicBezier(0.25f, 0.1f, 0.25f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in function.
        /// </summary>
        public static readonly CubicBezier EaseIn = new CubicBezier(0.42f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in-out function.
        /// </summary>
        public static readonly CubicBezier EaseInOut = new CubicBezier(0.42f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-out function.
        /// </summary>
        public static readonly CubicBezier EaseOut = new CubicBezier(0f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined linear function.
        /// </summary>
        public static readonly CubicBezier Linear = new CubicBezier(0f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined step-start function.
        /// </summary>
        public static readonly Steps StepStart = new Steps(1, true);
        /// <summary>
        /// Gets the pre-defined step-end function.
        /// </summary>
        public static readonly Steps StepEnd = new Steps(1, false);

        #endregion

        #region ctor

        private CSSTimingValue()
            : base(CssValueType.Primitive)
	    {
	    }

        #endregion

        #region Classes

        /// <summary>
        /// Specifies a stepping function, described above, taking two parameters.
        /// </summary>
        public sealed class Steps : CSSTimingValue
        {
            /// <summary>
            /// The first parameter specifies the number of intervals in the function. 
            /// The second parameter specifies the point at which the change of values
            /// occur within the interval. 
            /// </summary>
            /// <param name="intervals">It must be a positive integer (greater than 0).</param>
            /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
            public Steps(Int32 intervals, Boolean start = false)
            {
                Intervals = Math.Max(1, intervals);
                IsStart = start;
            }

            /// <summary>
            /// Gets the numbers of intervals.
            /// </summary>
            public Int32 Intervals
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets if the steps should occur in the beginning.
            /// </summary>
            public Boolean IsStart
            {
                get;
                private set;
            }

            /// <summary>
            /// Returns the CSS representation of the steps timing function.
            /// </summary>
            /// <returns>A string that resembles CSS code.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Steps, Intervals.ToString(CultureInfo.InvariantCulture), IsStart ? "start" : "end");
            }
        }

        /// <summary>
        /// Specifies a cubic-bezier curve. 
        /// </summary>
        public sealed class CubicBezier : CSSTimingValue
        {
            /// <summary>
            /// The four values specify points P1 and P2 of the curve as (x1, y1, x2, y2). Both
            /// x values must be in the range [0, 1] or the definition is invalid. The y values
            /// can exceed this range.
            /// </summary>
            /// <param name="x1">The x-coordinate of P1.</param>
            /// <param name="y1">The y-coordinate of P1.</param>
            /// <param name="x2">The x-coordinate of P2.</param>
            /// <param name="y2">The y-coordinate of P2.</param>
            public CubicBezier(Single x1, Single y1, Single x2, Single y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }

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

            /// <summary>
            /// Returns the CSS representation of the cubic bezier timing function.
            /// </summary>
            /// <returns>A string that resembles CSS code.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.CubicBezier, X1.ToString(CultureInfo.InvariantCulture), Y1.ToString(CultureInfo.InvariantCulture), X2.ToString(CultureInfo.InvariantCulture), Y2.ToString(CultureInfo.InvariantCulture));
            }
        }

        #endregion
    }
}
