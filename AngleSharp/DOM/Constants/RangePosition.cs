namespace AngleSharp.Dom
{
    /// <summary>
    /// An enumeration of possible values for position comparisons in a range object.
    /// </summary>
    public enum RangePosition : short
    {
        /// <summary>
        /// The position of the given point to the other point is before.
        /// </summary>
        Before = -1,
        /// <summary>
        /// The position of the given point to the other point is equal.
        /// </summary>
        Equal = 0,
        /// <summary>
        /// The position of the given point to the other point is after.
        /// </summary>
        After = 1
    }
}
