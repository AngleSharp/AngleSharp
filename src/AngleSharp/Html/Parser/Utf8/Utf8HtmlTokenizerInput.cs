#pragma warning disable CS1591 // Experimental implementation detail; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Frames streaming input, enforces the source-byte limit, and optionally repairs malformed UTF-8
/// before complete, well-formed spans reach <see cref="Utf8HtmlTokenizer"/>.
/// </summary>
public sealed class Utf8HtmlTokenizerInput
{
    private Utf8InputNormalizer _normalizer;
    private readonly Utf8HtmlTokenizer _tokenizer;
    private Boolean _completed;

    public Utf8HtmlTokenizerInput(
        Utf8HtmlTokenizer tokenizer,
        Utf8InputContract inputContract = Utf8InputContract.ArbitraryBytes,
        HtmlStreamingLimits? limits = null
    )
    {
        ArgumentNullException.ThrowIfNull(tokenizer);
        limits ??= HtmlStreamingLimits.Default;
        _tokenizer = tokenizer;
        _normalizer = new Utf8InputNormalizer(limits.MaximumInputBytes, inputContract);
    }

    public Utf8HtmlTokenizerCounters Counters =>
        _tokenizer.GetCounters(_normalizer.BytesConsumed);

    public void Write(ReadOnlyMemory<Byte> input)
    {
        ThrowIfCompleted();
        _tokenizer.RecordInputSegment();
        _normalizer.Write(_tokenizer, input.Span, yieldOnRequest: false);
    }

    public void Write(ReadOnlySpan<Byte> input)
    {
        ThrowIfCompleted();
        _tokenizer.RecordInputSegment();
        _normalizer.Write(_tokenizer, input, yieldOnRequest: false);
    }

    internal Int32 WriteUntilYield(ReadOnlySpan<Byte> input)
    {
        ThrowIfCompleted();
        _tokenizer.ResetYieldRequest();
        return _normalizer.Write(_tokenizer, input, yieldOnRequest: true);
    }

    public void Complete()
    {
        if (_completed)
            return;

        _normalizer.Complete(_tokenizer);
        _tokenizer.Complete();
        _completed = true;
    }

    private void ThrowIfCompleted()
    {
        if (_completed)
            throw new InvalidOperationException("The UTF-8 tokenizer input has already completed.");
    }
}
