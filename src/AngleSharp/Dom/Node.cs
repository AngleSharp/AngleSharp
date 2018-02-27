namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    abstract class Node : EventTarget, INode, IEquatable<INode>
    {
        #region Fields

        readonly NodeType _type;
        readonly String _name;
        readonly NodeFlags _flags;
        
        Url _baseUri;
        Node _parent;
        NodeList _children;
        private Document _owner;

        #endregion

        #region ctor

        internal Node(Document owner, String name, NodeType type = NodeType.Element, NodeFlags flags = NodeFlags.None)
        {
            _owner = owner;
            _name = name ?? String.Empty;
            _type = type;
            _children = this.CreateChildren();
            _flags = flags;
        }

        #endregion

        #region Public Properties

        public Boolean HasChildNodes
        {
            get { return _children.Length != 0; }
        }

        public String BaseUri
        {
            get { return BaseUrl?.Href ?? String.Empty; }
        }

        public Url BaseUrl
        {
            get
            {
                if (_baseUri != null)
                {
                    return _baseUri;
                }
                else if (_parent != null)
                {
                    foreach (var ancestor in this.Ancestors<Node>())
                    {
                        if (ancestor._baseUri != null)
                        {
                            return ancestor._baseUri;
                        }
                    }
                }

                var document = Owner;

                if (document != null)
                {
                    return document._baseUri ?? document.DocumentUrl;
                }
                else if (_type == NodeType.Document)
                {
                    document = (Document)this;
                    return document.DocumentUrl;
                }

                return null;
            }
            set { _baseUri = value; }
        }

        public NodeType NodeType 
        {
            get { return _type; }
        }

        public virtual String NodeValue 
        {
            get { return null; }
            set { }
        }

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

        public IElement ParentElement
        {
            get { return _parent as IElement; }
        }

        INodeList INode.ChildNodes
        {
            get { return _children; }
        }

        public String NodeName
        {
            get { return _name; }
        }

        #endregion

        #region Internal Properties

        internal Node PreviousSibling
        {
            get
            {
                if (_parent != null)
                {
                    var n = _parent._children.Length;

                    for (var i = 1; i < n; i++)
                    {
                        if (Object.ReferenceEquals(_parent._children[i], this))
                        {
                            return _parent._children[i - 1];
                        }
                    }
                }

                return null;
            }
        }

        internal Node NextSibling
        {
            get
            {
                if (_parent != null)
                {
                    var n = _parent._children.Length - 1;

                    for (var i = 0; i < n; i++)
                    {
                        if (Object.ReferenceEquals(_parent._children[i], this))
                        {
                            return _parent._children[i + 1];
                        }
                    }
                }

                return null;
            }
        }

        internal Node FirstChild
        {
            get { return _children.Length > 0 ? _children[0] : null; }
        }

        internal Node LastChild
        {
            get { return _children.Length > 0 ? _children[_children.Length - 1] : null; }
        }

        internal NodeFlags Flags
        {
            get { return _flags; }
        }

        internal NodeList ChildNodes
        {
            get { return _children; }
            set { _children = value; }
        }

        internal Node Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        internal Document Owner
        {
            get
            {
                if (_type == NodeType.Document)
                {
                    return default(Document);
                }

                return _owner;
            }
            set
            {
                foreach (var descendentAndSelf in this.DescendentsAndSelf<Node>())
                {
                    var oldDocument = descendentAndSelf.Owner;

                    if (!Object.ReferenceEquals(oldDocument, value))
                    {
                        descendentAndSelf._owner = value;

                        if (oldDocument != null)
                        {
                            NodeIsAdopted(oldDocument);
                        }
                    }
                }
            }
        }

        #endregion

        #region Internal Methods

        internal void AppendText(String s)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild == null)
            {
                AddNode(new TextNode(Owner, s));
            }
            else
            {
                lastChild.Append(s);
            }
        }

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
            {
                var node = new TextNode(Owner, s);
                InsertNode(index, node);
            }
        }

        #endregion

        #region Public Methods

        public virtual void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            writer.Write(TextContent);
        }

        public INode AppendChild(INode child)
        {
            return this.PreInsert(child, null);
        }

        public INode InsertChild(Int32 index, INode child)
        {
            var reference = index < _children.Length ? _children[index] : null;
            return this.PreInsert(child, reference);
        }

        public INode InsertBefore(INode newElement, INode referenceElement)
        {
            return this.PreInsert(newElement, referenceElement);
        }

        public INode ReplaceChild(INode newChild, INode oldChild)
        {
            return this.ReplaceChild(newChild as Node, oldChild as Node, false);
        }

        public INode RemoveChild(INode child)
        {
            return this.PreRemove(child);
        }

        public abstract INode Clone(Boolean deep = true);

        public DocumentPositions CompareDocumentPosition(INode otherNode)
        {
            if (Object.ReferenceEquals(this, otherNode))
            {
                return DocumentPositions.Same;
            }
            else if (!Object.ReferenceEquals(Owner, otherNode.Owner))
            {
                var relative = otherNode.GetHashCode() > GetHashCode() ? DocumentPositions.Following : DocumentPositions.Preceding;
                return DocumentPositions.Disconnected | DocumentPositions.ImplementationSpecific | relative;
            }
            else if (otherNode.IsAncestorOf(this))
            {
                return DocumentPositions.Contains | DocumentPositions.Preceding;
            }
            else if (otherNode.IsDescendantOf(this))
            {
                return DocumentPositions.ContainedBy | DocumentPositions.Following;
            }
            else if (otherNode.IsPreceding(this))
            {
                return DocumentPositions.Preceding;
            }

            return DocumentPositions.Following;
        }

        public Boolean Contains(INode otherNode)
        {
            return this.IsInclusiveAncestorOf(otherNode);
        }

        public void Normalize()
        {
            for (var i = 0; i < _children.Length; i++)
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
                        var owner = Owner;

                        while ((sibling = sibling.NextSibling as TextNode) != null)
                        {
                            sb.Append(sibling.Data);
                            end++;

                            owner.ForEachRange(m => m.Head == sibling, m => m.StartWith(text, length));
                            owner.ForEachRange(m => m.Tail == sibling, m => m.EndWith(text, length));
                            owner.ForEachRange(m => m.Head == sibling.Parent && m.Start == end, m => m.StartWith(text, length));
                            owner.ForEachRange(m => m.Tail == sibling.Parent && m.End == end, m => m.EndWith(text, length));

                            length += sibling.Length;
                        }

                        text.Replace(text.Length, 0, sb.ToPool());

                        for (var j = end; j > i; j--)
                        {
                            RemoveChild(_children[j], false);
                        }
                    }
                }
                else if (_children[i].HasChildNodes)
                {
                    _children[i].Normalize();
                }
            }
        }

        public String LookupNamespaceUri(String prefix)
        {
            if (String.IsNullOrEmpty(prefix))
            {
                prefix = null;
            }

            return LocateNamespace(prefix);
        }

        public String LookupPrefix(String namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                return null;
            }

            return LocatePrefix(namespaceUri);
        }

        public Boolean IsDefaultNamespace(String namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            var defaultNamespace = LocateNamespace(null);
            return namespaceUri.Is(defaultNamespace);
        }

        public virtual Boolean Equals(INode otherNode)
        {
            if (BaseUri.Is(otherNode.BaseUri) && NodeName.Is(otherNode.NodeName) && ChildNodes.Length == otherNode.ChildNodes.Length)
            {
                for (var i = 0; i < _children.Length; i++)
                {
                    if (!_children[i].Equals(otherNode.ChildNodes[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// For more information, see:
        /// https://dom.spec.whatwg.org/#validate-and-extract
        /// </summary>
        protected static void GetPrefixAndLocalName(String qualifiedName, ref String namespaceUri, out String prefix, out String localName)
        {
            if (!qualifiedName.IsXmlName())
                throw new DomException(DomError.InvalidCharacter);

            if (!qualifiedName.IsQualifiedName())
                throw new DomException(DomError.Namespace);

            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            var index = qualifiedName.IndexOf(Symbols.Colon);

            if (index > 0)
            {
                prefix = qualifiedName.Substring(0, index);
                localName = qualifiedName.Substring(index + 1);
            }
            else
            {
                prefix = null;
                localName = qualifiedName;
            }

            if (IsNamespaceError(prefix, namespaceUri, qualifiedName))
                throw new DomException(DomError.Namespace);
        }

        protected static Boolean IsNamespaceError(String prefix, String namespaceUri, String qualifiedName)
        {
            return (prefix != null && namespaceUri == null) || (prefix.Is(NamespaceNames.XmlPrefix) && !namespaceUri.Is(NamespaceNames.XmlUri)) ||
                ((qualifiedName.Is(NamespaceNames.XmlNsPrefix) || prefix.Is(NamespaceNames.XmlNsPrefix)) && !namespaceUri.Is(NamespaceNames.XmlNsUri)) ||
                (namespaceUri.Is(NamespaceNames.XmlNsUri) && (!qualifiedName.Is(NamespaceNames.XmlNsPrefix) && !prefix.Is(NamespaceNames.XmlNsPrefix)));
        }

        protected virtual String LocateNamespace(String prefix)
        {
            return _parent?.LocateNamespace(prefix);
        }

        protected virtual String LocatePrefix(String namespaceUri)
        {
            return _parent?.LocatePrefix(namespaceUri);
        }

        internal void ChangeOwner(Document document)
        {
            var oldDocument = Owner;
            _parent?.RemoveChild(this, false);
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

        internal void ReplaceAll(Node node, Boolean suppressObservers)
        {
            var document = Owner;

            if (node != null)
            {
                document.AdoptNode(node);
            }

            var removedNodes = new NodeList();
            var addedNodes = new NodeList();

            removedNodes.AddRange(_children);
            
            if (node != null)
            {
                if (node.NodeType == NodeType.DocumentFragment)
                {
                    addedNodes.AddRange(node._children);
                }
                else
                {
                    addedNodes.Add(node);
                }
            }

            for (var i = 0; i < removedNodes.Length; i++)
            {
                RemoveChild(removedNodes[i], true);
            }

            for (var i = 0; i < addedNodes.Length; i++)
            {
                InsertBefore(addedNodes[i], null, true);
            }

            if (!suppressObservers)
            {
                document.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    removedNodes: removedNodes));
            }
        }

        internal INode InsertBefore(Node newElement, Node referenceElement, Boolean suppressObservers)
        {
            var document = Owner;
            var count = newElement.NodeType == NodeType.DocumentFragment ? newElement.ChildNodes.Length : 1;

            if (referenceElement != null && document != null)
            {
                var childIndex = referenceElement.Index();
                document.ForEachRange(m => m.Head == this && m.Start > childIndex, m => m.StartWith(this, m.Start + count));
                document.ForEachRange(m => m.Tail == this && m.End > childIndex, m => m.EndWith(this, m.End + count));
            }

            if (newElement.NodeType == NodeType.Document || newElement.Contains(this))
                throw new DomException(DomError.HierarchyRequest);

            var addedNodes = new NodeList();
            var n = _children.Index(referenceElement);

            if (n == -1)
            {
                n = _children.Length;
            }
            
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

            if (!suppressObservers && document != null)
            {
                document.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    previousSibling: n > 0 ? _children[n - 1] : null,
                    nextSibling: referenceElement));
            }

            return newElement;
        }

        internal void RemoveChild(Node node, Boolean suppressObservers)
        {
            var document = Owner;
            var index = _children.Index(node);

            if (document != null)
            {
                document.ForEachRange(m => m.Head.IsInclusiveDescendantOf(node), m => m.StartWith(this, index));
                document.ForEachRange(m => m.Tail.IsInclusiveDescendantOf(node), m => m.EndWith(this, index));
                document.ForEachRange(m => m.Head == this && m.Start > index, m => m.StartWith(this, m.Start - 1));
                document.ForEachRange(m => m.Tail == this && m.End > index, m => m.EndWith(this, m.End - 1));
            }

            var oldPreviousSibling = index > 0 ? _children[index - 1] : null;

            if (!suppressObservers && document != null)
            {
                var removedNodes = new NodeList { node };

                document.QueueMutation(MutationRecord.ChildList(
                    target: this, 
                    removedNodes: removedNodes, 
                    previousSibling: oldPreviousSibling, 
                    nextSibling: node.NextSibling));

                document.AddTransientObserver(node);
            }

            RemoveNode(index, node);
            NodeIsRemoved(node, oldPreviousSibling);
        }

        internal INode ReplaceChild(Node node, Node child, Boolean suppressObservers)
        {
            if (this.IsEndPoint() || node.IsHostIncludingInclusiveAncestor(this))
                throw new DomException(DomError.HierarchyRequest);

            if (child.Parent != this)
                throw new DomException(DomError.NotFound);

            if (node.IsInsertable())
            {
                var parent = _parent as IDocument;
                var referenceChild = child.NextSibling;
                var document = Owner;
                var addedNodes = new NodeList();
                var removedNodes = new NodeList();

                if (parent != null)
                {
                    var forbidden = false;

                    switch (node._type)
                    {
                        case NodeType.DocumentType:
                            forbidden = parent.Doctype != child || child.IsPrecededByElement();
                            break;
                        case NodeType.Element:
                            forbidden = parent.DocumentElement != child || child.IsFollowedByDoctype();
                            break;
                        case NodeType.DocumentFragment:
                            var elements = node.GetElementCount();
                            forbidden = elements > 1 || node.HasTextNodes() || (elements == 1 && (parent.DocumentElement != child || child.IsFollowedByDoctype()));
                            break;
                    }

                    if (forbidden)
                        throw new DomException(DomError.HierarchyRequest);
                }

                if (referenceChild == node)
                {
                    referenceChild = node.NextSibling;
                }

                document?.AdoptNode(node);
                RemoveChild(child, true);
                InsertBefore(node, referenceChild, true);
                removedNodes.Add(child);

                if (node._type == NodeType.DocumentFragment)
                {
                    addedNodes.AddRange(node._children);
                }
                else
                {
                    addedNodes.Add(node);
                }

                if (!suppressObservers && document != null)
                {
                    document.QueueMutation(MutationRecord.ChildList(
                        target: this,
                        addedNodes: addedNodes,
                        removedNodes: removedNodes,
                        previousSibling: child.PreviousSibling,
                        nextSibling: referenceChild));
                }

                return child;
            }
            
            throw new DomException(DomError.HierarchyRequest);
        }

        /// <summary>
        /// Run any adopting steps defined for node in other applicable 
        /// specifications and pass node and oldDocument as parameters.
        /// </summary>
        internal virtual void NodeIsAdopted(Document oldDocument)
        {
        }

        /// <summary>
        /// Specifications may define insertion steps for all or some nodes.
        /// </summary>
        internal virtual void NodeIsInserted(Node newNode)
        {
            newNode.OnParentChanged();
        }

        /// <summary>
        /// Specifications may define removing steps for all or some nodes.
        /// </summary>
        internal virtual void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
        {
            removedNode.OnParentChanged();
        }

        protected virtual void OnParentChanged()
        {
            //TODO
        }

        protected void CloneNode(Node target, Boolean deep)
        {
            target._baseUri = _baseUri;

            if (deep)
            {
                foreach (var child in _children)
                {
                    target.AddNode((Node)child.Clone(true));
                }
            }
        }

        #endregion
    }
}
