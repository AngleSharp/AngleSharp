using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace AngleSharp.Io
{
    using System;

    /// <summary>
    /// Represents the default document loader. This class can be inherited.
    /// </summary>
    public class DefaultDocumentLoader : BaseLoader, IDocumentLoader
    {
        #region ctor

        /// <summary>
        /// Creates a new document loader.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="filter">The optional request filter to use.</param>
        public DefaultDocumentLoader(IBrowsingContext context, Predicate<Request> filter = null)
            : base(context, filter)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the data for the request asynchronously.
        /// </summary>
        /// <param name="request">The issued request.</param>
        /// <returns>The active download.</returns>
        public virtual IDownload FetchAsync(DocumentRequest request)
        {
            var data = new Request
            {
                Address = request.Target,
                Content = request.Body,
                Method = request.Method
            };

            foreach (var header in request.Headers)
            {
                data.Headers[header.Key] = header.Value;
            }

            return DownloadAsync(data, request.Source);
        }

        /// <summary>
        /// Opens a new document loaded from the specified request
        /// asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public async Task<IDocument> OpenAsync(IBrowsingContext context, DocumentRequest request, CancellationToken cancel = new CancellationToken())
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var download = FetchAsync(request);
            cancel.Register(download.Cancel);

            using (var response = await download.Task.ConfigureAwait(false))
            {
                if (response != null)
                {
                    return await context.OpenAsync(response, cancel).ConfigureAwait(false);
                }
            }

            return await context.OpenNewAsync(request.Target.Href, cancel).ConfigureAwait(false);
        }

        #endregion
    }
}
