namespace AngleSharp.Common;

using System;
using System.Buffers;
using System.Text;
using Text;

/// <summary>
/// Delegates implementation to <see cref="StringBuilder"/> obtained from <see cref="StringBuilderPool"/>
/// </summary>
internal sealed class StringBuilderBuffer : IMutableCharBuffer
{
    private Boolean _disposed = false;
    internal StringBuilder _sb = StringBuilderPool.Obtain();

    public void Append(Char c)
    {
        _sb.Append(c);
    }

    public void Discard()
    {
        _sb.Clear();
    }

    public Int32 Length => _sb.Length;

    public Int32 Capacity => _sb.Capacity;

    public IMutableCharBuffer Remove(Int32 startIndex, Int32 length)
    {
        _sb.Remove(startIndex, length);
        return this;
    }

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

    public IMutableCharBuffer Insert(Int32 index, Char c)
    {
        _sb.Insert(index, c);
        return this;
    }

    public IMutableCharBuffer Append(ReadOnlySpan<Char> str)
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        _sb.Append(str);
#else
        foreach (var c in str)
        {
            _sb.Append(c);
        }
#endif
        return this;
    }

    public Char this[Int32 i] => _sb[i];

    public ReadOnlyMemory<Char>? TryCopyTo(Char[] buffer)
    {
        if (_sb.Length > buffer.Length)
        {
            return null;
        }

        _sb.CopyTo(0, buffer, 0, _sb.Length);
        return buffer.AsMemory(0, _sb.Length);
    }

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
        _sb.CopyTo(0, lease.Data, 0, length);
        return MemoryExtensions.Equals(lease.Span.Slice(0, length), test, comparison);
    }

    public Boolean HasTextAt(ReadOnlySpan<Char> test, Int32 offset, Int32 length, StringComparison comparison = StringComparison.Ordinal)
    {
        using var lease = ArrayPool<Char>.Shared.Borrow(length);
        _sb.CopyTo(offset, lease.Data, 0, length);
        return MemoryExtensions.Equals(lease.Span.Slice(0, length), test, comparison);
    }

    public override String ToString()
    {
        return _sb.ToString();
    }
}