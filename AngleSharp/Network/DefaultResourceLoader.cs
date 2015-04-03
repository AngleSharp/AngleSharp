namespace AngleSharp.Network
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class DefaultResourceLoader : IResourceLoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IDocument _document;

        public DefaultResourceLoader(IEnumerable<IRequester> requesters, IDocument document)
        {
            _requesters = requesters;
            _document = document;
        }

        public Task<IResponse> LoadAsync(ResourceRequest request, CancellationToken cancel)
        {
            var data = new DefaultRequest
            {
                Address = request.Target,
                Method = HttpMethod.Get
            };

            return _requesters.LoadAsync(data, cancel);
        }
    }
}
