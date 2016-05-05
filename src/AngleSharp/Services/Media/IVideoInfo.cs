namespace AngleSharp.Services.Media
{
    using System;

    /// <summary>
    /// Contains information about a video file.
    /// </summary>
    public interface IVideoInfo : IMediaInfo
    {
        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        Int32 Width { get; }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        Int32 Height { get; }
    }
}
