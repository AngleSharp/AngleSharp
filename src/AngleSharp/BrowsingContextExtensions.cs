namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    public static class BrowsingContextExtensions
    {
        /// <summary>
        /// Opens a new document without any content in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The new, yet empty, document.</returns>
        public static Task<IDocument> OpenNewAsync(this IBrowsingContext context, String url = null)
        {
            return context.OpenAsync(m => m.Address(url));
        }

        /// <summary>
        /// Opens a new document created from the response asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (context == null)
            {
                context = BrowsingContext.New();
            }

            var options = new CreateDocumentOptions(response, context.Configuration);
            var factory = context.Configuration.GetFactory<IDocumentFactory>();
            return factory.CreateAsync(context, options, cancel);
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
                throw new ArgumentNullException(nameof(request));

            var loader = context.Loader;

            if (loader != null)
            {
                var download = loader.DownloadAsync(request);
                cancel.Register(download.Cancel);

                using (var response = await download.Task.ConfigureAwait(false))
                {
                    if (response != null)
                    {
                        return await context.OpenAsync(response, cancel).ConfigureAwait(false);
                    }
                }
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
                throw new ArgumentNullException(nameof(url));
            
            var request = DocumentRequest.Get(url);

            if (context != null && context.Active != null)
            {
                request.Referer = context.Active.DocumentUri;
            }

            return context.OpenAsync(request, cancel);
        }

        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request, CancellationToken cancel)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (var response = VirtualResponse.Create(request))
            {
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback without any ability to cancel it.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request)
        {
            return context.OpenAsync(request, CancellationToken.None);
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

        /// <summary>
        /// Opens a new document loaded from the provided address asynchronously
        /// in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="address">The address to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, String address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            return context.OpenAsync(Url.Create(address), CancellationToken.None);
        }

        /// <summary>
        /// Navigates to the given document. Includes the document in the
        /// session history and sets it as the active document.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="document">The new document.</param>
        public static void NavigateTo(this IBrowsingContext context, IDocument document)
        {
            context.SessionHistory?.PushState(document, document.Title, document.Url);
            context.Active = document;
        }
    }
}