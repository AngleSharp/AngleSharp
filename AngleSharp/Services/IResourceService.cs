namespace AngleSharp.Services
{
    using AngleSharp.Services.Media;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a service to create a specific resource handler.
    /// </summary>
    public interface IResourceService<TResource> : IService
        where TResource : IResourceInfo
    {
        /// <summary>
        /// Checks if the given type is supported.
        /// </summary>
        /// <param name="mimeType">The type of the resource.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Tries to create an inspector for the given resource.
        /// </summary>
        /// <param name="response">The response that contains the stream to the resource.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>A task that finishes with an inspector for the resource.</returns>
        Task<TResource> CreateAsync(IResponse response, CancellationToken cancel);
    }
}
