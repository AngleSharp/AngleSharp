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
                Method = request.Method,
            };

            foreach (var header in request.Headers)
            {
                data.Headers[header.Key] = header.Value;
            }

            return DownloadAsync(data, request.Source);
        }

        #endregion
    }
}
