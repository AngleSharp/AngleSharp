namespace AngleSharp.Html.Parser;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

/// <summary>
/// Reads the token stream consumed by tree construction.
/// </summary>
internal interface IHtmlTokenCursor
{
    /// <summary>Attempts to move to the next currently available token.</summary>
    Boolean TryMoveNext();

    /// <summary>
    /// Gets the current token. The reference remains valid until the next move.
    /// Consumers should bind it to a ref local; reading it by value copies the complete token carrier.
    /// </summary>
    ref StructHtmlToken Current { get; }
}

/// <summary>
/// Configures tokenizer behavior before input is consumed.
/// </summary>
internal interface IHtmlTokenizerConfiguration
{
    /// <summary>Configures tokenization before input is consumed.</summary>
    void Configure(
        HtmlTokenizerOptions options,
        Action<HtmlToken, TextRange>? onToken,
        Action<HtmlParseError, TextPosition> reportError);
}

/// <summary>
/// Applies tokenizer state changes requested by tree construction.
/// </summary>
internal interface IHtmlTokenizerFeedback
{
    /// <summary>Changes the tokenizer state requested by tree construction.</summary>
    void SetState(HtmlParseMode state);

    /// <summary>Changes whether character data tokens are currently accepted.</summary>
    void SetAcceptingCharacterData(Boolean value);
}

/// <summary>
/// Defines the complete tokenizer contract required by tree construction.
/// </summary>
internal interface IHtmlTokenSource :
    IHtmlTokenCursor,
    IHtmlTokenizerConfiguration,
    IHtmlTokenizerFeedback
{
}

/// <summary>
/// Waits for input when a cursor temporarily has no token available.
/// </summary>
internal interface IHtmlTokenAvailability
{
    /// <summary>Waits until more input may be available.</summary>
    Task WaitForInputAsync(CancellationToken cancellationToken);
}

/// <summary>
/// Defines a token source backed by asynchronously arriving input.
/// </summary>
internal interface IAsyncHtmlTokenSource : IHtmlTokenSource, IHtmlTokenAvailability
{
}
