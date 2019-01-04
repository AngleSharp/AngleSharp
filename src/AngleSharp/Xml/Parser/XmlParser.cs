namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using AngleSharp.Xml.Dom;
    using AngleSharp.Xml.Dom.Events;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates an instance of the XML parser front-end.
    /// </summary>
    public class XmlParser : EventTarget, IXmlParser
    {
        #region Fields

        private readonly XmlParserOptions _options;
        private readonly IBrowsingContext _context;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the XML parser is starting.
        /// </summary>
        public event DomEventHandler Parsing
        {
            add { AddEventListener(EventNames.Parsing, value); }
            remove { RemoveEventListener(EventNames.Parsing, value); }
        }

        /// <summary>
        /// Fired when the XML parser is finished.
        /// </summary>
        public event DomEventHandler Parsed
        {
            add { AddEventListener(EventNames.Parsed, value); }
            remove { RemoveEventListener(EventNames.Parsed, value); }
        }

        /// <summary>
        /// Fired when a XML parse error is encountered.
        /// </summary>
        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new parser with the default options and context.
        /// </summary>
        public XmlParser()
            : this(default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public XmlParser(XmlParserOptions options)
            : this(options, default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom configuration.
        /// </summary>
        /// <param name="context">The context to use.</param>
        internal XmlParser(IBrowsingContext context)
            : this(default(XmlParserOptions), context)
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
            _context = context ?? BrowsingContext.NewFrom<IXmlParser>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public XmlParserOptions Options => _options;

        /// <summary>
        /// Gets the specified context.
        /// </summary>
        public IBrowsingContext Context => _context;

        #endregion

        #region Methods

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public IXmlDocument ParseDocument(String source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        public IXmlDocument ParseDocument(Stream source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        public Task<IXmlDocument> ParseDocumentAsync(String source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            return ParseAsync(document, cancel);
        }

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        public Task<IXmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            return ParseAsync(document, cancel);
        }

        async Task<IDocument> IXmlParser.ParseDocumentAsync(IDocument document, CancellationToken cancel)
        {
            var parser = CreateBuilder((Document)document);
            InvokeEventListener(new XmlParseEvent(document, completed: false));
            await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
            InvokeEventListener(new XmlParseEvent(document, completed: true));
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
            var textSource = new TextSource(source, _context.GetDefaultEncoding());
            return CreateDocument(textSource);
        }

        private XmlDocument CreateDocument(TextSource textSource)
        {
            var document = new XmlDocument(_context, textSource);
            return document;
        }

        private XmlDomBuilder CreateBuilder(Document document)
        {
            var parser = new XmlDomBuilder(document);
            return parser;
        }


        private IXmlDocument Parse(XmlDocument document)
        {
            var parser = CreateBuilder(document);
            InvokeEventListener(new XmlParseEvent(document, completed: false));
            parser.Parse(_options);
            InvokeEventListener(new XmlParseEvent(document, completed: true));
            return document;
        }

        private async Task<IXmlDocument> ParseAsync(XmlDocument document, CancellationToken cancel)
        {
            var parser = CreateBuilder(document);
            InvokeEventListener(new XmlParseEvent(document, completed: false));
            await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
            InvokeEventListener(new XmlParseEvent(document, completed: true));
            return document;
        }

        #endregion
    }
}
