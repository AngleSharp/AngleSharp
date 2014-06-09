namespace AngleSharp.DOM
{
    using System;
    using System.Reflection;
    using AngleSharp.DOM.Collections;

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    [DomName("Node")]
    public class Node : INode, IHtmlObject
    {
        #region Fields

        /// <summary>
        /// The responsible document.
        /// </summary>
        protected Document _owner;
        /// <summary>
        /// The lower node.
        /// </summary>
        protected Node _parent;
        /// <summary>
        /// The upper nodes.
        /// </summary>
        protected NodeList _children;
        /// <summary>
        /// The node's name.
        /// </summary>
        protected String _name;
        /// <summary>
        /// The node's baseURI.
        /// </summary>
        protected String _baseURI;
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
        /// Gets a boolean value indicating whether the current Node has child nodes or not.
        /// </summary>
        [DomName("hasChildNodes")]
        public Boolean HasChilds
        {
            get { return _children.Length != 0; }
        }

        /// <summary>
        /// Gets or sets the absolute base URI of a node or null if unable to obtain an absolute URI.
        /// </summary>
        [DomName("baseURI")]
        public String BaseUri
        {
            get 
            {
                if (_baseURI != null)
                    return _baseURI;
                else if (_parent != null)
                    return _parent.BaseUri;
                else if (Owner != null)
                    return Owner.DocumentUri;

                return String.Empty;
            }
            set { _baseURI = value; }
        }

        /// <summary>
        /// Gets the node immediately preceding this node's parent's list of nodes, 
        /// null if the specified node is the first in that list.
        /// </summary>
        [DomName("previousSibling")]
        public Node PreviousSibling
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
        [DomName("nextSibling")]
        public Node NextSibling
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
        [DomName("firstChild")]
        public Node FirstChild
        {
            get { return _children.Length > 0 ? _children[0] : null; }
        }

        /// <summary>
        /// Gets the last child node of this node.
        /// </summary>
        [DomName("lastChild")]
        public Node LastChild
        {
            get { return _children.Length > 0 ? _children[_children.Length - 1] : null; }
        }

        /// <summary>
        /// Gets the type of this node.
        /// </summary>
        [DomName("nodeType")]
        public NodeType NodeType 
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets the value of the current node.
        /// </summary>
        [DomName("nodeValue")]
        public virtual String NodeValue 
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        [DomName("textContent")]
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

        /// <summary>
        /// Gets the owner document of the node.
        /// </summary>
        [DomName("ownerDocument")]
        public Document Owner 
        {
            get { return _owner; }
            internal set 
            {
                if (_owner == value)
                    return;
                else if (_owner != null && value != null)
                    throw new DOMException(ErrorCode.InUse);

                _owner = value;

                for (int i = 0; i < _children.Length; i++)
                    _children[i].Owner = value;
            }
        }

        /// <summary>
        /// Gets the parent node of this node, which is either an Element node, a Document node, or a DocumentFragment node.
        /// </summary>
        [DomName("parentNode")]
        public Node Parent
        {
            get { return _parent; }
            internal set { _parent = value; } 
        }

        /// <summary>
        /// Gets or sets the parent element of this node.
        /// </summary>
        [DomName("parentElement")]
        public Element ParentElement
        {
            get { return _parent as Element; }
        }

        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        [DomName("childNodes")]
        public NodeList ChildNodes
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the tag name for this node.
        /// </summary>
        [DomName("nodeName")]
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
            get { return IsInMathML && NodeName == Tags.AnnotationXml; }
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
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal virtual void OnAttributeChanged(String name)
        {
        }

        /// <summary>
        /// Appends the given character to the node.
        /// </summary>
        /// <param name="c">The character to append.</param>
        /// <returns>The node which contains the text.</returns>
        internal Node AppendText(Char c)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild != null)
            {
                lastChild.Append(c);
                return lastChild;
            }

            var element = new TextNode(c);
            return AppendChild(element);
        }

        /// <summary>
        /// Appends the given characters to the node.
        /// </summary>
        /// <param name="s">The characters to append.</param>
        /// <returns>The node which contains the text.</returns>
        internal Node AppendText(String s)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild != null)
            {
                lastChild.Append(s);
                return lastChild;
            }

            var element = new TextNode(s);
            return AppendChild(element);
        }

        /// <summary>
        /// Inserts the given character in the node.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="s">The characters to append.</param>
        /// <returns>The node which contains the text.</returns>
        internal Node InsertText(Int32 index, String s)
        {
            if (index > 0 && index <= _children.Length && _children[index - 1] is TextNode)
            {
                var node = (TextNode)_children[index - 1];
                node.Append(s);
                return node;
            }
            else if (index >= 0 && index < _children.Length && _children[index] is TextNode)
            {
                var node = (TextNode)_children[index];
                node.Insert(0, s);
                return node;
            }
            
            return InsertChild(index, new TextNode(s));
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
        [DomName("appendChild")]
        public virtual Node AppendChild(Node child)
        {
            if (child is DocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    AppendChild(childs[i]);
            }
            else if (child is Document || child.Contains(this))
            {
                throw new DOMException(ErrorCode.HierarchyRequestError);
            }
            else
            {
                if (child.Parent != null)
                    throw new DOMException(ErrorCode.InUse);

                child._parent = this;
                child.Owner = _owner ?? (this as Document);
                _children.Add(child);
            }

            return child;
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        [DomName("insertChild")]
        public virtual Node InsertChild(Int32 index, Node child)
        {
            if (child is DocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    InsertChild(index + i, childs[i]);
            }
            else if (child is Document || child.Contains(this))
            {
                throw new DOMException(ErrorCode.HierarchyRequestError);
            }
            else
            {
                if (child.Parent != null)
                    throw new DOMException(ErrorCode.InUse);

                child._parent = this;
                child.Owner = _owner ?? (this as Document);
                _children.Insert(index, child);
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
        [DomName("insertBefore")]
        public virtual Node InsertBefore(Node newElement, Node referenceElement)
        {
            if (newElement is Document || newElement.Contains(this))
                throw new DOMException(ErrorCode.HierarchyRequestError);

            var n = _children.Length;

            for (int i = 0; i < n; i++)
            {
                if (_children[i] == referenceElement)
                    return InsertChild(i, newElement);
            }

            return AppendChild(newElement);
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        [DomName("replaceChild")]
        public virtual Node ReplaceChild(Node newChild, Node oldChild)
        {
            if (newChild is Document || newChild.Contains(this))
                throw new DOMException(ErrorCode.HierarchyRequestError);
            else if (newChild == oldChild)
                return oldChild;
            else if (newChild.Parent != null)
                throw new DOMException(ErrorCode.InUse);

            var n = _children.Length;

            for (int i = 0; i < n; i++)
            {
                if (_children[i] == oldChild)
                {
                    RemoveChild(oldChild);
                    InsertChild(i, newChild);
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
        [DomName("removeChild")]
        public virtual Node RemoveChild(Node child)
        {
            if (_children.Contains(child))
            {
                child._parent = null;
                _children.Remove(child);
            }

            return child;
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DomName("cloneNode")]
        public virtual Node Clone(Boolean deep = true)
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
        [DomName("compareDocumentPosition")]
        public virtual DocumentPosition CompareDocumentPosition(Node otherNode)
        {
            if (this == otherNode)
                return DocumentPosition.Same;

            if(this.Owner != otherNode.Owner)
            {
                return DocumentPosition.Disconnected | DocumentPosition.ImplementationSpecific |
                    (otherNode.GetHashCode() > this.GetHashCode() ? DocumentPosition.Following : DocumentPosition.Preceding);
            }
            else if (this.Contains(otherNode))
            {
                return DocumentPosition.ContainedBy | DocumentPosition.Following;
            }
            else if (otherNode.Contains(this))
            {
                return DocumentPosition.Contains | DocumentPosition.Preceding;
            }
            
            return CompareRelativePositionInNodeList(_owner.ChildNodes, this, otherNode);
        }

        /// <summary>
        /// Indicates whether a node is a descendent of this node.
        /// </summary>
        /// <param name="otherNode">The node that's being compared against.</param>
        /// <returns>The return value is true if otherNode is a descendent of node, or node itself. Otherwise the return value is false.</returns>
        [DomName("contains")]
        public virtual Boolean Contains(Node otherNode)
        {
            if (otherNode == this)
                return true;

            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] == otherNode)
                    return true;
                else if (_children[i].Contains(otherNode))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Puts the specified node and all of its subtree into a "normalized" form. In a normalized subtree, no text nodes in the
        /// subtree are empty and there are no adjacent text nodes.
        /// </summary>
        /// <returns>The current node.</returns>
        [DomName("normalize")]
        public virtual Node Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] is TextNode)
                {
                    var text = (TextNode)_children[i];

                    if (text.Length == 0)
                    {
                        RemoveChild(text);
                        i--;
                    }
                    else
                    {
                        while (text.NextSibling is TextNode)
                        {
                            var t = (TextNode)text.NextSibling;
                            text.Append(t.Data);
                            RemoveChild(t);
                        }
                    }
                }
                else if(_children[i].ChildNodes.Length != 0)
                    _children[i].Normalize();
            }
            return this;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        [DomName("lookupNamespaceURI")]
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
        [DomName("lookupPrefix")]
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
        [DomName("isDefaultNamespace")]
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
        [DomName("isEqualNode")]
        public virtual Boolean IsEqualNode(Node otherNode)
        {
            if (this._baseURI != otherNode._baseURI)
                return false;

            if (this._name != otherNode._name)
                return false;

            if (this._children.Length != otherNode._children.Length)
                return false;

            for (int i = 0; i < this._children.Length; i++)
            {
                if(!this._children[i].IsEqualNode(otherNode._children[i]))
                    return false;
            }

            return true;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Runs the mutation macro as defined in 5.2.2 Mutation methods
        /// of http://www.w3.org/TR/domcore/.
        /// </summary>
        /// <param name="nodes">The nodes array to add.</param>
        /// <returns>A (single) node.</returns>
        protected Node MutationMacro(Node[] nodes)
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
        static DocumentPosition CompareRelativePositionInNodeList(NodeList list, Node nodeA, Node nodeB)
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
                return DocumentPosition.Preceding;
            else if (bPos < aPos)
                return DocumentPosition.Following;
            else if (aPos != -1 && bPos != -1)
                return CompareRelativePositionInNodeList(list[aPos].ChildNodes, nodeA, nodeB);

            return DocumentPosition.Disconnected;
        }

        /// <summary>
        /// Copies all (Node) properties of the source to the target.
        /// </summary>
        /// <param name="source">The source node.</param>
        /// <param name="target">The target node.</param>
        /// <param name="deep">Is a deep-copy required?</param>
        static protected void CopyProperties(Node source, Node target, Boolean deep)
        {
            target._baseURI = source._baseURI;
            target._name = source._name;
            target._type = source.NodeType;

            if (deep)
            {
                for (int i = 0; i < source._children.Length; i++)
                    target._children.Add(source._children[i].Clone(true));
            }
        }

        /// <summary>
        /// Checks if the given namespace name and URI are valid.
        /// </summary>
        /// <param name="namespaceName">The localName of the attribute.</param>
        /// <param name="namespaceURI">The value of the attribute.</param>
        /// <returns>Returns the result of the check.</returns>
        static protected bool IsValidNamespaceDeclaration(String namespaceName, String namespaceURI)
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
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public virtual String ToText()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var child in _children)
                sb.Append(child.ToText());

            return sb.ToPool();
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

            if (indent == 0)
                sb.AppendLine(content);
            else
            {
                sb.Append(String.Empty.PadRight(2 * indent, ' '));
                sb.AppendLine(content);
            }

            foreach (var child in _children)
                sb.Append(child.ToTree(indent + 1));

            return sb.ToPool();
        }

        #endregion
    }
}
