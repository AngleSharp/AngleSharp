#if NET10_0
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

/// <summary>
/// Models the UTF-8 decoding and final string-materialization work performed by the mature bounded source and the
/// native UTF-8 token adapter. Payloads use the real character-data, attribute-value, comment, and doctype-value
/// distribution extracted from each corpus.
/// </summary>
[MemoryDiagnoser, ShortRunJob]
public class Utf8BulkVsSparseDecodeBenchmark
{
    private const Int32 SegmentSize = 4096;

    private Byte[] _documentUtf8 = null!;
    private Byte[][] _payloadUtf8 = null!;
    private String[] _payloadUtf16 = null!;
    private Decoder _bulkDecoder = null!;
    private Char[] _bulkDecodeBuffer = null!;

    [Params("en.wikipedia.html", "stackoverflow.html", "youtube.html", "spiegel.html")]
    public String Corpus { get; set; } = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        _documentUtf8 = File.ReadAllBytes(Corpus);
        _payloadUtf16 = await ExtractDynamicPayloadsAsync(_documentUtf8).ConfigureAwait(false);
        _payloadUtf8 = new Byte[_payloadUtf16.Length][];
        for (var index = 0; index < _payloadUtf16.Length; index++)
        {
            _payloadUtf8[index] = Encoding.UTF8.GetBytes(_payloadUtf16[index]);
        }

        _bulkDecoder = Encoding.UTF8.GetDecoder();
        _bulkDecodeBuffer = new Char[Encoding.UTF8.GetMaxCharCount(SegmentSize)];

        var bulkChecksum = BulkDecodeThenCopyPayloadStrings();
        var sparseChecksum = SparseDecodePayloadStrings();
        var directChecksum = SparseDirectGetStringPayloads();
        if (bulkChecksum != sparseChecksum || bulkChecksum != directChecksum)
        {
            throw new InvalidOperationException(
                $"Checksums differ: bulk {bulkChecksum}, sparse {sparseChecksum}, direct {directChecksum}."
            );
        }
    }

    /// <summary>
    /// Mature-like model: decode every document byte in bounded chunks, then copy already-decoded payload characters
    /// into their final strings.
    /// </summary>
    [Benchmark(Baseline = true)]
    public Int32 BulkDecodeThenCopyPayloadStrings()
    {
        _bulkDecoder.Reset();
        for (var offset = 0; offset < _documentUtf8.Length;)
        {
            var inputLength = Math.Min(SegmentSize, _documentUtf8.Length - offset);
            var flush = offset + inputLength == _documentUtf8.Length;
            _bulkDecoder.Convert(
                _documentUtf8.AsSpan(offset, inputLength),
                _bulkDecodeBuffer,
                flush,
                out var bytesUsed,
                out _,
                out _
            );
            if (bytesUsed <= 0)
            {
                throw new InvalidOperationException("The bulk UTF-8 decoder made no progress.");
            }

            offset += bytesUsed;
        }

        var checksum = 0;
        foreach (var payload in _payloadUtf16)
        {
            var materialized = new String(payload.AsSpan());
            checksum = Mix(checksum, materialized);
        }
        return checksum;
    }

    /// <summary>
    /// Native-like model: decode only dynamic payload bytes directly into each final string, including the current
    /// stack-buffer path for payloads no longer than 64 bytes.
    /// </summary>
    [Benchmark]
    public Int32 SparseDecodePayloadStrings()
    {
        var checksum = 0;
        foreach (var payload in _payloadUtf8)
        {
            var materialized = DecodeSparse(payload);
            checksum = Mix(checksum, materialized);
        }
        return checksum;
    }

    /// <summary>
    /// System.Text.Json-like sparse model: materialize each dynamic payload with Encoding.UTF8.GetString directly,
    /// allowing the runtime to allocate and fill the final string without a temporary UTF-16 stack buffer.
    /// </summary>
    [Benchmark]
    public Int32 SparseDirectGetStringPayloads()
    {
        var checksum = 0;
        foreach (var payload in _payloadUtf8)
        {
            var materialized = DecodeSparseDirect(payload);
            checksum = Mix(checksum, materialized);
        }
        return checksum;
    }

    private static String DecodeSparseDirect(ReadOnlySpan<Byte> utf8) =>
        utf8.IsEmpty ? String.Empty : Encoding.UTF8.GetString(utf8);

    private static String DecodeSparse(ReadOnlySpan<Byte> utf8)
    {
        if (utf8.IsEmpty)
        {
            return String.Empty;
        }

        if (utf8.Length <= 64)
        {
            Span<Char> characters = stackalloc Char[utf8.Length];
            var written = Encoding.UTF8.GetChars(utf8, characters);
            return new String(characters[..written]);
        }

        return Encoding.UTF8.GetString(utf8);
    }

    private static Int32 Mix(Int32 checksum, String value) =>
        unchecked((checksum * 397) ^ value.Length ^ (value.Length == 0 ? 0 : value[0]));

    private static async Task<String[]> ExtractDynamicPayloadsAsync(Byte[] utf8)
    {
        var html = Encoding.UTF8.GetString(utf8);
        var context = BrowsingContext.New(Configuration.Default);
        var factory = context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
        var payloads = new List<String>();

        using var document = new HtmlDocument(context, new TextSource(html));
        using var tokenizer = new HtmlTokenizer(document.Source, HtmlEntityProvider.ResolverExtended);
        var inner = new HtmlTokenizerTokenSource(tokenizer);
        var source = new PayloadCollectingTokenSource(inner, payloads);
        using var builder = new HtmlDomBuilder(factory, document, tokenSource: source);
        await builder.ParseAsync(new HtmlParserOptions()).ConfigureAwait(false);
        return payloads.ToArray();
    }

    private sealed class PayloadCollectingTokenSource(
        IHtmlTokenSource inner,
        List<String> payloads) : IHtmlTokenSource, IHtmlTokenAvailability
    {
        public Boolean TryMoveNext()
        {
            if (!inner.TryMoveNext())
            {
                return false;
            }

            Record(ref inner.Current);
            return true;
        }

        public ref StructHtmlToken Current => ref inner.Current;

        public void Configure(
            HtmlTokenizerOptions options,
            Action<HtmlToken, TextRange> onToken,
            Action<HtmlParseError, TextPosition> reportError) =>
            inner.Configure(options, onToken, reportError);

        public void SetState(HtmlParseMode state) => inner.SetState(state);

        public void SetAcceptingCharacterData(Boolean value) => inner.SetAcceptingCharacterData(value);

        public Task WaitForInputAsync(CancellationToken cancellationToken) =>
            inner is IHtmlTokenAvailability availability
                ? availability.WaitForInputAsync(cancellationToken)
                : throw new InvalidOperationException("The wrapped token source cannot wait for input.");

        private void Record(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                    var attributes = token.Attributes;
                    for (var index = 0; index < attributes.Count; index++)
                    {
                        payloads.Add(attributes[index].Value.ToString());
                    }
                    break;
                case HtmlTokenType.Character:
                case HtmlTokenType.Comment:
                    payloads.Add(token.Data.ToString());
                    break;
                case HtmlTokenType.Doctype:
                    payloads.Add(token.Name.ToString());
                    if (!token.IsPublicIdentifierMissing)
                    {
                        payloads.Add(token.PublicIdentifier.ToString());
                    }
                    if (!token.IsSystemIdentifierMissing)
                    {
                        payloads.Add(token.SystemIdentifier.ToString());
                    }
                    break;
            }
        }
    }
}
#endif
