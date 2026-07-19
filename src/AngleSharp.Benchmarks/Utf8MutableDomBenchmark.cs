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
using AngleSharp.Html.Parser;
using AngleSharp.Html.Parser.Utf8;
using AngleSharp.Text;
using BenchmarkDotNet.Attributes;

namespace AngleSharp.Benchmarks;

/// <summary>
/// Compares mutable DOM construction from raw UTF-8 using the mature decoder/tokenizer and the arena-backed native
/// UTF-8 token adapter. Full-payload lanes publish the same ordinary mutable AngleSharp DOM. Payload-capture lanes
/// publish the same intentionally reduced mutable DOM from both tokenizers.
/// </summary>
[MemoryDiagnoser]
public class Utf8MutableDomBenchmark
{
    private const Int32 NetworkBufferSize = 4096;

    private static readonly HtmlParserOptions SkipDataTextOptions = new()
    {
        SkipDataText = true,
    };

    private static readonly HtmlParserOptions SkipRCDataTextOptions = new()
    {
        SkipRCDataText = true,
    };

    private static readonly HtmlParserOptions QueryAttributeOptions = new()
    {
        ShouldEmitAttribute = static (ref _, name) =>
            name.Span is "class" or "dt-eid" or "dt-params" or "href" or "src" or "alt",
    };

    private static readonly HtmlParserOptions StructureOnlyOptions = new()
    {
        SkipComments = true,
        SkipPlaintext = true,
        SkipRCDataText = true,
        SkipCDATA = true,
        SkipProcessingInstructions = true,
        SkipDataText = true,
        SkipScriptText = true,
        SkipRawText = true,
        ShouldEmitAttribute = static (ref _, _) => false,
    };

    private IBrowsingContext _context = null!;
    private IHtmlElementConstructionFactory _factory = null!;
    private HtmlParser _parser = null!;
    private HtmlParser _skipDataTextParser = null!;
    private HtmlParser _skipRCDataTextParser = null!;
    private HtmlParser _queryAttributeParser = null!;
    private HtmlParser _structureOnlyParser = null!;
    private Byte[] _utf8 = null!;
    private String _expectedMarkup = null!;

    [Params("page.html", "nbc.html", "utf8_edu.bin")]
    public String CorpusFile { get; set; } = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        _utf8 = File.ReadAllBytes(ResolveCorpusPath(CorpusFile));
        _context = BrowsingContext.New(Configuration.Default);
        _factory = _context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
        _parser = new HtmlParser(_context);
        _skipDataTextParser = new HtmlParser(SkipDataTextOptions, _context);
        _skipRCDataTextParser = new HtmlParser(SkipRCDataTextOptions, _context);
        _queryAttributeParser = new HtmlParser(QueryAttributeOptions, _context);
        _structureOnlyParser = new HtmlParser(StructureOnlyOptions, _context);

        using var expected = _parser.ParseDocument(Encoding.UTF8.GetString(_utf8));
        _expectedMarkup = expected.DocumentElement.OuterHtml;
        using var actual = await ParseUtf8Async(SingleChunk(_utf8)).ConfigureAwait(false);
        if (!String.Equals(actual.DocumentElement.OuterHtml, _expectedMarkup, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Arena-backed UTF-8 mutable DOM differs from the mature parser.");
        }

        await VerifyReducedDomAsync(
                _skipDataTextParser,
                SkipDataTextOptions,
                nameof(SkipDataTextOptions)
            )
            .ConfigureAwait(false);
        await VerifyReducedDomAsync(
                _skipRCDataTextParser,
                SkipRCDataTextOptions,
                nameof(SkipRCDataTextOptions)
            )
            .ConfigureAwait(false);
        await VerifyReducedDomAsync(
                _queryAttributeParser,
                QueryAttributeOptions,
                nameof(QueryAttributeOptions)
            )
            .ConfigureAwait(false);
        await VerifyReducedDomAsync(
                _structureOnlyParser,
                StructureOnlyOptions,
                nameof(StructureOnlyOptions)
            )
            .ConfigureAwait(false);
    }

    [Benchmark(Baseline = true), BenchmarkCategory("Network4K")]
    public async Task<Int32> AccumulatingUtf16Network4K()
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await _parser.ParseDocumentAsync(stream, default).ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public async Task<Int32> BoundedUtf16Network4K()
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await _parser
            .ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming, Encoding.UTF8)
            .ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public async Task<Int32> NativeUtf8Network4K()
    {
        using var document = await ParseUtf8Async(NetworkChunks(_utf8, NetworkBufferSize)).ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> BoundedUtf16SkipDataTextNetwork4K() =>
        ParseBoundedUtf16Async(_skipDataTextParser);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> NativeUtf8SkipDataTextNetwork4K() =>
        ParseNativeUtf8Async(SkipDataTextOptions);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> BoundedUtf16SkipRCDataTextNetwork4K() =>
        ParseBoundedUtf16Async(_skipRCDataTextParser);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> NativeUtf8SkipRCDataTextNetwork4K() =>
        ParseNativeUtf8Async(SkipRCDataTextOptions);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> BoundedUtf16QueryAttributesNetwork4K() =>
        ParseBoundedUtf16Async(_queryAttributeParser);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> NativeUtf8QueryAttributesNetwork4K() =>
        ParseNativeUtf8Async(QueryAttributeOptions);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> BoundedUtf16StructureOnlyNetwork4K() =>
        ParseBoundedUtf16Async(_structureOnlyParser);

    [Benchmark, BenchmarkCategory("Network4K", "PayloadCapture")]
    public Task<Int32> NativeUtf8StructureOnlyNetwork4K() =>
        ParseNativeUtf8Async(StructureOnlyOptions);

    [Benchmark, BenchmarkCategory("Network4K")]
    public async Task<Int32> TrustedUtf8Network4K()
    {
        using var document = await ParseUtf8Async(
                NetworkChunks(_utf8, NetworkBufferSize),
                Utf8InputContract.WellFormedUtf8
            )
            .ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Contiguous")]
    public Int32 MatureContiguousUtf16()
    {
        using var document = _parser.ParseDocument(Encoding.UTF8.GetString(_utf8));
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Contiguous")]
    public async Task<Int32> ArenaUtf8Contiguous()
    {
        using var document = await ParseUtf8Async(SingleChunk(_utf8)).ConfigureAwait(false);
        return document.All.Length;
    }

    private async Task<IDocument> ParseUtf8Async(
        IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
        Utf8InputContract inputContract = Utf8InputContract.ArbitraryBytes,
        HtmlParserOptions options = default
    )
    {
        var document = new HtmlDocument(_context, new TextSource(String.Empty));
        await using var tokenSource = new Utf8HtmlTokenSource(input, inputContract);
        using var builder = new HtmlDomBuilder(_factory, document, tokenSource: tokenSource);
        return await builder.ParseAsync(options).ConfigureAwait(false);
    }

    private async Task<Int32> ParseBoundedUtf16Async(HtmlParser parser)
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await parser
            .ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming, Encoding.UTF8)
            .ConfigureAwait(false);
        return document.All.Length;
    }

    private async Task<Int32> ParseNativeUtf8Async(HtmlParserOptions options)
    {
        using var document = await ParseUtf8Async(
                NetworkChunks(_utf8, NetworkBufferSize),
                options: options
            )
            .ConfigureAwait(false);
        return document.All.Length;
    }

    private async Task VerifyReducedDomAsync(
        HtmlParser matureParser,
        HtmlParserOptions options,
        String lane)
    {
        using var matureStream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var mature = await matureParser
            .ParseDocumentAsync(matureStream, HtmlStreamSourceMode.Streaming, Encoding.UTF8)
            .ConfigureAwait(false);
        using var native = await ParseUtf8Async(
                NetworkChunks(_utf8, NetworkBufferSize),
                options: options
            )
            .ConfigureAwait(false);
        var nativeMarkup = native.DocumentElement.OuterHtml;
        var matureMarkup = mature.DocumentElement.OuterHtml;
        if (!String.Equals(nativeMarkup, matureMarkup, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                $"Arena-backed UTF-8 mutable DOM differs from the mature parser in {lane}: "
                    + DescribeDifference(matureMarkup, nativeMarkup)
            );
        }
    }

    private static String DescribeDifference(String mature, String native)
    {
        var commonLength = Math.Min(mature.Length, native.Length);
        var offset = 0;
        while (offset < commonLength && mature[offset] == native[offset])
        {
            offset++;
        }

        const Int32 Radius = 80;
        var start = Math.Max(0, offset - Radius);
        var matureLength = Math.Min(mature.Length - start, Radius * 2);
        var nativeLength = Math.Min(native.Length - start, Radius * 2);
        return $"first difference at {offset}; mature length {mature.Length}, native length {native.Length}; "
            + $"mature='{mature.Substring(start, matureLength)}'; "
            + $"native='{native.Substring(start, nativeLength)}'";
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SingleChunk(Byte[] source)
    {
        await Task.CompletedTask;
        yield return source;
    }

    private static String ResolveCorpusPath(String fileName)
    {
        var relativePath = fileName switch
        {
            "page.html" => Path.Combine("src", "AngleSharp.Benchmarks", fileName),
            "nbc.html" => Path.Combine("src", "AngleSharp.Core.Tests", "Pages", fileName),
            "utf8_edu.bin" => Path.Combine(
                "src",
                "AngleSharp.Core.Tests",
                "Resources",
                fileName
            ),
            _ => fileName,
        };
        for (
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            directory is not null;
            directory = directory.Parent
        )
        {
            var candidate = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidate))
            {
                return candidate;
            }
        }

        throw new FileNotFoundException($"Could not locate UTF-8 benchmark corpus '{fileName}'.");
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> NetworkChunks(
        Byte[] source,
        Int32 bufferSize,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
        try
        {
            for (var offset = 0; offset < source.Length;)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var length = Math.Min(bufferSize, source.Length - offset);
                source.AsSpan(offset, length).CopyTo(buffer);
                offset += length;
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
}
#endif
