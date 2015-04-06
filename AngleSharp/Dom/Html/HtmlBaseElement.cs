namespace AngleSharp.Dom.Html
{
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
            : base(owner, Tags.Base, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            RegisterAttributeObserver(AttributeNames.Href, UpdateUrl);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        public String Href
        {
            get { return GetOwnAttribute(AttributeNames.Href); }
            set { SetOwnAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the default target frame.
        /// </summary>
        public String Target
        {
            get { return GetOwnAttribute(AttributeNames.Target); }
            set { SetOwnAttribute(AttributeNames.Target, value); }
        }

        #endregion

        #region Methods

        void UpdateUrl(String url)
        {
            Owner.BaseUrl = new Url(Owner.DocumentUrl, url ?? String.Empty);
        }

        #endregion
    }
}
