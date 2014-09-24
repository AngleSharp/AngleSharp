namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM.Media;
    using AngleSharp.Network;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Repesents a service to create an video controller.
    /// </summary>
    public interface IVideoService : IService
    {
        /// <summary>
        /// Checks if the given protocol is supported.
        /// </summary>
        /// <param name="mimeType">The type of the audio source.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Tries to create a video controller.
        /// </summary>
        /// <param name="response">The response that contains the video stream.</param>
        /// <returns>A task that finishes with a video controller, or nothing.</returns>
        Task<IMediaController> Create(IResponse response);
    }
}
