namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    sealed class HtmlBaseElement : HtmlElement, IHtmlBaseElement
    {
        #region ctor

        static HtmlBaseElement()
        {
            RegisterCallback<HtmlBaseElement>(AttributeNames.Href, (element, value) => element.UpdateUrl(value));
        }

        public HtmlBaseElement(Document owner, String prefix = null)
            : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String Href
        {
            get { return this.GetOwnAttribute(AttributeNames.Href); }
            set { this.SetOwnAttribute(AttributeNames.Href, value); }
        }

        public String Target
        {
            get { return this.GetOwnAttribute(AttributeNames.Target); }
            set { this.SetOwnAttribute(AttributeNames.Target, value); }
        }

        #endregion

        #region Methods

        void UpdateUrl(String url)
        {
            Owner.BaseUrl = new Url(Owner.DocumentUrl, url ?? String.Empty);
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

        #endregion
    }
}
