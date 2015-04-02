namespace AngleSharp.Network
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface used to handle top-level document requests. These requests
    /// include navigation tasks.
    /// </summary>
    public interface IDocumentLoader
    {
        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>
        /// The task that will eventually give the document's response data.
        /// </returns>
        Task<IResponse> LoadAsync(DocumentRequest request, CancellationToken cancel);
    }
}
