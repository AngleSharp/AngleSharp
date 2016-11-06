namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Extensions;
    using AngleSharp.Text;
    using AngleSharp.Xml.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of the XML parser front-end.
    /// </summary>
    public class XmlParser
    {
        #region Fields

        private readonly XmlParserOptions _options;
        private readonly IBrowsingContext _context;

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new parser with the default options and configuration.
        /// </summary>
        public XmlParser()
            : this(Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public XmlParser(XmlParserOptions options)
            : this(options, Configuration.Default)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        public XmlParser(IConfiguration configuration)
            : this(default(XmlParserOptions), configuration)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and configuration.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="configuration">The configuration to use.</param>
        public XmlParser(XmlParserOptions options, IConfiguration configuration)
            : this(options, BrowsingContext.New(configuration))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and the given context.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="context">The context to use.</param>
        public XmlParser(XmlParserOptions options, IBrowsingContext context)
        {
            _options = options;
            _context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public XmlParserOptions Options
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
        public IXmlDocument Parse(String source)
        {
            var document = CreateDocument(source);
            var parser = new XmlDomBuilder(document);
            parser.Parse(_options);
            return document;
        }

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        public IXmlDocument Parse(Stream source)
        {
            var document = CreateDocument(source);
            var parser = new XmlDomBuilder(document);
            parser.Parse(_options);
            return document;
        }

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        public Task<IXmlDocument> ParseAsync(String source)
        {
            return ParseAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        public Task<IXmlDocument> ParseAsync(Stream source)
        {
            return ParseAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        public async Task<IXmlDocument> ParseAsync(String source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var parser = new XmlDomBuilder(document);
            await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
            return document;
        }

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        public async Task<IXmlDocument> ParseAsync(Stream source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var parser = new XmlDomBuilder(document);
            await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
            return document;
        }

        #endregion

        #region Helpers

        private XmlDocument CreateDocument(String source)
        {
            var textSource = new TextSource(source);
            return CreateDocument(textSource);
        }

        private XmlDocument CreateDocument(Stream source)
        {
            var textSource = new TextSource(source, _context.Configuration.DefaultEncoding());
            return CreateDocument(textSource);
        }

        private XmlDocument CreateDocument(TextSource textSource)
        {
            var document = new XmlDocument(_context, textSource);
            return document;
        }

        #endregion
    }
}
