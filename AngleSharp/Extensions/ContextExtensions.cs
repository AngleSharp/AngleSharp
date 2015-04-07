namespace AngleSharp.Extensions
{
    using System.Diagnostics;
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
            var loader = context.Configuration.GetService<ILoaderService>();

            if (loader == null)
                return null;

            return loader.CreateDocumentLoader(context);
        }
    }
}
