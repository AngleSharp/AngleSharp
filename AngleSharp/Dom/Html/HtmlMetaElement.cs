namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
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
        public HtmlMetaElement(Document owner, String prefix = null)
            : base(owner, Tags.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        public String Content
        {
            get { return this.GetOwnAttribute(AttributeNames.Content); }
            set { this.SetOwnAttribute(AttributeNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets the charset attribute.
        /// </summary>
        public String Charset
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        public String HttpEquivalent
        {
            get { return this.GetOwnAttribute(AttributeNames.HttpEquiv); }
            set { this.SetOwnAttribute(AttributeNames.HttpEquiv, value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        public String Scheme
        {
            get { return this.GetOwnAttribute(AttributeNames.Scheme); }
            set { this.SetOwnAttribute(AttributeNames.Scheme, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
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

            if (charset != null)
            {
                charset = charset.Trim();

                if (TextEncoding.IsSupported(charset))
                    return TextEncoding.Resolve(charset);
            }

            var equiv = HttpEquivalent;
            var shouldParseContent = equiv != null && equiv.Equals(HeaderNames.ContentType, StringComparison.OrdinalIgnoreCase);
            return shouldParseContent ? TextEncoding.Parse(Content ?? String.Empty) : null;
        }

        #endregion
    }
}
