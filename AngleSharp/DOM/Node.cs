namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    public class Node : INode, IHtmlObject
    {
        #region Fields

        Document _owner;
        String _baseUri;
        Node _parent;
        NodeList _children;

        /// <summary>
        /// The node's name.
        /// </summary>
        protected String _name;
        /// <summary>
        /// The type of the node.
        /// </summary>
        protected NodeType _type;

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a new node.
        /// </summary>
        internal Node()
        {
            _name = String.Empty;
            _children = new NodeList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a boolean value indicating whether the current Node 
        /// has child nodes or not.
        /// </summary>
        public Boolean HasChilds
        {
            get { return _children.Length != 0; }
        }

        /// <summary>
        /// Gets or sets the absolute base URI of a node or null if
        /// unable to obtain an absolute URI.
        /// </summary>
        public String BaseUri
        {
            get 
            {
                if (_baseUri != null)
                    return _baseUri;
                else if (_parent != null)
                    return _parent.BaseUri;
                else if (Owner != null)
                    return Owner.DocumentUri;

                return String.Empty;
            }
            set { _baseUri = value; }
        }

        /// <summary>
        /// Gets the node immediately preceding this node's parent's list of nodes, 
        /// null if the specified node is the first in that list.
        /// </summary>
        public INode PreviousSibling
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
        /// Gets the node immediately following this node's parent's list of nodes,
        /// or null if the current node is the last node in that list.
        /// </summary>
        public INode NextSibling
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
        public INode FirstChild
        {
            get { return _children.Length > 0 ? _children[0] : null; }
        }

        /// <summary>
        /// Gets the last child node of this node.
        /// </summary>
        public INode LastChild
        {
            get { return _children.Length > 0 ? _children[_children.Length - 1] : null; }
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
            set 
            {
                for (int i = _children.Length - 1; i != -1; i--)
                    RemoveChild(_children[i]);

                AppendChild(new TextNode(value));
            }
        }

        IDocument INode.Owner
        {
            get { return _owner; }
        }

        /// <summary>
        /// Gets the owner document of the node.
        /// </summary>
        internal Document Owner 
        {
            get { return _owner; }
            set 
            {
                if (_owner == value)
                    return;
                else if (_owner != null && value != null)
                    throw new DomException(ErrorCode.InUse);

                _owner = value;

                for (int i = 0; i < _children.Length; i++)
                    _children[i].Owner = value;
            }
        }

        /// <summary>
        /// Gets the parent node.
        /// </summary>
        INode INode.Parent
        {
            get { return _parent; }
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
        /// Gets or sets the parent element of this node.
        /// </summary>
        public IElement ParentElement
        {
            get { return _parent as IElement; }
        }

        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        INodeList INode.ChildNodes
        {
            get { return _children; }
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
        /// Gets the tag name for this node.
        /// </summary>
        public String NodeName
        {
            get { return _name; }
            internal set {  _name = value; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        internal protected virtual Boolean IsSpecial
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if this node is in the HTML namespace.
        /// </summary>
        internal protected virtual Boolean IsInHtml
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if this node is the MathML namespace.
        /// </summary>
        internal protected virtual Boolean IsInMathML
        {
            get { return false; }
        }

        /// <summary>
        /// Gets if the node is in the MathML namespace and of type annotation-xml.
        /// </summary>
        internal Boolean IsInMathMLSVGReady
        {
            get { return IsInMathML && _name == Tags.AnnotationXml; }
        }

        /// <summary>
        /// Gets the status if the current node is in the MathML namespace.
        /// </summary>
        internal protected virtual Boolean IsInSvg
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if the node is a MathML text integration point.
        /// </summary>
        internal protected virtual Boolean IsMathMLTIP
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        internal protected virtual Boolean IsHtmlTIP
        {
            get { return false; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Appends the given characters to the node.
        /// </summary>
        /// <param name="s">The characters to append.</param>
        internal void AppendText(String s)
        {
            var lastChild = LastChild as IText;

            if (lastChild == null)
                AppendChild(new TextNode(s));
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
            if (index > 0 && index <= _children.Length && _children[index - 1] is IText)
            {
                var node = (IText)_children[index - 1];
                node.Append(s);
            }
            else if (index >= 0 && index < _children.Length && _children[index] is IText)
            {
                var node = (IText)_children[index];
                node.Insert(0, s);
            }
            else
                InsertChild(index, new TextNode(s));
        }

        /// <summary>
        /// Finds the index of the given node.
        /// </summary>
        /// <param name="node">The node which needs to know its index.</param>
        /// <returns>The index of the node or -1 if the node is not a child.</returns>
        internal Int32 IndexOf(Node node)
        {
            var n = _children.Length;

            for (var i = 0; i < n; i++)
            {
                if (_children[i] == node)
                    return i;
            }

            return -1;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public virtual INode AppendChild(INode child)
        {
            return DefaultAppendChild(child);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public virtual INode InsertChild(Int32 index, INode child)
        {
            return DefaultInsertChild(index, child);
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        public virtual INode InsertBefore(INode newElement, INode referenceElement)
        {
            return DefaultInsertBefore(newElement, referenceElement);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        public virtual INode ReplaceChild(INode newChild, INode oldChild)
        {
            return DefaultReplaceChild(newChild, oldChild);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public virtual INode RemoveChild(INode child)
        {
            return DefaultRemoveChild(child);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public virtual INode Clone(Boolean deep = true)
        {
            var node = new Node();
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Compares the position of the current node against another node in any other document.
        /// </summary>
        /// <param name="otherNode">The node that's being compared against.</param>
        /// <returns>The relationship that otherNode has with node, given in a bitmask.</returns>
        public virtual DocumentPositions CompareDocumentPosition(INode otherNode)
        {
            if (this == otherNode)
                return DocumentPositions.Same;

            if (_owner != otherNode.Owner)
                return DocumentPositions.Disconnected | DocumentPositions.ImplementationSpecific | (otherNode.GetHashCode() > GetHashCode() ? DocumentPositions.Following : DocumentPositions.Preceding);
            else if (Contains(otherNode))
                return DocumentPositions.ContainedBy | DocumentPositions.Following;
            else if (otherNode.Contains(this))
                return DocumentPositions.Contains | DocumentPositions.Preceding;
            
            return CompareRelativePositionInNodeList(_owner.ChildNodes, this, otherNode);
        }

        /// <summary>
        /// Indicates whether a node is a descendent of this node.
        /// </summary>
        /// <param name="otherNode">The node that's being compared against.</param>
        /// <returns>The return value is true if otherNode is a descendent of node, or node itself. Otherwise the return value is false.</returns>
        public virtual Boolean Contains(INode otherNode)
        {
            if (otherNode == this)
                return true;

            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] == otherNode || _children[i].Contains(otherNode))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Puts the specified node and all of its subtree into a "normalized" form. In a normalized subtree, no text nodes in the
        /// subtree are empty and there are no adjacent text nodes.
        /// </summary>
        public virtual void Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
            {
                var text = _children[i] as IText;

                if (text != null)
                {
                    if (text.Length == 0)
                    {
                        RemoveChild(text);
                        i--;
                    }
                    else
                    {
                        while (text.NextSibling is IText)
                        {
                            var t = (IText)text.NextSibling;
                            text.Append(t.Data);
                            RemoveChild(t);
                        }
                    }
                }
                else if (_children[i].ChildNodes.Length != 0)
                    _children[i].Normalize();
            }
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public virtual String LookupNamespaceUri(String prefix)
        {
            if (_parent != null)
                _parent.LookupNamespaceUri(prefix);

            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        public virtual String LookupPrefix(String namespaceURI)
        {
            if(_parent != null)
                return _parent.LookupPrefix(namespaceURI); 

            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        public virtual Boolean IsDefaultNamespace(String namespaceURI)
        {
            if (_parent != null)
                _parent.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Tests whether two nodes are equal.
        /// </summary>
        /// <param name="otherNode">The node to compare equality with.</param>
        /// <returns>True if they are equal, otherwise false.</returns>
        public virtual Boolean IsEqualNode(INode otherNode)
        {
            if (BaseUri != otherNode.BaseUri)
                return false;

            if (NodeName != otherNode.NodeName)
                return false;

            if (ChildNodes.Length != otherNode.ChildNodes.Length)
                return false;

            for (int i = 0; i < _children.Length; i++)
            {
                if (!_children[i].IsEqualNode(otherNode.ChildNodes[i]))
                    return false;
            }

            return true;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        protected INode DefaultAppendChild(INode child)
        {
            if (child is IDocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    DefaultAppendChild(childs[i]);
            }
            else if (child is IDocument || child.Contains(this))
            {
                throw new DomException(ErrorCode.HierarchyRequest);
            }
            else
            {
                if (child.Parent != null)
                    throw new DomException(ErrorCode.InUse);

                var childNode = child as Node;

                if (childNode != null)
                {
                    childNode._parent = this;
                    childNode.Owner = _owner ?? (this as Document);
                    _children.Add(childNode);
                }
            }

            return child;
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        protected INode DefaultInsertChild(Int32 index, INode child)
        {
            if (child is IDocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    DefaultInsertChild(index + i, childs[i]);
            }
            else if (child is IDocument || child.Contains(this))
            {
                throw new DomException(ErrorCode.HierarchyRequest);
            }
            else
            {
                if (child.Parent != null)
                    throw new DomException(ErrorCode.InUse);

                var childNode = child as Node;

                if (childNode != null)
                {
                    childNode._parent = this;
                    childNode.Owner = _owner ?? (this as Document);
                    _children.Insert(index, childNode);
                }
            }

            return child;
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        protected INode DefaultInsertBefore(INode newElement, INode referenceElement)
        {
            if (newElement is IDocument || newElement.Contains(this))
                throw new DomException(ErrorCode.HierarchyRequest);

            var n = _children.Length;

            for (int i = 0; i < n; i++)
            {
                if (_children[i] == referenceElement)
                    return DefaultInsertChild(i, newElement);
            }

            return DefaultAppendChild(newElement);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        protected INode DefaultReplaceChild(INode newChild, INode oldChild)
        {
            if (newChild is IDocument || newChild.Contains(this))
                throw new DomException(ErrorCode.HierarchyRequest);
            else if (newChild == oldChild)
                return oldChild;
            else if (newChild.Parent != null)
                throw new DomException(ErrorCode.InUse);

            var n = _children.Length;

            for (int i = 0; i < n; i++)
            {
                if (_children[i] == oldChild)
                {
                    DefaultRemoveChild(oldChild);
                    DefaultInsertChild(i, newChild);
                    return oldChild;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        protected INode DefaultRemoveChild(INode child)
        {
            var childNode = child as Node;

            if (childNode != null && _children.Contains(childNode))
            {
                childNode.Owner = null;
                childNode.Parent = null;
                _children.Remove(childNode);
            }

            return child;
        }

        /// <summary>
        /// Firing a simple event named e means that a trusted event with the name e,
        /// which does not bubble (except where otherwise stated) and is not cancelable
        /// (except where otherwise stated), and which uses the Event interface, must
        /// be created and dispatched at the given target.
        /// </summary>
        /// <param name="eventName">The name of the event to be fired.</param>
        /// <param name="bubble">Optional parameter to enable bubbling.</param>
        /// <param name="cancelable">Optional parameter to make it cancelable.</param>
        /// <returns>True if the element was cancelled, otherwise false.</returns>
        protected Boolean FireSimpleEvent(String eventName, Boolean bubble = false, Boolean cancelable = false)
        {
            //TODO
            //http://www.w3.org/html/wg/drafts/html/master/webappapis.html#fire-a-simple-event
            return false;
        }

        /// <summary>
        /// Gets the hyperreference of the given URL -
        /// transforming the given (relative) URL to an absolute URL
        /// if required.
        /// </summary>
        /// <param name="url">The given URL.</param>
        /// <returns>The absolute URL.</returns>
        protected String HyperRef(String url)
        {
            if (url == null || Location.IsAbsolute(url))
                return url;

            return Location.MakeAbsolute(BaseUri, url);
        }

        /// <summary>
        /// Runs the mutation macro as defined in 5.2.2 Mutation methods
        /// of http://www.w3.org/TR/domcore/.
        /// </summary>
        /// <param name="nodes">The nodes array to add.</param>
        /// <returns>A (single) node.</returns>
        protected INode MutationMacro(INode[] nodes)
        {
            if (nodes.Length > 1)
            {
                var node = new DocumentFragment();

                for (int i = 0; i < nodes.Length; i++)
                    node.AppendChild(nodes[i]);

                return node;
            }

            return nodes[0];
        }

        /// <summary>
        /// Compares the relative position in a node list.
        /// </summary>
        /// <param name="list">The node list as a base.</param>
        /// <param name="nodeA">The first node.</param>
        /// <param name="nodeB">The other node.</param>
        /// <returns>The position.</returns>
        static DocumentPositions CompareRelativePositionInNodeList(NodeList list, INode nodeA, INode nodeB)
        {
            var aPos = -1;
            var bPos = -1;

            for (int i = 0; i < list.Length; i++)
            {
                if (aPos == -1 && list[i].Contains(nodeA))
                    aPos = i;

                if (bPos == -1 && list[i].Contains(nodeB))
                    bPos = i;
            }

            if (aPos < bPos)
                return DocumentPositions.Preceding;
            else if (bPos < aPos)
                return DocumentPositions.Following;
            else if (aPos != -1 && bPos != -1)
                return CompareRelativePositionInNodeList(list[aPos].ChildNodes, nodeA, nodeB);

            return DocumentPositions.Disconnected;
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
            target._name = source._name;
            target._type = source.NodeType;

            if (deep)
            {
                for (int i = 0; i < source._children.Length; i++)
                    target._children.Add(source._children[i].Clone(true) as Node);//TODO remove cast ASAP
            }
        }

        /// <summary>
        /// Checks if the given namespace name and URI are valid.
        /// </summary>
        /// <param name="namespaceName">The localName of the attribute.</param>
        /// <param name="namespaceUri">The value of the attribute.</param>
        /// <returns>Returns the result of the check.</returns>
        static protected Boolean IsValidNamespaceDeclaration(String namespaceName, String namespaceUri)
        {
            if (namespaceName == Namespaces.Declaration)
                return false;

            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a string representation of the node.
        /// </summary>
        /// <returns>A string containing some information about the node.</returns>
        public override String ToString()
        {
            var attr = GetType().GetTypeInfo().GetCustomAttribute<DomNameAttribute>(true);

            if (attr != null)
                return attr.OfficialName;

            return "Object";
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public virtual String ToHtml()
        {
            return TextContent;
        }

        /// <summary>
        /// Returns a (string) tree representation of the node and all sub-nodes.
        /// </summary>
        /// <param name="indent">The optional indentation level.</param>
        /// <returns></returns>
        public String ToTree(Int32 indent = 0)
        {
            var sb = Pool.NewStringBuilder();
            var content = ToString();

            if (indent != 0)
                sb.Append(String.Empty.PadRight(2 * indent, ' '));
            
            sb.AppendLine(content);

            for (int i = 0; i < _children.Length; i++)
                sb.Append(_children[i].ToTree(indent + 1));

            return sb.ToPool();
        }

        #endregion

        #region Events

        /// <summary>
        /// Register an event handler of a specific event type on the Node.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="callback">The listener parameter indicates the EventListener function to be added.</param>
        /// <param name="capture">True indicates that the user wishes to initiate capture. After initiating
        /// capture, all events of the specified type will be dispatched to the registered listener before being
        /// dispatched to any Node beneath it in the DOM tree. Events which are bubbling upward through the tree
        /// will not trigger a listener designated to use capture.</param>
        public void AddEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an event listener from the Node.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="callback">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not.</param>
        public void RemoveEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispatch an event to this Node.
        /// </summary>
        /// <param name="ev">The event to dispatch.</param>
        /// <returns>False if at least one of the event handlers, which handled this event called preventDefault(). Otherwise true.</returns>
        public Boolean Dispatch(IEvent ev)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
