namespace AngleSharp.Media
{
    using AngleSharp.DOM.Media;
    using System;

    /// <summary>
    /// Contains information about a video file.
    /// </summary>
    public interface IVideoInfo
    {
        /// <summary>
        /// Gets the controller responsible for the media.
        /// </summary>
        IMediaController Controller { get; }

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
