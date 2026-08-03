namespace AngleSharp.Html.Parser;

using System;
using Tokens.Struct;

/// <summary>
/// Represents a set of options for the HTML tokenizer.
/// </summary>
public struct HtmlTokenizerOptions
{
    /// <summary>
    /// Creates a new tokenizer options instance from the <see cref="HtmlParserOptions"/> instance.
    /// </summary>
    /// <param name="htmlParserOptions"></param>
    public HtmlTokenizerOptions(HtmlParserOptions htmlParserOptions)
    {
        IsStrictMode = htmlParserOptions.IsStrictMode;
        IsSupportingProcessingInstructions = htmlParserOptions.IsSupportingProcessingInstructions;
        IsNotConsumingCharacterReferences = htmlParserOptions.IsNotConsumingCharacterReferences;
        IsPreservingAttributeNames = htmlParserOptions.IsPreservingAttributeNames;
        SkipRawText = htmlParserOptions.SkipRawText;
        SkipScriptText = htmlParserOptions.SkipScriptText;
        SkipDataText = htmlParserOptions.SkipDataText;
        ShouldEmitAttribute = htmlParserOptions.ShouldEmitAttribute ?? (static (ref StructHtmlToken _, ReadOnlyMemory<Char> _) => true);
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
    public ShouldEmitAttribute ShouldEmitAttribute { get; set; }

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
    /// Gets or sets if attribute names should not be normalized.
    /// Usually, attribute names will be only seen lower-cased. When this
    /// option is activated, the names will be taken as-is.
    /// </summary>
    public Boolean IsPreservingAttributeNames { get; set; }

    /// <summary>
    /// Gets or sets if the parsing of character references should
    /// be avoided.
    /// Note: With this option there is no way to determine from
    /// AngleSharp what character references have been fully valid
    /// vs. invalid.
    /// </summary>
    public Boolean IsNotConsumingCharacterReferences { get; set; }

    /// <summary>
    /// Gets or sets if XML processing instructions should be
    /// parsed into DOM nodes.
    /// </summary>
    public Boolean IsSupportingProcessingInstructions { get; set; }

    /// <summary>
    /// Gets or sets if errors should be treated as exceptions.
    /// </summary>
    public Boolean IsStrictMode { get; set; }
}