namespace AngleSharp.Dom;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Represents a static collection of elements returned by a query.
/// </summary>
internal sealed class QueryCollection : IHtmlCollection<IElement>, INodeList
{
    private readonly List<IElement> _elements;

    public QueryCollection(List<IElement> elements)
    {
        _elements = elements;
    }

    public IElement this[Int32 index] => _elements[index];

    public IElement? this[String id] => _elements.GetElementById(id);

    INode INodeList.this[Int32 index] => this[index];

    public Int32 Length => _elements.Count;

    public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        for (var i = 0; i < _elements.Count; i++)
        {
            _elements[i].ToHtml(writer, formatter);
        }
    }

    public IEnumerator<IElement> GetEnumerator() => _elements.GetEnumerator();

    IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => _elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _elements.GetEnumerator();
}