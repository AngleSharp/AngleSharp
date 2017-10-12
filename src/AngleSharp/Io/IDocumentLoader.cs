namespace AngleSharp.Io
{
    /// <summary>
    /// Interface used to handle top-level document requests. These requests
    /// include navigation tasks.
    /// </summary>
    public interface IDocumentLoader : ILoader
    {
        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The download instance to track.</returns>
        IDownload FetchAsync(DocumentRequest request);
    }
}
