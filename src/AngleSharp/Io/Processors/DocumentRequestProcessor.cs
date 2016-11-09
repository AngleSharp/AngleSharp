namespace AngleSharp.Io.Processors
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DocumentRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        private readonly IDocument _parentDocument;
        private readonly IBrowsingContext _context;

        #endregion

        #region ctor

        public DocumentRequestProcessor(IBrowsingContext context)
            : base(context?.GetService<IResourceLoader>())
        {
            _parentDocument = context.Active;
            _context = context;
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
            var context = new BrowsingContext(_context, Sandboxes.None);
            var encoding = _context.GetDefaultEncoding();
            var options = new CreateDocumentOptions(response, encoding, _parentDocument);
            var factory = _context.GetFactory<IDocumentFactory>();
            ChildDocument = await factory.CreateAsync(context, options, CancellationToken.None).ConfigureAwait(false);
        }

        #endregion
    }
}
