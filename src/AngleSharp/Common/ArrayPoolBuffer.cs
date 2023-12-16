namespace AngleSharp.Common;

using System;
using System.Buffers;

internal class ArrayPoolBuffer : IBuffer
{
    private Char[] _buffer;

    private Int32 _start = 0;
    private Int32 _idx = 0;

    // private Boolean _canLog = false;

    private Int32 Pointer => _start + _idx;

    public ArrayPoolBuffer(Int32 length)
    {
        _buffer = ArrayPool<Char>.Shared.Rent(length);
        Capacity = _buffer.Length;
        // _canLog = true;
    }

    public void Dispose()
    {
        //// if (_canLog)
        ////     Console.WriteLine($"Dispose() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        ReturnToPool();
    }

    public IBuffer Append(Char c)
    {
        //if (_canLog)
        //    Console.WriteLine($"Append('{c}') [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
        _buffer[Pointer] = c;
        _idx++;
        return this;
    }

    public void Discard()
    {
        //if (_canLog)
        //    Console.WriteLine($"Discard() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

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
        //if (_canLog)
        //    Console.WriteLine($"Remove({startIndex}, {length}) [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        var tail = _buffer.AsSpan(_start + startIndex + length);
        var dest = _buffer.AsSpan(_start + startIndex);
        tail.CopyTo(dest);
        _idx -= length;

        return this;
    }

    private Boolean _disposed;

    public void ReturnToPool()
    {
        //if (_canLog)
        //    Console.WriteLine($"ReturnToPool() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        if (!_disposed)
        {
            ArrayPool<Char>.Shared.Return(_buffer, false);
            _buffer = null!;
            _disposed = true;
        }
    }

    public IBuffer Insert(Int32 idx, Char c)
    {
        //if (_canLog)
        //    Console.WriteLine($"Insert({idx}, '{c}') [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

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
        //if (_canLog)
        //    Console.WriteLine($"Append(\"{str}\") [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

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
            //if (_canLog)
            //    Console.WriteLine($"this[{i}] [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");
            return _buffer[_start + i];
        }
    }

    private StringOrMemory GetData()
    {

        return new StringOrMemory(_buffer.AsMemory(_start, Length));
    }

    public StringOrMemory GetDataAndClear()
    {
        //if (_canLog)
        //    Console.WriteLine($"GetDataAndClear() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

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

    String IBuffer.ToString()
    {
        //if (_canLog)
        //    Console.WriteLine($"ToString() [Pointer={Pointer} Length={Length} Start={_start} Idx={_idx}]");

        return new StringOrMemory(_buffer.AsMemory(_start, Length)).String;
    }
}