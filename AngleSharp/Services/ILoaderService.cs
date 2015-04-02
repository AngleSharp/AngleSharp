namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Network;

    /// <summary>
    /// Represents a service to create loaders.
    /// </summary>
    public interface ILoaderService : IService
    {
        /// <summary>
        /// Creates a loader for documents. Returning null will disable loading
        /// documents from external sources.
        /// </summary>
        /// <param name="context">
        /// The originator of the document requests.
        /// </param>
        /// <returns>The new document loader.</returns>
        IDocumentLoader CreateDocumentLoader(IBrowsingContext context);

        /// <summary>
        /// Creates a loader for resources, such as images. Returning null will
        /// disable loading resources.
        /// </summary>
        /// <param name="document">
        /// The originator of the resource requests.
        /// </param>
        /// <returns>The new resource loader.</returns>
        IResourceLoader CreateResourceLoader(IDocument document);
    }
}
