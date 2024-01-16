namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Construction;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Creates an instance of the HTML parser front-end.
    /// </summary>
    public class HtmlParser : EventTarget, IHtmlParser
    {
        #region Fields

        private readonly HtmlParserOptions _options;
        private readonly IBrowsingContext _context;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the HTML parser is starting.
        /// </summary>
        public event DomEventHandler Parsing
        {
            add { AddEventListener(EventNames.Parsing, value); }
            remove { RemoveEventListener(EventNames.Parsing, value); }
        }

        /// <summary>
        /// Fired when the HTML parser is finished.
        /// </summary>
        public event DomEventHandler Parsed
        {
            add { AddEventListener(EventNames.Parsed, value); }
            remove { RemoveEventListener(EventNames.Parsed, value); }
        }

        /// <summary>
        /// Fired when a HTML parse error is encountered.
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
        public HtmlParser()
            : this(default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public HtmlParser(HtmlParserOptions options)
            : this(options, default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new parser with the custom context.
        /// </summary>
        /// <param name="context">The context to use.</param>
        internal HtmlParser(IBrowsingContext? context)
            : this(new HtmlParserOptions { IsScripting = context?.IsScripting() ?? false }, context)
        {
        }

        /// <summary>
        /// Creates a new parser with the custom options and the given context.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <param name="context">The context to use.</param>
        public HtmlParser(HtmlParserOptions options, IBrowsingContext? context)
        {
            _options = options;
            _context = context ?? BrowsingContext.NewFrom<IHtmlParser>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified options.
        /// </summary>
        public HtmlParserOptions Options => _options;

        #endregion

        #region Methods

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public IHtmlDocument ParseDocument(String source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        /// <summary>
        /// Parses the string and returns the head.
        /// </summary>
        public IHtmlHeadElement? ParseHead(String source)
        {
            var document = CreateDocument(source);
            return Parse(document, TagNames.Head).Head;
        }

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        public INodeList ParseFragment(Stream source, IElement contextElement)
        {
            var document = CreateDocument(source);
            return ParseFragment(document, contextElement);
        }

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        public INodeList ParseFragment(String source, IElement contextElement)
        {
            var document = CreateDocument(source);
            return ParseFragment(document, contextElement);
        }

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        public IHtmlDocument ParseDocument(Stream source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        /// <summary>
        /// Parses the read array of chars and returns the result.
        /// </summary>
        /// <param name="source">Array of chars to parse.</param>
        /// <param name="length">Length of array to parse. Or 0 if whole array should be parsed.</param>
        public IHtmlDocument ParseDocument(Char[] source, Int32 length = 0)
        {
            var document = CreateDocument(source, length);
            return Parse(document);
        }

        /// <summary>
        /// Parses the read only chunk of chars and returns the result.
        /// </summary>
        public IHtmlDocument ParseDocument(ReadOnlyMemory<Char> chars)
        {
            var document = CreateDocument(chars);
            return Parse(document);
        }

        /// <summary>
        /// Parses text source and returns result.
        /// </summary>
        public IHtmlDocument ParseDocument(TextSource source)
        {
            var document = CreateDocument(source);
            return Parse(document);
        }

        /// <summary>
        /// Parses the read only text source and returns the result.
        /// </summary>
        /// <param name="source">Read only text source.</param>
        /// <param name="middleware">Tokenizer middleware</param>
        /// <typeparam name="TDocument">Type of document to parse into, should implement <see cref="IConstructableDocument"/></typeparam>
        /// <typeparam name="TElement">Type of element to use for document construction, should implement <see cref="IConstructableElement"/></typeparam>
        /// <returns>Constructed TDocument instance</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no read-only construction factory is found for specified type arguments.
        /// </exception>
        /// <remarks>
        /// This method is intended for use with custom <see cref="IDomConstructionElementFactory{TDocument,TElement}"/> implementations.
        /// </remarks>
        public TDocument ParseDocument<TDocument, TElement>(TextSource source, TokenizerMiddleware? middleware = null)
             where TDocument : class, IConstructableDocument
             where TElement : class, IConstructableElement
        {
            var factory = _context.GetService<IDomConstructionElementFactory<TDocument, TElement>>()
                          ?? throw new InvalidOperationException("No read-only construction factory found.");

            TDocument document = factory.CreateDocument(source, _context);

            var builder = new HtmlDomBuilder<TDocument, TElement>(factory, document);

            if (HasEventListener(EventNames.Error))
            {
                builder.Error += (_, ev) => InvokeEventListener(ev);
            }

            builder.Parse(_options, middleware);

            return document;
        }

        /// <summary>
        /// Parses the stream and returns the head.
        /// </summary>
        public IHtmlHeadElement? ParseHead(Stream source)
        {
            var document = CreateDocument(source);
            return Parse(document, TagNames.Head).Head;
        }

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        public Task<IHtmlDocument> ParseDocumentAsync(String source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            return ParseAsync(document, cancel);
        }

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        public Task<IHtmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            return ParseAsync(document, cancel);
        }

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        public async Task<IHtmlHeadElement?> ParseHeadAsync(String source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var result = await ParseAsync(document, cancel, TagNames.Head).ConfigureAwait(false);
            return result.Head;
        }

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        public async Task<IHtmlHeadElement?> ParseHeadAsync(Stream source, CancellationToken cancel)
        {
            var document = CreateDocument(source);
            var result = await ParseAsync(document, cancel, TagNames.Head).ConfigureAwait(false);
            return result.Head;
        }

        async Task<IDocument> IHtmlParser.ParseDocumentAsync(IDocument document, CancellationToken cancel)
        {
            var doc = (HtmlDocument)document;
            return await ParseAsync(doc, cancel).ConfigureAwait(false);
        }

        #endregion

        #region Helpers

        private HtmlDocument CreateDocument(String source)
        {
            var textSource = new TextSource(source);
            return CreateDocument(textSource);
        }

        private HtmlDocument CreateDocument(Stream source)
        {
            var encoding = _context.GetDefaultEncoding();
            var textSource = new TextSource(source, encoding);
            return CreateDocument(textSource);
        }

        private HtmlDocument CreateDocument(ReadOnlyMemory<Char> chars)
        {
            var textSource = new TextSource(new ReadOnlyMemoryTextSource(chars));
            return CreateDocument(textSource);
        }

        private HtmlDocument CreateDocument(Char[] source, Int32 length = 0)
        {
            var textSource = new CharArrayTextSource(source, length == 0 ? source.Length : length);
            return CreateDocument(new TextSource(textSource));
        }

        private HtmlDocument CreateDocument(TextSource textSource)
        {
            var document = new HtmlDocument(_context, textSource);
            return document;
        }

        private HtmlDomBuilder CreateBuilder(HtmlDocument document, String? stopAt)
        {
            var options = new HtmlTokenizerOptions(_options);
            var factory = _context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
            var parser = new HtmlDomBuilder(factory, document, options, stopAt);
            if (HasEventListener(EventNames.Error))
            {
                parser.Error += (_, ev) => InvokeEventListener(ev);
            }
            return parser;
        }

        private IHtmlDocument Parse(HtmlDocument document, String? stopAt = null)
        {
            var parser = CreateBuilder(document, stopAt);
            InvokeHtmlParseEvent(document, completed: false);
            parser.Parse(_options);
            InvokeHtmlParseEvent(document, completed: true);
            return document;
        }

        private async Task<IHtmlDocument> ParseAsync(HtmlDocument document, CancellationToken cancel, String? stopAt = null)
        {
            var parser = CreateBuilder(document, stopAt);
            InvokeHtmlParseEvent(document, completed: false);
            await parser.ParseAsync(_options, null, cancel).ConfigureAwait(false);
            InvokeHtmlParseEvent(document, completed: true);
            return document;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InvokeHtmlParseEvent(HtmlDocument document, Boolean completed)
        {
            if (HasEventListeners)
            {
                InvokeEventListener(new HtmlParseEvent(document, completed));
            }
        }

        private INodeList ParseFragment(HtmlDocument document, IElement contextElement)
        {
            var parser = new HtmlDomBuilder(HtmlDomConstructionFactory.Instance, document);

            if (contextElement is Element element)
            {
                element = document.CreateElementFrom(element.LocalName, element.Prefix);
                var fragment = parser.ParseFragment(_options, element).DocumentElement;
                element.AppendNodes(fragment.ChildNodes.ToArray());
                return element.ChildNodes;
            }

            return parser.Parse(_options).ChildNodes;
        }

        #endregion
    }
}
