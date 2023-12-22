using System;
using System.IO;
using AngleSharp;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.ReadOnly.Html;

internal abstract class ReadOnlyElement : ReadOnlyNode, IReadOnlyElement
{
    protected static readonly ReadOnlyNamedNodeMap EmptyAttributes = new ReadOnlyNamedNodeMap();

    protected ReadOnlyNamedNodeMap? _attributes;

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
        _attributes ??= new ReadOnlyNamedNodeMap();
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

        _attributes ??= new ReadOnlyNamedNodeMap();
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