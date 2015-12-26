namespace AngleSharp.Network.Default
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the default resource loader. This class can be inherited.
    /// </summary>
    public class ResourceLoader : BaseLoader, IResourceLoader
    {
        /// <summary>
        /// Creates a new resource loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="configuration">The associated configuration..</param>
        /// <param name="filter">The optional request filter to use.</param>
        public ResourceLoader(IEnumerable<IRequester> requesters, IConfiguration configuration, Predicate<IRequest> filter = null)
            : base(requesters, configuration, filter)
        {
        }

        /// <summary>
        /// Loads the data for the request asynchronously.
        /// </summary>
        /// <param name="request">The issued request.</param>
        /// <returns>The active download.</returns>
        public virtual IDownload DownloadAsync(ResourceRequest request)
        {
            var data = new Request
            {
                Address = request.Target,
                Method = HttpMethod.Get
            };

            data.Headers[HeaderNames.Referer] = request.Source.Owner.DocumentUri;
            return DownloadAsync(data, request.Source);
        }
    }
}
