namespace AngleSharp.Dom
{
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

        // See CloneChildren; real documents rarely nest deeper than 40 levels
        private const Int32 MaximumCloneRecursion = 64;

        [ThreadStatic]
        private static CloneState? _cloneState;

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
                var owningDocument = OwningDocument;
                owningDocument?.MarkMutated();
                owningDocument?.QueueMutation(MutationRecord.ChildList(
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
            // same behaviour for a fraction of the cost, as on the attribute path. Records, like
            // the version, go to the owning document: a document's own children are observable.
            var owningDocument = OwningDocument;
            var reported = !suppressObservers && owningDocument is not null && owningDocument.HasMutationObservers;
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
                owningDocument?.MarkMutated();

                if (reported)
                {
                    owningDocument!.QueueMutation(MutationRecord.ChildList(
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
            var owningDocument = OwningDocument;
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
                owningDocument?.MarkMutated();

                // Both the record and the transient observer walk are pure observer bookkeeping:
                // one is thrown away by QueueMutation and the other iterates an empty list when
                // nothing is observing, so neither is worth its allocations then.
                if (owningDocument is not null && owningDocument.HasMutationObservers)
                {
                    var removedNodes = new NodeList { node };

                    owningDocument.QueueMutation(MutationRecord.ChildList(
                        target: this,
                        removedNodes: removedNodes,
                        previousSibling: oldPreviousSibling,
                        nextSibling: node.NextSibling));

                    owningDocument.AddTransientObserver(node);
                }
            }

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
                    var owningDocument = OwningDocument;
                    owningDocument?.MarkMutated();

                    if (owningDocument is not null && owningDocument.HasMutationObservers)
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

                        owningDocument.QueueMutation(MutationRecord.ChildList(
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

        // Recurses for the first levels and continues with an explicit stack below them, so deeply
        // nested documents cannot overflow the call stack
        private void Normalize(Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                NormalizeIteratively();
                return;
            }

            for (var i = 0; i < _children.Length; i++)
            {
                if (_children[i] is TextNode text)
                {
                    i = NormalizeText(text, i);
                }
                else if (_children[i].HasChildNodes)
                {
                    _children[i].Normalize(depth + 1);
                }
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
                var depth = 0;

                while (true)
                {
                    var descendInto = node.NormalizeChildrenFrom(start);

                    if (descendInto >= 0)
                    {
                        // Normalizing the child changes nothing at this level, so after the last
                        // child there is nothing to come back to
                        if (descendInto + 1 < node._children.Length)
                        {
                            resume.Push(descendInto + 1, depth);
                        }

                        node = node._children[descendInto];
                        start = 0;
                        depth++;
                    }
                    else if (resume.Count > 0)
                    {
                        start = resume.Pop(out var target);

                        for (; depth > target; depth--)
                        {
                            node = node._parent!;
                        }
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
                    i = NormalizeText(text, i);
                }
                else if (_children[i].HasChildNodes)
                {
                    return i;
                }
            }

            return -1;
        }

        // Removes the empty text child at the given index, or merges the text siblings that follow it
        // into it. Returns the index the caller's loop continues from.
        private Int32 NormalizeText(TextNode text, Int32 i)
        {
            var length = text.Length;

            if (length == 0)
            {
                RemoveChild(text, false);
                return i - 1;
            }

            // Nothing to merge, so no builder either, which takes the shared pool's lock twice
            if (i + 1 == _children.Length || _children[i + 1] is not TextNode)
            {
                text.Replace(length, 0, String.Empty);
                return i;
            }

            var sb = StringBuilderPool.Obtain();
            var end = i;
            var owner = Owner;

            // By index rather than NextSibling, which searches the children for the node every time
            while (end + 1 < _children.Length && _children[end + 1] is TextNode sibling)
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

            return i;
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
        public virtual Boolean Equals(INode? otherNode)
        {
            if (otherNode is null || !IsShallowEqual(this, otherNode))
            {
                return false;
            }

            // The spec compares the children recursively, which a deeply nested tree would overflow
            // the stack with. Both trees are walked in step instead, keeping a child index per level.
            INode node = this;
            var other = otherNode;
            var indices = default(Int32[]);
            var depth = 0;
            var index = 0;

            while (true)
            {
                var children = node.ChildNodes;

                if (index < children.Length)
                {
                    var child = children[index];
                    var otherChild = other.ChildNodes[index];

                    if (!IsShallowEqual(child, otherChild))
                    {
                        return false;
                    }

                    if (indices is null)
                    {
                        indices = new Int32[16];
                    }
                    else if (depth == indices.Length)
                    {
                        Array.Resize(ref indices, depth * 2);
                    }

                    indices[depth++] = index;
                    node = child;
                    other = otherChild;
                    index = 0;
                }
                else if (depth > 0)
                {
                    index = indices![--depth] + 1;
                    node = node.Parent!;
                    other = other.Parent!;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// https://dom.spec.whatwg.org/#concept-node-equals, except for comparing the children.
        /// </summary>
        private static Boolean IsShallowEqual(INode node, INode other)
        {
            if (node.NodeType != other.NodeType || node.ChildNodes.Length != other.ChildNodes.Length)
            {
                return false;
            }

            switch (node)
            {
                case IDocumentType doctype:
                    return other is IDocumentType otherDoctype &&
                        doctype.Name.Is(otherDoctype.Name) &&
                        doctype.PublicIdentifier.Is(otherDoctype.PublicIdentifier) &&
                        doctype.SystemIdentifier.Is(otherDoctype.SystemIdentifier);

                case IElement element:
                    return other is IElement otherElement && IsShallowEqual(element, otherElement);

                case IProcessingInstruction instruction:
                    return other is IProcessingInstruction otherInstruction &&
                        instruction.Target.Is(otherInstruction.Target) &&
                        instruction.Data.Is(otherInstruction.Data);

                case ICharacterData data:
                    return other is ICharacterData otherData && data.Data.Is(otherData.Data);

                default:
                    return true;
            }
        }

        private static Boolean IsShallowEqual(IElement element, IElement other)
        {
            if (!element.NamespaceUri.Is(other.NamespaceUri) || !element.Prefix.Is(other.Prefix) || !element.LocalName.Is(other.LocalName))
            {
                return false;
            }

            var attributes = element.Attributes;
            var otherAttributes = other.Attributes;

            if (attributes.Length != otherAttributes.Length)
            {
                return false;
            }

            // An element cannot carry two attributes with the same namespace and local name, so
            // looking each one up by that pair finds the only candidate in the other list.
            for (var i = 0; i < attributes.Length; i++)
            {
                var attribute = attributes[i]!;
                var match = otherAttributes.GetNamedItem(attribute.NamespaceUri, attribute.LocalName);

                if (match is null || !attribute.Value.Is(match.Value))
                {
                    return false;
                }
            }

            return true;
        }

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

            if (deep && _children.Length > 0)
            {
                CloneChildren(this, target, owner);
            }
        }

        /// <summary>
        /// Appends deep clones of the children of the source to the target.
        /// </summary>
        /// <remarks>
        /// Cloning recursively would overflow the stack on a deeply nested tree, but a loop costs
        /// real documents several percent, so the recursion only gives way below a fixed depth.
        /// From there the descendants are cloned in post-order in a loop, each through its own
        /// Clone, so every override still finishes its node after the children are attached: once
        /// it gets here, they are already waiting at the end of the pending clones.
        /// </remarks>
        internal static void CloneChildren(Node source, Node target, Document owner)
        {
            if (source._children.Length == 0)
            {
                return;
            }

            var state = _cloneState ??= new CloneState();
            var clones = state.Clones;
            var parents = state.Parents;
            var last = parents.Count - 1;
            var start = clones.Count;

            if (last >= 0 && parents[last].Key == source)
            {
                start = parents[last].Value;
                parents.RemoveAt(last);
            }
            else if (state.Depth < MaximumCloneRecursion)
            {
                state.Depth++;

                try
                {
                    var children = source._children;

                    for (var i = 0; i < children.Length; i++)
                    {
                        target.AddNode(children[i].Clone(owner, true));
                    }
                }
                finally
                {
                    state.Depth--;
                }

                return;
            }
            else
            {
                CloneDescendants(source, owner, state);
            }

            for (var i = start; i < clones.Count; i++)
            {
                target.AddNode(clones[i]);
            }

            clones.RemoveRange(start, clones.Count - start);
        }

        private static void CloneDescendants(Node root, Document owner, CloneState state)
        {
            var clones = state.Clones;
            var parents = state.Parents;
            var steps = state.Steps;
            var bottom = steps.Count;
            var clonesBottom = clones.Count;
            var parentsBottom = parents.Count;
            var step = new CloneStep(null, root, null, -1, clones.Count);

            try
            {
                while (true)
                {
                    var children = step.List._children;

                    if (step.Index < children.Length)
                    {
                        var child = children[step.Index++];
                        // A template's contents are cloned with it, as the template's own Clone asks
                        // for them after its children, so they go below the children on the pending clones.
                        var content = child is Html.Dom.HtmlTemplateElement template ? (Node)template.Content : null;

                        if (content is not null && content._children.Length > 0)
                        {
                            steps.Add(step);
                            step = new CloneStep(child, content, content, clones.Count, -1);
                        }
                        else if (child._children.Length > 0)
                        {
                            steps.Add(step);
                            step = new CloneStep(child, child, null, -1, clones.Count);
                        }
                        else
                        {
                            clones.Add(child.Clone(owner, true));
                        }
                    }
                    else if (step.Node is null)
                    {
                        return;
                    }
                    else if (step.List != step.Node)
                    {
                        step = new CloneStep(step.Node, step.Node, step.Content, step.ContentStart, clones.Count);
                    }
                    else
                    {
                        var height = step.Content is null ? step.ChildrenStart : step.ContentStart;
                        var parentsHeight = parents.Count;

                        if (step.Content is not null)
                        {
                            parents.Add(new KeyValuePair<Node, Int32>(step.Content, step.ContentStart));
                        }

                        // Only nodes with children ask for them, so only those get a run
                        if (step.Node._children.Length > 0)
                        {
                            parents.Add(new KeyValuePair<Node, Int32>(step.Node, step.ChildrenStart));
                        }

                        var clone = step.Node.Clone(owner, true);

                        // An override that never asks for its children leaves them behind.
                        clones.RemoveRange(height, clones.Count - height);
                        parents.RemoveRange(parentsHeight, parents.Count - parentsHeight);
                        clones.Add(clone);
                        step = steps[steps.Count - 1];
                        steps.RemoveAt(steps.Count - 1);
                    }
                }
            }
            catch
            {
                clones.RemoveRange(clonesBottom, clones.Count - clonesBottom);
                parents.RemoveRange(parentsBottom, parents.Count - parentsBottom);
                steps.RemoveRange(bottom, steps.Count - bottom);
                throw;
            }
        }

        // Finished clones still waiting for the clone of their parent, where each parent's run of
        // them starts, and the path down to the node whose descendants are being cloned
        private sealed class CloneState
        {
            // How many CloneChildren calls are recursing on this thread
            public Int32 Depth;
            public readonly List<Node> Clones = [];
            public readonly List<KeyValuePair<Node, Int32>> Parents = [];
            public readonly List<CloneStep> Steps = [];
        }

        private struct CloneStep(Node? node, Node list, Node? content, Int32 contentStart, Int32 childrenStart)
        {
            // The node to clone once its descendants are done; null for the root, which is cloned
            // by the caller
            public readonly Node? Node = node;
            // The node whose children are being cloned: the node itself, or its template contents
            public readonly Node List = list;
            public readonly Node? Content = content;
            public readonly Int32 ContentStart = contentStart;
            public readonly Int32 ChildrenStart = childrenStart;
            public Int32 Index = 0;
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
