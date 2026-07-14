namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for the IHtmlParser instances.
    /// </summary>
    public static class HtmlParserExtensions
    {
        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        public static Task<IHtmlDocument> ParseDocumentAsync(this IHtmlParser parser, String source) => parser.ParseDocumentAsync(source, CancellationToken.None);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        public static Task<IHtmlDocument> ParseDocumentAsync(this IHtmlParser parser, Stream source) => parser.ParseDocumentAsync(source, CancellationToken.None);

        /// <summary>
        /// Parses the stream asynchronously using the selected source mode.
        /// </summary>
        public static Task<IHtmlDocument> ParseDocumentAsync(
            this HtmlParser parser,
            Stream source,
            HtmlStreamSourceMode sourceMode) => parser.ParseDocumentAsync(source, CancellationToken.None, sourceMode);

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        public static Task<IHtmlHeadElement?> ParseHeadAsync(this IHtmlParser parser, String source) => parser.ParseHeadAsync(source, CancellationToken.None);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        public static Task<IHtmlHeadElement?> ParseHeadAsync(this IHtmlParser parser, Stream source) => parser.ParseHeadAsync(source, CancellationToken.None);

        /// <summary>
        /// Populates the given document asynchronously.
        /// </summary>
        public static Task<IDocument> ParseDocumentAsync(this IHtmlParser parser, IDocument document) => parser.ParseDocumentAsync(document, CancellationToken.None);
    }
}
