namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
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
        internal DocumentType(Document owner, String name)
            : base(owner, name, NodeType.DocumentType)
        {
            PublicIdentifier = String.Empty;
            SystemIdentifier = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list
        /// of nodes,  null if the current element is the first element in that
        /// list.
        /// </summary>
        public IElement PreviousElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent == null)
                    return null;

                var found = false;

                for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && parent.ChildNodes[i] is IElement)
                        return (IElement)parent.ChildNodes[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list
        /// of nodes, or null if the current element is the last element in
        /// that list.
        /// </summary>
        public IElement NextElementSibling
        {
            get
            {
                var parent = Parent;
                
                if (parent == null)
                    return null;

                var n = parent.ChildNodes.Length;
                var found = false;

                for (int i = 0; i < n; i++)
                {
                    if (parent.ChildNodes[i] == this)
                        found = true;
                    else if (found && parent.ChildNodes[i] is IElement)
                        return (IElement)parent.ChildNodes[i];
                }

                return null;
            }
        }

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
            get { return NodeName; }
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
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be 
        /// cloned, or false to clone only the specified node. 
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new DocumentType(Owner, Name)
            {
                PublicIdentifier = PublicIdentifier,
                SystemIdentifier = SystemIdentifier,
                InternalSubset = InternalSubset
            };
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Inserts nodes before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        public void Before(params INode[] nodes)
        {
            this.InsertBefore(nodes);
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public void After(params INode[] nodes)
        {
            this.InsertAfter(nodes);
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        public void Replace(params INode[] nodes)
        {
            this.ReplaceWith(nodes);
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        public void Remove()
        {
            this.RemoveFromParent();
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return formatter.Doctype(this);
        }

        #endregion

        #region Helpers

        protected override String LocateNamespace(String prefix)
        {
            return null;
        }

        protected override String LocatePrefix(String namespaceUri)
        {
            return null;
        }

        #endregion
    }
}
