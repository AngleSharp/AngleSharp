using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Common;
using AngleSharp.Html.Construction;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyNamedNodeMap : IConstructableNamedNodeMap, IReadOnlyNamedNodeMap
{
    protected readonly List<IConstructableAttr> _attributes;

    public ReadOnlyNamedNodeMap()
    {
        _attributes = new List<IConstructableAttr>(2);
    }

    IReadOnlyAttr? IReadOnlyNamedNodeMap.this[StringOrMemory name] => this[name] as IReadOnlyAttr;

    public IConstructableAttr? this[StringOrMemory name]
    {
        get
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                var attr = _attributes[i];
                if (attr.Name == name)
                {
                    return attr;
                }
            }

            return null;
        }
    }

    public Int32 Length => _attributes.Count;

    public bool SameAs(IConstructableNamedNodeMap? attributes)
    {
        if (attributes is null)
        {
            return false;
        }

        if (_attributes.Count != attributes.Length)
        {
            return false;
        }

        for (int i = 0; i < _attributes.Count; i++)
        {
            var src = _attributes[i];
            if (attributes[src.Name]?.Value != src.Value)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator<IReadOnlyAttr> IEnumerable<IReadOnlyAttr>.GetEnumerator()
    {
        return _attributes.OfType<IReadOnlyAttr>().GetEnumerator();
    }

    public IEnumerator<IConstructableAttr> GetEnumerator()
    {
        return _attributes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IConstructableAttr attr)
    {
        _attributes.Add(attr);
    }

    public void Remove(IConstructableAttr attr)
    {
        _attributes.Remove(attr);
    }

    public void Clear()
    {
        _attributes.Clear();
    }

    internal void AddOrUpdate(StringOrMemory name, StringOrMemory value)
    {
        var item = this[name];
        if (item == null)
        {
            _attributes.Add(new ReadOnlyAttr(name, value));
        }
        else
        {
            item.Value = value;
        }
    }
}