namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    sealed class HtmlBaseElement : HtmlElement, IHtmlBaseElement
    {
        #region Fields

        private Url? _frozenBaseUrl;

        #endregion

        #region ctor

        public HtmlBaseElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String? Href
        {
            get
            {
                var value = this.GetOwnAttribute(AttributeNames.Href) ?? String.Empty;
                var url = new Url(Owner.DocumentUrl, value);
                return url.IsInvalid ? value : url.Href;
            }
            set => this.SetOwnAttribute(AttributeNames.Href, value);
        }

        public String? Target
        {
            get => this.GetOwnAttribute(AttributeNames.Target);
            set => this.SetOwnAttribute(AttributeNames.Target, value);
        }

        #endregion

        #region Internal Methods

        internal Boolean HasHref => this.GetOwnAttribute(AttributeNames.Href) is not null;

        internal Url? FrozenBaseUrl => _frozenBaseUrl;

        internal override void SetupElement()
        {
            base.SetupElement();

            if (!IsInTemplateElement())
            {
                Owner.RefreshBaseUrl(this);
            }
        }

        internal void UpdateUrl(String? url)
        {
            Owner.RefreshBaseUrl(this);
        }

        internal void FreezeBaseUrl()
        {
            var fallback = Owner.DocumentUrl;
            var url = new Url(fallback, this.GetOwnAttribute(AttributeNames.Href) ?? String.Empty);
            _frozenBaseUrl = url.IsInvalid || url.Scheme.Is(ProtocolNames.Data) || url.Scheme.Is(ProtocolNames.JavaScript) ? new Url(fallback) : url;
        }

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = (HtmlBaseElement)base.Clone(owner, deep);
            node._frozenBaseUrl = _frozenBaseUrl is null ? null : new Url(_frozenBaseUrl);
            return node;
        }

        private Boolean IsInTemplateElement()
        {
            var ancestor = Parent;

            while (ancestor is not null)
            {
                if (ancestor is HtmlTemplateElement)
                {
                    return true;
                }

                ancestor = ancestor.Parent;
            }

            return false;
        }

        #endregion
    }
}
