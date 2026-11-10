namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement, IConnectedResourceElement
    {
        #region Fields

        private readonly ObjectRequestProcessor _request;
        private Boolean _isSetUp;

        #endregion

        #region ctor

        public HtmlEmbedElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _request = new ObjectRequestProcessor(owner.Context);
        }

        #endregion

        #region Properties

        public IDownload? CurrentDownload => _request?.Download;

        public String? Source
        {
            get => this.GetOwnAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public String? DisplayWidth
        {
            get => this.GetOwnAttribute(AttributeNames.Width);
            set => this.SetOwnAttribute(AttributeNames.Width, value);
        }

        public String? DisplayHeight
        {
            get => this.GetOwnAttribute(AttributeNames.Height);
            set => this.SetOwnAttribute(AttributeNames.Height, value);
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            _isSetUp = true;
            UpdateSource(this.GetOwnAttribute(AttributeNames.Src));
        }

        /// <summary>
        /// The resource is only fetched while the element is in a document (HTML 4.8.3/4.8.4),
        /// e.g. once innerHTML put it there.
        /// </summary>
        void IConnectedResourceElement.OnConnected()
        {
            if (_isSetUp)
            {
                UpdateSource(this.GetOwnAttribute(AttributeNames.Src));
            }
        }

        internal void UpdateSource(String? value)
        {
            // resolved against the document, which the attribute isn't
            if (value != null && this.IsInLoadingTree() && this.HyperReference(value) is { } url)
            {
                this.Process(_request, url);
            }
        }

        #endregion
    }
}
