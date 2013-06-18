using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a notation node.
    /// </summary>
    [DOM("Notation")]
    public sealed class Notation : Node
    {
        #region ctor

        /// <summary>
        /// Creates a new notation node.
        /// </summary>
        internal Notation()
        {
            _type = NodeType.Notation;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        [DOM("appendChild")]
        public override Node AppendChild(Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        [DOM("insertBefore")]
        public override Node InsertBefore(Node newElement, Node referenceElement)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        [DOM("insertChild")]
        public override Node InsertChild(Int32 index, Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        [DOM("removeChild")]
        public override Node RemoveChild(Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        [DOM("replaceChild")]
        public override Node ReplaceChild(Node newChild, Node oldChild)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = new Notation();
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        [DOM("lookupPrefix")]
        public override String LookupPrefix(String namespaceURI)
        {
            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        [DOM("isDefaultNamespace")]
        public override Boolean IsDefaultNamespace(String namespaceURI)
        {
            return false;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        [DOM("lookupNamespaceURI")]
        public override String LookupNamespaceURI(String prefix)
        {
            return null;
        }

        #endregion
    }
}
