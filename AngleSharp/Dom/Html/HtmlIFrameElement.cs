namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement
    {
        #region Fields

        readonly IBrowsingContext _context;
        SettableTokenList _sandbox;
        
        #endregion

        #region ctor

        public HtmlIFrameElement(Document owner, String prefix = null)
            : base(owner, Tags.Iframe, prefix, NodeFlags.LiteralText)
        {
            _context = owner.NewChildContext(Sandboxes.None);
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
            RegisterAttributeObserver(AttributeNames.SrcDoc, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public Alignment Align
        {
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the content of the page that the nested browsing context is to
        /// contain.
        /// </summary>
        public String ContentHtml
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcDoc); }
            set { this.SetOwnAttribute(AttributeNames.SrcDoc, value); }
        }

        /// <summary>
        /// Gets the sandbox security flags.
        /// </summary>
        public ISettableTokenList Sandbox
        {
            get
            { 
                if (_sandbox == null)
                {
                    _sandbox = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sandbox));
                    CreateBindings(_sandbox, AttributeNames.Sandbox);
                }

                return _sandbox;
            }
        }

        /// <summary>
        /// Gets or sets the value of the seamless attribute.
        /// </summary>
        public Boolean IsSeamless
        {
            get { return this.HasOwnAttribute(AttributeNames.SrcDoc); }
            set { this.SetOwnAttribute(AttributeNames.SrcDoc, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the frame's content can trigger the fullscreen mode.
        /// </summary>
        public Boolean IsFullscreenAllowed
        {
            get { return this.HasOwnAttribute(AttributeNames.AllowFullscreen); }
            set { this.SetOwnAttribute(AttributeNames.AllowFullscreen, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the frame's parent's window context.
        /// </summary>
        public IWindow ContentWindow
        {
            get { return _context.Current; }
        }

        #endregion

        #region Methods

        void UpdateSource(String _)
        {
            this.CancelTasks();
            var content = ContentHtml;
            var src = Source;

            if (content != null)
            {
                this.CreateTask(cancel => _context.OpenAsync(m => m.Content(content).Address(Owner.DocumentUri), cancel))
                    .ContinueWith(Finished);
            }
            else if (!String.IsNullOrEmpty(src) && !src.Is(BaseUri))
            {
                var url = this.HyperReference(src);
                var request = DocumentRequest.Get(url, source: this, referer: Owner.DocumentUri);
                this.CreateTask(cancel => _context.OpenAsync(request, cancel))
                    .ContinueWith(Finished);
            }
        }

        void Finished(Task<IDocument> task)
        {
            if (!task.IsFaulted)
                ContentDocument = task.Result;

            this.FireLoadOrErrorEvent(task);
        }

        #endregion
    }
}
