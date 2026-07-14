namespace AngleSharp.Html.Parser;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Dom.Events;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

/// <summary>
/// Represents a token source consumed by the HTML tree constructor.
/// </summary>
/// <remarks>
/// Implement this contract to feed an alternative tokenizer into
/// <see cref="HtmlParser.ParseDocumentAsync(IHtmlTokenSource, CancellationToken)"/>.
/// </remarks>
public interface IHtmlTokenSource : IDisposable
{
    /// <summary>Raised when a parse error is encountered.</summary>
    event EventHandler<HtmlErrorEvent>? Error;

    /// <summary>Gets or sets the tokenizer state requested by tree construction.</summary>
    HtmlParseMode State { get; set; }

    /// <summary>Gets or sets whether character data tokens are currently accepted.</summary>
    Boolean IsAcceptingCharacterData { get; set; }

    /// <summary>Gets or sets whether parse errors should be treated strictly.</summary>
    Boolean IsStrictMode { get; set; }

    /// <summary>Gets or sets whether processing instructions are supported.</summary>
    Boolean IsSupportingProcessingInstructions { get; set; }

    /// <summary>Gets or sets whether character references should remain unconsumed.</summary>
    Boolean IsNotConsumingCharacterReferences { get; set; }

    /// <summary>Gets or sets whether attribute-name casing should be preserved.</summary>
    Boolean IsPreservingAttributeNames { get; set; }

    /// <summary>Gets or sets whether raw-text tokens should be skipped.</summary>
    Boolean SkipRawText { get; set; }

    /// <summary>Gets or sets whether script-text tokens should be skipped.</summary>
    Boolean SkipScriptText { get; set; }

    /// <summary>Gets or sets whether data-text tokens should be skipped.</summary>
    Boolean SkipDataText { get; set; }

    /// <summary>Gets or sets whether comment tokens should be skipped.</summary>
    Boolean SkipComments { get; set; }

    /// <summary>Gets or sets whether plaintext tokens should be skipped.</summary>
    Boolean SkipPlaintext { get; set; }

    /// <summary>Gets or sets whether RCDATA tokens should be skipped.</summary>
    Boolean SkipRCDataText { get; set; }

    /// <summary>Gets or sets whether CDATA tokens should be skipped.</summary>
    Boolean SkipCDATA { get; set; }

    /// <summary>Gets or sets whether processing-instruction tokens should be skipped.</summary>
    Boolean SkipProcessingInstructions { get; set; }

    /// <summary>Gets or sets whether element position tracking is disabled.</summary>
    Boolean DisableElementPositionTracking { get; set; }

    /// <summary>Gets or sets the predicate controlling attribute emission.</summary>
    ShouldEmitAttribute ShouldEmitAttribute { get; set; }

    /// <summary>Gets or sets a callback invoked for emitted tokens.</summary>
    Action<HtmlToken, TextRange>? OnToken { get; set; }

    /// <summary>Attempts to get the next currently available token.</summary>
    Boolean TryGetStructToken(out StructHtmlToken token);

    /// <summary>Waits until more input may be available.</summary>
    Task WaitForInputAsync(CancellationToken cancellationToken);

    /// <summary>Reports a parse error discovered by tree construction.</summary>
    void RaiseErrorOccurred(HtmlParseError code, TextPosition position);
}
