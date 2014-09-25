namespace AngleSharp.Infrastructure
{
    using AngleSharp.Media;
using AngleSharp.Network;
using System;
using System.Threading;
using System.Threading.Tasks;

    /// <summary>
    /// Repesents a service to create a sound player.
    /// </summary>
    public interface IAudioService : IService
    {
        /// <summary>
        /// Checks if the given type is supported.
        /// </summary>
        /// <param name="mimeType">The type of the audio source.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Tries to create an audio inspector.
        /// </summary>
        /// <param name="response">The response that contains the audio stream.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>A task that finishes with an audio inspector.</returns>
        Task<IAudioInfo> CreateAsync(IResponse response, CancellationToken cancel);
    }
}
