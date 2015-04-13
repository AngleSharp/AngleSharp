namespace AngleSharp.Network.Default
{
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default document loader. This class can be inherited.
    /// </summary>
    public class DocumentLoader : IDocumentLoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IBrowsingContext _context;

        /// <summary>
        /// Creates a new document loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="context">The context for the document loader.</param>
        public DocumentLoader(IEnumerable<IRequester> requesters, IBrowsingContext context)
        {
            _requesters = requesters;
            _context = context;
        }

        /// <summary>
        /// Loads the data for the request asynchronously.
        /// </summary>
        /// <param name="request">The issued request.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task creating the response.</returns>
        public virtual Task<IResponse> LoadAsync(DocumentRequest request, CancellationToken cancel)
        {
            var events = _context.Configuration.Events;
            var data = new Request
            {
                Address = request.Target,
                Content = request.Body,
                Method = request.Method
            };

            if (request.MimeType != null)
                data.Headers[HeaderNames.ContentType] = request.MimeType;
            
            if (request.Referer != null)
                data.Headers[HeaderNames.Referer] = request.Referer;

            return _requesters.LoadAsync(data, events, cancel);
        }
    }
}
