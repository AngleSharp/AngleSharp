namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    sealed class HtmlBaseElement : HtmlElement, IHtmlBaseElement
    {
        #region ctor

        public HtmlBaseElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            owner.RegisterBaseElement();
        }

        #endregion

        #region Properties

        public String? Href
        {
            get
            {
                var value = this.GetOwnAttribute(AttributeNames.Href) ?? String.Empty;
                var url = new Url(Owner.FallbackBaseUrl, value);
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

        protected override void NodeIsAdopted(Document oldDocument)
        {
            base.NodeIsAdopted(oldDocument);
            Owner.RegisterBaseElement();
        }

        internal Boolean IsInertTemplateBase { get; private set; }

        internal void ActivateInDom() => IsInertTemplateBase = false;

        internal override void SetupElement()
        {
            // The parser stages template contents as children before moving
            // them into the inert fragment. A later DOM insertion activates a
            // moved base; ordinary DOM children of a template remain ordinary.
            for (var parent = Parent; parent is not null; parent = parent.Parent)
            {
                if (parent is IHtmlTemplateElement)
                {
                    IsInertTemplateBase = true;
                    break;
                }
            }

            base.SetupElement();

            var href = this.GetOwnAttribute(AttributeNames.Href);

            if (href != null)
            {
                UpdateUrl(href);
            }
        }

        internal void UpdateUrl(String url)
        {
            Owner.RefreshBaseUrl(this);
        }

        #endregion
    }
}
