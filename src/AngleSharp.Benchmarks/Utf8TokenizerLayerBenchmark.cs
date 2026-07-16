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
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Utf8;
using AngleSharp.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

/// <summary>Separates UTF-8 tokenization from adaptation to the mutable tree builder's UTF-16 token contract.</summary>
[MemoryDiagnoser, ShortRunJob]
public class Utf8TokenizerLayerBenchmark
{
    private const Int32 SegmentSize = 4096;

    private readonly CountingSink _sink = new();
    private readonly YieldingSink _yieldingSink = new();
    private Byte[] _utf8 = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        _utf8 = File.ReadAllBytes("page.html");

        var mature = MatureBoundedTokenizerNetwork4K();
        var adapted = await NativeAdaptedTokenSourceNetwork4K().ConfigureAwait(false);
        if (adapted != mature)
        {
            throw new InvalidOperationException(
                $"Native adapter emitted {adapted:N0} tokens; mature tokenizer emitted {mature:N0}."
            );
        }
    }

    [Benchmark(Baseline = true)]
    public Int32 MatureBoundedTokenizerNetwork4K()
    {
        using var stream = new NetworkReadStream(_utf8, SegmentSize);
        using var source = new TextSource(
            stream,
            Encoding.UTF8,
            StreamTextSourceMode.Bounded,
            encodingIsCertain: true
        );
        using var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.ResolverExtended);
        var count = 0;

        while (true)
        {
            ref var token = ref tokenizer.GetStructToken();
            count++;
            SetMode(token, static mode => { }, tokenizer);
            if (token.Type == HtmlTokenType.EndOfFile)
            {
                return count;
            }
        }
    }

    [Benchmark]
    public Int32 NativeBorrowedTokenizerNetwork4K()
    {
        _sink.Reset();
        var tokenizer = new Utf8HtmlTokenizer(_sink);
        for (var offset = 0; offset < _utf8.Length; offset += SegmentSize)
        {
            tokenizer.Write(_utf8.AsSpan(offset, Math.Min(SegmentSize, _utf8.Length - offset)));
        }
        tokenizer.Complete();
        return _sink.Tokens;
    }

    [Benchmark]
    public Int32 NativeYieldingTokenizerNetwork4K()
    {
        _yieldingSink.Reset();
        var tokenizer = new Utf8HtmlTokenizer(_yieldingSink);
        _yieldingSink.Tokenizer = tokenizer;

        for (var segmentOffset = 0; segmentOffset < _utf8.Length; segmentOffset += SegmentSize)
        {
            var segment = _utf8.AsSpan(
                segmentOffset,
                Math.Min(SegmentSize, _utf8.Length - segmentOffset)
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
        return _yieldingSink.Tokens;
    }

    [Benchmark]
    public Task<Int32> NativeAdaptedTokenSourceNetwork4K() =>
        RunNativeAdapter(NetworkChunks(_utf8, SegmentSize), new HtmlParserOptions());

    [Benchmark]
    public Task<Int32> NativeAdaptedTokenSourceSingleChunk() =>
        RunNativeAdapter(SingleChunk(_utf8), new HtmlParserOptions());

    [Benchmark]
    public Task<Int32> NativeAdaptedWithoutTextNetwork4K() =>
        RunNativeAdapter(
            NetworkChunks(_utf8, SegmentSize),
            new HtmlParserOptions
            {
                SkipDataText = true,
                SkipRawText = true,
                SkipScriptText = true,
                SkipPlaintext = true,
                SkipRCDataText = true,
            }
        );

    [Benchmark]
    public Task<Int32> NativeAdaptedWithoutAttributesNetwork4K() =>
        RunNativeAdapter(
            NetworkChunks(_utf8, SegmentSize),
            new HtmlParserOptions { ShouldEmitAttribute = static (ref _, _) => false }
        );

    [Benchmark]
    public Task<Int32> NativeAdaptedStructureOnlyNetwork4K() =>
        RunNativeAdapter(
            NetworkChunks(_utf8, SegmentSize),
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

    [Benchmark]
    public Task<Int32> NativeAdaptedStructureOnlyWithoutModeFeedbackNetwork4K() =>
        RunNativeAdapter(
            NetworkChunks(_utf8, SegmentSize),
            new HtmlParserOptions
            {
                SkipDataText = true,
                SkipRawText = true,
                SkipScriptText = true,
                SkipPlaintext = true,
                SkipRCDataText = true,
                ShouldEmitAttribute = static (ref _, _) => false,
            },
            updateMode: false
        );

    private static async Task<Int32> RunNativeAdapter(
        IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
        HtmlParserOptions parserOptions,
        Boolean updateMode = true
    )
    {
        await using var source = new Utf8HtmlTokenSource(input);
        source.Configure(
            new HtmlTokenizerOptions(parserOptions),
            onToken: null,
            reportError: static (_, _) => { }
        );
        Action<HtmlParseMode> setMode = source.SetState;
        var count = 0;

        while (true)
        {
            while (source.TryMoveNext())
            {
                ref var token = ref source.Current;
                count++;
                if (updateMode)
                {
                    SetMode(token, setMode, tokenizer: null);
                }
                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    return count;
                }
            }

            await source.WaitForInputAsync(default).ConfigureAwait(false);
        }
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SingleChunk(Byte[] source)
    {
        await Task.CompletedTask;
        yield return source;
    }

    private static void SetMode(
        in AngleSharp.Html.Parser.Tokens.Struct.StructHtmlToken token,
        Action<HtmlParseMode> setSourceMode,
        HtmlTokenizer tokenizer
    )
    {
        if (token.Type != HtmlTokenType.StartTag)
        {
            return;
        }

        var mode = token.Name switch
        {
            var name when name == TagNames.Title || name == TagNames.Textarea =>
                HtmlParseMode.RCData,
            var name
                when name == TagNames.Style
                    || name == TagNames.Xmp
                    || name == TagNames.Iframe
                    || name == TagNames.NoEmbed
                    || name == TagNames.NoFrames => HtmlParseMode.Rawtext,
            var name when name == TagNames.Script => HtmlParseMode.Script,
            var name when name == TagNames.Plaintext => HtmlParseMode.Plaintext,
            _ => HtmlParseMode.PCData,
        };

        if (mode != HtmlParseMode.PCData)
        {
            if (tokenizer is null)
            {
                setSourceMode(mode);
            }
            else
            {
                tokenizer.State = mode;
            }
        }
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> NetworkChunks(
        Byte[] source,
        Int32 segmentSize,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        var buffer = ArrayPool<Byte>.Shared.Rent(segmentSize);
        try
        {
            for (var offset = 0; offset < source.Length; offset += segmentSize)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var length = Math.Min(segmentSize, source.Length - offset);
                source.AsSpan(offset, length).CopyTo(buffer);
                await Task.CompletedTask;
                yield return buffer.AsMemory(0, length);
            }
        }
        finally
        {
            ArrayPool<Byte>.Shared.Return(buffer);
        }
    }

    private sealed class CountingSink : IUtf8HtmlTokenSink
    {
        public Int32 Tokens { get; private set; }

        public void Reset() => Tokens = 0;

        public void Text(ReadOnlySpan<Byte> utf8) => Tokens++;

        public void StartTag(ReadOnlySpan<Byte> name, UInt64 hash) => Tokens++;

        public void Attribute(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> value) { }

        public void StartTagEnd(Boolean selfClosing) { }

        public void EndTag(ReadOnlySpan<Byte> name, UInt64 hash) => Tokens++;

        public void Comment(ReadOnlySpan<Byte> value) => Tokens++;

        public void Doctype(in Utf8DoctypeToken doctype) => Tokens++;

        public Boolean WantsAttribute(ReadOnlySpan<Byte> name) => true;

        public void EndOfFile() => Tokens++;
    }

    private sealed class YieldingSink : IUtf8HtmlTokenSink
    {
        public Utf8HtmlTokenizer Tokenizer { private get; set; } = null!;

        public Int32 Tokens { get; private set; }

        public void Reset() => Tokens = 0;

        public void Text(ReadOnlySpan<Byte> utf8) => Tokens++;

        public void StartTag(ReadOnlySpan<Byte> name, UInt64 hash) => Tokens++;

        public void Attribute(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> value) { }

        public void StartTagEnd(Boolean selfClosing) => Tokenizer.RequestYield();

        public void EndTag(ReadOnlySpan<Byte> name, UInt64 hash)
        {
            Tokens++;
            Tokenizer.RequestYield();
        }

        public void Comment(ReadOnlySpan<Byte> value)
        {
            Tokens++;
            Tokenizer.RequestYield();
        }

        public void Doctype(in Utf8DoctypeToken doctype)
        {
            Tokens++;
            Tokenizer.RequestYield();
        }

        public Boolean WantsAttribute(ReadOnlySpan<Byte> name) => true;

        public void EndOfFile() => Tokens++;
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

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) =>
            Read(buffer.AsSpan(offset, count));

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

        public override void Flush() { }

        public override Int64 Seek(Int64 offset, SeekOrigin origin) =>
            throw new NotSupportedException();

        public override void SetLength(Int64 value) => throw new NotSupportedException();

        public override void Write(Byte[] buffer, Int32 offset, Int32 count) =>
            throw new NotSupportedException();
    }
}
#endif
