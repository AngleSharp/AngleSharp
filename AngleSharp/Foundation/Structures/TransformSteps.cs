namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Specifies a stepping function, described above, taking two parameters.
    /// </summary>
    public sealed class TransformSteps : TransformFunction
    {
        /// <summary>
        /// The first parameter specifies the number of intervals in the function. 
        /// The second parameter specifies the point at which the change of values
        /// occur within the interval. 
        /// </summary>
        /// <param name="intervals">It must be a positive integer (greater than 0).</param>
        /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
        public TransformSteps(Int32 intervals, Boolean start = false)
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
            return FunctionNames.Build(FunctionNames.Steps, Intervals.ToString(CultureInfo.InvariantCulture), IsStart ? Keywords.Start : Keywords.End);
        }
    }
}
