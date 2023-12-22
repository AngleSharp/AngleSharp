namespace AngleSharp.Common;

using System;
using System.Buffers;

internal class ArrayPoolBuffer : IBuffer
{
    private Char[] _buffer;
    private Int32 _start = 0;
    private Int32 _idx = 0;
    private Int32 Pointer => _start + _idx;

    public ArrayPoolBuffer(Int32 length)
    {
        _buffer = ArrayPool<Char>.Shared.Rent(length);
        Capacity = _buffer.Length;
    }

    public void Dispose()
    {
        ReturnToPool();
    }

    public IBuffer Append(Char c)
    {
        _buffer[Pointer] = c;
        _idx++;
        return this;
    }

    public void Discard()
    {
        Clear(false);
    }

    private void Clear(bool commit)
    {
        if (commit)
        {
            _start += _idx;
        }
        _idx = 0;
    }

    public Int32 Length => _idx;

    public Int32 Capacity { get; }

    public IBuffer Remove(Int32 startIndex, Int32 length)
    {
        var tail = _buffer.AsSpan(_start + startIndex + length);
        var dest = _buffer.AsSpan(_start + startIndex);
        tail.CopyTo(dest);
        _idx -= length;
        return this;
    }

    private Boolean _disposed;

    public void ReturnToPool()
    {
        if (!_disposed)
        {
            ArrayPool<Char>.Shared.Return(_buffer, false);
            _buffer = null!;
            _disposed = true;
        }
    }

    public IBuffer Insert(Int32 idx, Char c)
    {
        if ((UInt32)idx > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(idx));
        }

        if (Pointer + 1 > Capacity)
        {
            throw new InvalidOperationException("Buffer is full.");
        }

        Array.Copy(
            _buffer, _start + idx,
            _buffer, _start + idx + 1,
            Length - idx);

        _buffer[_start + idx] = c;
        _idx++;

        return this;
    }

    public IBuffer Append(ReadOnlySpan<Char> str)
    {
        if (Pointer + str.Length > Capacity)
        {
            throw new InvalidOperationException("Buffer is full.");
        }

        str.CopyTo(_buffer.AsSpan(Pointer));
        _idx += str.Length;
        return this;
    }

    public Char this[Int32 i] => _buffer[_start + i];

    private StringOrMemory GetData()
    {

        return new StringOrMemory(_buffer.AsMemory(_start, Length));
    }

    public StringOrMemory GetDataAndClear()
    {
        var result = GetData();
        Clear(true);
        return result;
    }

    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison = StringComparison.Ordinal)
    {
        var actual = _buffer.AsSpan(_start, Length);
        return MemoryExtensions.Equals(actual, test, comparison);
    }

    public Boolean HasTextAt(ReadOnlySpan<Char> test, Int32 offset, Int32 length, StringComparison comparison = StringComparison.Ordinal)
    {
        var actual = _buffer.AsSpan(_start + offset, length);
        return MemoryExtensions.Equals(actual, test, comparison);
    }

    String IBuffer.ToString() => new StringOrMemory(_buffer.AsMemory(_start, Length)).String;
}