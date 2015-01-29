namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a media time range.
    /// </summary>
    [DomName("TimeRanges")]
    public interface ITimeRanges
    {
        /// <summary>
        /// Gets the length of the range in frames.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Returns the time offset at which a specified time range begins.
        /// </summary>
        /// <param name="index">The range number to return the starting time for.</param>
        /// <returns>The time offset.</returns>
        [DomName("start")]
        Double Start(Int32 index);

        /// <summary>
        /// Returns the time offset at which a specified time range ends.
        /// </summary>
        /// <param name="index">The range number to return the ending time for.</param>
        /// <returns>The time offset.</returns>
        [DomName("end")]
        Double End(Int32 index);
    }
}
