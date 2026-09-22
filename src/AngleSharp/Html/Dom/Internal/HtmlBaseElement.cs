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

        internal override void SetupElement()
        {
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
