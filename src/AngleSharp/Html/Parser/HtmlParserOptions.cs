namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using Tokens;
    using Tokens.Struct;
    using AttributeName = System.ReadOnlyMemory<System.Char>;

    public struct HtmlTokenizerOptions
    {
        public HtmlTokenizerOptions(HtmlParserOptions htmlParserOptions)
        {
            IsStrictMode = htmlParserOptions.IsStrictMode;
            IsSupportingProcessingInstructions = htmlParserOptions.IsSupportingProcessingInstructions;
            IsNotConsumingCharacterReferences = htmlParserOptions.IsNotConsumingCharacterReferences;
            IsPreservingAttributeNames = htmlParserOptions.IsPreservingAttributeNames;

            SkipRawText = htmlParserOptions.SkipRawText;
            SkipScriptText = htmlParserOptions.SkipScriptText;
            SkipDataText = htmlParserOptions.SkipDataText;
            ShouldEmitAttribute = htmlParserOptions.ShouldEmitAttribute ?? (static (ref StructHtmlToken _, AttributeName _) => true);

            SkipDataText = htmlParserOptions.SkipDataText;
            SkipScriptText = htmlParserOptions.SkipScriptText;
            SkipRawText = htmlParserOptions.SkipRawText;
            SkipComments = htmlParserOptions.SkipComments;
            SkipPlaintext = htmlParserOptions.SkipPlaintext;
            SkipRCDataText = htmlParserOptions.SkipRCDataText;
            SkipCDATA = htmlParserOptions.SkipCDATA;
            SkipProcessingInstructions = htmlParserOptions.SkipProcessingInstructions;

            DisableElementPositionTracking = htmlParserOptions.DisableElementPositionTracking;
        }

        public Boolean DisableElementPositionTracking { get; set; }
        public Boolean SkipComments { get; set; }
        public Boolean SkipPlaintext { get; set; }
        public Boolean SkipRCDataText { get; set; }
        public Boolean SkipCDATA { get; set; }
        public Boolean SkipProcessingInstructions { get; set; }
        public ShouldEmitAttribute ShouldEmitAttribute { get; set; }
        public Boolean SkipDataText { get; set; }
        public Boolean SkipScriptText { get; set; }
        public Boolean SkipRawText { get; set; }
        public Boolean IsPreservingAttributeNames { get; set; }
        public Boolean IsNotConsumingCharacterReferences { get; set; }
        public Boolean IsSupportingProcessingInstructions { get; set; }
        public Boolean IsStrictMode { get; set; }
    }

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
        ///
        /// </summary>
        public Boolean SkipDataText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipScriptText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipRawText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipComments { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipPlaintext { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipRCDataText { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipCDATA { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Boolean SkipProcessingInstructions { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ShouldEmitAttribute? ShouldEmitAttribute { get; set; }


        /// <summary>
        ///
        /// </summary>
        public bool DisableElementPositionTracking { get; set; }
    }
}