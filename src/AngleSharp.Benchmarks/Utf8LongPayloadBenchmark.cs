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
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

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

    private async Task<IDocument> ParseNativeAsync()
    {
        var document = new HtmlDocument(_context, new TextSource(String.Empty));
        await using var tokenSource = new Utf8HtmlTokenSource(NetworkChunks(_utf8, NetworkBufferSize));
        using var builder = new HtmlDomBuilder(_factory, document, tokenSource: tokenSource);
        return await builder.ParseAsync(new HtmlParserOptions()).ConfigureAwait(false);
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
}

public enum Utf8PayloadProfile
{
    Ascii,
    TwoByte,
    FourByte,
}
#endif
