namespace AngleSharp.Dom.Html
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement, IDisposable
    {
        #region Fields

        readonly IBrowsingContext _context;
        CancellationTokenSource _cts;
        SettableTokenList _sandbox;
        Task _docTask;
        
        #endregion

        #region ctor

        public HtmlIFrameElement(Document owner, String prefix = null)
            : base(owner, Tags.Iframe, prefix, NodeFlags.LiteralText)
        {
            _context = owner.NewChildContext(Sandboxes.None);
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public Alignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the content of the page that the nested browsing context is to contain.
        /// </summary>
        public String ContentHtml
        {
            get { return GetOwnAttribute(AttributeNames.SrcDoc); }
            set { SetOwnAttribute(AttributeNames.SrcDoc, value); }
        }

        public ISettableTokenList Sandbox
        {
            get
            { 
                if (_sandbox == null)
                {
                    _sandbox = new SettableTokenList(GetOwnAttribute(AttributeNames.Sandbox));
                    _sandbox.Changed += (s, ev) => UpdateAttribute(AttributeNames.Sandbox, _sandbox.Value);
                }

                return _sandbox;
            }
        }

        /// <summary>
        /// Gets or sets the value of the seamless attribute.
        /// </summary>
        public Boolean IsSeamless
        {
            get { return GetOwnAttribute(AttributeNames.SrcDoc) != null; }
            set { SetOwnAttribute(AttributeNames.SrcDoc, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the frame's content can trigger the fullscreen mode.
        /// </summary>
        public Boolean IsFullscreenAllowed
        {
            get { return GetOwnAttribute(AttributeNames.AllowFullscreen) != null; }
            set { SetOwnAttribute(AttributeNames.AllowFullscreen, value ? String.Empty : null); }
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

        public void Dispose()
        {
            if (_cts != null)
                _cts.Cancel();

            _docTask = null;
            _cts = null;
        }

        void UpdateSource(String src)
        {
            if (_cts != null)
                _cts.Cancel();

            if (!String.IsNullOrEmpty(src))
            {
                var url = this.HyperReference(src);
                var request = new DocumentRequest(url)
                {
                    Source = this,
                    Referer = Owner.DocumentUri
                };
                _cts = new CancellationTokenSource();
                _docTask = _context.OpenAsync(request, _cts.Token);
            }
        }

        #endregion
    }
}
