#if NET10_0
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
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

namespace AngleSharp.Benchmarks;

internal static class TokenizerProfileHost
{
    private const Int32 LoopBatchSize = 8;

    public static void Run(String[] args)
    {
        var options = Options.Parse(args);
        var input = File.ReadAllBytes(options.Corpus);
        var scenario = CreateScenario(options.Lane, input, options.SegmentSize);

        var validation = scenario.Validate();
        var warmup = WarmupFor(scenario, options.Warmup);

        Directory.CreateDirectory(Path.GetDirectoryName(options.ReadyFile)!);
        File.WriteAllText(
            options.ReadyFile,
            $"pid={Environment.ProcessId};lane={options.Lane};corpus={Path.GetFileName(options.Corpus)};"
                + $"bytes={input.Length};runtime={Environment.Version};validation={validation};"
                + $"warmupIterations={warmup.Iterations}"
        );

        Console.WriteLine(
            $"READY pid={Environment.ProcessId} lane={options.Lane} corpus={Path.GetFileName(options.Corpus)} "
                + $"bytes={input.Length} validation={validation} warmupIterations={warmup.Iterations}"
        );
        Console.Out.Flush();

        while (!File.Exists(options.GoFile))
        {
            Thread.Sleep(10);
        }

        ProfileEvents.Log.ProfileStart(options.Lane, Path.GetFileName(options.Corpus), input.Length);
        var result = RunFor(scenario, options.Profile, warmup.Checksum);
        ProfileEvents.Log.ProfileStop(result.Iterations, result.Checksum);

        var resultLine =
            $"RESULT lane={options.Lane} corpus={Path.GetFileName(options.Corpus)} bytes={input.Length} "
                + $"runtime={Environment.Version} iterations={result.Iterations} checksum={result.Checksum} "
                + $"elapsedMs={result.Elapsed.TotalMilliseconds:F3} "
                + $"opsPerSecond={result.Iterations / result.Elapsed.TotalSeconds:F3} "
                + $"mbPerSecond={input.Length * (Double)result.Iterations / result.Elapsed.TotalSeconds / (1024 * 1024):F3} "
                + $"allocatedBytes={result.AllocatedBytes} "
                + $"allocatedBytesPerOp={result.AllocatedBytes / (Double)result.Iterations:F3} "
                + $"gen0={GC.CollectionCount(0)} gen1={GC.CollectionCount(1)} gen2={GC.CollectionCount(2)}";
        File.WriteAllText(options.ResultFile, resultLine);
        Console.WriteLine(resultLine);
    }

    private static IScenario CreateScenario(String lane, Byte[] input, Int32 segmentSize) =>
        lane.ToLowerInvariant() switch
        {
            "native-kernel" => new NativeKernelScenario(input, segmentSize),
            "native-dom" => new NativeDomScenario(input, segmentSize),
            "mature-dom" => new MatureDomScenario(input, segmentSize),
            _ => throw new ArgumentException($"Unknown profile lane '{lane}'.", nameof(lane)),
        };

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RunResult RunFor(
        IScenario scenario,
        TimeSpan duration,
        Int64 initialChecksum
    )
    {
        var iterations = 0L;
        var checksum = initialChecksum;
        var allocatedBefore = GC.GetAllocatedBytesForCurrentThread();
        var started = Stopwatch.GetTimestamp();
        var deadline = started + (Int64)(duration.TotalSeconds * Stopwatch.Frequency);

        do
        {
            for (var index = 0; index < LoopBatchSize; index++)
            {
                var value = scenario.RunOnce();
                checksum = unchecked((checksum * 397) ^ value);
                iterations++;
            }
        } while (Stopwatch.GetTimestamp() < deadline);

        var elapsed = Stopwatch.GetElapsedTime(started);
        var allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - allocatedBefore;
        GC.KeepAlive(scenario);
        return new RunResult(iterations, checksum, elapsed, allocatedBytes);
    }

    // Keep warmup samples out of reports rooted at RunFor while still exercising the exact same scenario.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RunResult WarmupFor(IScenario scenario, TimeSpan duration)
    {
        var iterations = 0L;
        var checksum = 0L;
        var allocatedBefore = GC.GetAllocatedBytesForCurrentThread();
        var started = Stopwatch.GetTimestamp();
        var deadline = started + (Int64)(duration.TotalSeconds * Stopwatch.Frequency);

        do
        {
            for (var index = 0; index < LoopBatchSize; index++)
            {
                var value = scenario.RunOnce();
                checksum = unchecked((checksum * 397) ^ value);
                iterations++;
            }
        } while (Stopwatch.GetTimestamp() < deadline);

        var elapsed = Stopwatch.GetElapsedTime(started);
        var allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - allocatedBefore;
        GC.KeepAlive(scenario);
        return new RunResult(iterations, checksum, elapsed, allocatedBytes);
    }

    private interface IScenario
    {
        Int32 RunOnce();

        String Validate();
    }

    private sealed class NativeKernelScenario(Byte[] input, Int32 segmentSize) : IScenario
    {
        public Int32 RunOnce()
        {
            var sink = new CountingSink();
            var tokenizer = new Utf8HtmlTokenizer(sink);
            for (var offset = 0; offset < input.Length; offset += segmentSize)
            {
                tokenizer.Write(input.AsSpan(offset, Math.Min(segmentSize, input.Length - offset)));
            }
            tokenizer.Complete();
            return sink.Checksum;
        }

        public String Validate() => $"kernel:{RunOnce()}";
    }

    private abstract class DomScenario(Byte[] input, Int32 segmentSize) : IScenario
    {
        protected readonly Byte[] Input = input;
        protected readonly Int32 SegmentSize = segmentSize;
        protected readonly IBrowsingContext Context = BrowsingContext.New(Configuration.Default);

        public abstract Int32 RunOnce();

        public String Validate()
        {
            using var document = Parse();
            var markup = document.DocumentElement.OuterHtml;
            return $"dom:{markup.Length}:{StableHash(markup):X16}";
        }

        protected abstract IDocument Parse();

        private static UInt64 StableHash(ReadOnlySpan<Char> value)
        {
            var hash = 14695981039346656037UL;
            foreach (var character in value)
            {
                hash = (hash ^ character) * 1099511628211UL;
            }
            return hash;
        }

        protected Int32 RunDomOnce()
        {
            using var document = Parse();
            var value = document.DocumentElement.ChildElementCount;
            GC.KeepAlive(document);
            return value;
        }
    }

    private sealed class NativeDomScenario(Byte[] input, Int32 segmentSize) :
        DomScenario(input, segmentSize)
    {
        private readonly IHtmlElementConstructionFactory _factory =
            BrowsingContext.New(Configuration.Default).GetService<IHtmlElementConstructionFactory>()
            ?? HtmlDomConstructionFactory.Instance;

        public override Int32 RunOnce() => RunDomOnce();

        protected override IDocument Parse()
        {
            var document = new HtmlDocument(Context, new TextSource(String.Empty));
            var source = new Utf8HtmlTokenSource(new SegmentedInput(Input, SegmentSize));
            try
            {
                using var builder = new HtmlDomBuilder(_factory, document, tokenSource: source);
                return builder.ParseAsync(new HtmlParserOptions()).GetAwaiter().GetResult();
            }
            finally
            {
                source.DisposeAsync().GetAwaiter().GetResult();
            }
        }
    }

    private sealed class MatureDomScenario(Byte[] input, Int32 segmentSize) :
        DomScenario(input, segmentSize)
    {
        private readonly HtmlParser _parser = new(BrowsingContext.New(Configuration.Default));

        public override Int32 RunOnce() => RunDomOnce();

        protected override IDocument Parse()
        {
            using var stream = new ProfileReadStream(Input, SegmentSize);
            return _parser
                .ParseDocumentAsync(
                    stream,
                    HtmlStreamSourceMode.Streaming,
                    Encoding.UTF8
                )
                .GetAwaiter()
                .GetResult();
        }
    }

    private sealed class SegmentedInput(Byte[] input, Int32 segmentSize) :
        IAsyncEnumerable<ReadOnlyMemory<Byte>>
    {
        public IAsyncEnumerator<ReadOnlyMemory<Byte>> GetAsyncEnumerator(
            CancellationToken cancellationToken = default
        ) => new Enumerator(input, segmentSize, cancellationToken);

        private sealed class Enumerator(
            Byte[] input,
            Int32 segmentSize,
            CancellationToken cancellationToken
        ) : IAsyncEnumerator<ReadOnlyMemory<Byte>>
        {
            private Int32 _offset;

            public ReadOnlyMemory<Byte> Current { get; private set; }

            public ValueTask<Boolean> MoveNextAsync()
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (_offset >= input.Length)
                {
                    Current = default;
                    return ValueTask.FromResult(false);
                }

                var length = Math.Min(segmentSize, input.Length - _offset);
                Current = input.AsMemory(_offset, length);
                _offset += length;
                return ValueTask.FromResult(true);
            }

            public ValueTask DisposeAsync() => ValueTask.CompletedTask;
        }
    }

    private sealed class ProfileReadStream(Byte[] input, Int32 maxReadSize) : Stream
    {
        private Int32 _position;

        public override Boolean CanRead => true;
        public override Boolean CanSeek => false;
        public override Boolean CanWrite => false;
        public override Int64 Length => input.Length;
        public override Int64 Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) =>
            Read(buffer.AsSpan(offset, count));

        public override Int32 Read(Span<Byte> buffer)
        {
            var length = Math.Min(Math.Min(buffer.Length, maxReadSize), input.Length - _position);
            if (length <= 0)
            {
                return 0;
            }

            input.AsSpan(_position, length).CopyTo(buffer);
            _position += length;
            return length;
        }

        public override ValueTask<Int32> ReadAsync(
            Memory<Byte> buffer,
            CancellationToken cancellationToken = default
        ) => ValueTask.FromResult(Read(buffer.Span));

        public override void Flush() { }
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(Int64 value) => throw new NotSupportedException();
        public override void Write(Byte[] buffer, Int32 offset, Int32 count) =>
            throw new NotSupportedException();
    }

    private sealed class CountingSink : IUtf8HtmlTokenSink
    {
        private Int32 _checksum;

        public Int32 Checksum => _checksum;

        public void Text(ReadOnlySpan<Byte> utf8) => Fold(1, utf8);

        public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
        {
            Fold(2, name.Verbatim);
            return Utf8HtmlStartTagCapture.Attributes;
        }

        public Boolean WantsAttribute(Utf8HtmlName name)
        {
            Fold(3, name.Verbatim);
            return true;
        }

        public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value)
        {
            Fold(4, name.Verbatim);
            Fold(5, value);
        }

        public void StartTagEnd(Boolean selfClosing) =>
            _checksum = unchecked((_checksum * 31) + (selfClosing ? 7 : 6));

        public void EndTag(Utf8HtmlName name) => Fold(8, name.Verbatim);

        public void Comment(ReadOnlySpan<Byte> utf8) => Fold(9, utf8);

        public void Doctype(ReadOnlySpan<Byte> utf8) => Fold(10, utf8);

        public void EndOfFile() => _checksum = unchecked((_checksum * 31) + 11);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Fold(Int32 kind, ReadOnlySpan<Byte> value)
        {
            var edge = value.IsEmpty ? 0 : value[0] | (value[^1] << 8);
            _checksum = unchecked((((_checksum * 31) + kind) * 31 + value.Length) * 31 + edge);
        }
    }

    [EventSource(Name = "AngleSharp-Tokenizer-Profile")]
    private sealed class ProfileEvents : EventSource
    {
        public static readonly ProfileEvents Log = new();

        [Event(1, Level = EventLevel.Informational)]
        public void ProfileStart(String lane, String corpus, Int32 bytes) =>
            WriteEvent(1, lane, corpus, bytes);

        [Event(2, Level = EventLevel.Informational)]
        public void ProfileStop(Int64 iterations, Int64 checksum) =>
            WriteEvent(2, iterations, checksum);
    }

    private sealed record Options(
        String Lane,
        String Corpus,
        Int32 SegmentSize,
        TimeSpan Warmup,
        TimeSpan Profile,
        String ReadyFile,
        String GoFile,
        String ResultFile
    )
    {
        public static Options Parse(String[] args)
        {
            var values = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            for (var index = 0; index < args.Length; index += 2)
            {
                if (index + 1 >= args.Length || !args[index].StartsWith("--", StringComparison.Ordinal))
                {
                    throw new ArgumentException("Profile-host arguments must be --name value pairs.");
                }
                values[args[index][2..]] = args[index + 1];
            }

            return new Options(
                Required("lane"),
                Path.GetFullPath(Required("corpus")),
                Int32.Parse(Value("segment-size", "4096")),
                TimeSpan.FromSeconds(Double.Parse(Value("warmup-seconds", "5"))),
                TimeSpan.FromSeconds(Double.Parse(Value("profile-seconds", "15"))),
                Path.GetFullPath(Required("ready-file")),
                Path.GetFullPath(Required("go-file")),
                Path.GetFullPath(Required("result-file"))
            );

            String Required(String name) =>
                values.TryGetValue(name, out var value)
                    ? value
                    : throw new ArgumentException($"Missing --{name}.");

            String Value(String name, String fallback) =>
                values.TryGetValue(name, out var value) ? value : fallback;
        }
    }

    private readonly record struct RunResult(
        Int64 Iterations,
        Int64 Checksum,
        TimeSpan Elapsed,
        Int64 AllocatedBytes
    );
}
#endif
