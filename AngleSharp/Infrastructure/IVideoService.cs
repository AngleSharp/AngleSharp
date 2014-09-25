namespace AngleSharp.Infrastructure
{
    using AngleSharp.Media;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Repesents a service to create a video player.
    /// </summary>
    public interface IVideoService : IService
    {
        /// <summary>
        /// Checks if the given type is supported.
        /// </summary>
        /// <param name="mimeType">The type of the audio source.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Tries to create a video inspector.
        /// </summary>
        /// <param name="response">The response that contains the video stream.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>A task that finishes with a video inspector.</returns>
        Task<IVideoInfo> CreateAsync(IResponse response, CancellationToken cancel);
    }
}
