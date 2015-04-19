namespace AngleSharp.Parser.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
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

        XmlTokenizer tokenizer;
        Boolean started;
        XmlDocument doc;
        List<Element> open;
        XmlTreeMode insert;
        Task task;
        Boolean standalone;
		Object sync;

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
        internal XmlParser(XmlDocument document)
        {
            tokenizer = new XmlTokenizer(document.Source, document.Options.Events);
			sync = new Object();
            started = false;
            doc = document;
            standalone = false;
            open = new List<Element>();
            insert = XmlTreeMode.Initial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current node.
        /// </summary>
        internal Node CurrentNode
        {
            get { return open.Count > 0 ? (Node)open[open.Count - 1] : (Node)doc; }
        }

        /// <summary>
        /// Gets the (maybe intermediate) result of the parsing process.
        /// </summary>
        public IXmlDocument Result
        {
            get
            {
                Parse();
                return doc;
            }
        }

        /// <summary>
        /// Gets if the XML is standalone.
        /// </summary>
        public Boolean Standalone
        {
            get { return standalone; }
        }

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return task != null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
			lock (sync)
			{
				if (!started)
				{
					started = true;
					task = Task.Run(() => Kernel());
				}
				else if (task == null)
					throw new InvalidOperationException("The parser has already run synchronously.");

				return task;
			}
        }

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public void Parse()
        {
			var run = false;

			lock (sync)
			{
				if (!started)
				{
					started = true;
					run = true;
				}
			}

			if (run)
				Kernel();
        }

        #endregion

        #region States

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Consume(XmlToken token)
        {
            switch (insert)
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
                standalone = tok.Standalone;

                if (!tok.IsEncodingMissing)
                    SetEncoding(tok.Encoding);

                if (!CheckVersion(tok.Version))
                    throw XmlError(XmlParseError.XmlDeclarationVersionUnsupported);
            }
            else
            {
                insert = XmlTreeMode.Prolog;
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
                case XmlTokenType.DOCTYPE:
                {
                    var tok = (XmlDoctypeToken)token;
                    doc.AppendChild(new DocumentType(doc, tok.Name)
                    {
                        SystemIdentifier = tok.SystemIdentifier,
                        PublicIdentifier = tok.PublicIdentifier
                    });
                    insert = XmlTreeMode.Misc;

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
                    var com = doc.CreateComment(tok.Data);
                    CurrentNode.AppendChild(com);
                    break;
                }
                case XmlTokenType.ProcessingInstruction:
                {
                    var tok = (XmlPIToken)token;
                    var pi = doc.CreateProcessingInstruction(tok.Target, tok.Content);
                    CurrentNode.AppendChild(pi);
                    break;
                }
                case XmlTokenType.StartTag:
                {
                    insert = XmlTreeMode.Body;
                    InBody(token);
                    break;
                }
                default:
                {
                    if (!token.IsIgnorable)
                        throw XmlError(XmlParseError.XmlMissingRoot);

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
                    var tag = new XmlElement(doc, tok.Name);
                    CurrentNode.AppendChild(tag);

                    if (!tok.IsSelfClosing)
                        open.Add(tag);
                    else if(open.Count == 0)
                        insert = XmlTreeMode.After;

                    for (int i = 0; i < tok.Attributes.Count; i++)
                        tag.SetAttribute(tok.Attributes[i].Key, tok.Attributes[i].Value.Trim());

                    break;
                }
                case XmlTokenType.EndTag:
                {
                    var tok = (XmlTagToken)token;

                    if (CurrentNode.NodeName != tok.Name)
                        throw XmlError(XmlParseError.TagClosingMismatch);

                    open.RemoveAt(open.Count - 1);

                    if (open.Count == 0)
                        insert = XmlTreeMode.After;

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
                    var str = tokenizer.GetEntity(tok);
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
                case XmlTokenType.EOF:
                {
                    throw XmlError(XmlParseError.EOF);
                }
                case XmlTokenType.DOCTYPE:
                {
                    throw XmlError(XmlParseError.XmlDoctypeAfterContent);
                }
                case XmlTokenType.Declaration:
                {
                    throw XmlError(XmlParseError.XmlDeclarationMisplaced);
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
                case XmlTokenType.EOF:
                {
                    break;
                }
                default:
                {
                    if (!token.IsIgnorable)
                        throw XmlError(XmlParseError.XmlMissingRoot);

                    break;
                }
            }
        }

        #endregion

        #region Helpers

        static Exception XmlError(XmlParseError code)
        {
            //TODO
            return new InvalidOperationException();
        }

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
            XmlToken token;

            do
            {
                token = tokenizer.Get();
                Consume(token);
            }
            while (token.Type != XmlTokenType.EOF);
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
                        doc.Source.CurrentEncoding = encoding;
                    }
                    catch (NotSupportedException)
                    {
                        //Restart();
                    }
                }
            }
        }

        #endregion
    }
}
