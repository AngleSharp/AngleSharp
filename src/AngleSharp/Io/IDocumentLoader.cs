using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

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

        /// <summary>
        /// Opens a new document loaded from the specified request
        /// asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        Task<IDocument> OpenAsync(IBrowsingContext context, DocumentRequest request, CancellationToken cancel = default(CancellationToken));
    }
}
