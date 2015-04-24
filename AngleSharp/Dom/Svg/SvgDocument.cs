namespace AngleSharp.Dom.Svg
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Xml;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Svg;
        }

        internal SvgDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        public override IElement DocumentElement
        {
            get { return RootElement; }
        }

        public ISvgSvgElement RootElement
        {
            get { return this.FindChild<ISvgSvgElement>(); }
        }

        public override String Title
        {
            get
            {
                var title = RootElement.FindChild<ISvgTitleElement>();

                if (title != null)
                    return title.TextContent.CollapseAndStrip();

                return String.Empty;
            }
            set
            {
                var title = RootElement.FindChild<ISvgTitleElement>();

                if (title == null)
                {
                    title = new SvgTitleElement(this);
                    RootElement.AppendChild(title);
                }

                title.TextContent = value;
            }
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new SvgDocument(Context, new TextSource(Source.Text));
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
        internal async static Task<SvgDocument> LoadAsync(IBrowsingContext context, IResponse response, CancellationToken cancelToken)
        {
            var contentType = response.Headers.GetOrDefault(HeaderNames.ContentType, MimeTypes.Svg);
            var source = new TextSource(response.Content, context.Configuration.DefaultEncoding());
            var document = new SvgDocument(context, source);
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
