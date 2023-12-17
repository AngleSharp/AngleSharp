namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using Common;


    public interface IHtmlTokenizer
    {
        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        HtmlToken Get();

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        event EventHandler<HtmlErrorEvent>? Error;

        void RaiseErrorOccurred(HtmlParseError code, TextPosition tokenPosition);

        HtmlParseMode State { get; set; }

        Boolean IsAcceptingCharacterData { get; set; }
        Boolean IsStrictMode { get; set; }
        Boolean IsSupportingProcessingInstructions { get; set; }
        Boolean IsNotConsumingCharacterReferences { get; set; }
        bool IsPreservingAttributeNames { get; set; }
    }

    public sealed class StructHtmlTokenizerAdapter : IHtmlTokenizer
    {
        #region Fields

        private readonly StructHtmlTokenizer _tokenizer;

        #endregion

        #region Events

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        public event EventHandler<HtmlErrorEvent>? Error
        {
            add => _tokenizer.Error += value;
            remove => _tokenizer.Error -= value;
        }

        #endregion

        #region ctor

        public StructHtmlTokenizerAdapter(IReadOnlyTextSource source, IEntityProvider resolver)
        {
            _tokenizer = new StructHtmlTokenizer(source, resolver);
        }

        #endregion

        #region Properties

        public Boolean SkipDataText
        {
            get => _tokenizer.SkipDataText;
            set => _tokenizer.SkipDataText = value;
        }

        public Boolean SkipScriptText
        {
            get => _tokenizer.SkipScriptText;
            set => _tokenizer.SkipScriptText = value;
        }

        public Boolean SkipRawText
        {
            get => _tokenizer.SkipRawText;
            set => _tokenizer.SkipRawText = value;
        }

        public Boolean SkipComments
        {
            get => _tokenizer.SkipComments;
            set => _tokenizer.SkipComments = value;
        }

        public Boolean SkipPlaintext
        {
            get => _tokenizer.SkipPlaintext;
            set => _tokenizer.SkipPlaintext = value;
        }

        public Boolean SkipRCDataText
        {
            get => _tokenizer.SkipRCDataText;
            set => _tokenizer.SkipRCDataText = value;
        }

        public Boolean SkipCDATA
        {
            get => _tokenizer.SkipCDATA;
            set => _tokenizer.SkipCDATA = value;
        }

        public Boolean SkipProcessingInstructions
        {
            get => _tokenizer.SkipProcessingInstructions;
            set => _tokenizer.SkipProcessingInstructions = value;
        }

        public ShouldEmitAttribute ShouldEmitAttribute
        {
            get => _tokenizer.ShouldEmitAttribute;
            set => _tokenizer.ShouldEmitAttribute = value;
        }

        /// <summary>
        /// Gets or sets if CDATA sections are accepted.
        /// </summary>
        public Boolean IsAcceptingCharacterData
        {
            get => _tokenizer.IsAcceptingCharacterData;
            set => _tokenizer.IsAcceptingCharacterData = value;
        }

        /// <summary>
        /// Gets or sets if attribute names should be taken as is.
        /// </summary>
        public Boolean IsPreservingAttributeNames
        {
            get => _tokenizer.IsPreservingAttributeNames;
            set => _tokenizer.IsPreservingAttributeNames = value;
        }

        /// <summary>
        /// Gets or sets if character references should be avoided.
        /// </summary>
        public Boolean IsNotConsumingCharacterReferences
        {
            get => _tokenizer.IsNotConsumingCharacterReferences;
            set => _tokenizer.IsNotConsumingCharacterReferences = value;
        }

        /// <summary>
        /// Gets or sets the current parse mode.
        /// </summary>
        public HtmlParseMode State
        {
            get => _tokenizer.State;
            set => _tokenizer.State = value;
        }

        /// <summary>
        /// Gets or sets if strict mode is used.
        /// </summary>
        public Boolean IsStrictMode
        {
            get => _tokenizer.IsStrictMode;
            set => _tokenizer.IsStrictMode = value;
        }

        /// <summary>
        /// Gets or sets if XML processing instructions should
        /// be parsed into DOM nodes.
        /// </summary>
        public Boolean IsSupportingProcessingInstructions
        {
            get => _tokenizer.IsSupportingProcessingInstructions;
            set => _tokenizer.IsSupportingProcessingInstructions = value;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        public HtmlToken Get()
        {
            return _tokenizer.Get().ToHtmlToken();
        }

        public void RaiseErrorOccurred(HtmlParseError code, TextPosition tokenPosition)
        {
            _tokenizer.RaiseErrorOccurred(code, tokenPosition);
        }

        #endregion
    }
}
