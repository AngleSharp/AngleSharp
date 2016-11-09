namespace AngleSharp.Io.Processors
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a request processor.
    /// </summary>
    public interface IRequestProcessor
    {
        /// <summary>
        /// Gets the current download, if any.
        /// </summary>
        IDownload Download { get; }

        /// <summary>
        /// Starts processing the given request by cancelling
        /// the current download if any.
        /// </summary>
        /// <param name="request">The new request.</param>
        /// <returns>The task handling the request.</returns>
        Task ProcessAsync(ResourceRequest request);
    }
}
