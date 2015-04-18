namespace AngleSharp
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;

    /// <summary>
    /// A handy helper to construct various kinds of documents from a given
    /// source code, URL or stream.
    /// </summary>
    [Obsolete("Implement IBrowsingContext or start using BrowsingContext.")]
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
        public static IDocument Html(IConfiguration configuration = null, String url = null)
        {
            var context = BrowsingContext.New(configuration);
            return context.OpenNewAsync(url).Result;
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
            
            var context = BrowsingContext.New(configuration);
            return context.OpenAsync(m => m.Content(sourceCode).Address(url)).Result;
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
            
            var context = BrowsingContext.New(configuration);
            return context.OpenAsync(Url.Convert(url), cancel);
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
            
            var context = BrowsingContext.New(configuration);
            return context.OpenAsync(m => m.Content(content).Address(url)).Result;
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
        public static Task<IDocument> HtmlAsync(Stream content, CancellationToken cancel, IConfiguration configuration = null, String url = null)
        {
            if (content == null)
                throw new ArgumentException("content");
            
            var context = BrowsingContext.New(configuration);
            return context.OpenAsync(m => m.Content(content).Address(url));
        }

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="contextElement">
        /// The optional context element to use.
        /// </param>
        /// <param name="configuration">
        /// Optional custom options to use for the document generation.
        /// </param>
        /// <returns>A list of parsed nodes.</returns>
        public static INodeList HtmlFragment(String sourceCode, IElement contextElement = null, IConfiguration configuration = null)
        {
            if (sourceCode == null)
                throw new ArgumentException("sourceCode");

            var context = BrowsingContext.New(configuration);
            var stream = new TextSource(sourceCode);
            var doc = new HtmlDocument(context, stream);
            var parser = new HtmlParser(doc);

            if (contextElement == null)
                return parser.Parse().ChildNodes;

            var owner = contextElement.Owner as Document;

            if (owner != null && owner.QuirksMode != QuirksMode.Off)
                doc.QuirksMode = owner.QuirksMode;

            return parser.ParseFragment(contextElement).DocumentElement.ChildNodes;
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
            var parser = new CssParser(sheet);
            return parser.Parse();
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
            var parser = new CssParser(sheet);
            return parser.Parse();
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
            var parser = new CssParser(sheet);
            return await parser.ParseAsync(cancel).ConfigureAwait(false);
        }

        #endregion
    }
}
