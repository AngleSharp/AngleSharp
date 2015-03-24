namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement, IDisposable
    {
        #region Fields

        readonly Document _doc;

        CancellationTokenSource _cts;
        SettableTokenList _sandbox;
        Task _docTask;
        
        #endregion

        #region ctor

        public HtmlIFrameElement(Document owner)
            : base(owner, Tags.Iframe, NodeFlags.LiteralText)
        {
            _doc = new Document(owner.NewChildContext(Sandboxes.None));
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
            get { return _doc != null ? _doc.DefaultView : null; }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (_cts != null)
                _cts.Cancel();

            _docTask = null;
            _cts = null;
            _doc.Dispose();
        }

        void UpdateSource(String src)
        {
            if (_cts != null)
                _cts.Cancel();

            if (!String.IsNullOrEmpty(src))
            {
                var url = this.HyperReference(src);
                var requester = Owner.Options.GetRequester(url.Scheme);

                if (requester == null)
                    return;

                _cts = new CancellationTokenSource();
                _docTask = LoadAsync(requester, url, _cts.Token);
            }
        }

        async Task LoadAsync(IRequester requester, Url url, CancellationToken cancel)
        {
            var response = await requester.LoadAsync(url).ConfigureAwait(false);

            if (response != null)
            {
                await _doc.LoadAsync(response, cancel).ConfigureAwait(false);
                response.Dispose();
            }
        }

        #endregion
    }
}
