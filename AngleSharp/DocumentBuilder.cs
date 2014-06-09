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
    using GlobalConfig = AngleSharp.Configuration;

    /// <summary>
    /// A handy helper to construct various kinds of documents
    /// from a given source code, URL or stream.
    /// </summary>
    public sealed class DocumentBuilder
    {
        #region Fields

        readonly IConfiguration configuration;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new builder and optionally sets the configuration.
        /// </summary>
        /// <param name="defaultConfiguration">The configuration to use. If this
        /// is not specified, then the default configuration will be used.</param>
        public DocumentBuilder(IConfiguration defaultConfiguration = null)
        {
            configuration = defaultConfiguration ?? GlobalConfig.Default;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration to use.
        /// </summary>
        public IConfiguration Configuration
        {
            get { return configuration; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds a new HTMLDocument with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public HTMLDocument FromHtml(String sourceCode)
        {
            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
        }

        /// <summary>
        /// Builds a new HTMLDocument with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed HTML document.</returns>
        public HTMLDocument FromHtml(Uri url)
        {
            return HtmlAsync(url).Result;
        }

        /// <summary>
        /// Builds a new HTMLDocument by asynchronously requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The task that constructs the HTML document.</returns>
        public Task<HTMLDocument> FromHtmlAsync(Uri url)
        {
            return HtmlAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Builds a new HTMLDocument by asynchronously requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="cancel">The cancellation token for cancelling the asynchronous request.</param>
        /// <returns>The task that constructs the HTML document.</returns>
        public async Task<HTMLDocument> FromHtmlAsync(Uri url, CancellationToken cancel)
        {
            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given source code string.
        /// </summary>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public CSSStyleSheet FromCss(String sourceCode)
        {
            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new CSSStyleSheet { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet with the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public CSSStyleSheet FromCss(Uri url)
        {
            return CssAsync(url).Result;
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public Task<CSSStyleSheet> FromCssAsync(Uri url)
        {
            return CssAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given URL.
        /// </summary>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <param name="cancel">The cancellation token for cancelling the asynchronous request.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public async Task<CSSStyleSheet> FromCssAsync(Uri url, CancellationToken cancel)
        {
            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new CSSStyleSheet { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates a new parser with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="document">The document to fill.</param>
        /// <param name="configuration">Options to use for the document generation.</param>
        static HtmlParser Construct(SourceManager source, HTMLDocument document, IConfiguration configuration)
        {
            var parser = new HtmlParser(document, source);
            parser.ParseError += (s, e) => configuration.ReportError(e);
            return parser;
        }

        /// <summary>
        /// Creates a new parser with the specified source.
        /// </summary>
        /// <param name="source">The code manager.</param>
        /// <param name="sheet">The document to fill.</param>
        /// <param name="configuration">Options to use for the document generation.</param>
        static CssParser Construct(SourceManager source, CSSStyleSheet sheet, IConfiguration configuration)
        {
            var parser = new CssParser(sheet, source);
            parser.ParseError += (s, e) => configuration.ReportError(e);
            return parser;
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
                configuration = GlobalConfig.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
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
                configuration = GlobalConfig.Default;

            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration, DocumentUri = url.OriginalString };
            var parser = Construct(source, doc, configuration);
            await parser.ParseAsync();
            return parser.Result;
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
                configuration = GlobalConfig.Default;

            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };
            var parser = Construct(source, doc, configuration);
            return parser.Result;
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
                configuration = GlobalConfig.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var doc = new HTMLDocument { Options = configuration };

            //Disable scripting for HTML fragments (security reasons)
            configuration.IsScripting = false;

            var parser = Construct(source, doc, configuration);

            if (context != null)
            {
                if (context.Owner != null && context.Owner.QuirksMode != QuirksMode.Off)
                    doc.QuirksMode = context.Owner.QuirksMode;

                parser.SwitchToFragment(context);
                return parser.Result.DocumentElement.ChildNodes;
            }

            return parser.Result.ChildNodes;
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
                configuration = GlobalConfig.Default;

            var source = new SourceManager(sourceCode, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Options = configuration };
            var parser = Construct(source, sheet, configuration);
            return parser.Result;
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
                configuration = GlobalConfig.Default;

            var stream = await configuration.LoadAsync(url, cancel, force: true);
            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Href = url.OriginalString, Options = configuration };
            var parser = Construct(source, sheet, configuration);
            await parser.ParseAsync();
            return parser.Result;
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
                configuration = GlobalConfig.Default;

            var source = new SourceManager(stream, configuration.DefaultEncoding());
            var sheet = new CSSStyleSheet { Options = configuration };
            var parser = Construct(source, sheet, configuration);
            return parser.Result;
        }

        #endregion
    }
}
