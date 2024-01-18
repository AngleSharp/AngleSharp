namespace AngleSharp.Html.Parser.Tokens.Struct;

using System;
using System.Collections.Generic;
using Common;

/// <summary>
/// Struct to hold attributes.
/// </summary>
public struct StructAttributes
{
    private Int32 _count;
    private MemoryHtmlAttributeToken _t0;
    private MemoryHtmlAttributeToken _t1;
    private MemoryHtmlAttributeToken _t2;
    private MemoryHtmlAttributeToken _t3;
    private List<MemoryHtmlAttributeToken> _tail;

    /// <summary>
    /// Adds an attribute to the list.
    /// </summary>
    /// <param name="item">Attribute to add.</param>
    public void Add(MemoryHtmlAttributeToken item)
    {
        switch (_count)
        {
            case 0:
                _t0 = item;
                _count = 1;
                return;
            case 1:
                _t1 = item;
                _count = 2;
                return;
            case 2:
                _t2 = item;
                _count = 3;
                return;
            case 3:
                _t3 = item;
                _count = 4;
                return;
            case > 3:
                _tail ??= new List<MemoryHtmlAttributeToken>(2);
                _tail.Add(item);
                _count++;
                break;
        }
    }

    /// <summary>
    /// Gets the number of attributes.
    /// </summary>
    public Int32 Count => _count;

    /// <summary>
    /// Removes the attribute at the given index.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void RemoveAt(Int32 index)
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
                switch (index)
                {
                    case 0:
                        _t0 = _t1;
                        _t1 = _t2;
                        _t2 = default;
                        break;
                    case 1:
                        _t1 = _t2;
                        _t2 = default;
                        break;
                    default:
                        _t2 = default;
                        break;
                }

                break;
            case 4:
                switch (index)
                {
                    case 0:
                        _t0 = _t1;
                        _t1 = _t2;
                        _t2 = _t3;
                        _t3 = default;
                        break;
                    case 1:
                        _t1 = _t2;
                        _t2 = _t3;
                        _t3 = default;
                        break;
                    case 2:
                        _t2 = _t3;
                        _t3 = default;
                        break;
                    default:
                        _t3 = default;
                        break;
                }

                break;
            default:
                switch (index)
                {
                    case 0:
                        _t0 = _t1;
                        _t1 = _t2;
                        _t2 = _t3;
                        _t3 = _tail[0];
                        _tail.RemoveAt(0);
                        break;
                    case 1:
                        _t1 = _t2;
                        _t2 = _t3;
                        _t3 = _tail[0];
                        _tail.RemoveAt(0);
                        break;
                    case 2:
                        _t2 = _t3;
                        _t3 = _tail[0];
                        _tail.RemoveAt(0);
                        break;
                    case 3:
                        _t3 = _tail[0];
                        _tail.RemoveAt(0);
                        break;
                    default:
                        _tail.RemoveAt(index - 4);
                        break;
                }

                break;
        }

        _count--;
    }

    /// <summary>
    /// Gets or sets the attribute at the given index.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public MemoryHtmlAttributeToken this[Int32 id]
    {
        get
        {
            if (id >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

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

    /// <summary>
    /// Checks if the attribute list contains the given attribute.
    /// </summary>
    /// <param name="name">Attribute name.</param>
    /// <param name="value">Attribute value.</param>
    public Boolean HasAttribute(StringOrMemory name, StringOrMemory value)
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
}