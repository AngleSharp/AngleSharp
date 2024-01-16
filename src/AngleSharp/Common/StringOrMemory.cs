namespace AngleSharp.Common;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a string and equivalent memory representation of this string.
/// Prevents multiple allocations of string by caching it
/// </summary>
public struct StringOrMemory
{
    private readonly ReadOnlyMemory<Char> _memory;

    /// <summary>
    /// Creates new instance of <see cref="StringOrMemory"/> from string
    /// </summary>
    public StringOrMemory(String str)
    {
        _memory = str.AsMemory();
    }

    /// <summary>
    /// Creates new instance of <see cref="StringOrMemory"/> from read only memory
    /// </summary>
    public StringOrMemory(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
    }

    /// <summary>
    /// Returns memory representation of string
    /// </summary>
    public readonly ReadOnlyMemory<Char> Memory => _memory;

    /// <summary>
    /// Length of string
    /// </summary>
    public Int32 Length => _memory.Length;

    /// <summary>
    /// Returns character at specified index
    /// </summary>
    public Char this[Int32 i] => _memory.Span[i];

    /// <summary>
    /// Checks if string is null or empty
    /// </summary>
    public Boolean IsNullOrEmpty => _memory.IsEmpty;

    /// <summary>
    /// Static empty string instance
    /// </summary>
    public static StringOrMemory Empty => new(String.Empty);

    /// <summary>
    /// Converts string to <see cref="StringOrMemory"/> implicitly
    /// </summary>
    public static implicit operator StringOrMemory(String str) => new(str);

    /// <summary>
    /// Converts <see cref="ReadOnlyMemory&lt;Char&gt;"/> to string implicitly
    /// </summary>
    public static implicit operator StringOrMemory(ReadOnlyMemory<Char> memory) => new(memory);

    /// <summary>
    /// Converts <see cref="StringOrMemory"/> to <see cref="ReadOnlyMemory&lt;Char&gt;"/>
    /// </summary>
    public static implicit operator ReadOnlyMemory<Char>(StringOrMemory str) => str.Memory;

    /// <summary>
    /// Converts <see cref="StringOrMemory"/> to <see cref="ReadOnlySpan&lt;Char&gt;"/>
    /// </summary>
    public static implicit operator ReadOnlySpan<Char>(StringOrMemory str) => str.Memory.Span;

    /// <summary>
    /// Equality operator for <see cref="StringOrMemory"/> and <see cref="String"/>
    /// </summary>
    public static Boolean operator ==(StringOrMemory left, String right) =>
        left.Memory.Span.SequenceEqual(right.AsSpan());

    /// <summary>
    /// Equality operator for <see cref="StringOrMemory"/> and <see cref="StringOrMemory"/>
    /// </summary>
    public static Boolean operator ==(StringOrMemory left, StringOrMemory right) =>
        left.Memory.Span.SequenceEqual(right.Memory.Span);

    /// <summary>
    /// Equality operator for <see cref="StringOrMemory"/> and <see cref="ReadOnlyMemory&lt;Char&gt;"/>
    /// </summary>
    public static Boolean operator ==(StringOrMemory left, ReadOnlyMemory<Char> right) =>
        left.Memory.Span.SequenceEqual(right.Span);

    /// <summary>
    /// Equality operator for <see cref="StringOrMemory"/> and <see cref="ReadOnlySpan&lt;Char&gt;"/>
    /// </summary>
    public static Boolean operator ==(StringOrMemory left, ReadOnlySpan<Char> right) =>
        left.Memory.Span.SequenceEqual(right);

    /// <summary>
    /// Inequality operator for <see cref="StringOrMemory"/> and <see cref="String"/>
    /// </summary>
    public static Boolean operator !=(StringOrMemory left, String right) => !(left == right);

    /// <summary>
    /// Inequality operator for <see cref="StringOrMemory"/> and <see cref="StringOrMemory"/>
    /// </summary>
    public static Boolean operator !=(StringOrMemory left, StringOrMemory right) => !(left == right);

    /// <summary>
    /// Inequality operator for <see cref="StringOrMemory"/> and <see cref="ReadOnlyMemory&lt;Char&gt;"/>
    /// </summary>
    public static Boolean operator !=(StringOrMemory left, ReadOnlyMemory<Char> right) => !(left == right);

    /// <summary>
    /// Inequality operator for <see cref="StringOrMemory"/> and <see cref="ReadOnlySpan&lt;Char&gt;"/>
    /// </summary>
    public static Boolean operator !=(StringOrMemory left, ReadOnlySpan<Char> right) => !(left == right);

    /// <summary>
    /// CLR equals implementation
    /// </summary>
    public Boolean Equals(StringOrMemory other)
    {
        return
            _memory.Equals(other._memory) || // checks pointers (e.g. same string or array parts)
            _memory.Span.SequenceEqual(other._memory.Span);
    }

    /// <summary>
    /// CLR equals implementation
    /// </summary>
    public override Boolean Equals(Object? obj)
    {
        return obj is StringOrMemory other && Equals(other);
    }

    /// <summary>
    /// Gets hash code of string
    /// </summary>
    public override Int32 GetHashCode()
    {
        return GetHashCode(_memory.Span);
    }

    /// <summary>
    /// Replace all occurrences of target character with replacement character
    /// </summary>
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
        return new StringOrMemory(this.ToString().Replace(target, replacement));
    }

    /// <inheritdoc/>
    public override String ToString()
    {
        if (_memory.IsEmpty)
        {
            return String.Empty;
        }

        // ToString here checks if pointer is already a string and also checks case when length is same as original string
        // important for cached string, usually from dictionaries
        return _memory.ToString();
    }

    private static Int32 GetHashCode(ReadOnlySpan<Char> span)
    {
#if NET8_0_OR_GREATER
        return String.GetHashCode(span);
#else
        var x = 352654597;
        for (var i = 0; i < span.Length; i++)
        {
            x = (x << 5) + x + (x >> 27) ^ span[i];
        }
        return x * 1566083941;
#endif
    }
}