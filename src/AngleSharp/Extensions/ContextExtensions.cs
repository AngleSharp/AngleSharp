namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for browsing contexts.
    /// </summary>
    [DebuggerStepThrough]
    static class ContextExtensions
    {
        /// <summary>
        /// Gets the document loader for the given context, by creating it if
        /// possible.
        /// </summary>
        /// <param name="context">The context that hosts the loader.</param>
        /// <returns>A document loader or null.</returns>
        public static IDocumentLoader CreateDocumentLoader(this IBrowsingContext context)
        {
            var service = context.Configuration.GetService<ILoaderService>();
            return service != null ? service.CreateDocumentLoader(context) : null;
        }

        /// <summary>
        /// Gets the resource loader for the given document, by creating it if
        /// possible.
        /// </summary>
        /// <param name="context">The context that hosts the loader.</param>
        /// <returns>A resource loader or null.</returns>
        public static IResourceLoader CreateResourceLoader(this IBrowsingContext context)
        {
            var loader = context.Configuration.GetService<ILoaderService>();
            return loader != null ? loader.CreateResourceLoader(context) : null;
        }

        /// <summary>
        /// Gets the history tracker for the given context, by creating it if
        /// possible.
        /// </summary>
        /// <param name="context">The context that needs to be tracked.</param>
        /// <returns>An history object or null.</returns>
        public static IHistory CreateHistory(this IBrowsingContext context)
        {
            var service = context.Configuration.GetService<IHistoryService>();

            if (service == null)
                return null;

            return service.CreateHistory(context);
        }
    }
}
