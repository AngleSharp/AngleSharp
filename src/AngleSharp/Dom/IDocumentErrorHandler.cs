namespace AngleSharp.Dom
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a handler of the errors creating a document from a response,
    /// e.g., a response that isn't well-formed XML. Without one (or if it doesn't
    /// handle the error) the error propagates, e.g., from OpenAsync.
    /// </summary>
    public interface IDocumentErrorHandler
    {
        /// <summary>
        /// Handles the error that occurred creating a document from a response.
        /// </summary>
        /// <param name="context">The browsing context of the document.</param>
        /// <param name="options">The options the document was created with.</param>
        /// <param name="error">The error that occurred.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The task resulting in the document to use instead, or null to let
        /// the error propagate.
        /// </returns>
        Task<IDocument?> HandleAsync(IBrowsingContext context, CreateDocumentOptions options, Exception error, CancellationToken cancellationToken);
    }
}
