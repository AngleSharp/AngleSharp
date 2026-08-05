
namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents the DomRectList class.
/// </summary>
[DomName("DOMRectList")]
[DomExposed("Window")]
public sealed class DomRectList : IDomRectList
{
    private readonly List<IDomRect> _items;

    /// <summary>
    /// Initializes a new instance of the DomRectList class.
    /// </summary>
    public DomRectList(IEnumerable<IDomRect>? items = null)
    {
        _items = items is null ? new List<IDomRect>() : [.. items];
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("length")]
    public Int32 Length => _items.Count;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("item")]
    public IDomRect? Item(Int32 index)
    {
        return index >= 0 && index < _items.Count ? _items[index] : null;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    public IEnumerator<IDomRect> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
