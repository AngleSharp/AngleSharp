namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    [DebuggerStepThrough]
    public static class ContextExtensions
    {
        #region Navigation

        /// <summary>
        /// Opens a new document without any content in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The new, yet empty, document.</returns>
        public static IDocument OpenNew(this IBrowsingContext context, String url = null)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var doc = new Document(context) { DocumentUri = url };
            context.NavigateTo(doc);
            doc.FinishLoading();
            return doc;
        }

        /// <summary>
        /// Opens a new document created from the response asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            else if (response == null)
                throw new ArgumentNullException("response");

            var doc = new Document(context);
            await doc.LoadAsync(response, cancel).ConfigureAwait(false);
            context.NavigateTo(doc);
            return doc;
        }

        /// <summary>
        /// Opens a new document loaded from the specified request
        /// asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, DocumentRequest request, CancellationToken cancel)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            else if (request == null)
                throw new ArgumentNullException("request");

            using (var response = await context.Loader.SendAsync(request, cancel).ConfigureAwait(false))
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Opens a new document loaded from the provided url asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            else if (url == null)
                throw new ArgumentNullException("url");

            var current = context.Active;
            var request = new DocumentRequest(url);

            if (current != null)
                request.Origin = current.Origin;

            return context.OpenAsync(request, cancel);
        }

        #endregion

        #region Internal

        /// <summary>
        /// Gets the document loader for the given context, by creating it if
        /// possible.
        /// </summary>
        /// <param name="context">The context that hosts the loader.</param>
        /// <returns>A document loader or null.</returns>
        internal static IDocumentLoader CreateLoader(this IBrowsingContext context)
        {
            var loader = context.Configuration.GetService<ILoaderService>();

            if (loader == null)
                return null;

            return loader.CreateDocumentLoader(context);
        }

        #endregion
    }
}