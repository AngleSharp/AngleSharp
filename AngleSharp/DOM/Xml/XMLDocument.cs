namespace AngleSharp.DOM.Xml
{
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument(ITextSource source)
            : base(source)
        {
            ContentType = MimeTypes.Xml;
        }

        internal XmlDocument(String source)
            : this(new TextSource(source))
        {
        }

        internal XmlDocument()
            : this(String.Empty)
        {
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new XmlDocument(Source.Text);
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
