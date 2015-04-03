namespace AngleSharp.Network
{
    using AngleSharp.Extensions;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class DefaultDocumentLoader : IDocumentLoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IBrowsingContext _context;

        public DefaultDocumentLoader(IEnumerable<IRequester> requesters, IBrowsingContext context)
        {
            _requesters = requesters;
            _context = context;
        }

        public Task<IResponse> LoadAsync(DocumentRequest request, CancellationToken cancel)
        {
            var data = new DefaultRequest
            {
                Address = request.Target,
                Content = request.Body,
                Method = request.Method
            };

            if (request.MimeType != null)
                data.Headers["mime-type"] = request.MimeType;

            return _requesters.LoadAsync(data, cancel);
        }
    }
}
