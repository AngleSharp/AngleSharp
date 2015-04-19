namespace AngleSharp.Dom.Xml
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Xml;

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

        /// <summary>
        /// Loads the document in the provided context from the given response.
        /// </summary>
        /// <param name="context">The browsing context.</param>
        /// <param name="response">The response to consider.</param>
        /// <param name="cancelToken">Token for cancellation.</param>
        /// <returns>The task that builds the document.</returns>
        internal async static Task<XmlDocument> LoadAsync(IBrowsingContext context, IResponse response, CancellationToken cancelToken)
        {
            var contentType = response.Headers.GetOrDefault(HeaderNames.ContentType, MimeTypes.Xml);
            var source = new TextSource(response.Content, context.Configuration.DefaultEncoding());
            var document = new XmlDocument(context, source);
            var parser = new XmlParser(document);
            document.ContentType = contentType;
            document.Referrer = response.Headers.GetOrDefault(HeaderNames.Referer, String.Empty);
            document.DocumentUri = response.Address.Href;
            document.Cookie = response.Headers.GetOrDefault(HeaderNames.SetCookie, String.Empty);
            document.ReadyState = DocumentReadyState.Loading;
            await parser.ParseAsync(cancelToken).ConfigureAwait(false);
            return document;
        }
    }
}
