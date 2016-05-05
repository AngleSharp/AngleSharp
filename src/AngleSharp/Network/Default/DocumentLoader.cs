namespace AngleSharp.Network.Default
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the default document loader. This class can be inherited.
    /// </summary>
    public class DocumentLoader : BaseLoader, IDocumentLoader
    {
        #region ctor

        /// <summary>
        /// Creates a new document loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="context">The context to use.</param>
        /// <param name="filter">The optional request filter to use.</param>
        public DocumentLoader(IEnumerable<IRequester> requesters, IBrowsingContext context, Predicate<IRequest> filter = null)
            : base(requesters, context, filter)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the data for the request asynchronously.
        /// </summary>
        /// <param name="request">The issued request.</param>
        /// <returns>The active download.</returns>
        public virtual IDownload DownloadAsync(DocumentRequest request)
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

            var cookie = GetCookie(request.Target);

            if (cookie != null)
            {
                data.Headers[HeaderNames.Cookie] = cookie;
            }

            return DownloadAsync(data, request.Source);
        }

        #endregion
    }
}
