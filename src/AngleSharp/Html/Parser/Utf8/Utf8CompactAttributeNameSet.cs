#pragma warning disable CS1591 // Experimental implementation detail; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

internal sealed class Utf8CompactAttributeNameSet
{
    private const Int32 InlineCapacity = 8;

    private UInt64 _item0;
    private UInt64 _item1;
    private UInt64 _item2;
    private UInt64 _item3;
    private UInt64 _item4;
    private UInt64 _item5;
    private UInt64 _item6;
    private UInt64 _item7;
    private UInt64[]? _overflow;
    private Int32 _count;

    public Boolean TryAdd(UInt64 key)
    {
        if (Contains(key))
        {
            return false;
        }

        if (_count < InlineCapacity)
        {
            SetInline(_count, key);
        }
        else
        {
            var index = _count - InlineCapacity;
            if (_overflow is null)
            {
                _overflow = new UInt64[InlineCapacity];
            }
            else if (index == _overflow.Length)
            {
                Array.Resize(ref _overflow, checked(_overflow.Length * 2));
            }
            _overflow[index] = key;
        }

        _count++;
        return true;
    }

    public void Reset() => _count = 0;

    private Boolean Contains(UInt64 key)
    {
        if (
            (_count > 0 && _item0 == key)
            || (_count > 1 && _item1 == key)
            || (_count > 2 && _item2 == key)
            || (_count > 3 && _item3 == key)
            || (_count > 4 && _item4 == key)
            || (_count > 5 && _item5 == key)
            || (_count > 6 && _item6 == key)
            || (_count > 7 && _item7 == key)
        )
        {
            return true;
        }

        return _count > InlineCapacity
            && _overflow.AsSpan(0, _count - InlineCapacity).Contains(key);
    }

    private void SetInline(Int32 index, UInt64 key)
    {
        switch (index)
        {
            case 0:
                _item0 = key;
                break;
            case 1:
                _item1 = key;
                break;
            case 2:
                _item2 = key;
                break;
            case 3:
                _item3 = key;
                break;
            case 4:
                _item4 = key;
                break;
            case 5:
                _item5 = key;
                break;
            case 6:
                _item6 = key;
                break;
            case 7:
                _item7 = key;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index));
        }
    }
}
