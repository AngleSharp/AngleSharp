namespace AngleSharp.Html.Parser;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

internal interface IHtmlTokenSource
{
    /// <summary>Configures tokenization before input is consumed.</summary>
    void Configure(
        HtmlTokenizerOptions options,
        Action<HtmlToken, TextRange>? onToken,
        Action<HtmlParseError, TextPosition> reportError);

    /// <summary>Changes the tokenizer state requested by tree construction.</summary>
    void SetState(HtmlParseMode state);

    /// <summary>Changes whether character data tokens are currently accepted.</summary>
    void SetAcceptingCharacterData(Boolean value);

    /// <summary>Attempts to get the next currently available token.</summary>
    Boolean TryGetStructToken(out StructHtmlToken token);

    /// <summary>Waits until more input may be available.</summary>
    Task WaitForInputAsync(CancellationToken cancellationToken);
}
