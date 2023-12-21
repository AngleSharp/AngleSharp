using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

public interface IReadOnlyNode: IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    // NodeType NodeType { get; }
    NodeFlags Flags { get; }
    IReadOnlyNode? Parent { get; }
    IReadOnlyNodeList ChildNodes { get; }
    HtmlToken? SourceReference { get; }
}

public interface IReadOnlyNodeList : IEnumerable<IReadOnlyNode>
{
    IReadOnlyElement this[Int32 index] { get; }
    Int32 Length { get; }
}

public interface IReadOnlyElement : IReadOnlyNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    IReadOnlyNamedNodeMap Attributes { get; }
}

public interface IReadOnlyDocument : IReadOnlyNode, IDisposable
{
    IReadOnlyElement Head { get; }
    IReadOnlyElement Body { get; }
}

public interface IReadOnlyNamedNodeMap : IEnumerable<IReadOnlyAttr>
{
    IReadOnlyAttr? this[Int32 index] { get; }
    IReadOnlyAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
}

internal interface IConstructableNode : IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    NodeFlags Flags { get; }
    IConstructableNode? Parent { get; internal set; }
    IConstructableNodeList ChildNodes { get; }
    HtmlToken? SourceReference { get; set; }

    void RemoveFromParent();
    void RemoveChild(IConstructableNode childNode);
    void RemoveNode(int idx, IConstructableNode childNode);
    void InsertNode(int idx, IConstructableNode childNode);
    void AddNode(IConstructableNode node);
    void AppendText(StringOrMemory text);
    void InsertText(int idx, StringOrMemory text);
    void AddComment(ref StructHtmlToken token);
}

internal interface IConstructableElement : IConstructableNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    IConstructableNamedNodeMap Attributes { get; }
    void SetAttribute(String? ns, StringOrMemory name, StringOrMemory value);
    void SetOwnAttribute(StringOrMemory name, StringOrMemory value);
    StringOrMemory GetAttribute(StringOrMemory @namespace, StringOrMemory name);
    void SetAttributes(StructAttributes tagAttributes);
    bool HasAttribute(StringOrMemory name);
    void SetupElement();
    IConstructableNode ShallowCopy();
}

internal interface IMathElement : IConstructableElement
{
}

internal interface ISvgElement : IConstructableElement
{
}

internal interface IMetaElement : IConstructableElement
{
    void Handle();
}

internal interface IScriptElement: IConstructableElement
{
    internal Task RunAsync(CancellationToken cancel);
    internal Boolean Prepare(IConstructableDocument document);
}

internal interface IFrameElement : IConstructableElement
{
}

internal interface ITemplateElement : IConstructableElement
{
    void PopulateFragment();
}

internal interface IConstructableDocument : IConstructableNode
{
    IReadOnlyTextSource Source { get; }

    QuirksMode QuirksMode { get; set; }

    IConstructableElement Head { get; }
    IConstructableElement DocumentElement { get; }

    void PerformMicrotaskCheckpoint();
    void ProvideStableState();

    void TrackError(Exception exception);
    Task WaitForReadyAsync(CancellationToken cancelToken);
    void ApplyManifest();
    void Clear();
}

internal class ReadOnlyCharacterData : ReadOnlyNode
{
    public StringOrMemory Content { get; }

    internal ReadOnlyCharacterData(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType)
        : this(owner, name, nodeType, StringOrMemory.Empty)
    {
    }

    internal ReadOnlyCharacterData(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType, StringOrMemory content)
        : base(owner, name, nodeType)
    {
        Content = content;
    }
}

internal class ReadOnlyProcessingInstruction : ReadOnlyCharacterData
{
    private ReadOnlyProcessingInstruction(ReadOnlyDocument? owner, StringOrMemory name)
        : base(owner, name, NodeType.ProcessingInstruction)
    {
    }

    public static ReadOnlyProcessingInstruction Create(ReadOnlyDocument? owner, StringOrMemory tokenData)
    {
        return new ReadOnlyProcessingInstruction(owner, tokenData);
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<?");
        writer.Write(NodeName.Memory.Span);
        writer.Write(" ");
        writer.Write(Content.Memory.Span);
        writer.Write("?>");
    }
}

internal class ReadOnlyTextNode : ReadOnlyCharacterData
{
    public ReadOnlyTextNode(ReadOnlyDocument? owner, StringOrMemory content)
        :  base(owner, "#text", NodeType.Text, content)
    {
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write(Content.Memory.Span);
    }
}

internal class ReadOnlyComment : ReadOnlyCharacterData
{
    public ReadOnlyComment(ReadOnlyDocument? owner, StringOrMemory tokenData)
        : base(owner, "#comment", NodeType.Comment, tokenData)
    {
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<!--");
        writer.Write(Content.Memory.Span);
        writer.Write("-->");
    }
}

public interface IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; internal set; }
}

public interface IConstructableNamedNodeMap : IEnumerable<IReadOnlyAttr>
{
    IReadOnlyAttr? this[Int32 index] { get; }
    IReadOnlyAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
    bool SameAs(IConstructableNamedNodeMap? attributes);
}

internal class ReadOnlyAttr : IReadOnlyAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; set; }

    public ReadOnlyAttr(StringOrMemory name, StringOrMemory value)
    {
        Name = name;
        Value = value;
    }
}

internal abstract class ReadOnlyNode : IConstructableNode, IReadOnlyNode
{
    protected readonly NodeFlags _flags;
    protected ConstructableNodeList? _childNodes;
    private IConstructableNode? _parent;
    protected static readonly ConstructableNodeList EmptyChildNodes = new ConstructableNodeList();

    public NodeFlags Flags => _flags;

    IReadOnlyNode? IReadOnlyNode.Parent => Parent as IReadOnlyNode;
    IReadOnlyNodeList IReadOnlyNode.ChildNodes => _childNodes ?? EmptyChildNodes;

    public StringOrMemory NodeName { get; internal set; }
    public ReadOnlyDocument? Owner => null;

    public ReadOnlyNode(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType = NodeType.Element, NodeFlags flags = NodeFlags.None)
    {
        NodeName = name;
        _childNodes = new ConstructableNodeList();
        _flags = flags;
        // _nodeType = nodeType;
    }

    public HtmlToken? SourceReference
    {
        get => null;
        set { }
    }

    public IConstructableNode? Parent
    {
        get => _parent;
        set => _parent = value;
    }

    public IConstructableNodeList ChildNodes => _childNodes ?? EmptyChildNodes;

    public void RemoveFromParent()
    {
        Parent?.RemoveChild(this);
    }

    public void RemoveChild(IConstructableNode childNode)
    {
        _childNodes?.Remove(childNode);
    }

    public void RemoveNode(int idx, IConstructableNode childNode)
    {
        childNode.Parent = null;
        _childNodes?.Remove(childNode);
    }

    public void InsertNode(int idx, IConstructableNode childNode)
    {
        _childNodes?.Insert(idx, childNode);
    }

    public void AddNode(IConstructableNode node)
    {
        _childNodes ??= new ConstructableNodeList();
        _childNodes.Add(node);
    }

    public void AppendText(StringOrMemory text)
    {
        AddNode(new ReadOnlyTextNode(Owner, text));
    }

    public void InsertText(int idx, StringOrMemory text)
    {
        var readOnlyTextNode = new ReadOnlyTextNode(Owner, text);
        _childNodes ??= new ConstructableNodeList();
        _childNodes.Insert(idx, readOnlyTextNode);
    }

    public void AddComment(ref StructHtmlToken token)
    {
        var readOnlyTextNode = new ReadOnlyComment(Owner, token.Data);
        _childNodes ??= new ConstructableNodeList();
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

internal class ReadOnlyDocumentType : ReadOnlyNode
{
    public ReadOnlyDocumentType(ReadOnlyDocument document, StringOrMemory nameString) : base(document, nameString, NodeType.DocumentType)
    {
    }

    public StringOrMemory SystemIdentifier { get; set; }
    public StringOrMemory PublicIdentifier { get; set; }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<!DOCTYPE html ");
        writer.Write(PublicIdentifier.Memory.Span);
        writer.Write(" ");
        writer.Write(SystemIdentifier.Memory.Span);
        writer.WriteLine(">");
        base.ToHtml(writer, formatter);
    }
}

internal class ReadOnlyDocument : ReadOnlyNode, IConstructableDocument, IReadOnlyDocument
{
    public ReadOnlyDocument() : base(null, "#document", NodeType.Document)
    {
    }

    public required IReadOnlyTextSource Source { get; init; }

    public QuirksMode QuirksMode { get; set; }

    public void TrackError(Exception exception)
    {
        // Console.WriteLine(exception);
    }

    public Task WaitForReadyAsync(CancellationToken cancelToken)
    {
        return Task.CompletedTask;
    }

    public IConstructableElement Head => throw new NotImplementedException();

    public IConstructableElement DocumentElement => throw new NotImplementedException();

    public void ApplyManifest()
    {
    }

    public void Clear()
    {
        ChildNodes.Clear();
    }

    public void PerformMicrotaskCheckpoint()
    {
    }

    public void ProvideStableState()
    {
    }

    public void SetupElement()
    {
    }

    public IReadOnlyElement Body => throw new NotImplementedException();
    // NodeType IReadOnlyNode.NodeType => _nodeType;
    IReadOnlyNode? IReadOnlyNode.Parent => throw new NotImplementedException();
    IReadOnlyNodeList IReadOnlyNode.ChildNodes => _childNodes ?? EmptyChildNodes;
    IReadOnlyElement IReadOnlyDocument.Head => throw new NotImplementedException();

    public void Dispose()
    {
        Source.Dispose();
    }
}

internal interface IConstructableNodeList : IEnumerable<IConstructableNode>
{
    IConstructableNode this[Int32 index] { get; }
    Int32 Length { get; }
    void Add(IConstructableNode node);
    void Clear();
}

internal class ConstructableNodeList : IConstructableNodeList, IReadOnlyNodeList
{
    private readonly List<IConstructableNode> _nodes;

    public ConstructableNodeList()
    {
        _nodes = new List<IConstructableNode>();
    }

    public IConstructableNode this[Int32 index] => _nodes[index];

    IReadOnlyElement IReadOnlyNodeList.this[int index] => throw new NotImplementedException();

    public Int32 Length => _nodes.Count;

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

internal class ReadOnlyElement : ReadOnlyNode, IReadOnlyElement
{
    protected static readonly ConstructableNamedNodeMap EmptyAttributes = new ConstructableNamedNodeMap();

    protected ConstructableNamedNodeMap? _attributes;
    // private StringOrMemory _localName;
    // private StringOrMemory _namespaceUri;

    public StringOrMemory LocalName
    {
        get => NodeName;
        internal set {}
    }

    public StringOrMemory NamespaceUri
    {
        get => StringOrMemory.Empty;
        internal set {}
    }

    public IConstructableNamedNodeMap Attributes => _attributes ?? EmptyAttributes;

    /// <inheritdoc />
    public ReadOnlyElement(
        ReadOnlyDocument owner,
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
        ReadOnlyDocument? owner,
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

        // NamespaceUri = namespaceUri;
        // LocalName = localName;
        _attributes = new ConstructableNamedNodeMap();
    }

    public StringOrMemory GetAttribute(StringOrMemory @namespace, StringOrMemory name)
    {
        if (_attributes is null)
        {
            return StringOrMemory.Empty;
        }

        return _attributes[name]?.Value ?? StringOrMemory.Empty;
    }

    public bool HasAttribute(StringOrMemory name)
    {
        if (_attributes is null)
        {
            return false;
        }

        return _attributes[name] is not null;
    }

    public void SetAttribute(String? ns, StringOrMemory name, StringOrMemory value)
    {
        _attributes ??= new ConstructableNamedNodeMap();
        var attr = _attributes[name];
        if (attr is not null)
        {
            attr.Value = value;
        }
        else
        {
            _attributes.Add(new ReadOnlyAttr(name, value));
        }
    }

    public void SetAttributes(StructAttributes attributes)
    {
        _attributes ??= new ConstructableNamedNodeMap();
        for (int i = 0; i < attributes.Count; i++)
        {
            _attributes.Add(new ReadOnlyAttr(attributes[i].Name, attributes[i].Value));
        }
    }

    public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
    {
        writer.Write("<");
        writer.Write(NodeName.Memory.Span);
        foreach (var attribute in _attributes ?? EmptyAttributes)
        {
            writer.Write(" ");
            writer.Write(attribute.Name.Memory.Span);
            writer.Write("=\"");
            writer.Write(attribute.Value.Memory.Span);
            writer.Write("\"");
        }
        writer.WriteLine(">");
        base.ToHtml(writer, formatter);
        writer.Write("</");
        writer.Write(NodeName.Memory.Span);
        writer.WriteLine(">");
    }

    IReadOnlyNamedNodeMap IReadOnlyElement.Attributes => _attributes ?? EmptyAttributes;
}

internal class ConstructableNamedNodeMap : IConstructableNamedNodeMap, IReadOnlyNamedNodeMap
{
    protected readonly List<IReadOnlyAttr> _attributes;

    public ConstructableNamedNodeMap()
    {
        _attributes = new List<IReadOnlyAttr>();
    }

    public IReadOnlyAttr? this[Int32 index] => _attributes.ElementAtOrDefault(index);
    public IReadOnlyAttr? this[StringOrMemory name] => _attributes.Find(attr => attr.Name == name);

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

class ReadOnlyHtmlElement : ReadOnlyElement, ISvgElement, IMathElement
{
    public ReadOnlyHtmlElement(ReadOnlyDocument? owner, StringOrMemory localName = default, StringOrMemory prefix = default, NodeFlags flags = NodeFlags.None)
        : base(owner, Combine(prefix, localName), localName, prefix, NamespaceNames.HtmlUri, flags | NodeFlags.HtmlMember)
    {
    }

    static StringOrMemory Combine(StringOrMemory prefix, StringOrMemory localName)
    {
        if (prefix.IsNullOrEmpty)
        {
            return localName;
        }

        return String.Concat(prefix.Memory.Span, ":", localName.Memory.Span);
    }

    public void SetOwnAttribute(StringOrMemory name, StringOrMemory value)
    {
        _attributes ??= new ConstructableNamedNodeMap();
        _attributes.AddOrUpdate(name, value);
    }

    public void SetupElement()
    {
        // todo: ??
    }

    public virtual IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlElement(Owner, LocalName, prefix: default, Flags)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}

class ReadOnlyHtmlMeta : ReadOnlyHtmlElement, IMetaElement
{
    public ReadOnlyHtmlMeta(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
    {
    }

    public void Handle()
    {
        // do nothing
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlMeta(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}

class ReadOnlyHtmlScript : ReadOnlyHtmlElement, IScriptElement
{
    public ReadOnlyHtmlScript(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        :base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
    {
    }

    public Boolean Prepare(IConstructableDocument document) => false;
    public Task RunAsync(CancellationToken cancel) => Task.CompletedTask;

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlScript(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}

class ReadOnlyHtmlTemplateElement : ReadOnlyHtmlElement, ITemplateElement
{
    public ReadOnlyHtmlTemplateElement(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
    {
    }

    public void PopulateFragment()
    {
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlTemplateElement(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}

class ReadOnlyHtmlFrameElement : ReadOnlyHtmlElement, IFrameElement
{
    public ReadOnlyHtmlFrameElement(ReadOnlyDocument? owner, StringOrMemory prefix = default)
        : base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
    {
    }

    public override IConstructableNode ShallowCopy()
    {
        var readOnlyElement = new ReadOnlyHtmlFrameElement(Owner)
        {
            _childNodes = _childNodes
        };
        return readOnlyElement;
    }
}
