namespace AngleSharp.Network
{
    /// <summary>
    /// Interface used to handle resource requests for a document. These
    /// requests include, but are not limited to, media, script and styling
    /// resources.
    /// </summary>
    public interface IResourceLoader : ILoader
    {
        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>
        /// The task that will eventually give the resource's response data.
        /// </returns>
        IDownload DownloadAsync(ResourceRequest request);
    }
}
