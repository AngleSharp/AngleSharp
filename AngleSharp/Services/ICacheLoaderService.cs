namespace AngleSharp.Services
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides methods to load cached resource.
    /// </summary>
    public interface ICacheLoaderService : IService
    {
        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="url">The address of the resource.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>The task that will eventually give the cached content stream.</returns>
        Task<Stream> LoadAsync(Url url, CancellationToken cancel);
    }
}
