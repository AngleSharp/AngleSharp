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
using AngleSharp.Html.Construction;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.ReadOnly.Html;
using AngleSharp.Text;


internal class ReadOnlyCharacterData : ReadOnlyNode
{
    internal ReadOnlyCharacterData(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType)
        : this(owner, name, nodeType, StringOrMemory.Empty)
    {
    }

    internal ReadOnlyCharacterData(ReadOnlyDocument? owner, StringOrMemory name, NodeType nodeType, StringOrMemory content)
        : base(owner, content, nodeType)
    {
    }

    public StringOrMemory Content => NodeName;
}

internal class ReadOnlyProcessingInstruction : ReadOnlyCharacterData, IReadOnlyProcessingInstructionNode
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

internal class ReadOnlyTextNode : ReadOnlyCharacterData, IReadOnlyTextNode
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

internal class ReadOnlyComment : ReadOnlyCharacterData, IReadOnlyCommentNode
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

internal class ReadOnlyAttr : IReadOnlyAttr, IConstructableAttr
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
    private static ReadOnlySpan<Char> WhiteSpace => " \t\r\n".AsSpan();
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
        _flags = flags;
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
        _childNodes ??= new ConstructableNodeList();
        childNode.Parent = this;
        _childNodes?.Insert(idx, childNode);
    }

    public void AddNode(IConstructableNode node)
    {
        _childNodes ??= new ConstructableNodeList();
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
    public IDisposable? Builder { get; set; }
    public QuirksMode QuirksMode { get; set; }

    public void TrackError(Exception exception)
    {
    }

    public Task WaitForReadyAsync(CancellationToken cancelToken)
    {
        return Task.CompletedTask;
    }

    public Task FinishLoadingAsync()
    {
        return Task.CompletedTask;
    }

    public IConstructableElement Head => throw new NotImplementedException();

    public IConstructableElement DocumentElement => throw new NotImplementedException();

    public bool IsLoading => false;

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

    IReadOnlyElement IReadOnlyDocument.Body => throw new NotImplementedException();
    IReadOnlyElement IReadOnlyDocument.Head => throw new NotImplementedException();
    IReadOnlyNode? IReadOnlyNode.Parent => throw new NotImplementedException();

    IReadOnlyNodeList IReadOnlyNode.ChildNodes => _childNodes ?? EmptyChildNodes;

    public void Dispose()
    {
        Source.Dispose();
        Builder?.Dispose();
    }
}

internal class ConstructableNodeList : IConstructableNodeList, IReadOnlyNodeList
{
    private readonly List<IConstructableNode> _nodes;

    public ConstructableNodeList()
    {
        _nodes = new List<IConstructableNode>();
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

internal abstract class ReadOnlyElement : ReadOnlyNode, IReadOnlyElement
{
    protected static readonly ConstructableNamedNodeMap EmptyAttributes = new ConstructableNamedNodeMap();

    protected ConstructableNamedNodeMap? _attributes;

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

    public ISourceReference? SourceReference
    {
        get => null;
        set {}
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
        if (attributes.Count == 0)
            return;

        _attributes ??= new ConstructableNamedNodeMap();
        for (int i = 0; i < attributes.Count; i++)
        {
            var attribute = attributes[i];
            SetAttribute(null, attribute.Name, attribute.Value);
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
    protected readonly List<IConstructableAttr> _attributes;

    public ConstructableNamedNodeMap()
    {
        _attributes = new List<IConstructableAttr>();
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

class ReadOnlyHtmlElement : ReadOnlyElement, IConstructableSvgElement, IConstructableMathElement, IReadOnlyElement
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

    public StringOrMemory Prefix => StringOrMemory.Empty;

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

class ReadOnlyHtmlMeta : ReadOnlyHtmlElement, IConstructableMetaElement
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

class ReadOnlyHtmlScript : ReadOnlyHtmlElement, IConstructableScriptElement
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

class ReadOnlyHtmlTemplateElement : ReadOnlyHtmlElement, IConstructableTemplateElement
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

class ReadOnlyHtmlFrameElement : ReadOnlyHtmlElement, IConstructableFrameElement
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
