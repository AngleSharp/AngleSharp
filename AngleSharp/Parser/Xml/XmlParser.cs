namespace AngleSharp.Parser.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Xml;
    using AngleSharp.Extensions;

    /// <summary>
    /// For more details: See the W3C Recommendation
    /// http://www.w3.org/TR/REC-xml/
    /// and a little bit about XML parser (XHTML context)
    /// http://www.w3.org/html/wg/drafts/html/master/the-xhtml-syntax.html#xml-parser.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class XmlParser
    {
        #region Fields

        readonly XmlTokenizer _tokenizer;
        readonly Document _document;
        readonly List<Element> _openElements;
        readonly Object _syncGuard;

        Boolean _started;
        XmlTreeMode _currentMode;
        Task<IDocument> _parsing;
        Boolean _standalone;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the XML parser with an new document
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        /// <param name="configuration">
        /// [Optional] The configuration to use.
        /// </param>
        public XmlParser(String source, IConfiguration configuration = null)
            : this(new XmlDocument(BrowsingContext.New(configuration), new TextSource(source)))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with an new document
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        /// <param name="configuration">
        /// [Optional] The configuration to use.
        /// </param>
        public XmlParser(Stream stream, IConfiguration configuration = null)
            : this(new XmlDocument(BrowsingContext.New(configuration), new TextSource(stream, configuration.DefaultEncoding())))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with the specified document.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        internal XmlParser(Document document)
        {
            _tokenizer = new XmlTokenizer(document.Source, document.Options.Events);
			_syncGuard = new Object();
            _started = false;
            _document = document;
            _standalone = false;
            _openElements = new List<Element>();
            _currentMode = XmlTreeMode.Initial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current node.
        /// </summary>
        internal Node CurrentNode
        {
            get
            {
                if (_openElements.Count > 0)
                    return _openElements[_openElements.Count - 1];
                
                return _document;
            }
        }

        /// <summary>
        /// Gets the (maybe intermediate) result of the parsing process.
        /// </summary>
        public IDocument Result
        {
            get { return _document; }
        }

        /// <summary>
        /// Gets if the XML is standalone.
        /// </summary>
        public Boolean IsStandalone
        {
            get { return _standalone; }
        }

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return _parsing != null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<IDocument> ParseAsync()
        {
            return ParseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>
        /// The task which could be awaited or continued differently.
        /// </returns>
        public Task<IDocument> ParseAsync(CancellationToken cancelToken)
        {
            lock (_syncGuard)
            {
                if (!_started)
                {
                    _started = true;
                    _parsing = KernelAsync(cancelToken);
                }
            }

            return _parsing;
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public IDocument Parse()
        {
            lock (_syncGuard)
            {
                if (!_started)
                {
                    _started = true;
                    Kernel();
                }
            }

            return _document;
        }

        #endregion

        #region States

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Consume(XmlToken token)
        {
            switch (_currentMode)
            {
                case XmlTreeMode.Initial:
                    Initial(token);
                    break;
                case XmlTreeMode.Prolog:
                    BeforeDoctype(token);
                    break;
                case XmlTreeMode.Misc:
                    InMisc(token);
                    break;
                case XmlTreeMode.Body:
                    InBody(token);
                    break;
                case XmlTreeMode.After:
                    AfterBody(token);
                    break;
            }
        }

        /// <summary>
        /// The initial state. Expects an XML declaration.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        void Initial(XmlToken token)
        {
            if (token.Type == XmlTokenType.Declaration)
            {
                var tok = (XmlDeclarationToken)token;
                _standalone = tok.Standalone;

                if (!tok.IsEncodingMissing)
                    SetEncoding(tok.Encoding);

                if (!CheckVersion(tok.Version))
                    throw XmlParseError.XmlDeclarationVersionUnsupported.At(token.Position);
            }
            else
            {
                _currentMode = XmlTreeMode.Prolog;
                BeforeDoctype(token);
            }
        }

        /// <summary>
        /// Before any doctype - still in the prolog. No declaration
        /// allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        void BeforeDoctype(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.Doctype:
                {
                    var tok = (XmlDoctypeToken)token;
                    _document.AppendChild(new DocumentType(_document, tok.Name)
                    {
                        SystemIdentifier = tok.SystemIdentifier,
                        PublicIdentifier = tok.PublicIdentifier
                    });
                    _currentMode = XmlTreeMode.Misc;

                    break;
                }
                default:
                {
                    InMisc(token);
                    break;
                }
            }
        }

        /// <summary>
        /// In the body state - no doctypes and declarations allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        void InMisc(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.Comment:
                {
                    var tok = (XmlCommentToken)token;
                    var com = _document.CreateComment(tok.Data);
                    CurrentNode.AppendChild(com);
                    break;
                }
                case XmlTokenType.ProcessingInstruction:
                {
                    var tok = (XmlPIToken)token;
                    var pi = _document.CreateProcessingInstruction(tok.Target, tok.Content);
                    CurrentNode.AppendChild(pi);
                    break;
                }
                case XmlTokenType.StartTag:
                {
                    _currentMode = XmlTreeMode.Body;
                    InBody(token);
                    break;
                }
                default:
                {
                    if (!token.IsIgnorable)
                        throw XmlParseError.XmlMissingRoot.At(token.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// In the body state - no doctypes and declarations allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        void InBody(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.StartTag:
                {
                    var tok = (XmlTagToken)token;
                    var tag = new XmlElement(_document, tok.Name);
                    CurrentNode.AppendChild(tag);

                    if (!tok.IsSelfClosing)
                        _openElements.Add(tag);
                    else if(_openElements.Count == 0)
                        _currentMode = XmlTreeMode.After;

                    for (int i = 0; i < tok.Attributes.Count; i++)
                        tag.SetAttribute(tok.Attributes[i].Key, tok.Attributes[i].Value.Trim());

                    break;
                }
                case XmlTokenType.EndTag:
                {
                    var tok = (XmlTagToken)token;

                    if (CurrentNode.NodeName != tok.Name)
                        throw XmlParseError.TagClosingMismatch.At(token.Position);

                    _openElements.RemoveAt(_openElements.Count - 1);

                    if (_openElements.Count == 0)
                        _currentMode = XmlTreeMode.After;

                    break;
                }
                case XmlTokenType.ProcessingInstruction:
                case XmlTokenType.Comment:
                {
                    InMisc(token);
                    break;
                }
                case XmlTokenType.Entity:
                {
                    var tok = (XmlEntityToken)token;
                    var str = tok.GetEntity();
                    CurrentNode.AppendText(str);
                    break;
                }
                case XmlTokenType.CData:
                {
                    var tok = (XmlCDataToken)token;
                    CurrentNode.AppendText(tok.Data);
                    break;
                }
                case XmlTokenType.Character:
                {
                    var tok = (XmlCharacterToken)token;
                    CurrentNode.AppendText(tok.Data.ToString());
                    break;
                }
                case XmlTokenType.EndOfFile:
                {
                    throw XmlParseError.EOF.At(token.Position);
                }
                case XmlTokenType.Doctype:
                {
                    throw XmlParseError.XmlDoctypeAfterContent.At(token.Position);
                }
                case XmlTokenType.Declaration:
                {
                    throw XmlParseError.XmlDeclarationMisplaced.At(token.Position);
                }
            }
        }

        /// <summary>
        /// After the body state - nothing except Comment PI S allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        void AfterBody(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.ProcessingInstruction:
                case XmlTokenType.Comment:
                {
                    InMisc(token);
                    break;
                }
                case XmlTokenType.EndOfFile:
                {
                    break;
                }
                default:
                {
                    if (!token.IsIgnorable)
                        throw XmlParseError.XmlMissingRoot.At(token.Position);

                    break;
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks the given version number.
        /// </summary>
        /// <param name="ver"></param>
        /// <returns></returns>
        Boolean CheckVersion(String ver)
        {
            var t = 0.0;

            if (Double.TryParse(ver, NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                return t >= 1.0 && t < 2.0;

            return false;
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        void Kernel()
        {
            var token = default(XmlToken);

            do
            {
                token = _tokenizer.Get();
                Consume(token);
            }
            while (token.Type != XmlTokenType.EndOfFile);
        }

        /// <summary>
        /// The kernel that is pulling the tokens into the parser.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to consider.</param>
        /// <returns>The task to await.</returns>
        async Task<IDocument> KernelAsync(CancellationToken cancelToken)
        {
            var source = _document.Source;
            var token = default(XmlToken);

            do
            {
                if (source.Length - source.Index < 1024)
                    await source.Prefetch(8192, cancelToken).ConfigureAwait(false);

                token = _tokenizer.Get();
                Consume(token);
            }
            while (token.Type != XmlTokenType.EndOfFile);

            return _document;
        }

        /// <summary>
        /// Sets the document's encoding to the given one.
        /// </summary>
        /// <param name="charSet">The encoding to use.</param>
        void SetEncoding(String charSet)
        {
            if (TextEncoding.IsSupported(charSet))
            {
                var encoding = TextEncoding.Resolve(charSet);

                if (encoding != null)
                {
                    try
                    {
                        _document.Source.CurrentEncoding = encoding;
                    }
                    catch (NotSupportedException)
                    {
                        _currentMode = XmlTreeMode.Initial;
                        _document.ReplaceAll(null, true);
                        _openElements.Clear();
                    }
                }
            }
        }

        #endregion
    }
}
