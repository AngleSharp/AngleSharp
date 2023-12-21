namespace AngleSharp.ReadOnly;

using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Dom;
using Html.Parser;
using Html.Parser.Tokens.Struct;

internal class ReadOnlyNodeList : IReadOnlyNodeList
{
    private readonly List<IReadOnlyNode> _nodes;

    public ReadOnlyNodeList()
    {
        _nodes = new List<IReadOnlyNode>();
    }

    public IReadOnlyNode this[Int32 index] => _nodes[index];

    public Int32 Length => _nodes.Count;

    public IEnumerator<IReadOnlyNode> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IReadOnlyNode node)
    {
        _nodes.Add(node);
    }

    public void Remove(IReadOnlyNode node)
    {
        _nodes.Remove(node);
    }

    public void Clear()
    {
        _nodes.Clear();
    }

    // public String ToHtml(IMarkupFormatter formatter)
    // {
    //     return formatter.CreateNodes(_nodes);
    // }
    //
    // public String ToHtml()
    // {
    //     return ToHtml(MarkupFormatter.Instance);
    // }
}

internal class ReadOnlyElement : ReadOnlyNode
{
    // public ReadOnlyElement(
    //     ReadOnlyElement? owner,
    //     StringOrMemory name,
    //     NodeType type = NodeType.Element,
    //     NodeFlags flags = NodeFlags.HtmlMember) : base(owner, name, type, flags)
    // {
    //     LocalName = name;
    //     ChildNodes = new ReadOnlyNodeList();
    //     Attributes = new ReadOnlyNamedNodeMap();
    // }

    /// <inheritdoc />
    public ReadOnlyElement(
        ReadOnlyHtmlDocument owner,
        StringOrMemory localName,
        StringOrMemory prefix = default,
        StringOrMemory namespaceUri = default,
        NodeFlags flags = NodeFlags.None)
        : this(owner,
            prefix.IsNullOrEmpty ? String.Concat(prefix.Memory.Span, ":", localName.Memory.Span) : localName,
            localName, prefix, namespaceUri, flags)
    {
    }

    /// <inheritdoc />
    public ReadOnlyElement(
        ReadOnlyHtmlDocument owner,
        StringOrMemory name,
        StringOrMemory localName,
        StringOrMemory prefix,
        StringOrMemory namespaceUri,
        NodeFlags flags = NodeFlags.None)

        : base(owner, name, NodeType.Element, flags)
    {
        // _localName = localName;
        // _prefix = prefix;
        // _namespace = namespaceUri;
        // _attributes = new NamedNodeMap(this);

        LocalName = localName;
        ChildNodes = new ReadOnlyNodeList();
        Attributes = new ReadOnlyNamedNodeMap();
    }

    public ReadOnlyElement? Parent { get; set; }

    public IReadOnlyNodeList ChildNodes { get; set; }

    public IReadOnlyNamedNodeMap Attributes { get; set; }

    public StringOrMemory LocalName { get; set; }

    public StringOrMemory NamespaceUri
    {
        get => StringOrMemory.Empty;
        set { }
    }

    public HtmlTreeMode? SelectMode(Boolean last, Stack<HtmlTreeMode> templateModes)
    {
        throw new NotImplementedException();
    }

    public void AddComment(ref StructHtmlToken token)
    {
        throw new NotImplementedException();
    }

    public void RemoveFromParent()
    {
        throw new NotImplementedException();
    }

    public void AddNode(IReadOnlyNode newElement)
    {
        ChildNodes.Add(newElement);
    }

    public void RemoveChild(IReadOnlyNode lastNode)
    {
        throw new NotImplementedException();
    }

    public void RemoveNode(int i, IReadOnlyNode childNode)
    {
        throw new NotImplementedException();
    }

    public StringOrMemory GetAttribute(StringOrMemory @namespace, StringOrMemory name)
    {
        throw new NotImplementedException();
    }

    public void SetupElement()
    {
        // throw new NotImplementedException();
    }

    public virtual ReadOnlyElement Setup(ref StructHtmlToken tag)
    {
        throw new NotImplementedException();
    }

    public void AppendText(StringOrMemory text)
    {
        ChildNodes.Add(new ReadOnlyTextNode(this.Owner, text));
    }

    public void InsertText(int idx, StringOrMemory text)
    {
        throw new NotImplementedException();
    }

    public void InsertNode(int i, ReadOnlyElement element)
    {
        throw new NotImplementedException();
    }

    public bool HasAttribute(StringOrMemory name)
    {
        throw new NotImplementedException();
    }

    public void SetAttribute(String? ns, StringOrMemory name, StringOrMemory value)
    {
        throw new NotImplementedException();
    }

    public override INode Clone(bool deep)
    {
        throw new NotImplementedException();
    }
}

internal class ReadOnlyNamedNodeMap : IReadOnlyNamedNodeMap
{
    private readonly List<IReadOnlyAttr> _attributes;

    public ReadOnlyNamedNodeMap()
    {
        _attributes = new List<IReadOnlyAttr>();
    }

    public IReadOnlyAttr? this[Int32 index] => _attributes[index];

    public IReadOnlyAttr? this[String name] => _attributes.Find(attr => attr.Name == name);

    public Int32 Length => _attributes.Count;

    public bool SameAs(IReadOnlyNamedNodeMap? elementAttributes)
    {
        throw new NotImplementedException();
    }

    public void FastAddItem(IReadOnlyAttr item)
    {
        Add(item);
    }

    public IEnumerator<IReadOnlyAttr> GetEnumerator()
    {
        return _attributes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IReadOnlyAttr attr)
    {
        _attributes.Add(attr);
    }

    public void Remove(IReadOnlyAttr attr)
    {
        _attributes.Remove(attr);
    }

    public void Clear()
    {
        _attributes.Clear();
    }
}