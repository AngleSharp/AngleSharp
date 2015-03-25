namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Linq;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    [DebuggerStepThrough]
    class Node : EventTarget, INode, IEquatable<INode>
    {
        #region Fields

        readonly NodeType _type;
        readonly String _name;
        readonly NodeFlags _flags;

        Document _owner;
        Url _baseUri;
        Node _parent;
        NodeList _children;

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a new node.
        /// </summary>
        internal Node(Document owner, String name, NodeType type = NodeType.Element, NodeFlags flags = NodeFlags.None)
        {
            _owner = owner;
            _name = name ?? String.Empty;
            _type = type;
            _children = new NodeList();
            _flags = flags;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a boolean value indicating whether the current Node has child
        /// nodes or not.
        /// </summary>
        public Boolean HasChildNodes
        {
            get { return _children.Length != 0; }
        }

        /// <summary>
        /// Gets the absolute base URI of a node or null if unable to obtain an
        /// absolute URI.
        /// </summary>
        public String BaseUri
        {
            get 
            {
                var url = BaseUrl;

                if (url != null)
                    return url.Href;

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the base url of the node.
        /// </summary>
        public Url BaseUrl
        {
            get
            {
                if (_baseUri != null)
                    return _baseUri;
                else if (_parent != null)
                    return _parent.BaseUrl;
                else if (_owner != null)
                    return _owner._baseUri ?? _owner.DocumentUrl;
                else if (_type == NodeType.Document)
                    return ((Document)this).DocumentUrl;

                return null;
            }
            set { _baseUri = value; }
        }

        /// <summary>
        /// Gets the type of this node.
        /// </summary>
        public NodeType NodeType 
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets the value of the current node.
        /// </summary>
        public virtual String NodeValue 
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        public virtual String TextContent
        {
            get { return null; }
            set { }
        }

        INode INode.PreviousSibling
        {
            get { return PreviousSibling; }
        }

        INode INode.NextSibling
        {
            get { return NextSibling; }
        }

        INode INode.FirstChild
        {
            get { return FirstChild; }
        }

        INode INode.LastChild
        {
            get { return LastChild; }
        }

        IDocument INode.Owner
        {
            get { return Owner; }
        }

        INode INode.Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets or sets the parent element of this node.
        /// </summary>
        public IElement ParentElement
        {
            get { return _parent as IElement; }
        }

        INodeList INode.ChildNodes
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the tag name for this node.
        /// </summary>
        public String NodeName
        {
            get { return _name; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the node immediately preceding this node's parent's list of
        /// nodes, null if the specified node is the first in that list.
        /// </summary>
        internal Node PreviousSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var n = _parent._children.Length;

                for (int i = 1; i < n; i++)
                {
                    if (_parent._children[i] == this)
                        return _parent._children[i - 1];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the node immediately following this node's parent's list of
        /// nodes, or null if the current node is the last node in that list.
        /// </summary>
        internal Node NextSibling
        {
            get
            {
                if (_parent == null)
                    return null;

                var n = _parent._children.Length - 1;

                for (int i = 0; i < n; i++)
                {
                    if (_parent._children[i] == this)
                        return _parent._children[i + 1];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the first child node of this node.
        /// </summary>
        internal Node FirstChild
        {
            get { return _children.Length > 0 ? _children[0] : null; }
        }

        /// <summary>
        /// Gets the last child node of this node.
        /// </summary>
        internal Node LastChild
        {
            get { return _children.Length > 0 ? _children[_children.Length - 1] : null; }
        }

        /// <summary>
        /// Gets the flags of this node.
        /// </summary>
        internal NodeFlags Flags
        {
            get { return _flags; }
        }

        /// <summary>
        /// Gets or sets the children of this node.
        /// </summary>
        internal NodeList ChildNodes
        {
            get { return _children; }
            set { _children = value; }
        }

        /// <summary>
        /// Gets the parent node of this node, which is either an Element node,
        /// a Document node, or a DocumentFragment node.
        /// </summary>
        internal Node Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Gets the owner document of the node.
        /// </summary>
        internal Document Owner
        {
            get { return _type != NodeType.Document ? _owner : null; }
            set
            {
                if (_owner == value)
                    return;

                var oldDocument = _owner;
                _owner = value;

                for (int i = 0; i < _children.Length; i++)
                    _children[i].Owner = value;

                if (oldDocument != null)
                    NodeIsAdopted(oldDocument);
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Appends the given characters to the node.
        /// </summary>
        /// <param name="s">The characters to append.</param>
        internal void AppendText(String s)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild == null)
                AddNode(new TextNode(Owner, s));
            else
                lastChild.Append(s);
        }

        /// <summary>
        /// Inserts the given character in the node.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="s">The characters to append.</param>
        internal void InsertText(Int32 index, String s)
        {
            if (index > 0 && index <= _children.Length && _children[index - 1].NodeType == NodeType.Text)
            {
                var node = (IText)_children[index - 1];
                node.Append(s);
            }
            else if (index >= 0 && index < _children.Length && _children[index].NodeType == NodeType.Text)
            {
                var node = (IText)_children[index];
                node.Insert(0, s);
            }
            else
                InsertNode(index, new TextNode(Owner, s));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public INode AppendChild(INode child)
        {
            return this.PreInsert(child, null);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified
        /// index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public INode InsertChild(Int32 index, INode child)
        {
            return this.PreInsert(child, _children[index]);
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of
        /// the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">
        /// The node before which newElement is inserted. If referenceElement
        /// is null, newElement is inserted at the end of the list of child
        /// nodes.
        /// </param>
        /// <returns>The inserted node.</returns>
        public INode InsertBefore(INode newElement, INode referenceElement)
        {
            return this.PreInsert(newElement, referenceElement);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">
        /// The new node to replace oldChild. If it already exists in the DOM,
        /// it is first removed.
        /// </param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>
        /// The replaced node. This is the same node as oldChild.
        /// </returns>
        public INode ReplaceChild(INode newChild, INode oldChild)
        {
            return this.ReplaceChild(newChild as Node, oldChild as Node, false);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public INode RemoveChild(INode child)
        {
            return this.PreRemove(child);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public virtual INode Clone(Boolean deep = true)
        {
            var node = new Node(_owner, _name, _type, _flags);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Compares the position of the current node against another node in
        /// any other document.
        /// </summary>
        /// <param name="otherNode">
        /// The node that's being compared against.
        /// </param>
        /// <returns>
        /// The relationship that otherNode has with node, given in a bitmask.
        /// </returns>
        public DocumentPositions CompareDocumentPosition(INode otherNode)
        {
            if (this == otherNode)
                return DocumentPositions.Same;

            if (_owner != otherNode.Owner)
                return DocumentPositions.Disconnected | DocumentPositions.ImplementationSpecific | (otherNode.GetHashCode() > GetHashCode() ? DocumentPositions.Following : DocumentPositions.Preceding);
            else if (otherNode.IsAncestorOf(this))
                return DocumentPositions.Contains | DocumentPositions.Preceding;
            else if (otherNode.IsDescendantOf(this))
                return DocumentPositions.ContainedBy | DocumentPositions.Following;
            else if (otherNode.IsPreceding(this))
                return DocumentPositions.Preceding;

            return DocumentPositions.Following;
        }

        /// <summary>
        /// Indicates whether a node is a descendent of this node.
        /// </summary>
        /// <param name="otherNode">
        /// The node that's being compared against.
        /// </param>
        /// <returns>
        /// The return value is true if otherNode is a descendent of node, or
        /// node itself. Otherwise the return value is false.
        /// </returns>
        public Boolean Contains(INode otherNode)
        {
            return this.IsInclusiveAncestorOf(otherNode);
        }

        /// <summary>
        /// Puts the specified node and all of its subtree into a "normalized"
        /// form. In a normalized subtree, no text nodes in the subtree are
        /// empty and there are no adjacent text nodes.
        /// </summary>
        public void Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
            {
                var text = _children[i] as TextNode;

                if (text != null)
                {
                    var length = text.Length;

                    if (length == 0)
                    {
                        RemoveChild(text, false);
                        i--;
                    }
                    else
                    {
                        var sb = Pool.NewStringBuilder();
                        var sibling = text;
                        var end = i;

                        while ((sibling = sibling.NextSibling as TextNode) != null)
                        {
                            sb.Append(sibling.Data);
                            end++;

                            _owner.ForEachRange(m => m.Head == sibling, m => m.StartWith(text, length));
                            _owner.ForEachRange(m => m.Tail == sibling, m => m.EndWith(text, length));
                            _owner.ForEachRange(m => m.Head == sibling.Parent && m.Start == end, m => m.StartWith(text, length));
                            _owner.ForEachRange(m => m.Tail == sibling.Parent && m.End == end, m => m.EndWith(text, length));

                            length += sibling.Length;
                        }

                        text.Replace(text.Length, 0, sb.ToPool());

                        for (int j = end; j > i; j--)
                            RemoveChild(_children[j], false);
                    }
                }
                else if (_children[i].HasChildNodes)
                    _children[i].Normalize();
            }
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on
        /// the given node if found (and null if not). Supplying null for the
        /// prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public String LookupNamespaceUri(String prefix)
        {
            if (String.IsNullOrEmpty(prefix))
                prefix = null;

            return LocateNamespace(prefix);
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if
        /// not. When multiple prefixes are possible, the result is
        /// implementation-dependent.
        /// </summary>
        /// <param name="namespaceUri">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        public String LookupPrefix(String namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                return null;

            return LocatePrefix(namespaceUri);
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the
        /// namespace is the default namespace on the given node or false if
        /// not.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string representing the namespace against which the element will
        /// be checked.
        /// </param>
        /// <returns>
        /// True if the given namespaceURI is the default namespace.
        /// </returns>
        public Boolean IsDefaultNamespace(String namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                namespaceUri = null;

            var defaultNamespace = LocateNamespace(null);
            return defaultNamespace == namespaceUri;
        }

        /// <summary>
        /// Tests whether two nodes are equal.
        /// </summary>
        /// <param name="otherNode">The node to compare equality with.</param>
        /// <returns>True if they are equal, otherwise false.</returns>
        public virtual Boolean Equals(INode otherNode)
        {
            if (BaseUri != otherNode.BaseUri || NodeName != otherNode.NodeName || ChildNodes.Length != otherNode.ChildNodes.Length)
                return false;

            for (int i = 0; i < _children.Length; i++)
            {
                if (!_children[i].Equals(otherNode.ChildNodes[i]))
                    return false;
            }

            return true;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Tries to locate the namespace of the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix of the namespace.</param>
        /// <returns>The namespace for the prefix.</returns>
        protected virtual String LocateNamespace(String prefix)
        {
            if (_parent != null)
                return _parent.LocateNamespace(prefix);

            return null;
        }

        /// <summary>
        /// Tries to locate the prefix with the namespace.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace assigned to the prefix.
        /// </param>
        /// <returns>The prefix for the namespace.</returns>
        protected virtual String LocatePrefix(String namespaceUri)
        {
            if (_parent != null)
                return _parent.LocatePrefix(namespaceUri);

            return null;
        }

        /// <summary>
        /// Adopts the current node for the provided document.
        /// </summary>
        /// <param name="document">The new owner of the node.</param>
        internal void ChangeOwner(Document document)
        {
            var oldDocument = _owner;

            if (_parent != null)
                _parent.RemoveChild(this, false);

            Owner = document;
            NodeIsAdopted(oldDocument);
        }

        internal void InsertNode(Int32 index, Node node)
        {
            node.Parent = this;
            _children.Insert(index, node);
        }

        internal void AddNode(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        internal void RemoveNode(Int32 index, Node node)
        {
            node.Parent = null;
            _children.RemoveAt(index);
        }

        /// <summary>
        /// Replaces all nodes with the given node, if any.
        /// </summary>
        /// <param name="node">The node to insert, if any.</param>
        /// <param name="suppressObservers">
        /// If mutation observers should be surpressed.
        /// </param>
        internal void ReplaceAll(Node node, Boolean suppressObservers)
        {
            if (node != null)
                _owner.AdoptNode(node);

            var removedNodes = new NodeList(_children);
            var addedNodes = new NodeList();
            
            if (node != null)
            {
                if (node.NodeType == NodeType.DocumentFragment)
                    addedNodes.AddRange(node._children);
                else
                    addedNodes.Add(node);
            }

            for (int i = 0; i < removedNodes.Length; i++)
                RemoveChild(removedNodes[i], true);

            for (int i = 0; i < addedNodes.Length; i++)
                InsertBefore(addedNodes[i], null, true);

            if (!suppressObservers)
            {
                _owner.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    removedNodes: removedNodes));
            }
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of
        /// the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">
        /// The node before which newElement is inserted. If referenceElement
        /// is null, newElement is inserted at the end of the list of child nodes.
        /// </param>
        /// <param name="suppressObservers">
        /// If mutation observers should be surpressed.
        /// </param>
        /// <returns>The inserted node.</returns>
        internal INode InsertBefore(Node newElement, Node referenceElement, Boolean suppressObservers)
        {
            var count = newElement.NodeType == NodeType.DocumentFragment ? newElement.ChildNodes.Length : 1;

            if (referenceElement != null)
            {
                var childIndex = referenceElement.Index();
                _owner.ForEachRange(m => m.Head == this && m.Start > childIndex, m => m.StartWith(this, m.Start + count));
                _owner.ForEachRange(m => m.Tail == this && m.End > childIndex, m => m.EndWith(this, m.End + count));
            }

            if (newElement.NodeType == NodeType.Document || newElement.Contains(this))
                throw new DomException(DomError.HierarchyRequest);

            var addedNodes = new NodeList();
            var n = _children.Index(referenceElement);

            if (n == -1)
                n = _children.Length;
            
            if (newElement._type == NodeType.DocumentFragment)
            {
                var end = n;
                var start = n;

                while (newElement.HasChildNodes)
                {
                    var child = newElement.ChildNodes[0];
                    newElement.RemoveChild(child, true);
                    InsertNode(end, child);
                    end++;
                }

                while (start < end)
                {
                    var child = _children[start];
                    addedNodes.Add(child);
                    NodeIsInserted(child);
                    start++;
                }
            }
            else
            {
                addedNodes.Add(newElement);
                InsertNode(n, newElement);
                NodeIsInserted(newElement);
            }

            if (!suppressObservers)
            {
                _owner.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    previousSibling: _children[n - 1],
                    nextSibling: referenceElement));
            }

            return newElement;
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="node">The child to remove.</param>
        /// <param name="suppressObservers">
        /// If mutation observers should be surpressed.
        /// </param>
        internal void RemoveChild(Node node, Boolean suppressObservers)
        {
            var index = _children.Index(node);

            _owner.ForEachRange(m => m.Head.IsInclusiveDescendantOf(node), m => m.StartWith(this, index));
            _owner.ForEachRange(m => m.Tail.IsInclusiveDescendantOf(node), m => m.EndWith(this, index));
            _owner.ForEachRange(m => m.Head == this && m.Start > index, m => m.StartWith(this, m.Start - 1));
            _owner.ForEachRange(m => m.Tail == this && m.End > index, m => m.EndWith(this, m.End - 1));

            var oldPreviousSibling = index > 0 ? _children[index - 1] : null;

            if (!suppressObservers)
            {
                var removedNodes = new NodeList();
                removedNodes.Add(node);

                _owner.QueueMutation(MutationRecord.ChildList(
                    target: this, 
                    removedNodes: removedNodes, 
                    previousSibling: oldPreviousSibling, 
                    nextSibling: node.NextSibling));

                _owner.AddTransientObserver(node);
            }

            RemoveNode(index, node);
            NodeIsRemoved(node, oldPreviousSibling);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="node">
        /// The new node to replace oldChild. If it already exists in the DOM,
        /// it is first removed.
        /// </param>
        /// <param name="child">The existing child to be replaced.</param>
        /// <param name="suppressObservers">
        /// If mutation observers should be surpressed.
        /// </param>
        /// <returns>
        /// The replaced node. This is the same node as oldChild.
        /// </returns>
        internal INode ReplaceChild(Node node, Node child, Boolean suppressObservers)
        {
            if (_type != NodeType.Document && _type != NodeType.DocumentFragment && _type != NodeType.Element)
                throw new DomException(DomError.HierarchyRequest);
            else if (node.IsHostIncludingInclusiveAncestor(this))
                throw new DomException(DomError.HierarchyRequest);
            else if (child.Parent != this)
                throw new DomException(DomError.NotFound);

            var type = node.NodeType;

            if (type == NodeType.Element || type == NodeType.Comment || type == NodeType.Text || 
                type == NodeType.ProcessingInstruction || type == NodeType.DocumentFragment || type == NodeType.DocumentType)
            {
                var document = _parent as IDocument;

                if (document != null)
                {
                    var forbidden = false;

                    switch (node._type)
                    {
                        case NodeType.DocumentType:
                            forbidden = document.Doctype != child || child.IsPrecededByElement();
                            break;
                        case NodeType.Element:
                            forbidden = document.DocumentElement != child || child.IsFollowedByDoctype();
                            break;
                        case NodeType.DocumentFragment:
                            var elements = node.GetElementCount();
                            forbidden = elements > 1 || node.HasTextNodes() || (elements == 1 && (document.DocumentElement != child || child.IsFollowedByDoctype()));
                            break;
                    }

                    if (forbidden)
                        throw new DomException(DomError.HierarchyRequest);
                }

                var referenceChild = child.NextSibling;

                if (referenceChild == node)
                    referenceChild = node.NextSibling;

                _owner.AdoptNode(node);
                RemoveChild(child, true);
                InsertBefore(node, referenceChild, true);
                var addedNodes = new NodeList();
                var removedNodes = new NodeList();
                removedNodes.Add(child);

                if (node._type == NodeType.DocumentFragment)
                    addedNodes.AddRange(node._children);
                else
                    addedNodes.Add(node);

                if (!suppressObservers)
                {
                    _owner.QueueMutation(MutationRecord.ChildList(
                        target: this,
                        addedNodes: addedNodes,
                        removedNodes: removedNodes,
                        previousSibling: child.PreviousSibling,
                        nextSibling: referenceChild));
                }

                return child;
            }
            else
                throw new DomException(DomError.HierarchyRequest);
        }

        internal virtual void NodeIsAdopted(Document oldDocument)
        {
            //Run any adopting steps defined for node in other applicable specifications and pass node and oldDocument as parameters.
        }

        internal virtual void NodeIsInserted(Node newNode)
        {
            //Specifications may define insertion steps for all or some nodes.
        }

        internal virtual void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
        {
            //Specifications may define removing steps for all or some nodes.
        }

        /// <summary>
        /// Copies all (Node) properties of the source to the target.
        /// </summary>
        /// <param name="source">The source node.</param>
        /// <param name="target">The target node.</param>
        /// <param name="deep">Is a deep-copy required?</param>
        static protected void CopyProperties(Node source, Node target, Boolean deep)
        {
            target._baseUri = source._baseUri;

            if (!deep)
                return;

            foreach (var child in source._children)
                target.AddNode((Node)child.Clone(true));
        }

        /// <summary>
        /// Returns an HTML-code representation of the node using the default
        /// HTML formatter.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public String ToHtml()
        {
            return ToHtml(HtmlMarkupFormatter.Instance);
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public virtual String ToHtml(IMarkupFormatter formatter)
        {
            return TextContent;
        }

        #endregion
    }
}
