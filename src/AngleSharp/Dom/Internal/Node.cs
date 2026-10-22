namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Common;
    using Html.Construction;

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    public abstract class Node : EventTarget, INode, IEquatable<INode>, IConstructableNode
    {
        #region Fields

        private readonly NodeType _type;
        private readonly String _name;
        private readonly NodeFlags _flags;

        private Url? _baseUri;
        private Node? _parent;
        private NodeList _children;
        private Document? _owner;

        #endregion

        #region ctor

        /// <inheritdoc />
        public Node(Document? owner, String name, NodeType type = NodeType.Element, NodeFlags flags = NodeFlags.None)
        {
            _owner = owner;
            _name = name ?? String.Empty;
            _type = type;
            _children = this.IsEndPoint() ? NodeList.Empty : [];
            _flags = flags;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public Boolean HasChildNodes => _children.Length != 0;

        /// <inheritdoc />
        public String BaseUri => BaseUrl?.Href ?? String.Empty;

        /// <inheritdoc />
        public Url? BaseUrl
        {
            get
            {
                if (_baseUri is not null)
                {
                    return _baseUri;
                }
                else if (_parent is not null)
                {
                    foreach (var ancestor in this.Ancestors<Node>())
                    {
                        if (ancestor._baseUri is not null)
                        {
                            return ancestor._baseUri;
                        }
                    }
                }

                var document = Owner;

                if (document is not null)
                {
                    return document.ActiveBaseUrl ?? document._baseUri ?? document.FallbackBaseUrl;
                }
                else if (_type == NodeType.Document)
                {
                    document = (Document)this;
                    return document.ActiveBaseUrl ?? _baseUri ?? document.FallbackBaseUrl;
                }

                return null;
            }
            set => _baseUri = value;
        }

        /// <inheritdoc />
        public NodeType NodeType => _type;

        /// <inheritdoc />
        public NodeFlags Flags => _flags;

        /// <inheritdoc />
        public virtual String NodeValue
        {
            get => null!;
            set { }
        }

        /// <inheritdoc />
        public virtual String TextContent
        {
            get => null!;
            set { }
        }

        INode? INode.PreviousSibling => PreviousSibling;

        INode? INode.NextSibling => NextSibling;

        INode? INode.FirstChild => FirstChild;

        INode? INode.LastChild => LastChild;

        IDocument INode.Owner => Owner;

        INode? INode.Parent => _parent;

        /// <inheritdoc />
        public IElement? ParentElement
        {
            get
            {
                // Avoids the interface cast helper, which is hot for descendant and
                // child combinators walking the ancestor chain of every candidate.
                var parent = _parent;
                return parent is not null && parent.NodeType == NodeType.Element ? (Element)parent : null;
            }
        }

        INodeList INode.ChildNodes => _children;

        /// <inheritdoc />
        public String NodeName => _name;

        #endregion

        #region Internal Properties

        internal Node? PreviousSibling
        {
            get
            {
                if (_parent is not null)
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

        internal Node? NextSibling
        {
            get
            {
                if (_parent is not null)
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

        internal Node? FirstChild => _children.Length > 0 ? _children[0] : null;

        internal Node? LastChild => _children.Length > 0 ? _children[_children.Length - 1] : null;

        internal NodeList ChildNodes
        {
            get => _children;
            set => _children = value;
        }

        internal Node? Parent
        {
            get => _parent;
            set => _parent = value;
        }

        /// <summary>
        /// Gets the document whose mutation version the tree mutation algorithms advance. It is
        /// the same as <see cref="Owner"/> except for a document node, which deliberately reports
        /// a null owner but is itself the document being changed - so appending to or removing
        /// from the document itself still versions the right document.
        /// </summary>
        private Document? OwningDocument => _owner ?? this as Document;

        internal Document Owner
        {
            get
            {
                if (_type == NodeType.Document)
                {
                    return default!; // Supress to avoid common case where this is non-null
                }

                return _owner!;
            }
            set
            {
                if (!Object.ReferenceEquals(Owner, value))
                {
                    foreach (var descendantAndSelf in this.DescendantsAndSelf<Node>())
                    {
                        var oldDocument = descendantAndSelf.Owner;

                        descendantAndSelf._owner = value;

                        if (oldDocument is not null)
                        {
                            descendantAndSelf.NodeIsAdopted(oldDocument);
                        }
                    }
                }
            }
        }

        #endregion

        #region Internal Methods

        internal void ReplaceAll(Node? node, Boolean suppressObservers)
        {
            var document = Owner;

            if (node is not null)
            {
                document.AdoptNode(node);
            }

            var removedNodes = new NodeList();
            var addedNodes = new NodeList();

            removedNodes.AddRange(_children);

            if (node is not null)
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
                InsertBefore(addedNodes[i], null, true, suppressBaseUrlUpdates: this is Html.Dom.HtmlTemplateElement);
            }

            if (!suppressObservers)
            {
                // This is the point that decided a mutation happened: the removals and inserts
                // above ran suppressed precisely so the whole replacement reports once. The two
                // node lists are load bearing here - the loops iterate them - so they are built
                // whether or not anything is observing. The owning document rather than the
                // owner, for the reason the three call sites below give: a document node reports none.
                OwningDocument?.MarkMutated();
                document.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    removedNodes: removedNodes));
            }

            ReplacedAll();
        }

        internal INode InsertBefore(Node newElement, Node? referenceElement, Boolean suppressObservers, Boolean suppressBaseUrlUpdates = false)
        {
            var document = Owner;

            if (referenceElement is not null && document is not null)
            {
                var childIndex = referenceElement.Index();

                foreach (var m in document.GetAttachedReferences<IPreInsert>())
                {
                    m.PreInsert(this, newElement, childIndex);
                }
            }

            if (newElement.NodeType == NodeType.Document || newElement.Contains(this))
            {
                throw new DomException(DomError.HierarchyRequest);
            }

            // The record costs a node list plus the record itself on every insert, and
            // QueueMutation throws both away again when nothing is observing. Asking first is the
            // same behaviour for a fraction of the cost, as on the attribute path.
            var reported = !suppressObservers && document is not null && document.HasMutationObservers;
            var addedNodes = reported ? new NodeList() : null;
            var n = _children.Index(referenceElement!);

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
                    addedNodes?.Add(child);
                    NodeIsInserted(child);

                    if (!suppressBaseUrlUpdates)
                    {
                        OwningDocument?.RefreshBaseUrlForTreeChange(child);
                    }

                    start++;
                }
            }
            else
            {
                addedNodes?.Add(newElement);
                InsertNode(n, newElement);
                NodeIsInserted(newElement);

                if (!suppressBaseUrlUpdates)
                {
                    OwningDocument?.RefreshBaseUrlForTreeChange(newElement);
                }
            }

            if (!suppressObservers)
            {
                // The insert is what decided a mutation happened - InsertNode above it is the raw
                // child list write the parser drives directly and must stay free of this. The
                // owning document rather than the owner, since a document node reports none.
                OwningDocument?.MarkMutated();

                if (reported)
                {
                    document!.QueueMutation(MutationRecord.ChildList(
                        target: this,
                        addedNodes: addedNodes,
                        previousSibling: n > 0 ? _children[n - 1] : null,
                        nextSibling: referenceElement));
                }
            }

            return newElement;
        }

        internal void RemoveChild(Node node, Boolean suppressObservers)
        {
            var document = Owner;
            var index = _children.Index(node);

            if (document is not null)
            {
                foreach (var m in document.GetAttachedReferences<IPreRemove>())
                {
                    m.PreRemove(this, node, index);
                }
            }

            var oldPreviousSibling = index > 0 ? _children[index - 1] : null;

            if (!suppressObservers)
            {
                // The removal is what decided a mutation happened - RemoveNode below it is the raw
                // child list write the parser drives directly and must stay free of this.
                OwningDocument?.MarkMutated();

                // Both the record and the transient observer walk are pure observer bookkeeping:
                // one is thrown away by QueueMutation and the other iterates an empty list when
                // nothing is observing, so neither is worth its allocations then.
                if (document is not null && document.HasMutationObservers)
                {
                    var removedNodes = new NodeList { node };

                    document.QueueMutation(MutationRecord.ChildList(
                        target: this,
                        removedNodes: removedNodes,
                        previousSibling: oldPreviousSibling,
                        nextSibling: node.NextSibling));

                    document.AddTransientObserver(node);
                }
            }

            var owningDocument = OwningDocument;
            RemoveNode(index, node);
            NodeIsRemoved(node, oldPreviousSibling);
            owningDocument?.RefreshBaseUrlForTreeChange(node);
        }

        internal Node ReplaceChild(Node node, Node child, Boolean suppressObservers)
        {
            if (this.IsEndPoint() || node.IsHostIncludingInclusiveAncestor(this))
            {
                throw new DomException(DomError.HierarchyRequest);
            }

            if (child.Parent != this)
            {
                throw new DomException(DomError.NotFound);
            }

            if (node.IsInsertable())
            {
                var referenceChild = child.NextSibling;
                var document = Owner;

                if (this is IDocument parent && IsChangeForbidden(node, parent, child))
                {
                    throw new DomException(DomError.HierarchyRequest);
                }

                if (Object.ReferenceEquals(referenceChild, node))
                {
                    referenceChild = node.NextSibling;
                }

                document?.AdoptNode(node);
                RemoveChild(child, true);
                InsertBefore(node, referenceChild, true);

                if (!suppressObservers)
                {
                    // The replacement is what decided a mutation happened: the remove and the
                    // insert above ran suppressed precisely so this reports once.
                    OwningDocument?.MarkMutated();

                    if (document is not null && document.HasMutationObservers)
                    {
                        var addedNodes = new NodeList();
                        var removedNodes = new NodeList { child };

                        if (node._type == NodeType.DocumentFragment)
                        {
                            addedNodes.AddRange(node._children);
                        }
                        else
                        {
                            addedNodes.Add(node);
                        }

                        document.QueueMutation(MutationRecord.ChildList(
                            target: this,
                            addedNodes: addedNodes,
                            removedNodes: removedNodes,
                            previousSibling: child.PreviousSibling,
                            nextSibling: referenceChild));
                    }
                }

                return child;
            }

            throw new DomException(DomError.HierarchyRequest);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when ReplaceAll was run.
        /// </summary>
        protected virtual void ReplacedAll() {}

        #endregion

        #region Public Methods

        /// <summary>
        /// Clones the current node using the new owner.
        /// </summary>
        /// <param name="newOwner">The new document owner, if any.</param>
        /// <param name="deep">True if a deep clone is wanted, otherwise false.</param>
        /// <returns>The cloned node.</returns>
        public abstract Node Clone(Document newOwner, Boolean deep);

        // AppendText, InsertText, InsertNode, AddNode and RemoveNode are the raw child list
        // operations the tree builder drives directly, once per node of a parsed document. They
        // change the tree and nothing else: no mutation record, no transient observer, no
        // advance of the document's mutation version. Everything of that kind belongs to the
        // caller that decided a mutation happened - ReplaceAll, InsertBefore, RemoveChild,
        // ReplaceChild - so that a parse comes out the other side having triggered none of it.

        /// <inheritdoc />
        public void AppendText(String s)
        {
            if (LastChild is TextNode lastChild)
            {
                lastChild.AppendData(s);
            }
            else
            {
                AddNode(new TextNode(Owner, s));
            }
        }

        /// <inheritdoc />
        public void InsertText(Int32 index, String s)
        {
            if (index > 0 && index <= _children.Length && _children[index - 1] is TextNode text1)
            {
                text1.AppendData(s);
            }
            else if (index >= 0 && index < _children.Length && _children[index] is TextNode text2)
            {
                text2.InsertData(0, s);
            }
            else
            {
                InsertNode(index, new TextNode(Owner, s));
            }
        }

        /// <inheritdoc />
        public void InsertNode(Int32 index, Node node)
        {
            node.Parent = this;
            _children.Insert(index, node);
        }

        /// <inheritdoc />
        public void AddNode(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        /// <inheritdoc />
        public void RemoveNode(Int32 index, Node node)
        {
            node.Parent = null;
            _children.RemoveAt(index);
        }

        /// <inheritdoc />
        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            var end = ParentElement;

            foreach (var node in this.GetDescendantsAndSelf())
            {
                if (node is IComment comment)
                {
                    writer.Write(formatter.Comment(comment));
                }
                else if (node is IProcessingInstruction processingInstruction)
                {
                    writer.Write(formatter.Processing(processingInstruction));
                }
                else if (node is ICharacterData characterData)
                {
                    if (characterData.Parent?.Flags.HasFlag(NodeFlags.LiteralText) ?? false)
                    {
                        writer.Write(formatter.LiteralText(characterData));
                    }
                    else
                    {
                        writer.Write(formatter.Text(characterData));
                    }
                }
                else if (node is IDocumentType docType)
                {
                    writer.Write(formatter.Doctype(docType));
                }
                else if (node is IElement element)
                {
                    var flags = element.Flags;
                    var selfClosing = flags.HasFlag(NodeFlags.SelfClosing);
                    writer.Write(formatter.OpenTag(element, selfClosing));

                    if (!selfClosing && flags.HasFlag(NodeFlags.LineTolerance) && element.FirstChild is IText text && text.Data.Has(Symbols.LineFeed))
                    {
                        writer.Write(Symbols.LineFeed);
                    }

                    if (element is Html.Dom.IHtmlTemplateElement template)
                    {
                        template.Content.ToHtml(writer, formatter);
                    }

                    if (!node.HasChildNodes)
                    {
                        writer.Write(formatter.CloseTag(element, selfClosing));
                    }
                }

                if (!node.HasChildNodes && node.NextSibling is null)
                {
                    var p = node.ParentElement;

                    while (p != end)
                    {
                        writer.Write(formatter.CloseTag(p!, p!.Flags.HasFlag(NodeFlags.SelfClosing)));
                        p = p.NextSibling is null ? p.ParentElement : end;
                    }
                }
            }
        }

        /// <inheritdoc />
        public INode AppendChild(INode child) => this.PreInsert(child, null);

        /// <inheritdoc />
        public INode InsertChild(Int32 index, INode child)
        {
            var reference = index < _children.Length ? _children[index] : null;
            return this.PreInsert(child, reference);
        }

        /// <inheritdoc />
        public INode InsertBefore(INode newElement, INode? referenceElement) => this.PreInsert(newElement, referenceElement);

        /// <inheritdoc />
        public INode ReplaceChild(INode newChild, INode oldChild) => this.ReplaceChild((Node)newChild, (Node)oldChild, false);


        /// <inheritdoc />
        public INode RemoveChild(INode child) => this.PreRemove(child);

        /// <inheritdoc />
        public INode Clone(Boolean deep = true) => Clone(Owner, deep);

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Boolean Contains(INode otherNode) => this.IsInclusiveAncestorOf(otherNode);

        /// <inheritdoc />
        public void Normalize() => Normalize(0);

        // Recurses for the first levels and continues iteratively below them, so deeply nested
        // documents cannot overflow the stack
        private void Normalize(Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                NormalizeIteratively();
                return;
            }

            var i = NormalizeChildrenFrom(0);

            while (i >= 0)
            {
                _children[i].Normalize(depth + 1);
                i = NormalizeChildrenFrom(i + 1);
            }
        }

        // It visits the nodes in the same order as the recursion, descending into a child as soon
        // as it is reached, so mutation records and live ranges see the same sequence of changes.
        private void NormalizeIteratively()
        {
            var resume = new ChildIndexStack(stackalloc Int32[ChildIndexStack.InitialDepth]);

            try
            {
                var node = this;
                var start = 0;

                while (true)
                {
                    var descendInto = node.NormalizeChildrenFrom(start);

                    if (descendInto >= 0)
                    {
                        resume.Push(descendInto + 1);
                        node = node._children[descendInto];
                        start = 0;
                    }
                    else if (resume.Count > 0)
                    {
                        start = resume.Pop();
                        node = node._parent!;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            finally
            {
                resume.Dispose();
            }
        }

        // Normalizes the children from the given index on, until it reaches a child with children of
        // its own, whose index it returns; -1 once all children are done.
        private Int32 NormalizeChildrenFrom(Int32 start)
        {
            for (var i = start; i < _children.Length; i++)
            {
                if (_children[i] is TextNode text)
                {
                    var length = text.Length;

                    if (length == 0)
                    {
                        RemoveChild(text, false);
                        i--;
                    }
                    else
                    {
                        var sb = StringBuilderPool.Obtain();
                        var sibling = text;
                        var end = i;
                        var owner = Owner;

                        while ((sibling = sibling.NextSibling as TextNode) != null)
                        {
                            sb.Append(sibling.Data);
                            end++;

                            foreach (var m in owner.GetAttachedReferences<Range>())
                            {
                                if (m.Head == sibling)
                                {
                                    m.StartWith(text, length);
                                }
                                if (m.Tail == sibling)
                                {
                                    m.EndWith(text, length);
                                }
                                if (m.Head == sibling.Parent && m.Start == end)
                                {
                                    m.StartWith(text, length);
                                }
                                if (m.Tail == sibling.Parent && m.End == end)
                                {
                                    m.EndWith(text, length);
                                }
                            }

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
                    return i;
                }
            }

            return -1;
        }

        /// <inheritdoc />
        public String? LookupNamespaceUri(String? prefix)
        {
            if (String.IsNullOrEmpty(prefix))
            {
                prefix = null;
            }

            return LocateNamespace(prefix!);
        }

        /// <inheritdoc />
        public String? LookupPrefix(String? namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                return null;
            }

            return LocatePrefix(namespaceUri!);
        }

        /// <inheritdoc />
        public Boolean IsDefaultNamespace(String? namespaceUri)
        {
            if (String.IsNullOrEmpty(namespaceUri))
            {
                namespaceUri = null;
            }

            var defaultNamespace = LocateNamespace(null!);
            return namespaceUri.Is(defaultNamespace);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public virtual Boolean Equals(INode? otherNode) =>
            otherNode is not null && EqualsShallow(otherNode) && ChildrenEqual(otherNode, 0);

        // Compares the descendants of two nodes whose shallow comparison succeeded, so their child
        // counts match. Recurses for the first levels and continues iteratively below them, so
        // deeply nested documents cannot overflow the stack.
        private Boolean ChildrenEqual(INode otherNode, Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                return ChildrenEqualIteratively(otherNode);
            }

            var otherChildren = otherNode.ChildNodes;

            for (var i = 0; i < _children.Length; i++)
            {
                var child = _children[i];
                var otherChild = otherChildren[i];

                if (!child.EqualsShallow(otherChild) || (child._children.Length > 0 && !child.ChildrenEqual(otherChild, depth + 1)))
                {
                    return false;
                }
            }

            return true;
        }

        private Boolean ChildrenEqualIteratively(INode otherNode)
        {
            var resume = new ChildIndexStack(stackalloc Int32[ChildIndexStack.InitialDepth]);

            try
            {
                var node = this;
                var other = otherNode;
                var children = node._children;
                var otherChildren = other.ChildNodes;
                var i = 0;

                while (true)
                {
                    if (i < children.Length)
                    {
                        var child = children[i];
                        var otherChild = otherChildren[i];
                        i++;

                        if (!child.EqualsShallow(otherChild))
                        {
                            return false;
                        }

                        if (child._children.Length > 0)
                        {
                            resume.Push(i);
                            node = child;
                            other = otherChild;
                            children = node._children;
                            otherChildren = other.ChildNodes;
                            i = 0;
                        }
                    }
                    else if (resume.Count > 0)
                    {
                        i = resume.Pop();
                        node = node._parent!;
                        other = other.Parent!;
                        children = node._children;
                        otherChildren = other.ChildNodes;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            finally
            {
                resume.Dispose();
            }
        }

        /// <summary>
        /// Compares this node with another one, without their children; the child counts must match.
        /// </summary>
        private protected virtual Boolean EqualsShallow(INode otherNode) =>
            BaseUri.Is(otherNode.BaseUri) && NodeName.Is(otherNode.NodeName) && ChildNodes.Length == otherNode.ChildNodes.Length;

        #endregion

        #region Helpers

        private static Boolean IsChangeForbidden(Node node, IDocument parent, Node child)
        {
            switch (node._type)
            {
                case NodeType.DocumentType:
                    return parent.Doctype != child || child.IsPrecededByElement();

                case NodeType.Element:
                    return parent.DocumentElement != child || child.IsFollowedByDoctype();

                case NodeType.DocumentFragment:
                    var elements = node.GetElementCount();
                    return elements > 1 || node.HasTextNodes() || (elements == 1 && (parent.DocumentElement != child || child.IsFollowedByDoctype()));

                default:
                    return false;
            }
        }

        /// <summary>
        /// For more information, see:
        /// https://dom.spec.whatwg.org/#validate-and-extract
        /// </summary>
        protected static void GetPrefixAndLocalName(String qualifiedName, ref String? namespaceUri, out String? prefix, out String localName)
        {
            if (!qualifiedName.IsXmlName())
            {
                throw new DomException(DomError.InvalidCharacter);
            }

            if (!qualifiedName.IsQualifiedName())
            {
                throw new DomException(DomError.Namespace);
            }

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
            {
                throw new DomException(DomError.Namespace);
            }
        }

        /// <inheritdoc />
        protected static Boolean IsNamespaceError(String? prefix, String? namespaceUri, String qualifiedName)
        {
            return (prefix is not null && namespaceUri is null) || (prefix.Is(NamespaceNames.XmlPrefix) && !namespaceUri.Is(NamespaceNames.XmlUri)) ||
                ((qualifiedName.Is(NamespaceNames.XmlNsPrefix) || prefix.Is(NamespaceNames.XmlNsPrefix)) && !namespaceUri.Is(NamespaceNames.XmlNsUri)) ||
                (namespaceUri.Is(NamespaceNames.XmlNsUri) && (!qualifiedName.Is(NamespaceNames.XmlNsPrefix) && !prefix.Is(NamespaceNames.XmlNsPrefix)));
        }

        /// <inheritdoc />
        protected virtual String? LocateNamespace(String prefix) => _parent?.LocateNamespace(prefix);

        /// <inheritdoc />
        protected virtual String? LocatePrefix(String namespaceUri) => _parent?.LocatePrefix(namespaceUri);

        /// <summary>
        /// Run any adopting steps defined for node in other applicable
        /// specifications and pass node and oldDocument as parameters.
        /// </summary>
        protected virtual void NodeIsAdopted(Document oldDocument)
        {
        }

        /// <summary>
        /// Specifications may define insertion steps for all or some nodes.
        /// </summary>
        protected virtual void NodeIsInserted(Node newNode) => newNode.OnParentChanged();

        /// <summary>
        /// Specifications may define removing steps for all or some nodes.
        /// </summary>
        protected virtual void NodeIsRemoved(Node removedNode, Node? oldPreviousSibling) => removedNode.OnParentChanged();

        /// <inheritdoc />
        protected virtual void OnParentChanged()
        {
            //TODO
        }

        /// <inheritdoc />
        protected void CloneNode(Node target, Document owner, Boolean deep)
        {
            target._baseUri = _baseUri;

            if (deep)
            {
                // A deep clone recurses through the Clone overrides, which cannot pass a depth, so the
                // levels are counted per thread. Below RecursionLimit the copy continues iteratively,
                // so deeply nested documents cannot overflow the stack.
                if (t_cloneDepth < ChildIndexStack.RecursionLimit)
                {
                    t_cloneDepth++;

                    try
                    {
                        foreach (var child in _children)
                        {
                            if (child is Node node)
                            {
                                var clone = node.Clone(owner, true);
                                target.AddNode(clone);
                            }
                        }
                    }
                    finally
                    {
                        t_cloneDepth--;
                    }
                }
                else
                {
                    CloneDescendants(this, target, owner);
                }
            }
        }

        [ThreadStatic]
        private static Int32 t_cloneDepth;

        /// <summary>
        /// Copies every descendant of source into target, in tree order. Iterative rather than one
        /// Clone(owner, true) call per level, so deeply nested documents cannot overflow the stack.
        /// Each descendant is copied by its own Clone(owner, false), so its cloning steps still run.
        /// Only two kinds of element need more, checked by their sealed types rather than with a
        /// virtual call on every node: a template's content is copied too, and a select restores
        /// its selected option once its options are in place.
        /// </summary>
        internal static void CloneDescendants(Node source, Node target, Document owner)
        {
            // Subtrees cloned besides the children, such as template contents, are copied after the
            // current walk, so a template nested in a template's content cannot recurse either.
            var subtrees = default(Stack<ClonePair>);
            var resume = new ChildIndexStack(stackalloc Int32[ChildIndexStack.InitialDepth]);

            try
            {
                while (true)
                {
                    var sourceParent = source;
                    var targetParent = target;
                    var children = sourceParent._children;
                    var i = 0;

                    while (true)
                    {
                        if (i < children.Length)
                        {
                            var child = children[i++];
                            Node copy;

                            if (child is HtmlTemplateElement template)
                            {
                                var templateCopy = template.CloneWithoutContent(owner);
                                subtrees ??= new Stack<ClonePair>();
                                subtrees.Push(new ClonePair(template.ContentFragment, templateCopy.ContentFragment));
                                copy = templateCopy;
                            }
                            else
                            {
                                copy = child.Clone(owner, false);
                            }

                            targetParent.AddNode(copy);

                            if (child._children.Length > 0)
                            {
                                resume.Push(i);
                                sourceParent = child;
                                targetParent = copy;
                                children = sourceParent._children;
                                i = 0;
                            }
                            else if (copy is HtmlSelectElement select)
                            {
                                select.RestoreSelectedOption();
                            }
                        }
                        else if (resume.Count > 0)
                        {
                            if (targetParent is HtmlSelectElement select)
                            {
                                select.RestoreSelectedOption();
                            }

                            i = resume.Pop();
                            // Each copy was added to its parent's copy before its children were copied
                            sourceParent = sourceParent._parent!;
                            targetParent = targetParent._parent!;
                            children = sourceParent._children;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (subtrees is null || subtrees.Count == 0)
                    {
                        return;
                    }

                    var next = subtrees.Pop();
                    source = next.Source;
                    target = next.Target;
                }
            }
            finally
            {
                resume.Dispose();
            }
        }

        /// <summary>
        /// A subtree waiting to be copied by CloneDescendants: the source and the node its copy goes
        /// into. A struct rather than a tuple, since net462 has no System.ValueTuple.
        /// </summary>
        private readonly struct ClonePair
        {
            public readonly Node Source;
            public readonly Node Target;

            public ClonePair(Node source, Node target)
            {
                Source = source;
                Target = target;
            }
        }

        #endregion

        #region Construction

        StringOrMemory IConstructableNode.NodeName => NodeName;

        /// <inheritdoc />
        NodeFlags IConstructableNode.Flags => _flags;

        IConstructableNode? IConstructableNode.Parent
        {
            get => Parent;
            set
            {
                if (value is not null)
                {
                    Parent = (Node)value;
                }
            }
        }

        IConstructableNodeList IConstructableNode.ChildNodes => ChildNodes;

        // The tree builder reaches these two while it reparents nodes - the adoption agency, and
        // the frameset error recovery that drops a body element - alongside the raw AddNode and
        // RemoveNode it uses for the rest of the same algorithms. They are suppressed for the same
        // reason those are raw: this is construction, not a mutation of a finished document, so it
        // reports nothing and leaves the mutation version alone. The node removed steps and the
        // pre-remove hooks still run, since those are structural rather than observer bookkeeping.

        void IConstructableNode.RemoveFromParent()
        {
            Parent?.RemoveChild(this, true);
        }

        void IConstructableNode.RemoveChild(IConstructableNode childNode)
        {
            RemoveChild((Node)childNode, true);
        }

        void IConstructableNode.RemoveNode(Int32 idx, IConstructableNode childNode)
        {
            RemoveNode(idx, (Node)childNode);
        }

        void IConstructableNode.InsertNode(Int32 idx, IConstructableNode childNode)
        {
            InsertNode(idx, (Node)childNode);
        }

        void IConstructableNode.AddNode(IConstructableNode node)
        {
            AddNode((Node)node);
        }

        private static ReadOnlySpan<Char> WhiteSpace => " \t\r\n".AsSpan();

        void IConstructableNode.AppendText(StringOrMemory text, Boolean emitWhiteSpaceOnly)
        {
            if (!emitWhiteSpaceOnly && text.Memory.Span.Trim(WhiteSpace).Length == 0)
            {
                return;
            }

            AppendText(text.ToString());
        }

        void IConstructableNode.InsertText(Int32 idx, StringOrMemory text, Boolean emitWhiteSpaceOnly)
        {
            if (!emitWhiteSpaceOnly && text.Memory.Span.Trim(WhiteSpace).Length == 0)
            {
                return;
            }

            InsertText(idx, text.ToString());
        }

        #endregion
    }
}
