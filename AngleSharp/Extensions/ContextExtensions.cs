namespace AngleSharp.Extensions
{
    using System.Diagnostics;
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using AngleSharp.Services;

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
        public static IDocumentLoader CreateLoader(this IBrowsingContext context)
        {
            var service = context.Configuration.GetService<ILoaderService>();

            if (service == null)
                return null;

            return service.CreateDocumentLoader(context);
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
