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

        /// <summary>
        /// Loads the document in the provided context from the given response.
        /// </summary>
        /// <param name="context">The browsing context.</param>
        /// <param name="response">The response to consider.</param>
        /// <param name="cancelToken">Token for cancellation.</param>
        /// <returns>The task that builds the document.</returns>
        internal async static Task<HtmlDocument> LoadAsync(IBrowsingContext context, IResponse response, CancellationToken cancelToken)
        {
            var contentType = response.Headers.GetOrDefault(HeaderNames.ContentType, MimeTypes.Html);
            var referrer = response.Headers.GetOrDefault(HeaderNames.Referer, String.Empty);
            var cookie = response.Headers.GetOrDefault(HeaderNames.SetCookie, String.Empty);
            var url = response.Address.Href;
            var config = context.Configuration;
            var events = config.Events;
            var source = new TextSource(response.Content, config.DefaultEncoding());
            var document = new HtmlDocument(context, source);
            document.ContentType = MimeTypes.Html;
            document.Referrer = referrer;
            document.DocumentUri = url;
            document.Cookie = cookie;
            document.Open(contentType);
            document.ReadyState = DocumentReadyState.Loading;
            var parser = new HtmlParser(document);
            var startEvent = new HtmlParseStartEvent(parser);

            if (events != null)
                events.Publish(startEvent);

            var result = await parser.ParseAsync(cancelToken).ConfigureAwait(false);
            startEvent.SetResult(result);
            return document;
        }
    }
}
