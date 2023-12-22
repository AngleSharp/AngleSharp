namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Mathml.Dom;
    using AngleSharp.Svg.Dom;
    using AngleSharp.Text;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Construction;
    using Parser;
    using Parser.Tokens.Struct;

    /// <summary>
    /// Represents a document node that contains only HTML nodes.
    /// </summary>
    public sealed class HtmlDocument : Document, IHtmlDocument, IConstructableDocument
    {
        #region Fields

        private readonly IElementFactory<Document, HtmlElement> _htmlFactory;
        private readonly IElementFactory<Document, MathElement> _mathFactory;
        private readonly IElementFactory<Document, SvgElement> _svgFactory;

        #endregion

        #region ctor

        internal HtmlDocument(IBrowsingContext? context, IReadOnlyTextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Html;
            _htmlFactory = Context.GetFactory<IElementFactory<Document, HtmlElement>>();
            _mathFactory = Context.GetFactory<IElementFactory<Document, MathElement>>();
            _svgFactory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
        }

        public HtmlDocument(IBrowsingContext? context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement => this.FindChild<HtmlHtmlElement>()!;

        public override IEntityProvider Entities => Context.GetProvider<IEntityProvider>() ?? HtmlEntityProvider.Resolver;

        #endregion

        #region Methods

        public HtmlElement CreateHtmlElement(String name, String? prefix = null, NodeFlags flags = NodeFlags.None) => _htmlFactory.Create(this, name, prefix, flags);

        public MathElement CreateMathElement(String name, String? prefix = null, NodeFlags flags = NodeFlags.None) => _mathFactory.Create(this, name, prefix, flags);

        public SvgElement CreateSvgElement(String name, String? prefix = null, NodeFlags flags = NodeFlags.None) => _svgFactory.Create(this, name, prefix, flags);

        public override Element CreateElementFrom(String name, String? prefix, NodeFlags flags = NodeFlags.None) => CreateHtmlElement(name, prefix, flags);

        public override Node Clone(Document owner, Boolean deep)
        {
            var source = new TextSource(Source.Text);
            var node = new HtmlDocument(Context, source);
            CloneDocument(node, deep);
            return node;
        }

        #endregion

        #region Helpers

        protected override String GetTitle()
        {
            var title = DocumentElement!.FindDescendant<IHtmlTitleElement>();
            return title?.TextContent.CollapseAndStrip() ?? base.GetTitle();
        }

        protected override void SetTitle(String? value)
        {
            var title = DocumentElement!.FindDescendant<IHtmlTitleElement>();

            if (title is null)
            {
                var head = Head;

                if (head is null)
                {
                    return;
                }

                title = new HtmlTitleElement(this);
                head.AppendChild(title);
            }

            title.TextContent = value!;
        }

        #endregion

        #region Construction

        IDisposable? IConstructableDocument.Builder { get; set; }

        QuirksMode IConstructableDocument.QuirksMode
        {
            get => QuirksMode;
            set => QuirksMode = value;
        }

        IConstructableElement? IConstructableDocument.Head => DocumentElement.FindChild<HtmlHeadElement>();

        IConstructableElement IConstructableDocument.DocumentElement => this.FindChild<HtmlHtmlElement>()!;

        void IConstructableDocument.PerformMicrotaskCheckpoint()
        {
            this.PerformMicrotaskCheckpoint();
        }

        void IConstructableDocument.ProvideStableState()
        {
            this.ProvideStableState();
        }

        void IConstructableDocument.AddComment(ref StructHtmlToken token)
        {
            HtmlDomBuilderExtensions.AddComment(this, ref token);
        }

        void IConstructableDocument.TrackError(Exception exception)
        {
            Context.TrackError(exception);
        }

        Task IConstructableDocument.WaitForReadyAsync(CancellationToken cancelToken)
        {
            return this.WaitForReadyAsync();
        }

        void IConstructableDocument.ApplyManifest()
        {
            this.ApplyManifest();
        }

        Boolean IConstructableDocument.IsLoading => IsLoading;

        Task IConstructableDocument.FinishLoadingAsync()
        {
            return this.FinishLoadingAsync();
        }

        #endregion

        public override void Dispose()
        {
            ((IConstructableDocument)this).Builder?.Dispose();
            base.Dispose();
        }
    }
}
