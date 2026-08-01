namespace AngleSharp.Benchmarks;

using System;
using BenchmarkDotNet.Attributes;
using Text;

[MemoryDiagnoser, ShortRunJob]
public class TextSourceDispatchBenchmark
{
    private IReadOnlyTextSource _interfaceSource;
    private CachedTypeChecks _cachedTypeChecks;

    [Params(32, 4096)]
    public Int32 Length { get; set; }

    [Params(SourceKind.CharArray, SourceKind.Memory, SourceKind.String)]
    public SourceKind Source { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var text = new String('a', Length);
        _interfaceSource = CreateSource(Source, text);
        _cachedTypeChecks = new CachedTypeChecks(CreateSource(Source, text));
    }

    [Benchmark(Baseline = true)]
    public Int32 ManualTypeChecks()
    {
        _cachedTypeChecks.Reset();
        return _cachedTypeChecks.Read(Length);
    }

    [Benchmark]
    public Int32 InterfaceDispatch()
    {
        _interfaceSource.Index = 0;
        var checksum = 0;

        for (var i = 0; i < Length; i++)
        {
            checksum += _interfaceSource.ReadCharacter();
        }

        return checksum;
    }

    private static IReadOnlyTextSource CreateSource(SourceKind kind, String text) => kind switch
    {
        SourceKind.CharArray => new CharArrayTextSource(text.ToCharArray(), text.Length),
        SourceKind.Memory => new ReadOnlyMemoryTextSource(text.AsMemory()),
        SourceKind.String => new StringTextSource(text),
        _ => throw new ArgumentOutOfRangeException(nameof(kind)),
    };

    public enum SourceKind
    {
        CharArray,
        Memory,
        String,
    }

    private sealed class CachedTypeChecks
    {
        private readonly IReadOnlyTextSource _source;
        private readonly StringTextSource _firstConcreteSource;
        private readonly CharArrayTextSource _charArraySource;
        private readonly ReadOnlyMemoryTextSource _memorySource;

        public CachedTypeChecks(IReadOnlyTextSource source)
        {
            _source = source;
            // Represents the first cached concrete-source check in BaseTokenizer.
            _firstConcreteSource = null;
            _charArraySource = source as CharArrayTextSource;
            _memorySource = source as ReadOnlyMemoryTextSource;
        }

        public void Reset() => _source.Index = 0;

        public Int32 Read(Int32 length)
        {
            var checksum = 0;

            for (var i = 0; i < length; i++)
            {
                checksum += ReadCharacter();
            }

            return checksum;
        }

        private Char ReadCharacter()
        {
            if (_firstConcreteSource is not null)
            {
                return _firstConcreteSource.ReadCharacter();
            }

            if (_charArraySource is not null)
            {
                return _charArraySource.ReadCharacter();
            }

            if (_memorySource is not null)
            {
                return _memorySource.ReadCharacter();
            }

            return _source.ReadCharacter();
        }
    }
}
