namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using System.Threading;
    using System.Threading.Tasks;

    class DocumentRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        readonly IDocument _parentDocument;
        readonly IConfiguration _options;
        IDocument _childDocument;

        #endregion

        #region ctor

        private DocumentRequestProcessor(IDocument document, IConfiguration options, IResourceLoader loader)
            : base(loader)
        {
            _parentDocument = document;
            _options = options;
        }

        internal static DocumentRequestProcessor Create(Element element)
        {
            var document = element.Owner;
            var options = document.Options;
            var loader = document.Loader;

            return options != null && loader != null ?
                new DocumentRequestProcessor(document, options, loader) : null;
        }

        #endregion

        #region Properties

        public IDocument Document
        {
            get { return _childDocument; }
        }

        #endregion

        #region Methods

        protected override async Task ProcessResponse(IResponse response)
        {
            var context = new BrowsingContext(_parentDocument.Context, Sandboxes.None);
            var options = new CreateDocumentOptions(response, _options)
            {
                ImportAncestor = _parentDocument
            };
            _childDocument = await context.OpenAsync(options, CancellationToken.None).ConfigureAwait(false);
        }

        #endregion
    }
}
