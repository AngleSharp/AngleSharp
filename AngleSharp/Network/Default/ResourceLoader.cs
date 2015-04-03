namespace AngleSharp.Network.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ResourceLoader : IResourceLoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IDocument _document;

        public ResourceLoader(IEnumerable<IRequester> requesters, IDocument document)
        {
            _requesters = requesters;
            _document = document;
        }

        public Task<IResponse> LoadAsync(ResourceRequest request, CancellationToken cancel)
        {
            var data = new Request
            {
                Address = request.Target,
                Method = HttpMethod.Get
            };

            return _requesters.LoadAsync(data, cancel);
        }
    }
}
