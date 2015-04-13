namespace AngleSharp
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    [DebuggerStepThrough]
    public static class BrowsingContextExtensions
    {
        #region Navigation

        /// <summary>
        /// Opens a new document without any content in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The new, yet empty, document.</returns>
        public static async Task<IDocument> OpenNewAsync(this IBrowsingContext context, String url = null)
        {
            var doc = new Document(context) { DocumentUri = url };
            doc.AppendChild(doc.CreateElement(Tags.Html));
            doc.DocumentElement.AppendChild(doc.CreateElement(Tags.Head));
            doc.DocumentElement.AppendChild(doc.CreateElement(Tags.Body));
            await doc.FinishLoading();
            doc.Context.NavigateTo(doc);
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
            if (response == null)
                throw new ArgumentNullException("response");

            var doc = new Document(context);
            await doc.LoadAsync(response, cancel).ConfigureAwait(false);
            doc.Context.NavigateTo(doc);
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
            if (request == null)
                throw new ArgumentNullException("request");

            var response = await context.Loader.SendAsync(request, cancel).ConfigureAwait(false);

            if (response != null)
            {
                var document = await context.OpenAsync(response, cancel).ConfigureAwait(false);
                response.Dispose();
                return document;
            }

            return await context.OpenNewAsync(request.Target.Href).ConfigureAwait(false);
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
            if (url == null)
                throw new ArgumentNullException("url");
            
            var request = new DocumentRequest(url);

            if (context != null && context.Active != null)
                request.Referer = context.Active.DocumentUri;

            return context.OpenAsync(request, cancel);
        }

        /// <summary>
        /// Opens a new document loaded from the provided url asynchronously in
        /// the given context without the ability to cancel it.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url)
        {
            return context.OpenAsync(url, CancellationToken.None);
        }

        #endregion
    }
}