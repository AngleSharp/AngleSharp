namespace AngleSharp.Browser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
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
            var target = request.Source is HtmlUrlBaseElement urlBase ? urlBase.Target : null;
            var context = _context.ResolveTargetContext(target);
            var loader = context.GetService<IDocumentLoader>();

            if (loader != null)
            {
                var download = loader.FetchAsync(request);
                cancel.Register(download.Cancel);

                using (var response = await download.Task.ConfigureAwait(false))
                {
                    if (response != null)
                    {
                        return await context.OpenAsync(response, cancel).ConfigureAwait(false);
                    }
                }
            }

            return await context.OpenNewAsync(request.Target.Href, cancel).ConfigureAwait(false);
        }

        public Boolean SupportsProtocol(String protocol) =>
            _context.GetServices<IRequester>().Any(m => m.SupportsProtocol(protocol));
    }
}
