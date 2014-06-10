namespace AngleSharp.DOM
{
    using System;
    using AngleSharp.DOM.Collections;

    /// <summary>
    /// A Node is an interface from which a number of DOM types inherit, 
    /// and allows these various types to be treated similarly.
    /// </summary>
    [DomName("Node")]
    public interface INode : IEventTarget
    {
        /// <summary>
        /// Gets a string representing the base URL. 
        /// </summary>
        [DomName("baseURI")]
        String BaseUri { get; }

        /// <summary>
        /// Gets a string containing the name of the Node. The structure
        /// of the name will differ with the name type. 
        /// </summary>
        [DomName("nodeName")]
        String NodeName { get; }

        /// <summary>
        /// Gets a live NodeList containing all the children of this
        /// node. NodeList being live means that if the children of
        /// the Node change, the NodeList object is automatically updated.
        /// </summary>
        [DomName("childNodes")]
        NodeList ChildNodes { get; }

        /// <summary>
        /// Clones the Node, and optionally, all of its contents.
        /// By default, it clones the content of the node.
        /// </summary>
        /// <param name="deep">Optionally: Sets if all of the content should be cloned as well.</param>
        /// <returns>The cloned Node.</returns>
        [DomName("cloneNode")]
        Node Clone(Boolean deep = true);

        /// <summary>
        /// Determines if two nodes are equal.
        /// </summary>
        /// <param name="otherNode">The node to be compared to the node that is executing the method.</param>
        /// <returns>True if the node specified in the otherNode parameter is equal to the current node.</returns>
        [DomName("isEqualNode")]
        Boolean IsEqualNode(Node otherNode);

        /// <summary>
        /// Compares the position of two nodes in a document.
        /// </summary>
        /// <param name="otherNode">The node to be compared to the reference node, which is the node executing the method.</param>
        /// <returns>The relation between the two nodes.</returns>
        [DomName("compareDocumentPosition")]
        DocumentPosition CompareDocumentPosition(Node otherNode);

        /// <summary>
        /// Cleans up all the text nodes under this element, i.e. merges
        /// adjacent and removes empty text nodes.
        /// </summary>
        [DomName("normalize")]
        void Normalize();

        /// <summary>
        /// Gets the Document that this node belongs to. If no document
        /// is associated with it, returns null.
        /// </summary>
        [DomName("ownerDocument")]
        Document Owner { get; }

        /// <summary>
        /// Gets an Element that is the parent of this node. If the node has
        /// no parent, or if that parent is not an Element, this property
        /// returns null.
        /// </summary>
        [DomName("parentElement")]
        Element ParentElement { get; }

        /// <summary>
        /// Gets a Node that is the parent of this node. If there is no such node,
        /// like if this node is the top of the tree or if doesn't participate in
        /// a tree, this property returns null.
        /// </summary>
        [DomName("parentNode")]
        Node Parent { get; }

        /// <summary>
        /// Returns true if other is an inclusive descendant of the context object,
        /// and false otherwise (including when other is null).
        /// </summary>
        /// <param name="otherNode">The Node to check the childs for.</param>
        /// <returns>True if the given node is contained within this Node, otherwise false.</returns>
        [DomName("contains")]
        Boolean Contains(Node otherNode);

        /// <summary>
        /// Gets a Node representing the first direct child node
        /// of the node, or null if the node has no child.
        /// </summary>
        [DomName("firstChild")]
        Node FirstChild { get; }

        /// <summary>
        /// Gets a Node representing the last direct child node
        /// of the node, or null if the node has no child.
        /// </summary>
        [DomName("lastChild")]
        Node LastChild { get; }

        /// <summary>
        /// Gets a Node representing the next node in the tree,
        /// or null if there isn't such node.
        /// </summary>
        [DomName("nextSibling")]
        Node NextSibling { get; }

        /// <summary>
        /// Gets a Node representing the previous node in the tree,
        /// or null if there isn't such node.
        /// </summary>
        [DomName("previousSibling")]
        Node PreviousSibling { get; }

        /// <summary>
        /// Indicates whether or not a namespace is the default
        /// namespace for a document.
        /// </summary>
        /// <param name="namespaceUri">The namespace to be compared to the default namespace.</param>
        /// <returns>True if the given namespace URI is the default for the current document.</returns>
        [DomName("isDefaultNamespace")]
        Boolean IsDefaultNamespace(String namespaceUri);

        /// <summary>
        /// Gets the Uniform Resource Identifier (URI) of the
        /// namespace associated with a namespace prefix, if any.
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
        /// Gets a string representing the value of an object. For most Node
        /// type, this returns null and any set operation is ignored.
        /// </summary>
        [DomName("nodeValue")]
        String NodeValue { get; set; }

        /// <summary>
        /// Gets a string representing the textual content of an element and
        /// all its descendants.
        /// </summary>
        [DomName("textContent")]
        String TextContent { get; set; }

        /// <summary>
        /// Gets a Boolean indicating if the element has any child nodes, or not.
        /// </summary>
        [DomName("hasChildNodes")]
        Boolean HasChilds { get; }

        /// <summary>
        /// Inserts a Node as the last child node of this element.
        /// </summary>
        /// <param name="child">The Node to be appended.</param>
        /// <returns>The current Node.</returns>
        [DomName("appendChild")]
        Node AppendChild(Node child);

        /// <summary>
        /// Inserts the first Node given in a parameter immediately
        /// before the second, child of this element, Node
        /// </summary>
        /// <param name="newElement">The Node to be inserted.</param>
        /// <param name="referenceElement">The element that will succeed the new element.</param>
        /// <returns>The current Node.</returns>
        [DomName("insertBefore")]
        Node InsertBefore(Node newElement, Node referenceElement);

        /// <summary>
        /// Removes a child node from the current element, which must
        /// be a child of the current node.
        /// </summary>
        /// <param name="child">The child to be removed.</param>
        /// <returns>The current Node.</returns>
        [DomName("removeChild")]
        Node RemoveChild(Node child);

        /// <summary>
        /// Replaces one child Node of the current one with the second
        /// one given in the parameters.
        /// </summary>
        /// <param name="newChild">The child to be inserted.</param>
        /// <param name="oldChild">The child to be removed.</param>
        /// <returns>The current Node.</returns>
        [DomName("replaceChild")]
        Node ReplaceChild(Node newChild, Node oldChild);
    }
}
