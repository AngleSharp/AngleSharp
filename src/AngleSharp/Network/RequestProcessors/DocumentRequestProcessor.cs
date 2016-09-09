namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using Services;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DocumentRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        private readonly IDocument _parentDocument;
        private readonly IConfiguration _configuration;

        #endregion

        #region ctor

        private DocumentRequestProcessor(IDocument document, IConfiguration configuration, IResourceLoader loader)
            : base(loader)
        {
            _parentDocument = document;
            _configuration = configuration;
        }

        internal static DocumentRequestProcessor Create(Element element)
        {
            var document = element.Owner;
            var configuration = document.Options;
            var loader = document.Loader;

            return configuration != null && loader != null ?
                new DocumentRequestProcessor(document, configuration, loader) : null;
        }

        #endregion

        #region Properties

        public IDocument ChildDocument
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var context = new BrowsingContext(_parentDocument.Context, Sandboxes.None);
            var options = new CreateDocumentOptions(response, _configuration, _parentDocument);
            var factory = _configuration.GetFactory<IDocumentFactory>();
            ChildDocument = await factory.CreateAsync(context, options, CancellationToken.None).ConfigureAwait(false);
        }

        #endregion
    }
}
