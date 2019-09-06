namespace AngleSharp.Io
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the default resource loader. This class can be inherited.
    /// </summary>
    public class DefaultResourceLoader : BaseLoader, IResourceLoader
    {
        #region ctor

        /// <summary>
        /// Creates a new resource loader.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="filter">The optional request filter to use.</param>
        public DefaultResourceLoader(IBrowsingContext context, Predicate<Request> filter = null)
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
        public virtual IDownload FetchAsync(ResourceRequest request)
        {
            var data = new Request
            {
                Address = request.Target,
                Content = Stream.Null,
                Method = HttpMethod.Get,
                Headers = new Dictionary<String, String>
                {
                    [HeaderNames.Referer] = request?.Source.Owner.DocumentUri ?? String.Empty,
                },
            };

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
