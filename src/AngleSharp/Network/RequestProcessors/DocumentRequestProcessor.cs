namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using Services;
    using System.Threading;
    using System.Threading.Tasks;

    class DocumentRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        readonly IDocument _parentDocument;
        readonly IConfiguration _configuration;
        IDocument _childDocument;

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

        public IDocument Document
        {
            get { return _childDocument; }
        }

        #endregion

        #region Methods

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var context = new BrowsingContext(_parentDocument.Context, Sandboxes.None);
            var options = new CreateDocumentOptions(response, _configuration, _parentDocument);
            var factory = _configuration.GetFactory<IDocumentFactory>();
            _childDocument = await factory.CreateAsync(context, options, CancellationToken.None).ConfigureAwait(false);
        }

        #endregion
    }
}
