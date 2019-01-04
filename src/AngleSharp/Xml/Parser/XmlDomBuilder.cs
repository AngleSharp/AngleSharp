namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using AngleSharp.Xml.Dom;
    using AngleSharp.Xml.Parser.Tokens;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the Tree construction as specified in the official W3C
    /// specification for XML:
    /// http://www.w3.org/TR/REC-xml/
    /// </summary>
    sealed class XmlDomBuilder
    {
        #region Fields

        private readonly XmlTokenizer _tokenizer;
        private readonly Document _document;
        private readonly List<Element> _openElements;

        private XmlParserOptions _options;
        private XmlTreeMode _currentMode;
        private Boolean _standalone;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the XML parser.
        /// </summary>
        /// <param name="document">The document instance to be filled.</param>
        internal XmlDomBuilder(Document document)
        {
            _tokenizer = new XmlTokenizer(document.Source, document.Entities);
            _document = document;
            _standalone = false;
            _openElements = new List<Element>();
            _currentMode = XmlTreeMode.Initial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the document was detected to be standalone.
        /// </summary>
        public Boolean IsStandalone => _standalone;

        /// <summary>
        /// Gets the current node.
        /// </summary>
        public Node CurrentNode
        {
            get
            {
                if (_openElements.Count > 0)
                {
                    return _openElements[_openElements.Count - 1];
                }
                
                return _document;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        public async Task<Document> ParseAsync(XmlParserOptions options, CancellationToken cancelToken)
        {
            var source = _document.Source;
            var token = default(XmlToken);
            _options = options;
            _tokenizer.IsSuppressingErrors = options.IsSuppressingErrors;

            do
            {
                if (source.Length - source.Index < 1024)
                {
                    await source.PrefetchAsync(8192, cancelToken).ConfigureAwait(false);
                }

                token = _tokenizer.Get();
                Consume(token);
            }
            while (token.Type != XmlTokenType.EndOfFile);

            return _document;
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        /// <param name="options">The options to use for parsing.</param>
        public Document Parse(XmlParserOptions options)
        {
            var token = default(XmlToken);
            _options = options;
            _tokenizer.IsSuppressingErrors = options.IsSuppressingErrors;

            do
            {
                token = _tokenizer.Get();
                Consume(token);
            }
            while (token.Type != XmlTokenType.EndOfFile);

            return _document;
        }

        #endregion

        #region States

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        private void Consume(XmlToken token)
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
        private void Initial(XmlToken token)
        {
            if (token.Type == XmlTokenType.Declaration)
            {
                var declarationToken = (XmlDeclarationToken)token;
                _standalone = declarationToken.Standalone;

                if (!declarationToken.IsEncodingMissing)
                {
                    SetEncoding(declarationToken.Encoding);
                }

                if (!CheckVersion(declarationToken.Version) && !_options.IsSuppressingErrors)
                {
                    throw XmlParseError.XmlDeclarationVersionUnsupported.At(token.Position);
                }
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
        private void BeforeDoctype(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.Doctype:
                {
                    var doctypeToken = (XmlDoctypeToken)token;
                    var doctypeNode = new DocumentType(_document, doctypeToken.Name)
                    {
                        SystemIdentifier = doctypeToken.SystemIdentifier,
                        PublicIdentifier = doctypeToken.PublicIdentifier
                    };
                    _document.AppendChild(doctypeNode);
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
        private void InMisc(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.Comment:
                {
                    var commenToken = (XmlCommentToken)token;
                    var commentNode = _document.CreateComment(commenToken.Data);
                    CurrentNode.AppendChild(commentNode);
                    break;
                }
                case XmlTokenType.ProcessingInstruction:
                {
                    var piToken = (XmlPIToken)token;
                    var piNode = _document.CreateProcessingInstruction(piToken.Target, piToken.Content);
                    CurrentNode.AppendChild(piNode);
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
                    if (!token.IsIgnorable && !_options.IsSuppressingErrors)
                    {
                        throw XmlParseError.XmlMissingRoot.At(token.Position);
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// In the body state - no doctypes and declarations allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        private void InBody(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.StartTag:
                {
                    var tagToken = (XmlTagToken)token;
                    var element = CreateElement(tagToken.Name);
                    CurrentNode.AppendChild(element);

                    for (var i = 0; i < tagToken.Attributes.Count; i++)
                    {
                        var attr = tagToken.Attributes[i];
                        var item = CreateAttribute(attr.Key, attr.Value.Trim());
                        element.Attributes.FastAddItem(item);
                    }

                    if (!tagToken.IsSelfClosing)
                    {
                        _openElements.Add(element);
                    }
                    else if (_openElements.Count == 0)
                    {
                        _currentMode = XmlTreeMode.After;
                    }

                    if (_options.OnCreated != null)
                    {
                        _options.OnCreated.Invoke(element, tagToken.Position);
                    }

                    break;
                }
                case XmlTokenType.EndTag:
                {
                    var tagToken = (XmlTagToken)token;

                    if (!CurrentNode.NodeName.Is(tagToken.Name))
                    {
                        if (_options.IsSuppressingErrors)
                        {
                            break;
                        }

                        throw XmlParseError.TagClosingMismatch.At(token.Position);
                    }

                    _openElements.RemoveAt(_openElements.Count - 1);

                    if (_openElements.Count == 0)
                    {
                        _currentMode = XmlTreeMode.After;
                    }

                    break;
                }
                case XmlTokenType.ProcessingInstruction:
                case XmlTokenType.Comment:
                {
                    InMisc(token);
                    break;
                }
                case XmlTokenType.CData:
                {
                    var cdataToken = (XmlCDataToken)token;
                    CurrentNode.AppendText(cdataToken.Data);
                    break;
                }
                case XmlTokenType.Character:
                {
                    var charToken = (XmlCharacterToken)token;
                    CurrentNode.AppendText(charToken.Data);
                    break;
                }
                case XmlTokenType.EndOfFile:
                {
                    if (_options.IsSuppressingErrors)
                    {
                        break;
                    }

                    throw XmlParseError.EOF.At(token.Position);
                }
                case XmlTokenType.Doctype:
                {
                    if (_options.IsSuppressingErrors)
                    {
                        break;
                    }

                    throw XmlParseError.XmlDoctypeAfterContent.At(token.Position);
                }
                case XmlTokenType.Declaration:
                {
                    if (_options.IsSuppressingErrors)
                    {
                        break;
                    }

                    throw XmlParseError.XmlDeclarationMisplaced.At(token.Position);
                }
            }
        }

        /// <summary>
        /// After the body state - nothing except Comment PI S allowed.
        /// </summary>
        /// <param name="token">The consumed token.</param>
        private void AfterBody(XmlToken token)
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
                    if (!token.IsIgnorable && !_options.IsSuppressingErrors)
                    {
                        throw XmlParseError.XmlMissingRoot.At(token.Position);
                    }

                    break;
                }
            }
        }

        #endregion

        #region Helpers

        private static Element CreateElement(Document document, String name, String prefix)
        {
            return new XmlElement(document, name, prefix);
        }

        private Element CreateElement(String name)
        {
            var prefix = default(String);
            var colon = name.IndexOf(Symbols.Colon);

            if (colon > 0 && colon < name.Length - 1)
            {
                prefix = name.Substring(0, colon);
                name = name.Substring(colon + 1);
            }

            return _document.CreateElementFrom(name, prefix);
        }

        private Attr CreateAttribute(String name, String value)
        {
            var colon = name.IndexOf(Symbols.Colon);

            if (colon > 0 && colon < name.Length - 1)
            {
                var prefix = name.Substring(0, colon);
                var ns = NamespaceNames.XmlNsUri;

                if (!prefix.Is(NamespaceNames.XmlNsPrefix))
                {
                    ns = CurrentNode.LookupNamespaceUri(prefix);
                }
                
                return new Attr(prefix, name.Substring(colon + 1), value, ns);
            }

            return new Attr(name, value);
        }

        private static Boolean CheckVersion(String ver)
        {
            var t = ver.ToDouble(0.0);
            return t >= 1.0 && t < 2.0;
        }

        private void SetEncoding(String charSet)
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
