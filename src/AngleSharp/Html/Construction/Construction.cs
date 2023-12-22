namespace AngleSharp.Html.Construction;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Common;
using Parser.Tokens.Struct;
using Text;

internal interface IConstructableAttr
{
    public StringOrMemory Name { get; }
    public StringOrMemory Value { get; internal set; }
};

internal interface IConstructableNamedNodeMap
{
    IConstructableAttr? this[StringOrMemory name] { get; }
    Int32 Length { get; }
    bool SameAs(IConstructableNamedNodeMap? attributes);
}

internal interface IConstructableNode : IMarkupFormattable
{
    StringOrMemory NodeName { get; }
    NodeFlags Flags { get; }
    IConstructableNode? Parent { get; internal set; }
    IConstructableNodeList ChildNodes { get; }

    void RemoveFromParent();
    void RemoveChild(IConstructableNode childNode);
    void RemoveNode(int idx, IConstructableNode childNode);
    void InsertNode(int idx, IConstructableNode childNode);
    void AddNode(IConstructableNode node);
    void AppendText(StringOrMemory text, bool emitWhiteSpaceOnly = false);
    void InsertText(int idx, StringOrMemory text, bool emitWhiteSpaceOnly = false);
}

internal interface IConstructableNodeList : IEnumerable<IConstructableNode>
{
    IConstructableNode this[Int32 index] { get; }
    Int32 Length { get; }
    void Clear();
}

public class SourceReference : ISourceReference
{
    public SourceReference(TextPosition position)
    {
        Position = position;
    }

    public TextPosition Position { get; }
}

internal interface IConstructableElement : IConstructableNode
{
    StringOrMemory NamespaceUri { get; }
    StringOrMemory LocalName { get; }
    IConstructableNamedNodeMap Attributes { get; }
    ISourceReference? SourceReference { get; set; }

    void SetAttribute(String? ns, StringOrMemory name, StringOrMemory value);
    void SetOwnAttribute(StringOrMemory name, StringOrMemory value);
    StringOrMemory GetAttribute(StringOrMemory @namespace, StringOrMemory name);
    void SetAttributes(StructAttributes tagAttributes);
    bool HasAttribute(StringOrMemory name);
    void SetupElement();

    void AddComment(ref StructHtmlToken token);

    IConstructableNode ShallowCopy();
}

internal interface IConstructableMathElement : IConstructableElement;

internal interface IConstructableSvgElement : IConstructableElement;

internal interface IConstructableMetaElement : IConstructableElement
{
    void Handle();
}

internal interface IConstructableScriptElement: IConstructableElement
{
    internal Task RunAsync(CancellationToken cancel);
    internal Boolean Prepare(IConstructableDocument document);
}

internal interface IConstructableFrameElement : IConstructableElement;

internal interface IConstructableTemplateElement : IConstructableElement
{
    void PopulateFragment();
}

internal interface IConstructableDocument : IConstructableNode
{
    IReadOnlyTextSource Source { get; }
    IDisposable? Builder { get; set; }

    QuirksMode QuirksMode { get; set; }

    IConstructableElement? Head { get; }
    IConstructableElement DocumentElement { get; }

    void PerformMicrotaskCheckpoint();
    void ProvideStableState();

    void AddComment(ref StructHtmlToken token);

    void TrackError(Exception exception);
    Task WaitForReadyAsync(CancellationToken cancelToken);
    void ApplyManifest();
    void Clear();
}
