namespace AngleSharp.Common;

using System;

public struct StringOrMemory
{
    private String? _string;
    private readonly ReadOnlyMemory<Char> _memory;

    public StringOrMemory(String str)
    {
        _memory = str.AsMemory();
        _string = str;
    }

    public StringOrMemory(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
        _string = null;
    }

    public readonly ReadOnlyMemory<Char> Memory => _memory;

    public String String
    {
        get
        {
            if (_memory.IsEmpty)
            {
                return String.Empty;
            }

            // ToString here checks if pointer is already a string and also checks case when length is same as original string
            // important for cached string, usually from dictionaries
            return _string ??= _memory.Span.ToString();
        }
    }

    public Int32 Length => _memory.Length;
    public Char this[Int32 i] => _memory.Span[i];
    public Boolean IsNullOrEmpty => _memory.IsEmpty;
    public static StringOrMemory Empty => new StringOrMemory(String.Empty);

    public static implicit operator StringOrMemory(String str) => new StringOrMemory(str);
    public static implicit operator StringOrMemory(ReadOnlyMemory<Char> memory) => new StringOrMemory(memory);
    public static implicit operator ReadOnlyMemory<Char>(StringOrMemory str) => str.Memory;
    public static implicit operator ReadOnlySpan<Char>(StringOrMemory str) => str.Memory.Span;

    public static Boolean operator ==(StringOrMemory left, String right) => left.Memory.Span.SequenceEqual(right.AsSpan());
    public static Boolean operator ==(StringOrMemory left, StringOrMemory right) => left.Memory.Span.SequenceEqual(right.Memory.Span);
    public static Boolean operator ==(StringOrMemory left, ReadOnlyMemory<Char> right) => left.Memory.Span.SequenceEqual(right.Span);
    public static Boolean operator ==(StringOrMemory left, ReadOnlySpan<Char> right) => left.Memory.Span.SequenceEqual(right);

    public static Boolean operator !=(StringOrMemory left, String right) => !(left == right);
    public static Boolean operator !=(StringOrMemory left, StringOrMemory right) => !(left == right);
    public static Boolean operator !=(StringOrMemory left, ReadOnlyMemory<Char> right) => !(left == right);
    public static Boolean operator !=(StringOrMemory left, ReadOnlySpan<Char> right) => !(left == right);

    public Boolean Equals(StringOrMemory other)
    {
        return _memory.Span.SequenceEqual(other._memory.Span);
    }

    public override Boolean Equals(Object? obj)
    {
        return obj is StringOrMemory other && Equals(other);
    }

    public override Int32 GetHashCode()
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        return String.GetHashCode(_memory.Span);
#else
        return String.GetHashCode();
#endif
    }

    public StringOrMemory Replace(Char target, Char replacement)
    {
#if NET8_0_OR_GREATER
        if (_memory.Length < 128)
        {
            Span<Char> tmp = stackalloc Char[_memory.Length];
            _memory.Span.Replace(tmp, target, replacement);
            return new StringOrMemory(tmp.ToString());
        }
#endif
        return new StringOrMemory(String.Replace(target, replacement));
    }

    public override String ToString()
    {
        return String;
    }
}

static class StringOrMemoryExtensions
{
    public static Boolean Is(this StringOrMemory str, StringOrMemory other) => str == other;

    public static Boolean Is(this StringOrMemory str, String other) => str == other;

    public static Boolean Isi(this StringOrMemory str, StringOrMemory other) =>
        str.Memory.Span.Equals(other.Memory.Span, StringComparison.OrdinalIgnoreCase);

    public static Boolean Isi(this StringOrMemory str, String other) =>
        str.Memory.Span.Equals(other.AsSpan(), StringComparison.OrdinalIgnoreCase);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c, StringOrMemory d) =>
        str.Is(a) || str.Is(b) || str.Is(c) || str.Is(d);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c) =>
        str.Is(a) || str.Is(b) || str.Is(c);

    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b) => str.Is(a) || str.Is(b);

    public static Boolean StartsWith(this StringOrMemory str, String test, StringComparison comparison) =>
        str.Memory.Span.StartsWith(test.AsSpan(), comparison);

    public static Boolean Equals(this StringOrMemory str, String test, StringComparison comparison) =>
        str.Memory.Span.Equals(test.AsSpan(), comparison);
}