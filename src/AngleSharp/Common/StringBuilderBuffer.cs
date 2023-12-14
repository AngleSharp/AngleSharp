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

    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison)
    {
        using var lease = ArrayPool<Char>.Shared.Borrow(_sb.Length);
        _sb.CopyTo(0, lease.Span, _sb.Length);
        return MemoryExtensions.Equals(lease.Span, test, comparison);
    }

    public Boolean Has(CheckBuffer test, StringComparison comparison)
    {
        using var lease = ArrayPool<Char>.Shared.Borrow(_sb.Length);
        _sb.CopyTo(0, lease.Span, _sb.Length);
        return test(lease.Span);
    }

    public override String ToString()
    {
        return _sb.ToString();
    }
}