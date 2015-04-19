namespace AngleSharp.Dom.Html
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Html;

    /// <summary>
    /// Represents a document node that contains only HTML nodes.
    /// </summary>
    sealed class HtmlDocument : Document, IHtmlDocument
    {
        internal HtmlDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Html;
        }

        internal HtmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }
        
        public override IElement DocumentElement
        {
            get { return this.FindChild<HtmlHtmlElement>(); }
        }

        public override String Title
        {
            get
            {
                var title = DocumentElement.FindDescendant<IHtmlTitleElement>();

                if (title != null)
                    return title.TextContent.CollapseAndStrip();

                return String.Empty;
            }
            set
            {
                var title = DocumentElement.FindDescendant<IHtmlTitleElement>();

                if (title == null)
                {
                    var head = Head;

                    if (head == null)
                        return;

                    title = new HtmlTitleElement(this);
                    head.AppendChild(title);
                }

                title.TextContent = value;
            }
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new HtmlDocument(Context, new TextSource(Source.Text));
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
        internal async static Task<HtmlDocument> LoadAsync(IBrowsingContext context, IResponse response, CancellationToken cancelToken)
        {
            var config = context.Configuration;
            var events = config.Events;
            var source = new TextSource(response.Content, config.DefaultEncoding());
            var document = new HtmlDocument(context, source);
            var parser = new HtmlParser(document);
            var startEvent = new HtmlParseStartEvent(parser);
            document.ContentType = response.Headers.GetOrDefault(HeaderNames.ContentType, MimeTypes.Html);
            document.Referrer = response.Headers.GetOrDefault(HeaderNames.Referer, String.Empty);
            document.DocumentUri = response.Address.Href;
            document.Cookie = response.Headers.GetOrDefault(HeaderNames.SetCookie, String.Empty);
            document.ReadyState = DocumentReadyState.Loading;

            if (events != null)
                events.Publish(startEvent);

            var result = await parser.ParseAsync(cancelToken).ConfigureAwait(false);
            startEvent.SetResult(result);
            return document;
        }
    }
}
