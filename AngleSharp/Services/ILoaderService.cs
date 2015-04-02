namespace AngleSharp.Services
{
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
        /// <returns>The new document loader.</returns>
        IDocumentLoader CreateDocumentLoader();

        /// <summary>
        /// Creates a loader for resources, such as images. Returning null will
        /// disable loading resources.
        /// </summary>
        /// <returns>The new resource loader.</returns>
        IResourceLoader CreateResourceLoader();
    }
}
