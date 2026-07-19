#if NET10_0
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Parser.Utf8;
using AngleSharp.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

using System.Linq;

[MemoryDiagnoser, ShortRunJob]
public class Utf8LongPayloadBenchmark
{
    private const Int32 PayloadBytes = 1024 * 1024;
    private const Int32 NetworkBufferSize = 4096;

    private IBrowsingContext _context = null!;
    private IHtmlElementConstructionFactory _factory = null!;
    private HtmlParser _parser = null!;
    private Byte[] _utf8 = null!;
    private String _expectedMarkup = null!;
    private readonly CountingSink _borrowedSink = new();
    private readonly YieldingSink _yieldingSink = new();

    [Params(Utf8PayloadProfile.Ascii, Utf8PayloadProfile.TwoByte, Utf8PayloadProfile.FourByte)]
    public Utf8PayloadProfile Profile { get; set; }

    [GlobalSetup]
    public async Task Setup()
    {
        var payload = Profile switch
        {
            Utf8PayloadProfile.Ascii => new String('a', PayloadBytes),
            Utf8PayloadProfile.TwoByte => new String('\u0416', PayloadBytes / 2),
            Utf8PayloadProfile.FourByte => String.Concat(
                System.Linq.Enumerable.Repeat("\U0001F642", PayloadBytes / 4)
            ),
            _ => throw new ArgumentOutOfRangeException(),
        };
        var html = "<!doctype html><html><body><main>" + payload + "</main></body></html>";
        _utf8 = Encoding.UTF8.GetBytes(html);
        _context = BrowsingContext.New(Configuration.Default);
        _factory = _context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
        _parser = new HtmlParser(_context);

        using var expected = _parser.ParseDocument(html);
        _expectedMarkup = expected.DocumentElement.OuterHtml;
        using var actual = await ParseNativeAsync().ConfigureAwait(false);
        if (!String.Equals(actual.DocumentElement.OuterHtml, _expectedMarkup, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Native UTF-8 mutable DOM differs on the long-payload fixture.");
        }
    }

    [Benchmark(Baseline = true)]
    public async Task<Int32> MatureBoundedUtf16Network4K()
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await _parser
            .ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming, Encoding.UTF8)
            .ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark]
    public async Task<Int32> NativeUtf8Network4K()
    {
        using var document = await ParseNativeAsync().ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark]
    public Int32 NativeBorrowedTokenizerNetwork4K()
    {
        _borrowedSink.Reset();
        var tokenizer = new Utf8HtmlTokenizer(_borrowedSink);
        WriteSegmented(tokenizer, _utf8);
        tokenizer.Complete();
        return _borrowedSink.Checksum;
    }

    [Benchmark]
    public Int32 NativeYieldingTokenizerNetwork4K()
    {
        _yieldingSink.Reset();
        var tokenizer = new Utf8HtmlTokenizer(_yieldingSink);
        _yieldingSink.Tokenizer = tokenizer;

        for (var segmentOffset = 0; segmentOffset < _utf8.Length; segmentOffset += NetworkBufferSize)
        {
            var segment = _utf8.AsSpan(
                segmentOffset,
                Math.Min(NetworkBufferSize, _utf8.Length - segmentOffset)
            );
            for (var offset = 0; offset < segment.Length; )
            {
                var consumed = tokenizer.WriteUntilYield(segment[offset..]);
                if (consumed <= 0)
                {
                    throw new InvalidOperationException("Yielding tokenizer made no progress.");
                }
                offset += consumed;
            }
        }

        tokenizer.Complete();
        return _yieldingSink.Checksum;
    }

    [Benchmark]
    public Task<Int32> NativeAdaptedTokenSourceNetwork4K() =>
        RunNativeAdapterAsync(new HtmlParserOptions());

    [Benchmark]
    public Task<Int32> NativeAdaptedStructureOnlyNetwork4K() =>
        RunNativeAdapterAsync(
            new HtmlParserOptions
            {
                SkipDataText = true,
                SkipRawText = true,
                SkipScriptText = true,
                SkipPlaintext = true,
                SkipRCDataText = true,
                ShouldEmitAttribute = static (ref _, _) => false,
            }
        );

    private async Task<IDocument> ParseNativeAsync()
    {
        var document = new HtmlDocument(_context, new TextSource(String.Empty));
        await using var tokenSource = new Utf8HtmlTokenSource(NetworkChunks(_utf8, NetworkBufferSize));
        using var builder = new HtmlDomBuilder(_factory, document, tokenSource: tokenSource);
        return await builder.ParseAsync(new HtmlParserOptions()).ConfigureAwait(false);
    }

    private async Task<Int32> RunNativeAdapterAsync(HtmlParserOptions parserOptions)
    {
        await using var source = new Utf8HtmlTokenSource(NetworkChunks(_utf8, NetworkBufferSize));
        source.Configure(
            new HtmlTokenizerOptions(parserOptions),
            onToken: null,
            reportError: static (_, _) => { }
        );
        var checksum = 0;

        while (true)
        {
            while (source.TryMoveNext())
            {
                ref var token = ref source.Current;
                checksum = unchecked((checksum * 31) + (Int32)token.Type);
                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    return checksum;
                }
            }

            await source.WaitForInputAsync(default).ConfigureAwait(false);
        }
    }

    private static void WriteSegmented(Utf8HtmlTokenizer tokenizer, Byte[] source)
    {
        for (var offset = 0; offset < source.Length; offset += NetworkBufferSize)
        {
            tokenizer.Write(
                source.AsSpan(offset, Math.Min(NetworkBufferSize, source.Length - offset))
            );
        }
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> NetworkChunks(
        Byte[] source,
        Int32 bufferSize,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
        try
        {
            for (var offset = 0; offset < source.Length; offset += bufferSize)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var length = Math.Min(bufferSize, source.Length - offset);
                source.AsSpan(offset, length).CopyTo(buffer);
                yield return buffer.AsMemory(0, length);
            }
        }
        finally
        {
            ArrayPool<Byte>.Shared.Return(buffer);
        }
    }

    private sealed class NetworkReadStream(Byte[] source, Int32 maxReadSize) : Stream
    {
        private Int32 _position;

        public override Boolean CanRead => true;
        public override Boolean CanSeek => false;
        public override Boolean CanWrite => false;
        public override Int64 Length => source.Length;
        public override Int64 Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => Read(buffer.AsSpan(offset, count));

        public override Int32 Read(Span<Byte> buffer)
        {
            var length = Math.Min(Math.Min(buffer.Length, maxReadSize), source.Length - _position);
            if (length <= 0)
            {
                return 0;
            }
            source.AsSpan(_position, length).CopyTo(buffer);
            _position += length;
            return length;
        }

        public override Task<Int32> ReadAsync(
            Byte[] buffer,
            Int32 offset,
            Int32 count,
            CancellationToken cancellationToken) => Task.FromResult(Read(buffer, offset, count));

        public override ValueTask<Int32> ReadAsync(
            Memory<Byte> buffer,
            CancellationToken cancellationToken = default) => ValueTask.FromResult(Read(buffer.Span));

        public override void Flush() { }
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(Int64 value) => throw new NotSupportedException();
        public override void Write(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
    }

    private class CountingSink : IUtf8HtmlTokenSink
    {
        public Int32 Checksum { get; protected set; }

        public void Reset() => Checksum = 0;

        public void Text(ReadOnlySpan<Byte> utf8) => Fold(1, utf8.Length);

        public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
        {
            Fold(2, name.Verbatim.Length);
            return Utf8HtmlStartTagCapture.Attributes;
        }

        public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) =>
            Fold(name.Verbatim.Length, value.Length);

        public virtual void StartTagEnd(Boolean selfClosing) => Fold(3, selfClosing ? 1 : 0);

        public virtual void EndTag(Utf8HtmlName name) => Fold(4, name.Verbatim.Length);

        public virtual void Comment(ReadOnlySpan<Byte> value) => Fold(5, value.Length);

        public virtual void Doctype(in Utf8DoctypeToken doctype) => Fold(6, doctype.Name.Length);

        public Boolean WantsAttribute(Utf8HtmlName name) => true;

        public void EndOfFile() => Fold(7, 0);

        protected void Fold(Int32 kind, Int32 length) =>
            Checksum = unchecked(((Checksum * 31) + kind) * 31 + length);
    }

    private sealed class YieldingSink : CountingSink
    {
        public Utf8HtmlTokenizer Tokenizer { private get; set; } = null!;

        public override void StartTagEnd(Boolean selfClosing)
        {
            base.StartTagEnd(selfClosing);
            Tokenizer.RequestYield();
        }

        public override void EndTag(Utf8HtmlName name)
        {
            base.EndTag(name);
            Tokenizer.RequestYield();
        }

        public override void Comment(ReadOnlySpan<Byte> value)
        {
            base.Comment(value);
            Tokenizer.RequestYield();
        }

        public override void Doctype(in Utf8DoctypeToken doctype)
        {
            base.Doctype(doctype);
            Tokenizer.RequestYield();
        }
    }
}

public enum Utf8PayloadProfile
{
    Ascii,
    TwoByte,
    FourByte,
}

[MemoryDiagnoser]
[SimpleJob(warmupCount: 5, iterationCount: 15)]
public class RoughBench
{
    private const Int32 PayloadUnits = 16 * 1024;
    private const Int32 UnitsPerSlice = 8;

    // "aЖ🙂"
    private const Int32 BytesPerUnit = 7;
    private const Int32 CharsPerUnit = 4;

    private const Int32 BytesPerSlice = UnitsPerSlice * BytesPerUnit; // 56
    private const Int32 CharsPerSlice = UnitsPerSlice * CharsPerUnit; // 32

    private Byte[] _utf8 = null!;
    private Char[] _chars = null!;

    [GlobalSetup]
    public void Setup()
    {
        var text = String.Concat(
            Enumerable.Repeat("aЖ🙂", PayloadUnits)
        );

        _utf8 = Encoding.UTF8.GetBytes(text);
        _chars = text.ToCharArray();

        if (_utf8.Length != PayloadUnits * BytesPerUnit)
        {
            throw new InvalidOperationException(
                $"Unexpected UTF-8 size: {_utf8.Length}."
            );
        }

        if (_chars.Length != PayloadUnits * CharsPerUnit)
        {
            throw new InvalidOperationException(
                $"Unexpected UTF-16 size: {_chars.Length}."
            );
        }
    }

    [Benchmark(Baseline = true)]
    public Int32 DecodeOnceThenSlice()
    {
        // Include the one-time decode in every benchmark operation.
        var chars = new Char[Encoding.UTF8.GetCharCount(_utf8)];
        var written = Encoding.UTF8.GetChars(_utf8, chars);

        var checksum = 0;
        var sliceCount = PayloadUnits - UnitsPerSlice + 1;

        for (var unitOffset = 0; unitOffset < sliceCount; unitOffset++)
        {
            var charOffset = unitOffset * CharsPerUnit;

            var value = new String(
                chars,
                charOffset,
                CharsPerSlice
            );

            checksum = unchecked((checksum * 31) + value.Length);
        }

        return checksum + written;
    }

    [Benchmark]
    public Int32 DecodeEachSlice()
    {
        var checksum = 0;
        var sliceCount = PayloadUnits - UnitsPerSlice + 1;

        for (var unitOffset = 0; unitOffset < sliceCount; unitOffset++)
        {
            var byteOffset = unitOffset * BytesPerUnit;

            var value = Encoding.UTF8.GetString(
                _utf8,
                byteOffset,
                BytesPerSlice
            );

            checksum = unchecked((checksum * 31) + value.Length);
        }

        return checksum;
    }
}

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 5)]
public class Utf8TranscodingBreakEvenBenchmark
{
    private const Int32 TokenCount = 32 * 1024;

    // a    = 1 UTF-8 byte, 1 UTF-16 char
    // Ж    = 2 UTF-8 bytes, 1 UTF-16 char
    // 🙂   = 4 UTF-8 bytes, 2 UTF-16 chars
    //
    // Each token:
    //   7 UTF-8 bytes
    //   4 UTF-16 code units
    private const String Token = "aЖ🙂";
    private const Int32 BytesPerToken = 7;
    private const Int32 CharsPerToken = 4;

    private Byte[] _utf8 = null!;
    private Int32[] _selectedTokens = null!;

    [Params(1, 2, 5, 10, 15, 20, 25, 30, 40, 50, 75, 100)]
    public Int32 Percent { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var text = String.Concat(Enumerable.Repeat(Token, TokenCount));
        _utf8 = Encoding.UTF8.GetBytes(text);

        if (_utf8.Length != TokenCount * BytesPerToken)
        {
            throw new InvalidOperationException(
                $"Expected {TokenCount * BytesPerToken} bytes, got {_utf8.Length}."
            );
        }

        var selectedCount = Math.Max(
            1,
            TokenCount * Percent / 100
        );

        // Deterministic evenly distributed selection.
        // This avoids testing only a prefix of the input.
        _selectedTokens = new Int32[selectedCount];

        for (var i = 0; i < selectedCount; i++)
        {
            _selectedTokens[i] =
                (Int32)((Int64)i * TokenCount / selectedCount);
        }
    }

    [Benchmark(Baseline = true)]
    public Int32 DecodeWholePayloadThenMaterializeSelected()
    {
        var requiredChars = Encoding.UTF8.GetCharCount(_utf8);
        var buffer = ArrayPool<Char>.Shared.Rent(requiredChars);

        try
        {
            var written = Encoding.UTF8.GetChars(
                _utf8,
                buffer
            );

            var chars = buffer.AsSpan(0, written);
            var checksum = 17;

            foreach (var tokenIndex in _selectedTokens)
            {
                var value = new String(
                    chars.Slice(
                        tokenIndex * CharsPerToken,
                        CharsPerToken
                    )
                );

                checksum = Consume(checksum, value);
            }

            return checksum;
        }
        finally
        {
            ArrayPool<Char>.Shared.Return(buffer);
        }
    }

    [Benchmark]
    public Int32 DecodeSelectedSlices()
    {
        var checksum = 17;

        foreach (var tokenIndex in _selectedTokens)
        {
            var value = Encoding.UTF8.GetString(
                _utf8,
                tokenIndex * BytesPerToken,
                BytesPerToken
            );

            checksum = Consume(checksum, value);
        }

        return checksum;
    }

    private static Int32 Consume(Int32 checksum, String value)
    {
        // Observe content, not merely Length.
        return unchecked(
            ((checksum * 31) + value.Length) * 31
            + value[0]
            + value[^1]
        );
    }
}

#endif
