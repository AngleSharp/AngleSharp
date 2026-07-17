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
/// UTF-8 token adapter. All lanes publish the same ordinary mutable AngleSharp DOM.
/// </summary>
[MemoryDiagnoser]
public class Utf8MutableDomBenchmark
{
    private const Int32 NetworkBufferSize = 4096;

    private IBrowsingContext _context = null!;
    private IHtmlElementConstructionFactory _factory = null!;
    private HtmlParser _parser = null!;
    private Byte[] _utf8 = null!;
    private String _expectedMarkup = null!;

    [Params("page.html", "nbc.html", "utf8_edu.bin", "spiegel.html")]
    public String CorpusFile { get; set; } = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        _utf8 = File.ReadAllBytes(CorpusFile);
        _context = BrowsingContext.New(Configuration.Default);
        _factory = _context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
        _parser = new HtmlParser(_context);

        using var expected = _parser.ParseDocument(Encoding.UTF8.GetString(_utf8));
        _expectedMarkup = expected.DocumentElement.OuterHtml;
        using var actual = await ParseUtf8Async(SingleChunk(_utf8)).ConfigureAwait(false);
        if (!String.Equals(actual.DocumentElement.OuterHtml, _expectedMarkup, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Arena-backed UTF-8 mutable DOM differs from the mature parser.");
        }
    }

    [Benchmark(Baseline = true), BenchmarkCategory("Network4K")]
    public async Task<Int32> AccumulatingUtf16Network4K()
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await _parser.ParseDocumentAsync(stream, default).ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Network4K")]
    public async Task<Int32> BoundedUtf16Network4K()
    {
        using var stream = new NetworkReadStream(_utf8, NetworkBufferSize);
        using var document = await _parser
            .ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming, Encoding.UTF8)
            .ConfigureAwait(false);
        return document.All.Length;
    }

    [Benchmark, BenchmarkCategory("Network4K")]
    public async Task<Int32> NativeUtf8Network4K()
    {
        using var document = await ParseUtf8Async(NetworkChunks(_utf8, NetworkBufferSize)).ConfigureAwait(false);
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

    private async Task<IDocument> ParseUtf8Async(IAsyncEnumerable<ReadOnlyMemory<Byte>> input)
    {
        var document = new HtmlDocument(_context, new TextSource(String.Empty));
        await using var tokenSource = new Utf8HtmlTokenSource(input);
        using var builder = new HtmlDomBuilder(_factory, document, tokenSource: tokenSource);
        return await builder.ParseAsync(new HtmlParserOptions()).ConfigureAwait(false);
    }

    private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SingleChunk(Byte[] source)
    {
        await Task.CompletedTask;
        yield return source;
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
