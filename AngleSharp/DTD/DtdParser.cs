using AngleSharp.Events;
using System;
using System.Diagnostics;
using System.Text;
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
        Boolean _isinternal;
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
            get { return _isinternal; }
            set 
            { 
                _isinternal = value;
                tokenizer.End = value ? Specification.SBC : Specification.EOF;
            }
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
            var start = _src.InsertionPoint;
            DtdToken token;

            do
            {
                token = tokenizer.Get();
                Consume(token);
            }
            while (token.Type != DtdTokenType.EOF);

            var end = _src.InsertionPoint;
            var sb = new StringBuilder();
            _src.InsertionPoint = start;
            var c = _src.Current;

            for (int i = start; i < end; i++)
            {
                sb.Append(c);
                c = _src.Next;                
            }

            _result.Text = sb.ToString();
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
                    _result.AddProcessingInstruction(((DtdPIToken)token).ToElement());
                    break;
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
