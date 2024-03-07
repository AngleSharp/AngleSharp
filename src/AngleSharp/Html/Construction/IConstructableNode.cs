namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Common;
using AngleSharp.Dom;

/// <summary>
/// Represents a constructable node. Lowest common denominator for all DOM nodes.
/// </summary>
public interface IConstructableNode
{
    /// <summary>
    /// Gets a string containing the name of the Node. The structure of the
    /// name will differ with the name type.
    /// </summary>
    StringOrMemory NodeName { get; }

    /// <summary>
    /// Gets the associated node flags.
    /// </summary>
    NodeFlags Flags { get; }

    /// <summary>
    /// Gets a node that is the parent of this node. If there is no such
    /// node, like if this node is the top of the tree or if doesn't
    /// participate in a tree, this property returns null.
    /// </summary>
    IConstructableNode? Parent { get; set; }

    /// <summary>
    /// Gets a live NodeList containing all the children of this node.
    /// Being live means that if the children of the node change, the
    /// NodeList object is automatically updated.
    /// </summary>
    IConstructableNodeList ChildNodes { get; }

    /// <summary>
    /// Removes this node from its parent if any.
    /// </summary>
    void RemoveFromParent();

    /// <summary>
    /// Tries to remove the given child node from the list of children.
    /// </summary>
    void RemoveChild(IConstructableNode childNode);

    /// <summary>
    /// Removes the child node at the given index.
    /// </summary>
    void RemoveNode(Int32 idx, IConstructableNode childNode);

    /// <summary>
    /// Inserts the given child node at the given index.
    /// </summary>
    void InsertNode(Int32 idx, IConstructableNode childNode);

    /// <summary>
    /// Appends the given child node to the list of children.
    /// </summary>
    void AddNode(IConstructableNode node);

    /// <summary>
    /// Appends the given text to the list of children as a text node.
    /// </summary>
    /// <param name="text">Text to convert to text node</param>
    /// <param name="emitWhiteSpaceOnly">Flag to tell if whitespace only text node should be added</param>
    void AppendText(StringOrMemory text, Boolean emitWhiteSpaceOnly = false);

    /// <summary>
    /// Inserts the given text at the given index as a text node.
    /// </summary>
    /// <param name="idx">Index</param>
    /// <param name="text">Text to convert to text node</param>
    /// <param name="emitWhiteSpaceOnly">Flag to tell if whitespace only text node should be added</param>
    void InsertText(Int32 idx, StringOrMemory text, Boolean emitWhiteSpaceOnly = false);
}