namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Specifies a stepping function, described above, taking two parameters.
    /// </summary>
    public sealed class StepsTimingFunction : ITimingFunction
    {
        #region Fields

        readonly Int32 _intervals;
        readonly Boolean _start;

        #endregion

        #region ctor

        /// <summary>
        /// The first parameter specifies the number of intervals in the function. 
        /// The second parameter specifies the point at which the change of values
        /// occur within the interval. 
        /// </summary>
        /// <param name="intervals">It must be a positive integer (greater than 0).</param>
        /// <param name="start">Optional: If not specified then the change occurs at the end.</param>
        public StepsTimingFunction(Int32 intervals, Boolean start = false)
        {
            _intervals = Math.Max(1, intervals);
            _start = start;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the numbers of intervals.
        /// </summary>
        public Int32 Intervals
        {
            get { return _intervals; }
        }

        /// <summary>
        /// Gets if the steps should occur in the beginning.
        /// </summary>
        public Boolean IsStart
        {
            get { return _start; }
        }

        #endregion
    }
}
