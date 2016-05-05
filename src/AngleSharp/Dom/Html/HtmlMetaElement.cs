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

        public HtmlMetaElement(Document owner, String prefix = null)
            : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String Content
        {
            get { return this.GetOwnAttribute(AttributeNames.Content); }
            set { this.SetOwnAttribute(AttributeNames.Content, value); }
        }

        public String Charset
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        public String HttpEquivalent
        {
            get { return this.GetOwnAttribute(AttributeNames.HttpEquiv); }
            set { this.SetOwnAttribute(AttributeNames.HttpEquiv, value); }
        }

        public String Scheme
        {
            get { return this.GetOwnAttribute(AttributeNames.Scheme); }
            set { this.SetOwnAttribute(AttributeNames.Scheme, value); }
        }

        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        #endregion

        #region Methods

        public Encoding GetEncoding()
        {
            var charset = Charset;

            if (charset != null)
            {
                charset = charset.Trim();

                if (TextEncoding.IsSupported(charset))
                {
                    return TextEncoding.Resolve(charset);
                }
            }

            var equiv = HttpEquivalent;
            var shouldParseContent = equiv != null && equiv.Isi(HeaderNames.ContentType);
            return shouldParseContent ? TextEncoding.Parse(Content ?? String.Empty) : null;
        }

        #endregion
    }
}
