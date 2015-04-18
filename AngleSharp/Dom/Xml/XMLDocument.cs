namespace AngleSharp.Dom.Xml
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Network;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Xml;
        }

        internal XmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        public override IElement DocumentElement
        {
            get { return this.FindChild<IElement>(); }
        }

        public override String Title
        {
            get { return String.Empty; }
            set { }
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new XmlDocument(Context, new TextSource(Source.Text));
            CopyProperties(this, node, deep);
            CopyDocumentProperties(this, node, deep);
            return node;
        }
    }
}
