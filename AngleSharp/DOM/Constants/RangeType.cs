namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration with possible values on how to compare boundary points.
    /// </summary>
    public enum RangeType : ushort
    {
        /// <summary>
        /// From the start to the start (periodic).
        /// </summary>
        [DomName("START_TO_START")]
        StartToStart = 0,
        /// <summary>
        /// From the start to the end (non-periodic).
        /// </summary>
        [DomName("START_TO_END")]
        StartToEnd = 1,
        /// <summary>
        /// From the end to the end (periodic).
        /// </summary>
        [DomName("END_TO_END")]
        EndToEnd = 2,
        /// <summary>
        /// From the end to the start (non-periodic).
        /// </summary>
        [DomName("END_TO_START")]
        EndToStart = 3
    }
}
