namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Contains a number of options for the HTML parser.
    /// </summary>
    public struct HtmlParserOptions
    {
        /// <summary>
        /// Gets or sets if the document is embedded.
        /// </summary>
        public Boolean IsEmbedded { get; set; }

        /// <summary>
        /// Gets or sets if custom elements can be used everywhere. Otherwise,
        /// custom elements can only be used in locations specified by the
        /// official W3C spec (e.g., in the body).
        /// </summary>
        public Boolean IsAcceptingCustomElementsEverywhere { get; set; }

        /// <summary>
        /// Gets or sets if attribute names should not be normalized.
        /// Usually, attribute names will be only seen lower-cased. When this
        /// option is activated, the names will be taken as-is.
        /// </summary>
        public Boolean IsPreservingAttributeNames { get; set; }

        /// <summary>
        /// Gets or sets if frames should not be supported. Once
        /// set this will ignore frame elements and respect
        /// noframes elements.
        /// </summary>
        public Boolean IsNotSupportingFrames { get; set; }

        /// <summary>
        /// Gets or sets if scripting is allowed.
        /// </summary>
        public Boolean IsScripting { get; set; }

        /// <summary>
        /// Gets or sets if errors should be treated as exceptions.
        /// </summary>
        public Boolean IsStrictMode { get; set; }

        /// <summary>
        /// Gets or sets if XML processing instructions should be
        /// parsed into DOM nodes.
        /// </summary>
        public Boolean IsSupportingProcessingInstructions { get; set; }

        /// <summary>
        /// Gets or sets if references to the original source document
        /// should be kept on the elements in form of their tokens.
        /// </summary>
        public Boolean IsKeepingSourceReferences { get; set; }

        /// <summary>
        /// Gets or sets if the parsing of character references should
        /// be avoided.
        /// Note: With this option there is no way to determine from
        /// AngleSharp what character references have been fully valid
        /// vs. invalid.
        /// </summary>
        public Boolean IsNotConsumingCharacterReferences { get; set; }

        /// <summary>
        /// Gets or sets the callback once a new element was created.
        /// </summary>
        public Action<IElement, TextPosition>? OnCreated { get; set; }

        /// <summary>
        /// Prevents the tokenizer from tracking the position of elements.
        /// </summary>
        public Boolean DisableElementPositionTracking { get; set; }

        /// <summary>
        /// Should the tokenizer skip comment tokens.
        /// </summary>
        public Boolean SkipComments { get; set; }

        /// <summary>
        /// Should the tokenizer skip plaintext tokens.
        /// </summary>
        public Boolean SkipPlaintext { get; set; }

        /// <summary>
        /// Should the tokenizer skip RCDATA text tokens.
        /// </summary>
        public Boolean SkipRCDataText { get; set; }

        /// <summary>
        /// Should the tokenizer skip CDATA text tokens.
        /// </summary>
        public Boolean SkipCDATA { get; set; }

        /// <summary>
        /// Should the tokenizer skip processing instruction tokens.
        /// </summary>
        public Boolean SkipProcessingInstructions { get; set; }

        /// <summary>
        /// Gets or set the delegate which determines if an attribute should be emitted.
        /// </summary>
        public ShouldEmitAttribute? ShouldEmitAttribute { get; set; }

        /// <summary>
        /// Should the tokenizer skip data text tokens.
        /// </summary>
        public Boolean SkipDataText { get; set; }

        /// <summary>
        /// Should the tokenizer skip script text tokens.
        /// </summary>
        public Boolean SkipScriptText { get; set; }

        /// <summary>
        /// Should the tokenizer skip raw text tokens.
        /// </summary>
        public Boolean SkipRawText { get; set; }
    }
}