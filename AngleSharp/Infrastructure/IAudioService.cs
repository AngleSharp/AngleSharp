namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM.Media;
    using AngleSharp.Network;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Repesents a service to create an audio controller.
    /// </summary>
    public interface IAudioService : IService
    {
        /// <summary>
        /// Checks if the given protocol is supported.
        /// </summary>
        /// <param name="mimeType">The type of the audio source.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Tries to create a media controller.
        /// </summary>
        /// <param name="response">The response that contains the audio stream.</param>
        /// <returns>A task that finishes with a media controller, or nothing.</returns>
        Task<IMediaController> Create(IResponse response);
    }
}
