using AngleSharp.Events;
using AngleSharp.Xml;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The parser for the Document Type Definition.
    /// Can be used internally and externally.
    /// </summary>
    [DebuggerStepThrough]
    sealed class DtdParser : IParser
    {
        #region Members

        DtdTokenizer tokenizer;
        DtdContainer _result;
        SourceManager _src;

        #endregion

        #region Events

        /// <summary>
        /// The event is fired when an error occured.
        /// This will actually be never fired. Errors in DTD
        /// will result in exceptions!
        /// </summary>
        public event ParseErrorEventHandler ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Dtd parser for the given (DTD) content.
        /// </summary>
        /// <param name="content">The DTD to parse.</param>
        public DtdParser(String content)
            : this(new DtdContainer(), new SourceManager(content))
        {
        }

        /// <summary>
        /// Creates a new Dtd parser that uses the given container
        /// as the result for parsing the given source.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <param name="source">The source to parse.</param>
        public DtdParser(DtdContainer container, SourceManager source)
        {
            tokenizer = new DtdTokenizer(source);
            _result = container;
            _src = source;

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the DTD is internal.
        /// </summary>
        public Boolean IsInternal
        {
            get { return tokenizer.IsExternal; }
            set { tokenizer.IsExternal = !value; }
        }

        /// <summary>
        /// Gets the async state - has to be false.
        /// </summary>
        public Boolean IsAsync
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the DtdContainer, i.e. the result of the computation.
        /// </summary>
        public DtdContainer Result
        {
            get { return _result; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method will not work for the DTD parser. The DTD has to be
        /// parsed synchronously.
        /// </summary>
        /// <returns>An exception.</returns>
        public Task ParseAsync()
        {
            throw new NotImplementedException("Parsing the DTD has no async implementation.");
        }

        /// <summary>
        /// Parses the set source.
        /// </summary>
        public void Parse()
        {
            DtdToken token;

            do
            {
                token = tokenizer.Get();
                Consume(token);
            }
            while (token.Type != DtdTokenType.EOF);

            _result.Text = tokenizer.Content;
        }

        #endregion

        #region Consuming

        void Consume(DtdToken token)
        {
            switch (token.Type)
            {
                case DtdTokenType.Attribute:
                    _result.AddAttribute(((DtdAttributeToken)token).ToElement());
                    break;

                case DtdTokenType.Comment:
                    _result.AddComment(((DtdCommentToken)token).ToElement());
                    break;

                case DtdTokenType.Element:
                    _result.AddElement(((DtdElementToken)token).ToElement());
                    break;

                case DtdTokenType.Entity:
                    AddEntity((DtdEntityToken)token);
                    break;

                case DtdTokenType.Notation:
                    _result.AddNotation(((DtdNotationToken)token).ToElement());
                    break;

                case DtdTokenType.ProcessingInstruction:
                    var pi = (DtdPIToken)token;

                    if (String.Compare(pi.Target, Tags.XML, StringComparison.OrdinalIgnoreCase) == 0)
                        HandleXmlDeclaration(pi);
                    else
                        _result.AddProcessingInstruction(pi.ToElement());

                    break;
            }
        }

        void HandleXmlDeclaration(DtdPIToken pi)
        {
            if (!tokenizer.IsExternal)
                throw Errors.GetException(ErrorCode.XmlInvalidPI);

            var xml = String.Format("<test {0} />", pi.Content);
            var tok = new XmlTokenizer(new SourceManager(xml));
            var dec = tok.Get() as XmlTagToken;
            var encoding = dec.GetAttribute("encoding");

            if (!String.IsNullOrEmpty(encoding))
                SetEncoding(encoding);
        }

        /// <summary>
        /// Sets the document's encoding to the given one.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        void SetEncoding(String encoding)
        {
            //TODO
            return;

            if (DocumentEncoding.IsSupported(encoding))
            {
                var enc = DocumentEncoding.Resolve(encoding);

                if (enc != null)
                    _src.Encoding = enc;
            }
        }

        void AddEntity(DtdEntityToken token)
        {
            if (token.IsParameter)
                tokenizer.AddParameter(token.ToElement());
            else
                _result.AddEntity(token.ToElement());
        }

        #endregion
    }
}
