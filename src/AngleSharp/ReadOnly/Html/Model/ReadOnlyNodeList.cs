using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Construction;
using AngleSharp.ReadOnly.Html;

internal class ReadOnlyNodeList : IConstructableNodeList, IReadOnlyNodeList
{
    private readonly List<IConstructableNode> _nodes;

    public ReadOnlyNodeList()
    {
        _nodes = new List<IConstructableNode>(2);
    }

    public Int32 Length => _nodes.Count;

    public IConstructableNode this[Int32 index] => _nodes[index];
    IReadOnlyNode IReadOnlyNodeList.this[Int32 index] => (_nodes[index] as IReadOnlyNode)!;

    IEnumerator<IReadOnlyNode> IEnumerable<IReadOnlyNode>.GetEnumerator()
    {
        return _nodes.OfType<IReadOnlyNode>().GetEnumerator();
    }

    public IEnumerator<IConstructableNode> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IConstructableNode node)
    {
        _nodes.Add(node);
    }

    public void Remove(IConstructableNode node)
    {
        node.Parent = null;
        _nodes.Remove(node);
    }

    public void Clear()
    {
        foreach (var node in _nodes)
        {
            node.Parent = null;
        }
        _nodes.Clear();
    }

    public void Insert(int idx, IConstructableNode node)
    {
        _nodes.Insert(idx, node);
    }
}