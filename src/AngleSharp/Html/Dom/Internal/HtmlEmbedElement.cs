namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement
    {
        #region Fields

        private readonly ObjectRequestProcessor _request;

        #endregion

        #region ctor

        public HtmlEmbedElement(Document owner, String prefix = null)
            : base(owner, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _request = new ObjectRequestProcessor(owner.Context);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload => _request?.Download;

        public String Source
        {
            get => this.GetOwnAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public String DisplayWidth
        {
            get => this.GetOwnAttribute(AttributeNames.Width);
            set => this.SetOwnAttribute(AttributeNames.Width, value);
        }

        public String DisplayHeight
        {
            get => this.GetOwnAttribute(AttributeNames.Height);
            set => this.SetOwnAttribute(AttributeNames.Height, value);
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSource(this.GetOwnAttribute(AttributeNames.Src));
        }

        internal void UpdateSource(String value)
        {
            if (value != null)
            {
                var url = new Url(Source);
                this.Process(_request, url);
            }
        }

        #endregion
    }
}
