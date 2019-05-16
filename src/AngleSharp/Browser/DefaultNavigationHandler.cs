namespace AngleSharp.Browser
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DefaultNavigationHandler : INavigationHandler
    {
        private readonly IBrowsingContext _context;

        public DefaultNavigationHandler(IBrowsingContext context)
        {
            _context = context;
        }

        public async Task<IDocument> NavigateAsync(DocumentRequest request, CancellationToken cancel)
        {
            var download = _context.GetService<IDocumentLoader>()?.FetchAsync(request);

            if (download != null)
            {
                var response = await download.Task.ConfigureAwait(false);
                return await _context.OpenAsync(response, cancel).ConfigureAwait(false);
            }

            return null;
        }

        public Boolean SupportsProtocol(String protocol) =>
            _context.GetServices<IRequester>().Any(m => m.SupportsProtocol(protocol));
    }
}
