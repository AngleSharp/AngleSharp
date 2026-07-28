namespace AngleSharp.Benchmarks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using Common;
using Text;

/// <summary>
/// Settles how <c>BaseTokenizer.ReadCharFromSource</c> should dispatch to the concrete
/// text source: the historical null-check chain over typed fields, a cached byte enum
/// plus a cast to the sealed type, a switch on the type pattern directly, the same enum
/// with an unchecked cast, or plain interface dispatch.
/// </summary>
[MemoryDiagnoser, MediumRunJob]
public class SourceDispatchBenchmark
{
    private const Int32 Reads = 8192;

    public enum SourceKind
    {
        ReadOnlyMemory,
        String,
#if NET8_0_OR_GREATER
        Bytes,
#endif
        Custom,
    }

    public static IEnumerable<SourceKind> SourceKinds => Enum.GetValues(typeof(SourceKind)).Cast<SourceKind>();

    [ParamsSource(nameof(SourceKinds))]
    public SourceKind Kind { get; set; }

    private IReadOnlyTextSource _source;
    private ChainReader _chain;
    private EnumReader _enum;
    private TypeSwitchReader _typeSwitch;
    private UnsafeEnumReader _unsafeEnum;
    private InterfaceReader _iface;

    [GlobalSetup]
    public void Setup()
    {
        var html = String.Concat(Enumerable.Repeat("<p>Hello World.<br>How are you?</p>", 512));

        _source = Kind switch
        {
            SourceKind.ReadOnlyMemory => new ReadOnlyMemoryTextSource(html.AsMemory()),
            SourceKind.String => new StringTextSource(html),
#if NET8_0_OR_GREATER
            SourceKind.Bytes => new ReadOnlyByteTextSource(Encoding.UTF8.GetBytes(html), Encoding.UTF8),
#endif
            SourceKind.Custom => new CustomTextSource(html),
            _ => throw new ArgumentOutOfRangeException(nameof(Kind)),
        };

        _chain = new ChainReader(_source);
        _enum = new EnumReader(_source);
        _typeSwitch = new TypeSwitchReader(_source);
        _unsafeEnum = new UnsafeEnumReader(_source);
        _iface = new InterfaceReader(_source);
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = Reads)]
    public Int32 NullCheckChain()
    {
        _source.Index = 0;
        return _chain.Consume(Reads);
    }

    [Benchmark(OperationsPerInvoke = Reads)]
    public Int32 EnumThenCast()
    {
        _source.Index = 0;
        return _enum.Consume(Reads);
    }

    [Benchmark(OperationsPerInvoke = Reads)]
    public Int32 TypePatternSwitch()
    {
        _source.Index = 0;
        return _typeSwitch.Consume(Reads);
    }

    [Benchmark(OperationsPerInvoke = Reads)]
    public Int32 EnumThenUnsafeAs()
    {
        _source.Index = 0;
        return _unsafeEnum.Consume(Reads);
    }

    [Benchmark(OperationsPerInvoke = Reads)]
    public Int32 InterfaceDispatch()
    {
        _source.Index = 0;
        return _iface.Consume(Reads);
    }

    #region Dispatch variants

    private enum TextSourceKind : Byte
    {
        Unknown = 0,
        ReadOnlyMemory,
        String,
        CharArray,
        Writable,
        Streaming,
#if NET8_0_OR_GREATER
        Bytes,
#endif
    }

    private static TextSourceKind Classify(IReadOnlyTextSource source) => source switch
    {
        ReadOnlyMemoryTextSource => TextSourceKind.ReadOnlyMemory,
        StringTextSource => TextSourceKind.String,
        CharArrayTextSource => TextSourceKind.CharArray,
        StringBuilderTextSource => TextSourceKind.Writable,
        WindowTextSource => TextSourceKind.Streaming,
#if NET8_0_OR_GREATER
        ReadOnlyByteTextSource => TextSourceKind.Bytes,
#endif
        _ => TextSourceKind.Unknown,
    };

    /// <summary>Upstream today: nullable typed fields, three of six sources covered.</summary>
    private sealed class ChainReader
    {
        private readonly IReadOnlyTextSource _source;
        private readonly StringBuilderTextSource _wts;
        private readonly CharArrayTextSource _cats;
        private readonly ReadOnlyMemoryTextSource _roms;

        public ChainReader(IReadOnlyTextSource source)
        {
            _source = source;
            if (source is StringBuilderTextSource wts) _wts = wts;
            else if (source is CharArrayTextSource cats) _cats = cats;
            else if (source is ReadOnlyMemoryTextSource roms) _roms = roms;
        }

        private Char Read()
        {
            if (_wts is not null) return _wts.ReadCharacter();
            if (_cats is not null) return _cats.ReadCharacter();
            if (_roms is not null) return _roms.ReadCharacter();
            return _source.ReadCharacter();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Int32 Consume(Int32 count)
        {
            var hash = 0;
            for (var i = 0; i < count; i++)
            {
                var c = Read();
                hash = c == '<' ? (hash * 31) + Read() : (hash * 31) + c;
            }
            return hash;
        }
    }

    /// <summary>Cached byte enum, then a checked cast to the sealed type.</summary>
    private sealed class EnumReader
    {
        private readonly IReadOnlyTextSource _source;
        private readonly TextSourceKind _kind;

        public EnumReader(IReadOnlyTextSource source)
        {
            _source = source;
            _kind = Classify(source);
        }

        private Char Read() => _kind switch
        {
            TextSourceKind.ReadOnlyMemory => ((ReadOnlyMemoryTextSource)_source).ReadCharacter(),
            TextSourceKind.String => ((StringTextSource)_source).ReadCharacter(),
            TextSourceKind.CharArray => ((CharArrayTextSource)_source).ReadCharacter(),
            TextSourceKind.Writable => ((StringBuilderTextSource)_source).ReadCharacter(),
            TextSourceKind.Streaming => ((WindowTextSource)_source).ReadCharacter(),
#if NET8_0_OR_GREATER
            TextSourceKind.Bytes => ((ReadOnlyByteTextSource)_source).ReadCharacter(),
#endif
            _ => _source.ReadCharacter(),
        };

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Int32 Consume(Int32 count)
        {
            var hash = 0;
            for (var i = 0; i < count; i++)
            {
                var c = Read();
                hash = c == '<' ? (hash * 31) + Read() : (hash * 31) + c;
            }
            return hash;
        }
    }

    /// <summary>Same coverage, no extra field: switch on the type pattern.</summary>
    private sealed class TypeSwitchReader
    {
        private readonly IReadOnlyTextSource _source;

        public TypeSwitchReader(IReadOnlyTextSource source) => _source = source;

        private Char Read() => _source switch
        {
            ReadOnlyMemoryTextSource s => s.ReadCharacter(),
            StringTextSource s => s.ReadCharacter(),
            CharArrayTextSource s => s.ReadCharacter(),
            StringBuilderTextSource s => s.ReadCharacter(),
            WindowTextSource s => s.ReadCharacter(),
#if NET8_0_OR_GREATER
            ReadOnlyByteTextSource s => s.ReadCharacter(),
#endif
            var s => s.ReadCharacter(),
        };

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Int32 Consume(Int32 count)
        {
            var hash = 0;
            for (var i = 0; i < count; i++)
            {
                var c = Read();
                hash = c == '<' ? (hash * 31) + Read() : (hash * 31) + c;
            }
            return hash;
        }
    }

    /// <summary>Enum plus an unchecked cast; sound because the kind came from the same object.</summary>
    private sealed class UnsafeEnumReader
    {
        private readonly IReadOnlyTextSource _source;
        private readonly TextSourceKind _kind;

        public UnsafeEnumReader(IReadOnlyTextSource source)
        {
            _source = source;
            _kind = Classify(source);
        }

        private Char Read() => _kind switch
        {
            TextSourceKind.ReadOnlyMemory => Unsafe.As<ReadOnlyMemoryTextSource>(_source).ReadCharacter(),
            TextSourceKind.String => Unsafe.As<StringTextSource>(_source).ReadCharacter(),
            TextSourceKind.CharArray => Unsafe.As<CharArrayTextSource>(_source).ReadCharacter(),
            TextSourceKind.Writable => Unsafe.As<StringBuilderTextSource>(_source).ReadCharacter(),
            TextSourceKind.Streaming => Unsafe.As<WindowTextSource>(_source).ReadCharacter(),
#if NET8_0_OR_GREATER
            TextSourceKind.Bytes => Unsafe.As<ReadOnlyByteTextSource>(_source).ReadCharacter(),
#endif
            _ => _source.ReadCharacter(),
        };

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Int32 Consume(Int32 count)
        {
            var hash = 0;
            for (var i = 0; i < count; i++)
            {
                var c = Read();
                hash = c == '<' ? (hash * 31) + Read() : (hash * 31) + c;
            }
            return hash;
        }
    }

    /// <summary>No specialization: this is what Dynamic PGO has to rescue on its own.</summary>
    private sealed class InterfaceReader
    {
        private readonly IReadOnlyTextSource _source;

        public InterfaceReader(IReadOnlyTextSource source) => _source = source;

        private Char Read() => _source.ReadCharacter();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Int32 Consume(Int32 count)
        {
            var hash = 0;
            for (var i = 0; i < count; i++)
            {
                var c = Read();
                hash = c == '<' ? (hash * 31) + Read() : (hash * 31) + c;
            }
            return hash;
        }
    }

    #endregion

    #region Stand-ins for the internal sources

    /// <summary>Shaped like the internal WritableTextSource, which benchmarks cannot reference.</summary>
    private sealed class StringBuilderTextSource : IReadOnlyTextSource
    {
        private readonly StringBuilder _content;
        private Int32 _index;

        public StringBuilderTextSource(String content) => _content = new StringBuilder(content);

        public String Text => _content.ToString();
        public Int32 Length => _content.Length;
        public Encoding CurrentEncoding { get; set; } = Encoding.UTF8;
        public Int32 Index { get => _index; set => _index = value; }
        public Char this[Int32 index] => _content[index];

        public Char ReadCharacter() => _index < _content.Length ? _content[_index++] : Symbols.EndOfFile;
        public String ReadCharacters(Int32 characters) => throw new NotSupportedException();
        public StringOrMemory ReadMemory(Int32 characters) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAsync(Int32 length, System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAllAsync(System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public Boolean TryGetContentLength(out Int32 length) { length = _content.Length; return true; }
        public void Dispose() { }
    }

    /// <summary>Shaped like the internal StreamingTextSource, which benchmarks cannot reference.</summary>
    private sealed class WindowTextSource : IReadOnlyTextSource
    {
        private readonly Char[] _buffer;
        private readonly Int32 _length;
        private readonly Int32 _bufferStart;
        private Int32 _index;

        public WindowTextSource(String content)
        {
            _buffer = content.ToCharArray();
            _length = _buffer.Length;
            _bufferStart = 0;
        }

        public String Text => new(_buffer, 0, _length);
        public Int32 Length => _length;
        public Encoding CurrentEncoding { get; set; } = Encoding.UTF8;
        public Int32 Index { get => _index; set => _index = value; }
        public Char this[Int32 index] => _buffer[index - _bufferStart];

        public Char ReadCharacter()
        {
            if (_index < _length) return _buffer[_index++ - _bufferStart];
            _index++;
            return Symbols.EndOfFile;
        }

        public String ReadCharacters(Int32 characters) => throw new NotSupportedException();
        public StringOrMemory ReadMemory(Int32 characters) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAsync(Int32 length, System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAllAsync(System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public Boolean TryGetContentLength(out Int32 length) { length = _length; return true; }
        public void Dispose() { }
    }

    /// <summary>A third-party implementation, so the fallback arm is genuinely exercised.</summary>
    private sealed class CustomTextSource : IReadOnlyTextSource
    {
        private readonly String _content;
        private Int32 _index;

        public CustomTextSource(String content) => _content = content;

        public String Text => _content;
        public Int32 Length => _content.Length;
        public Encoding CurrentEncoding { get; set; } = Encoding.UTF8;
        public Int32 Index { get => _index; set => _index = value; }
        public Char this[Int32 index] => _content[index];

        public Char ReadCharacter() => _index < _content.Length ? _content[_index++] : Symbols.EndOfFile;
        public String ReadCharacters(Int32 characters) => throw new NotSupportedException();
        public StringOrMemory ReadMemory(Int32 characters) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAsync(Int32 length, System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public System.Threading.Tasks.Task PrefetchAllAsync(System.Threading.CancellationToken cancellationToken) => throw new NotSupportedException();
        public Boolean TryGetContentLength(out Int32 length) { length = _content.Length; return true; }
        public void Dispose() { }
    }

    #endregion
}
