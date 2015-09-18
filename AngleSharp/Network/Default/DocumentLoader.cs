namespace AngleSharp.Network.Default
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default document loader. This class can be inherited.
    /// </summary>
    public class DocumentLoader : IDocumentLoader
    {
        #region Fields

        readonly IEnumerable<IRequester> _requesters;
        readonly IBrowsingContext _context;
        readonly Predicate<IRequest> _filter;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="context">The context for the document loader.</param>
        /// <param name="filter">The optional request filter to use.</param>
        public DocumentLoader(IEnumerable<IRequester> requesters, IBrowsingContext context, Predicate<IRequest> filter = null)
        {
            _requesters = requesters;
            _context = context;
            _filter = filter ?? (_ => true);
        }

        #endregion

        #region Methods

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

            foreach (var header in request.Headers)
                data.Headers[header.Key] = header.Value;

            var cookie = _context.Configuration.GetCookie(request.Target.Origin);

            if (cookie != null)
                data.Headers[HeaderNames.Cookie] = cookie;

            return _filter(data) ? _requesters.LoadAsync(data, events, cancel) : TaskEx.FromResult(default(IResponse));
        }

        #endregion
    }
}
