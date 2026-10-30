namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Contains a number of options for the HTML parser.
    /// </summary>
    public struct HtmlParserOptions
    {
        /// <summary>
        /// The element nesting depth used when <see cref="MaximumDepth"/> is not set.
        /// </summary>
        public static readonly Int32 DefaultMaximumDepth = 512;

        private Int32 _maximumDepth;

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
        /// Gets or sets if whitespace-only text nodes should be kept when parsing into a custom
        /// document through a generic tree-construction entry point. Custom documents omit these
        /// nodes by default, while the standard AngleSharp DOM always keeps them.
        /// </summary>
        public Boolean IsKeepingWhitespaceTextNodes { get; set; }

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
        /// Gets or sets the callback once a new token is read.
        /// </summary>
        public Action<HtmlToken, TextRange>? OnToken { get; set; }

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

        /// <summary>
        /// Gets or sets the deepest level at which the parser nests elements, counting the
        /// root element as level 1. Defaults to <see cref="DefaultMaximumDepth"/>; zero or a
        /// negative value restores the default and <see cref="Int32.MaxValue"/> lifts the limit.
        /// </summary>
        /// <remarks>
        /// HTML puts no limit on nesting, but code walking a tree that is thousands of levels
        /// deep recursively can overflow the stack, which ends the process. Once the limit is
        /// reached, further elements are attached as following siblings of the deepest element
        /// instead of as its children, as browsers do. Nothing of the document is dropped, but
        /// for such documents the tree deviates from the specification, and text that follows
        /// a closed sibling stays in its own element, so it can come out of source order.
        /// </remarks>
        public Int32 MaximumDepth
        {
            readonly get => _maximumDepth > 0 ? _maximumDepth : DefaultMaximumDepth;
            set => _maximumDepth = value;
        }
    }
}