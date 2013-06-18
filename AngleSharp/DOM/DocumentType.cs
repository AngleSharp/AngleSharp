using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents the document type node.
    /// </summary>
    [DOM("DocumentType")]
    public sealed class DocumentType : Node
    {
        #region Constants

        /// <summary>
        /// Gets the !DOCTYPE constant.
        /// </summary>
        internal const string Tag = "!DOCTYPE";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document type node.
        /// </summary>
        internal DocumentType()
        {
            _type = NodeType.DocumentType;
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the document type.
        /// </summary>
        [DOM("name")]
        public String Name 
        {
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the public ID of the document type.
        /// </summary>
        [DOM("publicId")]
        public String PublicId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the system ID of the document type.
        /// </summary>
        [DOM("systemId")]
        public String SystemId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the internal subset of the document type.
        /// </summary>
        [DOM("internalSubset")]
        public String InternalSubset 
        { 
            get; 
            set; 
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
        public override Node InsertChild(int index, Node child)
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
            var node = new DocumentType();
            CopyProperties(this, node, deep);
            node.Name = this.Name;
            node.PublicId = this.PublicId;
            node.SystemId = this.SystemId;
            node.InternalSubset = this.InternalSubset;
            return node;
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
        /// Inserts nodes before the current doctype.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current doctype.</returns>
        [DOM("before")]
        public DocumentType Before(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, this);
            }

            return this;
        }

        /// <summary>
        /// Inserts nodes after the current doctype.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current doctype.</returns>
        [DOM("after")]
        public DocumentType After(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.InsertBefore(node, NextSibling);
            }

            return this;
        }

        /// <summary>
        /// Replaces the current doctype with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        /// <returns>The current doctype.</returns>
        [DOM("replace")]
        public DocumentType Replace(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                _parent.ReplaceChild(node, this);
            }

            return this;
        }

        /// <summary>
        /// Removes the current doctype from the parent.
        /// </summary>
        /// <returns>The current doctype.</returns>
        [DOM("remove")]
        public DocumentType Remove()
        {
            if (_parent != null)
                _parent.RemoveChild(this);

            return this;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            return String.Format("<!DOCTYPE html{0}{1}>", 
                String.IsNullOrEmpty(PublicId) ? "" : " PUBLIC \"" + PublicId + "\"", 
                String.IsNullOrEmpty(SystemId) ? "" : (String.IsNullOrEmpty(PublicId) ? " SYSTEM" : "") + " \"" + SystemId + "\"");
        }

        #endregion
    }
}
