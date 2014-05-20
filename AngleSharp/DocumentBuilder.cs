namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using AngleSharp.Parser;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A handy helper to construct various kinds of documents
    /// from a given source code, URL or stream.
    /// </summary>
    public sealed class DocumentBuilder
    {
        #region Fields

        IParser parser;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="document">The document to fill.</param>
        /// <param name="configuration">Options to use for the document generation.</param>
        DocumentBuilder(SourceManager source, HTMLDocument document, IConfiguration configuration)
        {
            parser = new HtmlParser(document, source);
			parser.ParseError += (s, e) => configuration.ReportError(e);
        }

        /// <summary>
        /// Creates a new builder with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="sheet">The document to fill.</param>
        /// <param name="configuration">Options to use for the document generation.</param>
        DocumentBuilder(SourceManager source, CSSStyleSheet sheet, IConfiguration configuration)
        {
            sheet.Options = configuration;
            parser = new CssParser(sheet, source);
            parser.ParseError += (s, e) => configuration.ReportError(e);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of an HTML parsing.
        /// </summary>
        public HTMLDocument HtmlResult
        {
            get { return ((HtmlParser)parser).Result; }
        }

        /// <summary>
        /// Gets the result of a CSS parsing.
        /// </summary>
        public CSSStyleSheet CssResult
        {
            get { return ((CssParser)parser).Result; }
        }

        #endregion

        #region HTML Construction

        /// <summary>
        /// Builds a new HTMLDocument with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(String sourceCode, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
            var db = new DocumentBuilder(source, doc, configuration);
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Uri url, IConfiguration configuration = null)
        {
            return HtmlAsync(url, configuration).Result;
        }

        /// <summary>
        /// Builds a new HTMLDocument by asynchronously requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The task that constructs the HTML document.</returns>
        public static Task<HTMLDocument> HtmlAsync(Uri url, IConfiguration configuration = null)
        {
            return HtmlAsync(url, CancellationToken.None, configuration);
        }

        /// <summary>
        /// Builds a new HTMLDocument by asynchronously requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="cancel">The cancellation token for cancelling the asynchronous request.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The task that constructs the HTML document.</returns>
        public static async Task<HTMLDocument> HtmlAsync(Uri url, CancellationToken cancel, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration, DocumentUri = url.OriginalString };
            var db = new DocumentBuilder(source, doc, configuration);
            await db.parser.ParseAsync();
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given (network) stream.
        /// </summary>
        /// <param name="stream">The stream of chars to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed HTML document.</returns>
        public static HTMLDocument Html(Stream stream, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
			var db = new DocumentBuilder(source, doc, configuration);
            return db.HtmlResult;
        }

        /// <summary>
        /// Builds a list of nodes according with 8.4 Parsing HTML fragments.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="context">[Optional] The context node to use.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>A list of parsed nodes.</returns>
        public static NodeList HtmlFragment(String sourceCode, Node context = null, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };

            //Disable scripting for HTML fragments (security reasons)
            configuration.IsScripting = false;

            var db = new DocumentBuilder(source, doc, configuration);

            if (context != null)
            {
                if (context.OwnerDocument != null && context.OwnerDocument.QuirksMode != QuirksMode.Off)
                    doc.QuirksMode = context.OwnerDocument.QuirksMode;

                var parser = (HtmlParser)db.parser;
                parser.SwitchToFragment(context);
                return parser.Result.DocumentElement.ChildNodes;
            }

            return db.HtmlResult.ChildNodes;
        }

        #endregion

        #region CSS Construction

        /// <summary>
        /// Builds a new CSSStyleSheet with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(String sourceCode, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Options = configuration };
			var db = new DocumentBuilder(source, sheet, configuration);
            return db.CssResult;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(Uri url, IConfiguration configuration = null)
        {
            return CssAsync(url, configuration).Result;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static Task<CSSStyleSheet> CssAsync(Uri url, IConfiguration configuration = null)
        {
            return CssAsync(url, CancellationToken.None, configuration);
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="cancel">The cancellation token for cancelling the asynchronous request.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static async Task<CSSStyleSheet> CssAsync(Uri url, CancellationToken cancel, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Href = url.OriginalString, Options = configuration };
            var db = new DocumentBuilder(source, sheet, configuration);
            await db.parser.ParseAsync();
            return db.CssResult;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given network stream.
        /// </summary>
        /// <param name="stream">The stream of chars to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static CSSStyleSheet Css(Stream stream, IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Options = configuration };
			var db = new DocumentBuilder(source, sheet, configuration);
            return db.CssResult;
        }

        #endregion
    }
}
