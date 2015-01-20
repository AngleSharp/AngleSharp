namespace AngleSharp.DOM.Xml
{
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument(IBrowsingContext context, ITextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Xml;
        }

        internal XmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new XmlDocument(Context, new TextSource(Source.Text));
            CopyProperties(this, node, deep);
            CopyDocumentProperties(this, node, deep);
            return node;
        }

        public void LoadXml(String url)
        {
            Location.Href = url;
        }
    }
}
