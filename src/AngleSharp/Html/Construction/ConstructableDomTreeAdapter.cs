namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Text;

internal readonly struct ConstructableDomNode : IHtmlTreeConstructionNode<ConstructableDomNode>, IEquatable<ConstructableDomNode>
{
    private readonly IConstructableNode? _node;

    public ConstructableDomNode(IConstructableNode? node)
    {
        _node = node;
    }

    public Boolean IsNull => _node is null;
    public Boolean IsTemplate => _node is IConstructableTemplateElement;
    public Boolean IsForm => _node is IConstructableFormElement;
    public Boolean IsScript => _node is IConstructableScriptElement;
    public StringOrMemory NodeName => _node!.NodeName;
    public StringOrMemory LocalName => Element.LocalName;
    public StringOrMemory Prefix => Element.Prefix;
    public StringOrMemory NamespaceUri => Element.NamespaceUri;
    public NodeFlags Flags => _node!.Flags;
    public ConstructableDomNode Parent => new(_node!.Parent);
    public Int32 ChildCount => _node!.ChildNodes.Length;
    public IElement? AsDomElement => _node as IElement;

    internal IConstructableElement ConstructableElement => Element;

    private IConstructableElement Element => (IConstructableElement)_node!;

    public ConstructableDomNode ChildAt(Int32 index) => new(_node!.ChildNodes[index]);

    public void ClearChildren() => _node!.ChildNodes.Clear();

    public void RemoveFromParent() => _node!.RemoveFromParent();

    public void RemoveChild(ConstructableDomNode child) => _node!.RemoveChild(child._node!);

    public void RemoveNode(Int32 index, ConstructableDomNode child) => _node!.RemoveNode(index, child._node!);

    public void InsertNode(Int32 index, ConstructableDomNode child) => _node!.InsertNode(index, child._node!);

    public void AddNode(ConstructableDomNode child) => _node!.AddNode(child._node!);

    public void AppendText(StringOrMemory text, Boolean emitWhiteSpaceOnly = false) =>
        _node!.AppendText(text, emitWhiteSpaceOnly);

    public void InsertText(Int32 index, StringOrMemory text, Boolean emitWhiteSpaceOnly = false) =>
        _node!.InsertText(index, text, emitWhiteSpaceOnly);

    public void AddComment(ref StructHtmlToken token) => Element.AddComment(ref token);

    public StringOrMemory GetAttribute(StringOrMemory namespaceUri, StringOrMemory localName) =>
        Element.GetAttribute(namespaceUri, localName);

    public Boolean HasAttribute(StringOrMemory name) => Element.HasAttribute(name);

    public void SetAttribute(String? namespaceUri, StringOrMemory name, StringOrMemory value) =>
        Element.SetAttribute(namespaceUri, name, value);

    public void SetOwnAttribute(StringOrMemory name, StringOrMemory value) => Element.SetOwnAttribute(name, value);

    public void SetAttributes(in StructAttributes attributes)
    {
        if (Element is IConstructableElementAttributesByRef byRef)
        {
            byRef.SetAttributes(in attributes);
        }
        else
        {
            Element.SetAttributes(attributes);
        }
    }

    public Boolean AttributesSame(ConstructableDomNode other) =>
        Element.Attributes.SameAs(other.Element.Attributes);

    public void SetupElement() => Element.SetupElement();

    public ConstructableDomNode ShallowCopy() => new(Element.ShallowCopy());

    public void SetSourceReference(ISourceReference sourceReference) => Element.SourceReference = sourceReference;

    public void PopulateFragment() => ((IConstructableTemplateElement)_node!).PopulateFragment();

    public void HandleMeta() => ((IConstructableMetaElement)_node!).Handle();

    public Boolean PrepareScript(IConstructableDocument document) =>
        ((IConstructableScriptElement)_node!).Prepare(document);

    public Task RunScriptAsync(CancellationToken cancel) =>
        ((IConstructableScriptElement)_node!).RunAsync(cancel);

    public Boolean Equals(ConstructableDomNode other) => ReferenceEquals(_node, other._node);

    public override Boolean Equals(Object? obj) => obj is ConstructableDomNode other && Equals(other);

    public override Int32 GetHashCode() => _node?.GetHashCode() ?? 0;

    public static Boolean operator ==(ConstructableDomNode left, ConstructableDomNode right) => left.Equals(right);

    public static Boolean operator !=(ConstructableDomNode left, ConstructableDomNode right) => !left.Equals(right);
}

internal sealed class ConstructableDomTreeFactory<TDocument, TElement>
    : IHtmlTreeConstructionFactory<TDocument, ConstructableDomNode>
    where TDocument : class, IConstructableDocument
    where TElement : class, IConstructableElement
{
    private readonly IDomConstructionElementFactory<TDocument, TElement> _factory;

    public ConstructableDomTreeFactory(IDomConstructionElementFactory<TDocument, TElement> factory)
    {
        _factory = factory;
    }

    public ConstructableDomNode Create(
        TDocument document,
        StringOrMemory localName,
        StringOrMemory prefix = default,
        NodeFlags flags = NodeFlags.None
    ) => new(_factory.Create(document, localName, prefix, flags));

    public ConstructableDomNode CreateNoScript(TDocument document, Boolean scripting) =>
        new(_factory.CreateNoScript(document, scripting));

    public ConstructableDomNode CreateDocumentType(
        TDocument document,
        StringOrMemory name,
        StringOrMemory publicIdentifier,
        StringOrMemory systemIdentifier
    ) => new(_factory.CreateDocumentType(document, name, publicIdentifier, systemIdentifier));

    public ConstructableDomNode CreateMath(TDocument document, StringOrMemory name = default) =>
        new(_factory.CreateMath(document, name));

    public ConstructableDomNode CreateSvg(TDocument document, StringOrMemory name = default) =>
        new(_factory.CreateSvg(document, name));

    public ConstructableDomNode CreateMeta(TDocument document) => new(_factory.CreateMeta(document));

    public ConstructableDomNode CreateScript(TDocument document, Boolean parserInserted, Boolean started) =>
        new(_factory.CreateScript(document, parserInserted, started));

    public ConstructableDomNode CreateFrame(TDocument document) => new(_factory.CreateFrame(document));

    public ConstructableDomNode CreateTemplate(TDocument document) => new(_factory.CreateTemplate(document));

    public ConstructableDomNode CreateForm(TDocument document) => new(_factory.CreateForm(document));

    public ConstructableDomNode CreateUnknown(TDocument document, StringOrMemory tagName) =>
        new(_factory.CreateUnknown(document, tagName));

    public TDocument CreateDocument(TextSource source, IBrowsingContext? context = null) =>
        _factory.CreateDocument(source, context);

    public ConstructableDomNode GetDocumentNode(TDocument document) => new(document);

    public ConstructableDomNode GetDocumentElement(TDocument document) => new(document.DocumentElement);

    public ConstructableDomNode GetHead(TDocument document) => new(document.Head);
}
