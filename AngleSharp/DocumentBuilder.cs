namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A handy helper to construct various kinds of documents from a given
    /// source code, URL or stream.
    /// </summary>
    public static class DocumentBuilder
    {
        #region HTML Construction

        /// <summary>
        /// Builds a new empty HTML Document with the provided configuration.
        /// </summary>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The constructed HTML document.</returns>
        public static IDocument Html(IConfiguration configuration, String url = null)
        {
            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            return browsingContext.OpenNew(url);
        }

        /// <summary>
        /// Builds a new HTML Document with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The constructed HTML document.</returns>
        public static IDocument Html(String sourceCode, IConfiguration configuration = null, String url = null)
        {
            if (sourceCode == null)
                throw new ArgumentNullException("sourceCode");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var stream = new TextSource(sourceCode);
            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            var doc = new Document(browsingContext, stream) { DocumentUri = url };
            browsingContext.NavigateTo(doc);
            return ParserFor(doc).Parse();
        }

        /// <summary>
        /// Builds a new HTML Document with the given URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <returns>The constructed HTML document.</returns>
        public static IDocument Html(Uri url, IConfiguration configuration = null)
        {
            return HtmlAsync(url, configuration).Result;
        }

        /// <summary>
        /// Builds a new HTML Document by asynchronously requesting the given
        /// URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <returns>The task that constructs the HTML document.</returns>
        public static Task<IDocument> HtmlAsync(Uri url, IConfiguration configuration = null)
        {
            return HtmlAsync(url, CancellationToken.None, configuration);
        }

        /// <summary>
        /// Builds a new HTML Document by asynchronously requesting the given
        /// URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="cancel">
        /// The cancellation token for cancelling the asynchronous request.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <returns>The task that constructs the HTML document.</returns>
        public static Task<IDocument> HtmlAsync(Uri url, CancellationToken cancel, IConfiguration configuration = null)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            return browsingContext.OpenAsync(new Url(url), cancel);
        }

        /// <summary>
        /// Builds a new HTML Document with the given (network) stream.
        /// </summary>
        /// <param name="content">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The constructed HTML document.</returns>
        public static IDocument Html(Stream content, IConfiguration configuration = null, String url = null)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var stream = new TextSource(content, configuration.DefaultEncoding());
            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            var doc = new Document(browsingContext, stream) { DocumentUri = url };
            browsingContext.NavigateTo(doc);
            return ParserFor(doc).Parse();
        }

        /// <summary>
        /// Builds a new HTML Document asynchronously with the given (network)
        /// stream.
        /// </summary>
        /// <param name="content">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The task to construct the HTML document.</returns>
        public static Task<IDocument> HtmlAsync(Stream content, IConfiguration configuration = null, String url = null)
        {
            return HtmlAsync(content, CancellationToken.None, configuration, url);
        }

        /// <summary>
        /// Builds a new HTML Document asynchronously with the given (network)
        /// stream.
        /// </summary>
        /// <param name="content">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="cancel">
        /// The cancellation token for cancelling the asynchronous request.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The task to construct the HTML document.</returns>
        public static async Task<IDocument> HtmlAsync(Stream content, CancellationToken cancel, IConfiguration configuration = null, String url = null)
        {
            if (content == null)
                throw new ArgumentException("content");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var stream = new TextSource(content, configuration.DefaultEncoding());
            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            var doc = new Document(browsingContext, stream) { DocumentUri = url };
            browsingContext.NavigateTo(doc);
            return await ParserFor(doc).ParseAsync(cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="context">
        /// The optional context element to use.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <returns>A list of parsed nodes.</returns>
        public static INodeList HtmlFragment(String sourceCode, IElement context = null, IConfiguration configuration = null)
        {
            if (sourceCode == null)
                throw new ArgumentException("sourceCode");

            if (configuration == null)
                configuration = new Configuration();

            var browsingContext = new SimpleBrowsingContext(configuration, Sandboxes.None);
            var stream = new TextSource(sourceCode);
            var doc = new Document(browsingContext, stream);
            var node = context as Element;
            var parser = ParserFor(doc);

            if (node == null)
                return parser.Parse().ChildNodes;

            var owner = node.Owner;

            if (owner != null && owner.QuirksMode != QuirksMode.Off)
                doc.QuirksMode = owner.QuirksMode;

            return parser.ParseFragment(node).DocumentElement.ChildNodes;
        }

        #endregion

        #region CSS Construction

        /// <summary>
        /// Builds a new empty CSS StyleSheet with the provided configuration.
        /// </summary>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <param name="url">The optional base URL of the sheet.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static ICssStyleSheet Css(IConfiguration configuration = null, String url = null)
        {
            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            return new CssStyleSheet(configuration) { Href = url };
        }

        /// <summary>
        /// Builds a new CSS StyleSheet with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <param name="url">The optional base URL of the sheet.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static ICssStyleSheet Css(String sourceCode, IConfiguration configuration = null, String url = null)
        {
            if (sourceCode == null)
                throw new ArgumentException("sourceCode");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var stream = new TextSource(sourceCode);
            var sheet = new CssStyleSheet(configuration, stream) { Href = url };
            return ParserFor(sheet).Parse();
        }

        /// <summary>
        /// Builds a new CSS StyleSheet with the given URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static ICssStyleSheet Css(Uri url, IConfiguration configuration = null)
        {
            return CssAsync(url, configuration).Result;
        }

        /// <summary>
        /// Builds a new CSS StyleSheet asynchronously by requesting the given
        /// URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> CssAsync(Uri url, IConfiguration configuration = null)
        {
            return CssAsync(url, CancellationToken.None, configuration);
        }

        /// <summary>
        /// Builds a new CSS StyleSheet asynchronously by requesting the given
        /// URL.
        /// </summary>
        /// <param name="url">
        /// The URL which points to the address containing the source code.
        /// </param>
        /// <param name="cancel">
        /// The cancellation token for cancelling the asynchronous request.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static async Task<ICssStyleSheet> CssAsync(Uri url, CancellationToken cancel, IConfiguration configuration = null)
        {
            if (url == null)
                throw new ArgumentException("url");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var requester = configuration.GetRequester(url.Scheme) ?? new DefaultRequester();
            
            using (var response = await requester.LoadAsync(new Url(url), cancel).ConfigureAwait(false))
            {
                var source = new TextSource(response.Content, configuration.DefaultEncoding());
                var sheet = new CssStyleSheet(configuration, source) { Href = url.OriginalString };
                return await ParserFor(sheet).ParseAsync(cancel).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Builds a new CSS StyleSheet with the given (network) stream.
        /// </summary>
        /// <param name="stream">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <param name="url">The optional base URL of the sheet.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static ICssStyleSheet Css(Stream stream, IConfiguration configuration = null, String url = null)
        {
            if (stream == null)
                throw new ArgumentException("stream");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var source = new TextSource(stream, configuration.DefaultEncoding());
            var sheet = new CssStyleSheet(configuration, source) { Href = url };
            return ParserFor(sheet).Parse();
        }

        /// <summary>
        /// Builds a new CSS StyleSheet asynchronously by requesting the given
        /// (network) stream.
        /// </summary>
        /// <param name="stream">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <param name="url">The optional base URL of the sheet.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> CssAsync(Stream stream, IConfiguration configuration = null, String url = null)
        {
            return CssAsync(stream, CancellationToken.None, configuration, url);
        }

        /// <summary>
        /// Builds a new CSS StyleSheet asynchronously by requesting the given
        /// (network) stream.
        /// </summary>
        /// <param name="stream">
        /// The stream of chars to use as source code.
        /// </param>
        /// <param name="cancel">
        /// The cancellation token for cancelling the asynchronous request.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the sheet generation.
        /// </param>
        /// <param name="url">The optional base URL of the sheet.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static async Task<ICssStyleSheet> CssAsync(Stream stream, CancellationToken cancel, IConfiguration configuration = null, String url = null)
        {
            if (stream == null)
                throw new ArgumentException("stream");

            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            var source = new TextSource(stream, configuration.DefaultEncoding());
            var sheet = new CssStyleSheet(configuration, source) { Href = url };
            return await ParserFor(sheet).ParseAsync(cancel).ConfigureAwait(false);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates a new parser with the specified source.
        /// </summary>
        /// <param name="document">The document to fill.</param>
        static HtmlParser ParserFor(Document document)
        {
            return new HtmlParser(document);
        }

        /// <summary>
        /// Creates a new parser with the specified source.
        /// </summary>
        /// <param name="sheet">The document to fill.</param>
        static CssParser ParserFor(CssStyleSheet sheet)
        {
            return new CssParser(sheet);
        }

        #endregion
    }
}
