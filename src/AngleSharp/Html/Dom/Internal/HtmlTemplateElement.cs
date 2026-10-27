namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using Construction;

    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HtmlTemplateElement : HtmlElement, IHtmlTemplateElement, IConstructableTemplateElement
    {
        #region Fields

        private readonly DocumentFragment _content;

        #endregion

        #region ctor

        public HtmlTemplateElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
            _content = new DocumentFragment(owner, this);
        }

        #endregion

        #region Properties

        public IDocumentFragment Content => _content;

        // The parser keeps children on the element until it closes the template.
        internal Boolean IsParsingContent { get; private set; } = true;

        #endregion

        #region Methods

        public override Node Clone(Document owner, Boolean deep)
        {
            var template = new HtmlTemplateElement(owner);
            CloneElement(template, owner, deep);

            // The contents are cloned only with the children, as the template's cloning steps say
            if (deep)
            {
                CloneChildren(_content, template._content, owner);
            }

            return template;
        }

        /// <summary>
        /// Turns a declarative shadow root (a template with a valid shadowrootmode attribute,
        /// e.g. &lt;template shadowrootmode="open"&gt;) into the shadow root of its parent, which
        /// takes the template's children, while the template itself leaves the tree.
        /// </summary>
        /// <returns>True if the shadow root has been attached, otherwise false.</returns>
        internal Boolean TryAttachDeclarativeShadowRoot()
        {
            var mode = GetAttribute(AttributeNames.ShadowRootMode);
            var host = Parent as Element;

            if (host is null || host.ShadowRoot is not null || TagNames.AllNoShadowRoot.Contains(host.LocalName) ||
                !(mode.Isi("open") || mode.Isi("closed")))
            {
                return false;
            }

            var shadowRoot = (Node)host.AttachShadow(mode.Isi("open") ? ShadowRootMode.Open : ShadowRootMode.Closed);

            while (HasChildNodes)
            {
                var node = ChildNodes[0];
                RemoveNode(0, node);
                shadowRoot.AddNode(node);
            }

            host.RemoveNode(host.ChildNodes.Index(this), this);
            return true;
        }

        public void PopulateFragment()
        {
            while (HasChildNodes)
            {
                var node = ChildNodes[0];
                RemoveNode(0, node);
                _content.AddNode(node);
            }

            Owner.RefreshBaseUrlForTreeChange(_content);
        }

        #endregion

        #region Helpers

        internal override void SetupElement()
        {
            base.SetupElement();
            IsParsingContent = false;
        }

        protected override void ReplacedAll() => PopulateFragment();

        protected override void NodeIsAdopted(Document oldDocument) => _content.Owner = Owner;

        #endregion
    }
}
