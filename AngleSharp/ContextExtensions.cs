namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    public static class ContextExtensions
    {
        #region Browsing Context

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response)
        {
            return context.OpenAsync(response, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            var src = new TextSource(response.Content, context.Configuration.DefaultEncoding());
            var doc = new Document { Context = context };
            return await doc.LoadAsync(response, cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url)
        {
            return context.OpenAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel)
        {
            var config = context.Configuration;
            var requester = config.GetRequester(url.Scheme);

            if (requester == null)
                return null;

            using (var response = await requester.LoadAsync(url, cancel).ConfigureAwait(false))
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
        }

        #endregion
    }
}
