namespace AngleSharp.Html.Construction;

using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Parser.Tokens.Struct;

/// <summary>
/// Represents a stable node identity used by the HTML tree-construction algorithm.
/// </summary>
/// <typeparam name="TSelf">The value-type identity exposed by a construction backend.</typeparam>
/// <remarks>
/// Unlike <see cref="IConstructableNode"/>, this contract does not require one CLR object per
/// parsed node. A backend may implement it with an arena reference and an integer handle while
/// still supporting all tree mutations required by the HTML parsing specification.
/// </remarks>
public interface IHtmlTreeConstructionNode<TSelf> : IEquatable<TSelf>
    where TSelf : struct, IHtmlTreeConstructionNode<TSelf>
{
    /// <summary>Gets whether this value is the backend's null node.</summary>
    Boolean IsNull { get; }

    /// <summary>Gets whether this node is a template element.</summary>
    Boolean IsTemplate { get; }

    /// <summary>Gets whether this node is a form element.</summary>
    Boolean IsForm { get; }

    /// <summary>Gets whether this node is a script element.</summary>
    Boolean IsScript { get; }

    /// <summary>Gets the node name.</summary>
    StringOrMemory NodeName { get; }

    /// <summary>Gets the local element name.</summary>
    StringOrMemory LocalName { get; }

    /// <summary>Gets the namespace prefix.</summary>
    StringOrMemory Prefix { get; }

    /// <summary>Gets the namespace URI.</summary>
    StringOrMemory NamespaceUri { get; }

    /// <summary>Gets the parser-relevant node flags.</summary>
    NodeFlags Flags { get; }

    /// <summary>Gets the current parent or the null node.</summary>
    TSelf Parent { get; }

    /// <summary>Gets the number of children.</summary>
    Int32 ChildCount { get; }

    /// <summary>Gets a child by index.</summary>
    TSelf ChildAt(Int32 index);

    /// <summary>Removes every child from this node.</summary>
    void ClearChildren();

    /// <summary>Removes this node from its parent.</summary>
    void RemoveFromParent();

    /// <summary>Removes a child from this node.</summary>
    void RemoveChild(TSelf child);

    /// <summary>Removes the child at the supplied index.</summary>
    void RemoveNode(Int32 index, TSelf child);

    /// <summary>Inserts a child at the supplied index.</summary>
    void InsertNode(Int32 index, TSelf child);

    /// <summary>Appends a child.</summary>
    void AddNode(TSelf child);

    /// <summary>Appends character data.</summary>
    void AppendText(StringOrMemory text, Boolean emitWhiteSpaceOnly = false);

    /// <summary>Inserts character data at the supplied child index.</summary>
    void InsertText(Int32 index, StringOrMemory text, Boolean emitWhiteSpaceOnly = false);

    /// <summary>Adds a comment represented by the token.</summary>
    void AddComment(ref StructHtmlToken token);

    /// <summary>Gets an attribute value or an empty value when it is absent.</summary>
    StringOrMemory GetAttribute(StringOrMemory namespaceUri, StringOrMemory localName);

    /// <summary>Gets whether an attribute is present.</summary>
    Boolean HasAttribute(StringOrMemory name);

    /// <summary>Sets an attribute value.</summary>
    void SetAttribute(String? namespaceUri, StringOrMemory name, StringOrMemory value);

    /// <summary>Sets a parser-owned attribute without namespace validation.</summary>
    void SetOwnAttribute(StringOrMemory name, StringOrMemory value);

    /// <summary>Sets attributes directly from a tokenizer attribute buffer.</summary>
    void SetAttributes(in StructAttributes attributes);

    /// <summary>Gets whether this node and another node have equivalent attributes.</summary>
    Boolean AttributesSame(TSelf other);

    /// <summary>Completes element-specific setup.</summary>
    void SetupElement();

    /// <summary>Creates a shallow element copy with a distinct identity.</summary>
    TSelf ShallowCopy();

    /// <summary>Stores a source reference when requested by parser options.</summary>
    void SetSourceReference(ISourceReference sourceReference);

    /// <summary>Moves template children into template contents.</summary>
    void PopulateFragment();

    /// <summary>Runs meta-element handling.</summary>
    void HandleMeta();

    /// <summary>Prepares a script element for execution.</summary>
    Boolean PrepareScript(IConstructableDocument document);

    /// <summary>Runs a prepared script element.</summary>
    Task RunScriptAsync(CancellationToken cancel);

    /// <summary>
    /// Gets the full DOM element represented by this identity, if the backend creates one.
    /// </summary>
    IElement? AsDomElement { get; }
}
