namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    sealed class HTMLBaseElement : HTMLElement, IHtmlBaseElement
    {
        #region ctor

        /// <summary>
        /// Creates a HTML base element.
        /// </summary>
        public HTMLBaseElement(Document owner)
            : base(owner, Tags.Base, NodeFlags.Special | NodeFlags.SelfClosing)
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
            get { return GetAttribute(AttributeNames.Href); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the default target frame.
        /// </summary>
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
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
