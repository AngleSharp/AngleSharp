namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the document type node.
    /// </summary>
    sealed class DocumentType : Node, IDocumentType
    {
        #region ctor

        /// <summary>
        /// Creates a new document type node.
        /// </summary>
        internal DocumentType()
        {
            _type = NodeType.DocumentType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of defined entities.
        /// </summary>
        public IEnumerable<Entity> Entities
        {
            get { return Enumerable.Empty<Entity>(); }
        }

        /// <summary>
        /// Gets a list of defined notations.
        /// </summary>
        public IEnumerable<Notation> Notations
        {
            get { return Enumerable.Empty<Notation>(); }
        }

        /// <summary>
        /// Gets or sets the name of the document type.
        /// </summary>
        public String Name 
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the public ID of the document type.
        /// </summary>
        public String PublicIdentifier 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the system ID of the document type.
        /// </summary>
        public String SystemIdentifier 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the internal subset of the document type.
        /// </summary>
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
        public override INode AppendChild(Node child)
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
        public override INode InsertBefore(Node newElement, INode referenceElement)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public override INode InsertChild(int index, Node child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public override INode RemoveChild(INode child)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        public override INode ReplaceChild(Node newChild, Node oldChild)
        {
            throw new DOMException(ErrorCode.NotSupported);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new DocumentType();
            CopyProperties(this, node, deep);
            node.Name = this.Name;
            node.PublicIdentifier = this.PublicIdentifier;
            node.SystemIdentifier = this.SystemIdentifier;
            node.InternalSubset = this.InternalSubset;
            return node;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public override String LookupNamespaceUri(String prefix)
        {
            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        public override String LookupPrefix(String namespaceURI)
        {
            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        public override Boolean IsDefaultNamespace(String namespaceURI)
        {
            return false;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            return String.Format("<!DOCTYPE {0}{1}{2}>",
                Name,
                String.IsNullOrEmpty(PublicIdentifier) ? "" : " PUBLIC \"" + PublicIdentifier + "\"", 
                String.IsNullOrEmpty(SystemIdentifier) ? "" : (String.IsNullOrEmpty(PublicIdentifier) ? " SYSTEM" : "") + " \"" + SystemIdentifier + "\"");
        }

        #endregion
    }
}
