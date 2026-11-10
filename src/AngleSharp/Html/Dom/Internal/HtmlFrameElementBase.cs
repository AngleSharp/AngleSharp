namespace AngleSharp.Html.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    abstract class HtmlFrameElementBase : HtmlFrameOwnerElement, IConnectedResourceElement
    {
        #region Fields

        private IBrowsingContext? _context;
        private FrameRequestProcessor _request;
        private Boolean _isSetUp;

        #endregion

        #region ctor

        public HtmlFrameElementBase(Document owner, String name, String? prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
            _request = new FrameRequestProcessor(owner.Context, this);
        }

        #endregion

        #region Properties

        public IDownload? CurrentDownload => _request?.Download;

        public String? Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        public String? Source
        {
            get => this.GetUrlAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? Scrolling
        {
            get => this.GetOwnAttribute(AttributeNames.Scrolling);
            set => this.SetOwnAttribute(AttributeNames.Scrolling, value);
        }

        public IDocument? ContentDocument => _context?.Active ?? GetInitialDocument();

        public String? LongDesc
        {
            get => this.GetOwnAttribute(AttributeNames.LongDesc);
            set => this.SetOwnAttribute(AttributeNames.LongDesc, value);
        }

        public String? FrameBorder
        {
            get => this.GetOwnAttribute(AttributeNames.FrameBorder);
            set => this.SetOwnAttribute(AttributeNames.FrameBorder, value);
        }

        public IBrowsingContext NestedContext
        {
            get
            {
                // a download finishing on another thread must end up in the same context
                lock (_request)
                {
                    return _context ??= NewChildContext();
                }
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// A frame in a document shows a document from the start: about:blank, unless it has
        /// something else to show (HTML 4.8.5). It's made once somebody asks for it, so that a
        /// page isn't paying for frames only a script could tell the difference of.
        /// </summary>
        private IDocument? GetInitialDocument()
        {
            if (!HasNothingToLoad || !CustomElementRegistry.IsConnected(this))
            {
                return null;
            }

            lock (_request)
            {
                var context = NestedContext;

                if (context.Active is null)
                {
                    (context as BrowsingContext)?.UpdateSecurity(GetSandboxFlags());
                    var parser = context.GetService<IHtmlParser>() as HtmlParser ?? new HtmlParser(context);
                    var document = parser.ParseDocument(String.Empty);
                    ((Document)document).UseBaseUrlOfCreator();
                    context.Active = document;
                }

                return context.Active;
            }
        }

        private Boolean HasNothingToLoad
        {
            get
            {
                var source = this.GetOwnAttribute(AttributeNames.Src);
                return (String.IsNullOrEmpty(source) || source.Isi("about:blank")) && GetContentHtml() is null;
            }
        }

        /// <inheritdoc />
        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            // not right here: the insertion isn't done yet, and a listener may do anything
            if (HasNothingToLoad && CustomElementRegistry.IsConnected(this))
            {
                Owner.QueueTask(() => this.FireSimpleEvent(EventNames.Load));
            }
        }

        internal virtual String GetContentHtml()
        {
            return null!;
        }

        internal override void SetupElement()
        {
            base.SetupElement();
            _isSetUp = true;

            _ = NestedContext;

            if (this.GetOwnAttribute(AttributeNames.Src) != null)
            {
                UpdateSource();
            }
        }

        /// <summary>
        /// A frame navigates once it is inserted into a document (HTML 4.8.5), e.g. after
        /// innerHTML parsed it, or a script made it and set its source before appending it.
        /// </summary>
        void IConnectedResourceElement.OnConnected()
        {
            // the parser inserts a frame before it set it up, which loads it then
            if (_isSetUp && !HasNothingToLoad)
            {
                UpdateSource();
            }
        }

        internal void UpdateSource()
        {
            // a frame outside of a document has no browsing context to navigate
            if (!this.IsInLoadingTree())
            {
                return;
            }

            var content = GetContentHtml();
            var source = Source;

            if ((source != null && source != Owner.DocumentUri) || content != null)
            {
                var url = this.HyperReference(source!);
                this.Process(_request, url!);
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// The flags the frame is sandboxed with for the next document (a change of the attribute
        /// only applies once the frame navigates): its own and those of what is around it.
        /// </summary>
        internal virtual Sandboxes GetSandboxFlags() => Context?.Security ?? Sandboxes.None;

        private IBrowsingContext NewChildContext()
        {
            var childContext = default(IBrowsingContext);
            if (Context is BrowsingContext context)
            {
                childContext  = context.CreateChild(Name, Sandboxes.None, true);
            }
            else
            {
                childContext  = Context.CreateChild(Name, Sandboxes.None);
            }
            Owner.AttachReference(childContext);
            return childContext;
        }

        #endregion
    }
}
