namespace AngleSharp.Common;

using System;

/// <summary>
///
/// </summary>
public struct StringOrMemory
{
    private String? _string;
    private readonly ReadOnlyMemory<Char> _memory;

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    public StringOrMemory(String str)
    {
        _memory = str.AsMemory();
        _string = str;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    public StringOrMemory(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
        _string = null;
    }

    /// <summary>
    ///
    /// </summary>
    public readonly ReadOnlyMemory<Char> Memory => _memory;

    /// <summary>
    ///
    /// </summary>
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
            // return _memory.Span.ToString();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public Int32 Length => _memory.Length;

    /// <summary>
    ///
    /// </summary>
    /// <param name="i"></param>
    /// <exception cref="NotImplementedException"></exception>
    public Char this[Int32 i]
    {
        get => _memory.Span[i];
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public Boolean IsNullOrEmpty => _memory.IsEmpty;

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static implicit operator StringOrMemory(String str) => new StringOrMemory(str);

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    /// <returns></returns>
    public static implicit operator StringOrMemory(ReadOnlyMemory<Char> memory) => new StringOrMemory(memory);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static implicit operator ReadOnlyMemory<Char>(StringOrMemory str) => str.Memory;

    // /// <summary>
    // ///
    // /// </summary>
    // /// <param name="str"></param>
    // /// <returns></returns>
    // public static implicit operator String(StringOrMemory str) => str.String;

    /// <summary>
    ///
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Boolean operator ==(StringOrMemory left, StringOrMemory right)
    {
        return left.Memory.Equals(right.Memory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Boolean operator ==(StringOrMemory left, string right)
    {
        return left.Memory.Span.SequenceEqual(right);
    }

    public static bool operator !=(StringOrMemory left, string right)
    {
        return !left.Memory.Span.SequenceEqual(right);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Boolean operator !=(StringOrMemory left, StringOrMemory right)
    {
        return !left.Memory.Equals(right.Memory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Boolean Equals(StringOrMemory other)
    {
        return _memory.Equals(other._memory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override Boolean Equals(Object? obj)
    {
        return obj is StringOrMemory other && Equals(other);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override Int32 GetHashCode()
    {
        return HashCode.Combine(_memory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="target"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public StringOrMemory Replace(Char target, Char replacement)
    {
        if (_memory.Length < 128)
        {
            Span<Char> tmp = stackalloc Char[_memory.Length];
            _memory.Span.Replace(tmp, target, replacement);
            return new StringOrMemory(tmp.CreateString());
        }

        return new StringOrMemory(String.Replace(target, replacement));
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override String ToString()
    {
        return String;
    }

    public static StringOrMemory Empty => new StringOrMemory(String.Empty);
}

/// <summary>
///
/// </summary>
public static class StringOrMemoryExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Boolean Is(this StringOrMemory str, StringOrMemory other)
    {
        return str.Memory.Span.SequenceEqual(other.Memory.Span);
    }

    public static Boolean Is(this StringOrMemory str, string other)
    {
        return str.Memory.Span.SequenceEqual(other);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Boolean Isi(this StringOrMemory str, StringOrMemory other)
    {
        return str.Memory.Span.Equals(other.Memory.Span, StringComparison.OrdinalIgnoreCase);
    }

    public static Boolean Isi(this StringOrMemory str, string other)
    {
        return str.Memory.Span.Equals(other.AsSpan(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c, StringOrMemory d) =>
        str.Is(a) || str.Is(b) || str.Is(c) || str.Is(d);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b, StringOrMemory c) =>
        str.Is(a) || str.Is(b) || str.Is(c);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a, StringOrMemory b) =>
        str.Is(a) || str.Is(b);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Boolean IsOneOf(this StringOrMemory str, StringOrMemory a) =>
        str.Is(a);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="test"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Boolean StartsWith(this StringOrMemory str, String test, StringComparison comparison)
    {
        return str.Memory.Span.StartsWith(test.AsSpan(), comparison);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="test"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public static Boolean Equals(this StringOrMemory str, String test, StringComparison comparison)
    {
        return str.Memory.Span.Equals(test.AsSpan(), comparison);
    }
}