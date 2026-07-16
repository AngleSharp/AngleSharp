namespace AngleSharp.Html.Parser;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

internal sealed class HtmlTokenizerTokenSource(HtmlTokenizer tokenizer) : IHtmlTokenSource
{
    public void Configure(
        HtmlTokenizerOptions options,
        Action<HtmlToken, TextRange>? onToken,
        Action<HtmlParseError, TextPosition> reportError)
    {
        tokenizer.IsStrictMode = options.IsStrictMode;
        tokenizer.IsSupportingProcessingInstructions = options.IsSupportingProcessingInstructions;
        tokenizer.IsNotConsumingCharacterReferences = options.IsNotConsumingCharacterReferences;
        tokenizer.IsPreservingAttributeNames = options.IsPreservingAttributeNames;
        tokenizer.SkipRawText = options.SkipRawText;
        tokenizer.SkipScriptText = options.SkipScriptText;
        tokenizer.SkipDataText = options.SkipDataText;
        tokenizer.ShouldEmitAttribute = options.ShouldEmitAttribute;
        tokenizer.SkipComments = options.SkipComments;
        tokenizer.SkipPlaintext = options.SkipPlaintext;
        tokenizer.SkipRCDataText = options.SkipRCDataText;
        tokenizer.SkipCDATA = options.SkipCDATA;
        tokenizer.SkipProcessingInstructions = options.SkipProcessingInstructions;
        tokenizer.DisableElementPositionTracking = options.DisableElementPositionTracking;
        tokenizer.OnToken = onToken;
        tokenizer.ErrorSink = reportError;
    }

    public void SetState(HtmlParseMode state) => tokenizer.State = state;

    public void SetAcceptingCharacterData(Boolean value) => tokenizer.IsAcceptingCharacterData = value;

    public Boolean TryGetStructToken(out StructHtmlToken token)
    {
        token = tokenizer.GetStructToken();
        return true;
    }

    public Task WaitForInputAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
