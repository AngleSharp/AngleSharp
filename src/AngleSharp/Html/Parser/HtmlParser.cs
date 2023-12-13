using System.Runtime.CompilerServices;

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
        /// Parses the stream and returns the result.
        /// </summary>
        public IHtmlDocument ParseDocument(IReadOnlyTextSource source)
        {
            var document = CreateDocument(source);
            return Parse(document);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public IHtmlDocument ParseDocument(IReadOnlyTextSource source, HtmlParserOptions options)
        {
            var document = CreateDocument(source);
            return Parse(document);
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

        private HtmlDocument CreateDocument(IReadOnlyTextSource textSource)
        {
            var document = new HtmlDocument(_context, textSource);
            return document;
        }

        private HtmlDomBuilder CreateBuilder(HtmlDocument document, String? stopAt)
        {
            var parser = new HtmlDomBuilder(document, stopAt);

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
            await parser.ParseAsync(_options, cancel).ConfigureAwait(false);
            InvokeHtmlParseEvent(document, completed: true);
            return document;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InvokeHtmlParseEvent(HtmlDocument document, bool completed)
        {
            if (HasEventListeners)
            {
                InvokeEventListener(new HtmlParseEvent(document, completed));
            }
        }

        private INodeList ParseFragment(HtmlDocument document, IElement contextElement)
        {
            var parser = new HtmlDomBuilder(document);

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
