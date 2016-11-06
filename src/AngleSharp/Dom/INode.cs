namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A Node is an interface from which a number of DOM types inherit, and
    /// allows these various types to be treated similarly.
    /// </summary>
    [DomName("Node")]
    public interface INode : IEventTarget, IMarkupFormattable
    {
        /// <summary>
        /// Gets a string representing the base URL. 
        /// </summary>
        [DomName("baseURI")]
        String BaseUri { get; }

        /// <summary>
        /// Gets the base url.
        /// </summary>
        Url BaseUrl { get; }

        /// <summary>
        /// Gets a string containing the name of the Node. The structure of the
        /// name will differ with the name type. 
        /// </summary>
        [DomName("nodeName")]
        String NodeName { get; }

        /// <summary>
        /// Gets a live NodeList containing all the children of this node.
        /// Being live means that if the children of the node change, the
        /// NodeList object is automatically updated.
        /// </summary>
        [DomName("childNodes")]
        INodeList ChildNodes { get; }

        /// <summary>
        /// Clones the node, and optionally, all of its contents.
        /// By default, it clones the content of the node.
        /// </summary>
        /// <param name="deep">
        /// Optionally: Sets if all of the content should be cloned as well.
        /// </param>
        /// <returns>The cloned node.</returns>
        [DomName("cloneNode")]
        INode Clone(Boolean deep = true);

        /// <summary>
        /// Determines if two nodes are equal.
        /// </summary>
        /// <param name="otherNode">
        /// The node to be compared to the node that is executing the method.
        /// </param>
        /// <returns>
        /// True if the node specified in the otherNode parameter is equal to
        /// the current node.
        /// </returns>
        [DomName("isEqualNode")]
        Boolean Equals(INode otherNode);

        /// <summary>
        /// Compares the position of two nodes in a document.
        /// </summary>
        /// <param name="otherNode">
        /// The node to be compared to the reference node, which is the node
        /// executing the method.
        /// </param>
        /// <returns>The relation between the two nodes.</returns>
        [DomName("compareDocumentPosition")]
        DocumentPositions CompareDocumentPosition(INode otherNode);

        /// <summary>
        /// Cleans up all the text nodes under this element, i.e. merges
        /// adjacent and removes empty text nodes.
        /// </summary>
        [DomName("normalize")]
        void Normalize();

        /// <summary>
        /// Gets the Document that this node belongs to. If no document is
        /// associated with it, returns null.
        /// </summary>
        [DomName("ownerDocument")]
        IDocument Owner { get; }

        /// <summary>
        /// Gets an Element that is the parent of this node. If the node has no
        /// parent, or if that parent is not an Element, this property returns
        /// null.
        /// </summary>
        [DomName("parentElement")]
        IElement ParentElement { get; }

        /// <summary>
        /// Gets a node that is the parent of this node. If there is no such
        /// node, like if this node is the top of the tree or if doesn't
        /// participate in a tree, this property returns null.
        /// </summary>
        [DomName("parentNode")]
        INode Parent { get; }

        /// <summary>
        /// Returns true if other is an inclusive descendant of the context
        /// object, and false otherwise (including when other is null).
        /// </summary>
        /// <param name="otherNode">The Node to check the childs for.</param>
        /// <returns>
        /// True if the given node is contained within this Node, otherwise
        /// false.
        /// </returns>
        [DomName("contains")]
        Boolean Contains(INode otherNode);

        /// <summary>
        /// Gets a Node representing the first direct child node of the node,
        /// or null if the node has no child.
        /// </summary>
        [DomName("firstChild")]
        INode FirstChild { get; }

        /// <summary>
        /// Gets a node representing the last direct child node of the node,
        /// or null if the node has no child.
        /// </summary>
        [DomName("lastChild")]
        INode LastChild { get; }

        /// <summary>
        /// Gets a Node representing the next node in the tree, or null if
        /// there isn't such node.
        /// </summary>
        [DomName("nextSibling")]
        INode NextSibling { get; }

        /// <summary>
        /// Gets a Node representing the previous node in the tree, or null if
        /// there isn't such node.
        /// </summary>
        [DomName("previousSibling")]
        INode PreviousSibling { get; }

        /// <summary>
        /// Indicates whether or not a namespace is the default namespace for a
        /// document.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace to be compared to the default namespace.
        /// </param>
        /// <returns>
        /// True if the given namespace URI is the default for the current
        /// document.
        /// </returns>
        [DomName("isDefaultNamespace")]
        Boolean IsDefaultNamespace(String namespaceUri);

        /// <summary>
        /// Gets the Uniform Resource Identifier (URI) of the namespace
        /// associated with a namespace prefix, if any.
        /// </summary>
        /// <param name="prefix">The namespace prefix.</param>
        /// <returns>The URI of the namespace.</returns>
        [DomName("lookupNamespaceURI")]
        String LookupNamespaceUri(String prefix);

        /// <summary>
        /// Gets the namespace prefix associated with a Uniform
        /// Resource Identifier (URI), if any.
        /// </summary>
        /// <param name="namespaceUri">The URI.</param>
        /// <returns>The namespace prefix associated with the URI.</returns>
        [DomName("lookupPrefix")]
        String LookupPrefix(String namespaceUri);

        /// <summary>
        /// Gets an unsigned short representing the type of the node. 
        /// </summary>
        [DomName("nodeType")]
        NodeType NodeType { get; }

        /// <summary>
        /// Gets or sets a string representing the value of an object. For most
        /// node types, this returns null and any set operation is ignored.
        /// </summary>
        [DomName("nodeValue")]
        String NodeValue { get; set; }

        /// <summary>
        /// Gets or sets the textual content of an element and all its
        /// descendants.
        /// </summary>
        [DomName("textContent")]
        String TextContent { get; set; }

        /// <summary>
        /// Gets an indicator if the element has any child nodes, or not.
        /// </summary>
        [DomName("hasChildNodes")]
        Boolean HasChildNodes { get; }

        /// <summary>
        /// Inserts a node as the last child node of this element.
        /// </summary>
        /// <param name="child">The node to be appended.</param>
        /// <returns>The appended Node.</returns>
        [DomName("appendChild")]
        INode AppendChild(INode child);

        /// <summary>
        /// Inserts the newElement immediately before the referenceElement.
        /// </summary>
        /// <param name="newElement">The node to be inserted.</param>
        /// <param name="referenceElement">
        /// The existing child element that will succeed the new element.
        /// </param>
        /// <returns>The inserted node.</returns>
        [DomName("insertBefore")]
        INode InsertBefore(INode newElement, INode referenceElement);

        /// <summary>
        /// Removes a child node from the current element, which must be a
        /// child of the current node.
        /// </summary>
        /// <param name="child">The child to be removed.</param>
        /// <returns>The removed node.</returns>
        [DomName("removeChild")]
        INode RemoveChild(INode child);

        /// <summary>
        /// Replaces one child node of the current one with the second one
        /// given in the parameters.
        /// </summary>
        /// <param name="newChild">The child to be inserted.</param>
        /// <param name="oldChild">The child to be removed.</param>
        /// <returns>The old node, if any.</returns>
        [DomName("replaceChild")]
        INode ReplaceChild(INode newChild, INode oldChild);
    }
}
