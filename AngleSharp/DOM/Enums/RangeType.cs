using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// An enumeration with possible values on how to compare boundary points.
    /// </summary>
    public enum RangeType : ushort
    {
        /// <summary>
        /// From the start to the start (periodic).
        /// </summary>
        StartToStart = 0,
        /// <summary>
        /// From the start to the end (non-periodic).
        /// </summary>
        StartToEnd = 1,
        /// <summary>
        /// From the end to the end (periodic).
        /// </summary>
        EndToEnd = 2,
        /// <summary>
        /// From the end to the start (non-periodic).
        /// </summary>
        EndToStart = 3
    }
}
