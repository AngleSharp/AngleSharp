namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using Services;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of the HTML parser front-end.
    /// </summary>
    public class HtmlParser
    {
        #region Fields

        readonly HtmlParserOptions _options;
        readonly IBrowsingContext _context;

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new parser with the default options and configuration.
        /// </summary>
        public HtmlParser()
            : this(Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public HtmlParser(HtmlParserOptions options)
            : this(options, Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        public HtmlParser(IConfiguration configuration)
            : this(new HtmlParserOptions { IsScripting = configuration.IsScripting() }, configuration)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and configuration.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="configuration">The configuration to use.</param>
        public HtmlParser(HtmlParserOptions options, IConfiguration configuration)
            : this(options, BrowsingContext.New(configuration))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and the given context.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="context">The context to use.</param>
        public HtmlParser(HtmlParserOptions options, IBrowsingContext context)
        {
            _options = options;
            _context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public HtmlParserOptions Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Gets the specified context.
        /// </summary>
        public IBrowsingContext Context
        {
            get { return _context; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public IHtmlDocument Parse(String source)
        {
            var document = CreateDocument(source);
            var parser = new HtmlDomBuilder(document);
            return parser.Parse(_options);
        }

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public INodeList ParseFragment(String source, IElement context)
        {
            var document = CreateDocument(source);
            var parser = new HtmlDomBuilder(document);

            if (context != null)
            {
                var element = context as Element;

                if (element == null)
                {
                    var configuration = document.Options;
                    var factory = configuration.GetFactory<IElementFactory<HtmlElement>>();
                    element = factory.Create(document, context.LocalName, context.Prefix);
                }

                return parser.ParseFragment(_options, element).DocumentElement.ChildNodes;
            }

            return parser.Parse(_options).ChildNodes;
        }

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        public IHtmlDocument Parse(Stream source)
        {
            var document = CreateDocument(source);
            var parser = new HtmlDomBuilder(document);
            return parser.Parse(_options);
        }

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        public Task<IHtmlDocument> ParseAsync(String source)
        {
            return ParseAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        public Task<IHtmlDocument> ParseAsync(Stream source)
        {
            return ParseAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        public async Task<IHtmlDocument> ParseAsync(String source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var parser = new HtmlDomBuilder(document);
            return await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        public async Task<IHtmlDocument> ParseAsync(Stream source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var parser = new HtmlDomBuilder(document);
            return await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
        }

        #endregion

        #region Helpers

        HtmlDocument CreateDocument(String source)
        {
            var textSource = new TextSource(source);
            return CreateDocument(textSource);
        }

        HtmlDocument CreateDocument(Stream source)
        {
            var textSource = new TextSource(source, _context.Configuration.DefaultEncoding());
            return CreateDocument(textSource);
        }

        HtmlDocument CreateDocument(TextSource textSource)
        {
            var document = new HtmlDocument(_context, textSource);
            return document;
        }

        #endregion
    }
}
