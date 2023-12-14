namespace AngleSharp.Common;

using System;
using System.Buffers;
using System.Text;
using Text;

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
    public String String => _string ??= _memory.Span.CreateString();
}

/// <summary>
///
/// </summary>
public interface IBuffer : IDisposable
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    IBuffer Append(Char c);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    IBuffer Clear();

    /// <summary>
    ///
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    ///
    /// </summary>
    Int32 Capacity { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    IBuffer Remove(Int32 start, Int32 length);

    /// <summary>
    ///
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="dest"></param>
    /// <param name="length"></param>
    void CopyTo(Int32 offset, Span<Char> dest, Int32 length);

    /// <summary>
    ///
    /// </summary>
    void ReturnToPool();

    /// <summary>
    ///
    /// </summary>
    /// <param name="index"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    IBuffer Insert(Int32 index, Char c);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    IBuffer Append(ReadOnlySpan<Char> str);

    /// <summary>
    /// Gets the character at the specified index.
    /// </summary>
    /// <param name="i"></param>
    Char this[Int32 i] { get; }

    /// <summary>
    /// Gets the data as a memory.
    /// </summary>
    /// <returns></returns>
    StringOrMemory GetData();
}

internal class ArrayPoolBuffer : IBuffer
{
    private Char[] _buffer;
    private Int32 index = 0;

    public ArrayPoolBuffer(Int32 length)
    {
        _buffer = ArrayPool<Char>.Shared.Rent(length);
        Capacity = length;
        Length = 0;
    }

    public void Dispose()
    {
        ReturnToPool();
    }

    public IBuffer Append(Char c)
    {
        _buffer[index++] = c;
        return this;
    }

    public IBuffer Clear()
    {
        index = 0;
        return this;
    }

    public Int32 Length { get; private set; }

    public Int32 Capacity { get; }

    public IBuffer Remove(Int32 startIndex, Int32 length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        if (length > Length - startIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(length)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        if (Length == length && startIndex == 0)
        {
            Length = 0;
            return this;
        }

        if (length > 0)
        {
            Length -= length;
            Array.Copy(_buffer, startIndex + length, _buffer, startIndex, Length - startIndex);
        }

        return this;
    }

    public void CopyTo(Int32 offset, Span<Char> dest, Int32 length)
    {
        _buffer.AsSpan(offset, length).CopyTo(dest);
    }

    private Boolean _disposed = false;

    public void ReturnToPool()
    {
        if (!_disposed)
        {
            ArrayPool<Char>.Shared.Return(_buffer);
            _buffer = null!;
            _disposed = true;
        }
    }

    public IBuffer Insert(Int32 index, Char c)
    {
        if ((UInt32)index > (UInt32)Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        _buffer[index] = c;

        return this;
    }

    public IBuffer Append(ReadOnlySpan<Char> str)
    {
        if (index + str.Length > Capacity)
        {
            throw new ArgumentOutOfRangeException(nameof(str)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        str.CopyTo(_buffer.AsSpan(index));
        index += str.Length;
        return this;
    }

    public Char this[Int32 i] => _buffer[i];

    public StringOrMemory GetData()
    {
        return new StringOrMemory(_buffer.AsMemory(0, Length));
    }

    public override String ToString()
    {
        return _buffer.AsSpan(0, Length).CreateString();
    }
}

internal class StringBuilderBuffer : IBuffer
{
    private StringBuilder _sb = StringBuilderPool.Obtain();

    public IBuffer Append(Char c)
    {
        _sb.Append(c);
        return this;
    }

    public IBuffer Clear()
    {
        _sb.Clear();
        return this;
    }

    public Int32 Length => _sb.Length;

    public Int32 Capacity => _sb.Capacity;

    public IBuffer Remove(Int32 startIndex, Int32 length)
    {
        _sb.Remove(startIndex, length);
        return this;
    }

    public void CopyTo(Int32 offset, Span<Char> dest, Int32 length)
    {
        _sb.CopyTo(offset, dest, length);
    }

    private Boolean _disposed = false;

    public void ReturnToPool()
    {
        if (_disposed)
        {
            StringBuilderPool.ReturnToPool(_sb);
            _sb = null!;
            _disposed = true;
        }
    }

    public void Dispose()
    {
        ReturnToPool();
    }

    public IBuffer Insert(Int32 index, Char c)
    {
        _sb.Insert(index, c);
        return this;
    }

    public IBuffer Append(ReadOnlySpan<Char> str)
    {
        _sb.Append(str);
        return this;
    }

    public Char this[Int32 i] => _sb[i];

    public StringOrMemory GetData()
    {
        return new StringOrMemory(_sb.ToString());
    }

    public override String ToString()
    {
        return _sb.ToString();
    }
}

