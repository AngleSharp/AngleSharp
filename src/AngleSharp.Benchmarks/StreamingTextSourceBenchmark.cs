#if NET8_0_OR_GREATER
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;

namespace AngleSharp.Benchmarks;

[MemoryDiagnoser, ShortRunJob]
public class StreamingTextSourceBenchmark
{
    private const Int32 BufferSize = 4096;
    private readonly HtmlParser _parser = new();
    private Byte[] _utf8 = null!;

    [GlobalSetup]
    public void Setup() => _utf8 = File.ReadAllBytes("page.html");

    [Benchmark(Baseline = true)]
    public async Task<Int32> AccumulatingSource()
    {
        using var stream = new NetworkReadStream(_utf8, BufferSize);
        using var document = await _parser.ParseDocumentAsync(stream).ConfigureAwait(false);
        return document.DocumentElement.ChildElementCount;
    }

    [Benchmark]
    public async Task<Int32> BoundedSource()
    {
        using var stream = new NetworkReadStream(_utf8, BufferSize);
        using var document = await _parser.ParseDocumentAsync(
            stream,
            HtmlStreamSourceMode.Streaming,
            System.Text.Encoding.UTF8).ConfigureAwait(false);
        return document.DocumentElement.ChildElementCount;
    }

    [Benchmark]
    public async Task<Int32> AutomaticBoundedSource()
    {
        using var stream = new NetworkReadStream(_utf8, BufferSize);
        using var document = await _parser.ParseDocumentAsync(
            stream,
            HtmlStreamSourceMode.Streaming).ConfigureAwait(false);
        return document.DocumentElement.ChildElementCount;
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
