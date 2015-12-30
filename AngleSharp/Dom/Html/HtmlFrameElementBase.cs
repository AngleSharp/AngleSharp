namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    abstract class HtmlFrameElementBase : HtmlFrameOwnerElement
    {
        #region Fields

        IBrowsingContext _context;
        FrameElementRequest _download;

        #endregion

        #region ctor

        public HtmlFrameElementBase(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        public String Scrolling
        {
            get { return this.GetOwnAttribute(AttributeNames.Scrolling); }
            set { this.SetOwnAttribute(AttributeNames.Scrolling, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is
        /// available, or null otherwise.
        /// </summary>
        public IDocument ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image
        /// or frame.
        /// </summary>
        public String LongDesc
        {
            get { return this.GetOwnAttribute(AttributeNames.LongDesc); }
            set { this.SetOwnAttribute(AttributeNames.LongDesc, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        public String FrameBorder
        {
            get { return this.GetOwnAttribute(AttributeNames.FrameBorder); }
            set { this.SetOwnAttribute(AttributeNames.FrameBorder, value); }
        }

        /// <summary>
        /// Gets the nested context responsible for the content.
        /// </summary>
        protected IBrowsingContext NestedContext
        {
            get { return _context ?? (_context = Owner.NewChildContext(Sandboxes.None)); }
        }

        #endregion

        #region Methods

        protected virtual String GetContentHtml()
        {
            return null;
        }

        protected void UpdateSource(String _)
        {
            if (_download != null)
            {
                _download.Cancel();
            }

            var content = GetContentHtml();
            var source = Source;
            var document = Owner;

            if ((source != null || content != null) && document != null)
            {
                var download = new FrameElementRequest(document, this, NestedContext, content, source);
                var task = download.Perform(result => ContentDocument = result);
                document.DelayLoad(task);
                _download = download;
            }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var src = this.GetOwnAttribute(AttributeNames.Src);
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);

            if (src != null)
            {
                UpdateSource(src);
            }
        }

        #endregion

        #region Request Wrapper

        sealed class FrameElementRequest
        {
            readonly Document _document;
            readonly HtmlFrameElementBase _element;
            readonly IBrowsingContext _context;
            readonly String _htmlContent;
            readonly String _requestUrl;
            readonly CancellationTokenSource _cts;
            IDownload _download;

            public FrameElementRequest(Document document, HtmlFrameElementBase element, IBrowsingContext context, String htmlContent, String requestUrl)
            {
                _document = document;
                _element = element;
                _context = context;
                _htmlContent = htmlContent;
                _requestUrl = requestUrl;
                _cts = new CancellationTokenSource();
            }

            public async Task Perform(Action<IDocument> callback)
            {
                var document = await GetDocumentAsync().ConfigureAwait(false);
                callback(document);
            }

            public void Cancel()
            {
                _cts.Cancel();
            }

            async Task<IDocument> GetDocumentAsync()
            {
                var referer = _document.DocumentUri;
                var loader = _document.Loader;

                if (_htmlContent == null && !String.IsNullOrEmpty(_requestUrl) && !_requestUrl.Is(_element.BaseUri) && loader != null)
                {
                    var cancel = _cts.Token;
                    var url = _element.HyperReference(_requestUrl);
                    var request = _element.CreateRequestFor(url);
                    _download = loader.DownloadAsync(request);
                    cancel.Register(_download.Cancel);
                    var response = await _download.Task.ConfigureAwait(false);
                    return await _context.OpenAsync(response, cancel).ConfigureAwait(false);
                }

                return await _context.OpenAsync(m => m.Content(_htmlContent).Address(referer), _cts.Token).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
