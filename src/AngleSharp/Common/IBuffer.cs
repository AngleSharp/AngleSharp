namespace AngleSharp.Common;

using System;
using System.Buffers;
using System.Text;
using Text;

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
    int Length { get; }

    /// <summary>
    ///
    /// </summary>
    int Capacity { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    IBuffer Remove(int start, int length);

    /// <summary>
    ///
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="dest"></param>
    /// <param name="length"></param>
    void CopyTo(int offset, Span<char> dest, int length);

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
    IBuffer Insert(int index, char c);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    IBuffer Append(ReadOnlySpan<char> str);

    /// <summary>
    /// Gets the character at the specified index.
    /// </summary>
    /// <param name="i"></param>
    Char this[Int32 i] { get; }

    /// <summary>
    /// Gets the data as a memory.
    /// </summary>
    /// <returns></returns>
    ReadOnlyMemory<char> GetData();
}

internal class ArrayPoolBuffer : IBuffer
{
    private char[] _buffer;
    private int index = 0;

    public ArrayPoolBuffer(int length)
    {
        _buffer = ArrayPool<char>.Shared.Rent(length);
        Capacity = length;
        Length = 0;
    }

    public void Dispose()
    {
        ReturnToPool();
    }

    public IBuffer Append(char c)
    {
        _buffer[index++] = c;
        return this;
    }

    public IBuffer Clear()
    {
        index = 0;
        return this;
    }

    public int Length { get; private set; }

    public int Capacity { get; }

    public IBuffer Remove(int startIndex, int length)
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

    public void CopyTo(int offset, Span<Char> dest, int length)
    {
        _buffer.AsSpan(offset, length).CopyTo(dest);
    }

    private bool _disposed = false;

    public void ReturnToPool()
    {
        if (!_disposed)
        {
            ArrayPool<char>.Shared.Return(_buffer);
            _buffer = null!;
            _disposed = true;
        }
    }

    public IBuffer Insert(int index, char c)
    {
        if ((uint)index > (uint)Length)
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

    public char this[int i] => _buffer[i];

    public ReadOnlyMemory<Char> GetData()
    {
        return _buffer.AsMemory(0, Length);
    }
}

internal class StringBuilderBuffer : IBuffer
{
    private StringBuilder _sb;

    public StringBuilderBuffer(StringBuilder sb)
    {
        _sb = sb;
    }

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

    public int Length => _sb.Length;

    public int Capacity => _sb.Capacity;

    public IBuffer Remove(int startIndex, int length)
    {
        _sb.Remove(startIndex, length);
        return this;
    }

    public void CopyTo(int offset, Span<char> dest, int length)
    {
        _sb.CopyTo(offset, dest, length);
    }

    private bool _disposed = false;

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

    public IBuffer Insert(int index, char c)
    {
        _sb.Insert(index, c);
        return this;
    }

    public IBuffer Append(ReadOnlySpan<char> str)
    {
        _sb.Append(str);
        return this;
    }

    public char this[int i] => _sb[i];

    public ReadOnlyMemory<Char> GetData()
    {
        return _sb.ToString().AsMemory();
    }

    public override String ToString()
    {
        return _sb.ToString();
    }
}

