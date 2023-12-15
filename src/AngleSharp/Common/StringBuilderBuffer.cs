namespace AngleSharp.Common;

using System;
using System.Buffers;
using System.Text;
using Text;

internal class StringBuilderBuffer : IBuffer
{
    private StringBuilder _sb = StringBuilderPool.Obtain();

    public IBuffer Append(Char c)
    {
        _sb.Append(c);
        return this;
    }

    public void Discard()
    {
        _sb.Clear();
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

    private StringOrMemory GetData()
    {
        return new StringOrMemory(_sb.ToString());
    }

    public StringOrMemory GetDataAndClear()
    {
        var temp = GetData();
        Discard();
        return temp;
    }

    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison)
    {
        var length = _sb.Length;

        using var lease = ArrayPool<Char>.Shared.Borrow(length);
        _sb.CopyTo(0, lease.Span, length);
        return MemoryExtensions.Equals(lease.Span.Slice(0, length), test, comparison);
    }

    public Boolean HasTextAt(ReadOnlySpan<Char> test, int offset, int length, StringComparison comparison = StringComparison.Ordinal)
    {
        using var lease = ArrayPool<Char>.Shared.Borrow(length);
        _sb.CopyTo(offset, lease.Span, length);
        var test2 = _sb.ToString(offset, length);
        return MemoryExtensions.Equals(lease.Span.Slice(0, length), test, comparison);
    }

    public override String ToString()
    {
        return _sb.ToString();
    }
}