namespace AngleSharp.Common;

using System;
using System.Buffers;

internal class ArrayPoolBuffer : IBuffer
{
    private Char[] _buffer;

    private Int32 _start = 0;
    private Int32 _idx = 0;

    private Boolean _canLog = false;

    private Int32 Pointer => _start + _idx;

    public ArrayPoolBuffer(Int32 length)
    {
        _buffer = ArrayPool<Char>.Shared.Rent(length);
        Capacity = length;
        // _canLog = true;
    }

    public void Dispose()
    {
        if (_canLog)
            Console.WriteLine($"Dispose() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        ReturnToPool();
    }

    public IBuffer Append(Char c)
    {
        if (_canLog)
            Console.WriteLine($"Append('{c}') [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        _buffer[Pointer] = c;
        _idx++;
        return this;
    }

    public IBuffer Clear()
    {
        if (_canLog)
            Console.WriteLine($"Clear() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        _start += _idx;
        _idx = 0;
        return this;
    }

    public Int32 Length => _idx;

    public Int32 Capacity { get; }

    public IBuffer Remove(Int32 startIndex, Int32 length)
    {
        if (_canLog)
            Console.WriteLine($"Remove({startIndex}, {length}) [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        // x x x x [x x x x x x x  x  x  x  x] x  x  x  x  x  x
        // 1 2 3 4  5 6 7 8 9 0 10 11 12 13 14 15 16
        var tail = _buffer.AsSpan(_start + startIndex + length);
        var dest = _buffer.AsSpan(_start + startIndex);
        tail.CopyTo(dest);
        _idx -= length;

        return this;
    }

    public void CopyTo(Int32 offset, Span<Char> dest, Int32 length)
    {
        if (_canLog)
            Console.WriteLine($"CopyTo({offset}, dest, {length}) [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        if ((UInt32)offset + (UInt32)length > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        _buffer.AsSpan(_start + offset, length).CopyTo(dest);
    }

    private Boolean _disposed;

    public void ReturnToPool()
    {
        if (_canLog)
            Console.WriteLine($"ReturnToPool() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        if (!_disposed)
        {
            ArrayPool<Char>.Shared.Return(_buffer);
            _buffer = null!;
            _disposed = true;
        }
    }

    public IBuffer Insert(Int32 idx, Char c)
    {
        if (_canLog)
            Console.WriteLine($"Insert({idx}, '{c}') [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        if ((UInt32)idx > Length)
        {
            throw new ArgumentOutOfRangeException(nameof(idx)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        if (Pointer + 1 > Capacity)
        {
            throw new NotImplementedException();
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
        if (_canLog)
            Console.WriteLine($"Append(\"{str}\") [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        if (Pointer + str.Length > Capacity)
        {
            throw new ArgumentOutOfRangeException(nameof(str)/*, SR.ArgumentOutOfRange_IndexMustBeLessOrEqual*/);
        }

        str.CopyTo(_buffer.AsSpan(Pointer));
        _idx += str.Length;
        return this;
    }

    public Char this[Int32 i]
    {
        get
        {
            if (_canLog)
                Console.WriteLine($"this[{i}] [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
            return _buffer[_start + i];
        }
    }

    public StringOrMemory GetData()
    {
        if (_canLog)
            Console.WriteLine($"GetData() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        return new StringOrMemory(_buffer.AsMemory(_start, Length));
    }

    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison)
    {
        return MemoryExtensions.Equals(_buffer.AsSpan(_start, Length), test, comparison);
    }

    public Boolean Has(CheckBuffer test, StringComparison comparison)
    {
        return test(_buffer.AsSpan(_start, Length));
    }

    public override String ToString()
    {
        if (_canLog)
            Console.WriteLine($"ToString() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        return new StringOrMemory(_buffer.AsMemory(_start, Length)).String;
    }
}