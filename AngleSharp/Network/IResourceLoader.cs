namespace AngleSharp.Network
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface used to handle resource requests for a document. These
    /// requests include, but are not limited to, media, script and styling
    /// resources.
    /// </summary>
    public interface IResourceLoader
    {
        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>
        /// The task that will eventually give the resource's response data.
        /// </returns>
        Task<IResponse> LoadAsync(ResourceRequest request, CancellationToken cancel);
    }
}
