namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Xml;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Svg;
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
        /// <param name="options">The creation options to consider.</param>
        /// <param name="cancelToken">Token for cancellation.</param>
        /// <returns>The task that builds the document.</returns>
        internal async static Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
        {
            var document = new SvgDocument(context, options.Source);
            var evt = new HtmlParseStartEvent(document);
            var events = context.Configuration.Events;
            var parser = new XmlDomBuilder(document);
            document.Setup(options);
            context.NavigateTo(document);

            if (events != null)
                events.Publish(evt);

            await parser.ParseAsync(default(XmlParserOptions), cancelToken).ConfigureAwait(false);
            evt.FireEnd();
            return document;
        }
    }
}
