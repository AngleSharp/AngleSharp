namespace AngleSharp.Network.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default resource loader. This class can be inherited.
    /// </summary>
    public class ResourceLoader : IResourceLoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IDocument _document;

        /// <summary>
        /// Creates a new resource loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="document">The document hosting the resources.</param>
        public ResourceLoader(IEnumerable<IRequester> requesters, IDocument document)
        {
            _requesters = requesters;
            _document = document;
        }

        /// <summary>
        /// Loads the data for the request asynchronously.
        /// </summary>
        /// <param name="request">The issued request.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task creating the response.</returns>
        public virtual Task<IResponse> LoadAsync(ResourceRequest request, CancellationToken cancel)
        {
            var events = _document.Context.Configuration.Events;
            var data = new Request
            {
                Address = request.Target,
                Method = HttpMethod.Get
            };

            data.Headers[HeaderNames.Referer] = request.Source.Owner.DocumentUri;
            return _requesters.LoadAsync(data, events, cancel);
        }
    }
}
