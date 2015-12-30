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

        /// <summary>
        /// Creates a HTML base element.
        /// </summary>
        public HtmlBaseElement(Document owner, String prefix = null)
            : base(owner, TagNames.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        public String Href
        {
            get { return this.GetOwnAttribute(AttributeNames.Href); }
            set { this.SetOwnAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the default target frame.
        /// </summary>
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
            RegisterAttributeObserver(AttributeNames.Href, UpdateUrl);

            if (href != null)
            {
                UpdateUrl(href);
            }
        }

        #endregion
    }
}
