namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Text;

    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    sealed class HtmlMetaElement : HtmlElement, IHtmlMetaElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML meta element.
        /// </summary>
        public HtmlMetaElement(Document owner)
            : base(owner, Tags.Meta, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        public String Content
        {
            get { return GetAttribute(AttributeNames.Content); }
            set { SetAttribute(AttributeNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets the charset attribute.
        /// </summary>
        public String Charset
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        public String HttpEquivalent
        {
            get { return GetAttribute(AttributeNames.HttpEquiv); }
            set { SetAttribute(AttributeNames.HttpEquiv, value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        public String Scheme
        {
            get { return GetAttribute(AttributeNames.Scheme); }
            set { SetAttribute(AttributeNames.Scheme, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the specified charset, if there has been any specified.
        /// </summary>
        /// <returns>The encoding or null.</returns>
        public Encoding GetEncoding()
        {
            var charset = Charset;

            if (charset != null && DocumentEncoding.IsSupported(charset))
                return DocumentEncoding.Resolve(charset);

            var equiv = HttpEquivalent;

            if (equiv != null && equiv.Equals(HeaderNames.ContentType, StringComparison.OrdinalIgnoreCase))
                return DocumentEncoding.Parse(Content ?? String.Empty);

            return null;
        }

        #endregion
    }
}
