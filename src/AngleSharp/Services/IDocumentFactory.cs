namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the factory for creating documents from responses.
    /// </summary>
    public interface IDocumentFactory
    {
        /// <summary>
        /// Creates a new attribute selector from the given arguments.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="options">The options to consider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task creating the document from the response.</returns>
        Task<IDocument> CreateAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken);
    }
}
