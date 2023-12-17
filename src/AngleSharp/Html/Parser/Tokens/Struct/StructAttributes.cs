namespace AngleSharp.Html.Parser.Tokens.Struct;

using System;
using System.Collections.Generic;
using Common;

#nullable disable
public struct StructAttributes
{
    private Int32 _count;
    private MemoryHtmlAttributeToken _t0;
    private MemoryHtmlAttributeToken _t1;
    private MemoryHtmlAttributeToken _t2;
    private MemoryHtmlAttributeToken _t3;
    private List<MemoryHtmlAttributeToken> _tail;

    public void Add(MemoryHtmlAttributeToken item)
    {
        if (_count == 0)
        {
            _t0 = item;
            _count = 1;
            return;
        }

        if (_count == 1)
        {
            _t1 = item;
            _count = 2;
            return;
        }

        if (_count == 2)
        {
            _t2 = item;
            _count = 3;
            return;
        }

        if (_count == 3)
        {
            _t3 = item;
            _count = 4;
            return;
        }

        if (_count > 3)
        {
            _tail ??= new List<MemoryHtmlAttributeToken>();
            _tail.Add(item);
            _count++;
        }
    }

    public int Count => _count;

    public void RemoveAt(int index)
    {
        if (index > _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        switch (_count)
        {
            case 1:
                _t0 = default;
                break;
            case 2:
                if (index == 0)
                {
                    _t0 = _t1;
                    _t1 = default;
                }
                else
                {
                    _t1 = default;
                }

                break;
            case 3:
                if (index == 0)
                {
                    _t0 = _t1;
                    _t1 = _t2;
                    _t2 = default;
                }
                else if (index == 1)
                {
                    _t1 = _t2;
                    _t2 = default;
                }
                else
                {
                    _t2 = default;
                }

                break;
            case 4:
                if (index == 0)
                {
                    _t0 = _t1;
                    _t1 = _t2;
                    _t2 = _t3;
                    _t3 = default;
                }
                else if (index == 1)
                {
                    _t1 = _t2;
                    _t2 = _t3;
                    _t3 = default;
                }
                else if (index == 2)
                {
                    _t2 = _t3;
                    _t3 = default;
                }
                else
                {
                    _t3 = default;
                }

                break;
            default:
                if (index == 0)
                {
                    _t0 = _t1;
                    _t1 = _t2;
                    _t2 = _t3;
                    _t3 = _tail[0];
                    _tail.RemoveAt(0);
                }
                else if (index == 1)
                {
                    _t1 = _t2;
                    _t2 = _t3;
                    _t3 = _tail[0];
                    _tail.RemoveAt(0);
                }
                else if (index == 2)
                {
                    _t2 = _t3;
                    _t3 = _tail[0];
                    _tail.RemoveAt(0);
                }
                else if (index == 3)
                {
                    _t3 = _tail[0];
                    _tail.RemoveAt(0);
                }
                else
                {
                    _tail.RemoveAt(index - 4);
                }

                break;
        }

        _count--;
    }

    public MemoryHtmlAttributeToken this[int id]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(id, _count);

            return id switch
            {
                0 => _t0,
                1 => _t1,
                2 => _t2,
                3 => _t3,
                _ => _tail[id - 4]
            };
        }

        set
        {
            if (id >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            switch (id)
            {
                case 0:
                    _t0 = value;
                    break;
                case 1:
                    _t1 = value;
                    break;
                case 2:
                    _t2 = value;
                    break;
                case 3:
                    _t3 = value;
                    break;
                default:
                    _tail[id - 4] = value;
                    break;
            }
        }

    }

    public bool HasAttribute(StringOrMemory name, StringOrMemory value)
    {
        for (var i = 0; i < _count; i++)
        {
            var attr = this[i];

            if (attr.Name.Equals(name) && attr.Value.Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    public bool HasAttribute(String name, String value)
    {
        for (var i = 0; i < _count; i++)
        {
            var attr = this[i];

            if (attr.Name.Isi(name) && attr.Value.Is(value))
            {
                return true;
            }
        }

        return false;
    }
}