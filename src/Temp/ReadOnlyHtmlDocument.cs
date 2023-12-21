namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;
using Text;

internal class ReadOnlyHtmlDocument : ReadOnlyElement
{
    public ReadOnlyHtmlDocument(
        ReadOnlyElement? owner = null,
        StringOrMemory name = default,
        NodeType type = NodeType.Element,
        NodeFlags flags = NodeFlags.HtmlMember) : base(owner, name, type, flags)
    {
        Source = new PrefetchedTextSource("");

    }

    public IReadOnlyTextSource Source { get; init; }

    public QuirksMode QuirksMode { get; set; }

    public ReadOnlyHtmlHeadElement? Head { get; set; }

    public ReadOnlyElement? DocumentElement { get; set; }

    public void AddNode(ReadOnlyElement readOnlyDocumentType)
    {
        ChildNodes.Add(readOnlyDocumentType);
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void TrackError(NotSupportedException notSupportedException)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyElement CreateMathElement(StringOrMemory tagName)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyElement CreateSvgElement(StringOrMemory tagName)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyElement CreateHtmlElement(StringOrMemory tagName)
    {
        if (tagName == TagNames.P)
        {
            return new ReadOnlyHtmlElement(this, tagName, NodeType.Element, NodeFlags.Special | NodeFlags.ImplicitlyClosed | NodeFlags.ImpliedEnd);
        }

        return new ReadOnlyElement(this, tagName);
    }

    public void ApplyManifest()
    {
        // throw new NotImplementedException();
    }
}