using System;
using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.ReadOnly.Html;

internal abstract class ReadOnlyNode : IConstructableNode, IReadOnlyNode
{
    private static ReadOnlySpan<Char> WhiteSpace => " \t\r\n".AsSpan();
    protected readonly NodeFlags _flags;
    protected ReadOnlyNodeList? _childNodes;
    protected IConstructableNode? _parent;
    protected static readonly ReadOnlyNodeList EmptyChildNodes = new ReadOnlyNodeList();

    public NodeFlags Flags => _flags;

    protected ReadOnlyNodeList _ChildNodes => _childNodes ?? EmptyChildNodes;

    IReadOnlyNode? IReadOnlyNode.Parent => Parent as IReadOnlyNode;
    IReadOnlyNodeList IReadOnlyNode.ChildNodes => _ChildNodes;

    public StringOrMemory NodeName { get; internal set; }
    public ReadOnlyDocument? Owner => null;

    public ReadOnlyNode(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType = NodeType.Element, NodeFlags flags = NodeFlags.None)
    {
        NodeName = name;
        _flags = flags;
    }

    public IConstructableNode? Parent
    {
        get => _parent;
        set => _parent = value;
    }

    public IConstructableNodeList ChildNodes => _ChildNodes;

    public void RemoveFromParent()
    {
        Parent?.RemoveChild(this);
    }

    public void RemoveChild(IConstructableNode childNode)
    {
        childNode.Parent = null;
        _childNodes?.Remove(childNode);
    }

    public void RemoveNode(int idx, IConstructableNode childNode)
    {
        childNode.Parent = null;
        _childNodes?.Remove(childNode);
    }

    public void InsertNode(int idx, IConstructableNode childNode)
    {
        _childNodes ??= new ReadOnlyNodeList();
        childNode.Parent = this;
        _childNodes?.Insert(idx, childNode);
    }

    public void AddNode(IConstructableNode node)
    {
        _childNodes ??= new ReadOnlyNodeList();
        node.Parent = this;
        _childNodes.Add(node);
    }

    public void AppendText(StringOrMemory text, bool emitWhiteSpaceOnly = false)
    {
        if (!emitWhiteSpaceOnly && text.Memory.Span.Trim(WhiteSpace).Length == 0)
        {
            return;
        }

        AddNode(new ReadOnlyTextNode(Owner, text));
    }

    public void InsertText(int idx, StringOrMemory text, bool emitWhiteSpaceOnly = false)
    {
        if (!emitWhiteSpaceOnly && text.Memory.Span.Trim(WhiteSpace).Length == 0)
        {
            return;
        }

        var readOnlyTextNode = new ReadOnlyTextNode(Owner, text);
        _childNodes ??= new ReadOnlyNodeList();
        _childNodes.Insert(idx, readOnlyTextNode);
    }

    public void AddComment(ref StructHtmlToken token)
    {
        var readOnlyTextNode = new ReadOnlyComment(Owner, token.Data);
        _childNodes ??= new ReadOnlyNodeList();
        _childNodes.Add(readOnlyTextNode);
    }

    public virtual void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        if (_childNodes == null)
        {
            return;
        }

        foreach (var node in _childNodes)
        {
            node.ToHtml(writer, formatter);
        }
    }
}