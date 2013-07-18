using AngleSharp.Xml;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The parser for the Document Type Definition.
    /// Can be used internally and externally.
    /// </summary>
    sealed class DtdParser : IParser
    {
        #region Members

        DtdTokenizer tokenizer;
        Boolean _isinternal;
        DtdContainer _result;
        SourceManager _src;

        #endregion

        #region Events

        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;
        private string dtd;

        #endregion

        #region ctor

        public DtdParser(String content)
            : this(new DtdContainer(), new SourceManager(content))
        {
        }

        internal DtdParser(DtdContainer container, SourceManager source)
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

        public Boolean IsInternal
        {
            get { return _isinternal; }
            set 
            { 
                _isinternal = value;
                tokenizer.End = value ? Specification.SBC : Specification.EOF;
            }
        }

        public Boolean IsAsync
        {
            get { return false; }
        }

        public DtdContainer Result
        {
            get { return _result; }
        }

        #endregion

        #region Methods

        public Task ParseAsync()
        {
            throw new NotImplementedException("Parsing the DTD has no async implementation.");
        }

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
                    _result.AddEntity(((DtdEntityToken)token).ToElement());
                    break;
                case DtdTokenType.Notation:
                    _result.AddNotation(((DtdNotationToken)token).ToElement());
                    break;
                case DtdTokenType.ProcessingInstruction:
                    _result.AddProcessingInstruction(((DtdPIToken)token).ToElement());
                    break;
            }
        }

        #endregion
    }
}
