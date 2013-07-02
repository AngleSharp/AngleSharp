using System;
using System.Text;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using System.Collections.Generic;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    [DOM("Node")]
    public class Node : INode, IHTMLObject
    {
        #region Members

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
        /// The attributes of the node.
        /// </summary>
        protected NamedNodeMap _attributes;
        /// <summary>
        /// The node's name.
        /// </summary>
        protected String _name;
        /// <summary>
        /// The node's namespace.
        /// </summary>
        protected String _ns;
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
            _attributes = new NamedNodeMap(this);
            _children = new NodeList();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets a boolean value indicating whether the current Node has attribute nodes or not.
        /// </summary>
        [DOM("hasAttributes")]
        public Boolean HasAttributes
        {
            get { return _attributes.Length != 0; }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the current Node has child nodes or not.
        /// </summary>
        [DOM("hasChildNodes")]
        public Boolean HasChildNodes
        {
            get { return _children.Length != 0; }
        }

        /// <summary>
        /// Gets or sets the absolute base URI of a node or null if unable to obtain an absolute URI.
        /// </summary>
        [DOM("baseURI")]
        public String BaseURI
        {
            get 
            {
                if (_baseURI != null)
                    return _baseURI;
                else if (_parent != null)
                    return _parent.BaseURI;

                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the local part of the qualified name of this node.
        /// </summary>
        [DOM("localName")]
        public String LocalName
        {
            get { return _name.Contains(':') ? _name.Substring(_name.IndexOf(':') + 1) : _name; }
        }

        /// <summary>
        /// Gets or sets the namespace URI of this node.
        /// </summary>
        [DOM("namespaceURI")]
        public String NamespaceURI
        {
            get { return _ns; }
            set { _ns = value; }
        }

        /// <summary>
        /// Gets the node immediately preceding this node's parent's list of nodes, 
        /// null if the specified node is the first in that list.
        /// </summary>
        [DOM("previousSibling")]
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
        [DOM("nextSibling")]
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
        [DOM("firstChild")]
        public Node FirstChild
        {
            get { return _children.Length > 0 ? _children[0] : null; }
        }

        /// <summary>
        /// Gets the last child node of this node.
        /// </summary>
        [DOM("lastChild")]
        public Node LastChild
        {
            get { return _children.Length > 0 ? _children[_children.Length - 1] : null; }
        }

        /// <summary>
        /// Gets the type of this node.
        /// </summary>
        [DOM("nodeType")]
        public NodeType NodeType 
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets the value of the current node.
        /// </summary>
        [DOM("nodeValue")]
        public virtual String NodeValue 
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// Gets the namespace prefix of the specified node, or null if no prefix is specified.
        /// </summary>
        [DOM("prefix")]
        public String Prefix
        {
            get { return _name.Contains(':') ? _name.Substring(0, _name.IndexOf(':')) : null; }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        [DOM("textContent")]
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
        [DOM("ownerDocument")]
        public Document OwnerDocument 
        {
            get { return _owner; }
            internal set 
            {
                if (_owner == value)
                    return;
                else if (_owner != null && value != null)
                    throw new DOMException(ErrorCode.InUse);
                else if(_owner != null)
                    _owner.DereferenceNode(this);
                else
                    value.ReferenceNode(this);

                _owner = value;

                for (int i = 0; i < _children.Length; i++)
                    _children[i].OwnerDocument = value;
            }
        }

        /// <summary>
        /// Gets the parent node of this node, which is either an Element node, a Document node, or a DocumentFragment node.
        /// </summary>
        [DOM("parentNode")]
        public Node ParentNode
        {
            get { return _parent; }
            internal set { _parent = value; } 
        }

        /// <summary>
        /// Gets or sets the parent element of this node.
        /// </summary>
        [DOM("parentElement")]
        public Element ParentElement
        {
            get { return _parent as Element; }
        }

        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        [DOM("childNodes")]
        public NodeList ChildNodes
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the tag name for this node.
        /// </summary>
        [DOM("nodeName")]
        public String NodeName
        {
            get { return _name; }
            internal set {  _name = value; }
        }

        /// <summary>
        /// Gets the attributes for this node.
        /// </summary>
        [DOM("attributes")]
        public NamedNodeMap Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        internal protected virtual bool IsSpecial
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if this node is in the HTML namespace.
        /// </summary>
        internal protected virtual bool IsInHtml
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if this node is the MathML namespace.
        /// </summary>
        internal protected virtual bool IsInMathML
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if the current node is in the MathML namespace.
        /// </summary>
        internal protected virtual bool IsInSvg
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if the node is a MathML text integration point.
        /// </summary>
        internal protected virtual bool IsMathMLTIP
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        internal protected virtual bool IsHtmlTIP
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
        internal Node AppendText(char c)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild != null)
                return lastChild.AppendData(c);

            var element = new TextNode(c);
            return AppendChild(element);
        }

        /// <summary>
        /// Appends the given characters to the node.
        /// </summary>
        /// <param name="s">The characters to append.</param>
        /// <returns>The node which contains the text.</returns>
        internal Node AppendText(string s)
        {
            var lastChild = LastChild as TextNode;

            if (lastChild != null)
                return lastChild.AppendData(s);

            var element = new TextNode(s);
            return AppendChild(element);
        }

        /// <summary>
        /// Inserts the given character in the node.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="c">The character to append.</param>
        /// <returns>The node which contains the text.</returns>
        internal Node InsertText(int index, char c)
        {
            if (index > 0 && index <= _children.Length && _children[index - 1] is TextNode)
                return ((TextNode)_children[index - 1]).AppendData(c);
            else if (index >= 0 && index < _children.Length && _children[index] is TextNode)
                return ((TextNode)_children[index]).InsertData(0, c);
            
            return InsertChild(index, new TextNode(c));
        }

        /// <summary>
        /// Finds the index of the given node.
        /// </summary>
        /// <param name="node">The node which needs to know its index.</param>
        /// <returns>The index of the node or -1 if the node is not a child.</returns>
        internal int IndexOf(Node node)
        {
            var n = _children.Length;

            for (int i = 0; i < n; i++)
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
        [DOM("appendChild")]
        public virtual Node AppendChild(Node child)
        {
            if (child is DocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    AppendChild(childs[i]);
            }
            else if (child is Attr || child is Document || child.Contains(this))
            {
                throw new DOMException(ErrorCode.HierarchyRequestError);
            }
            else
            {
                if (child.ParentNode != null)
                    throw new DOMException(ErrorCode.InUse);

                child._parent = this;
                child.OwnerDocument = _owner ?? (this as Document);
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
        [DOM("insertChild")]
        public virtual Node InsertChild(Int32 index, Node child)
        {
            if (child is DocumentFragment)
            {
                var childs = child.ChildNodes;

                for (int i = 0; i < childs.Length; i++)
                    InsertChild(index + i, childs[i]);
            }
            else if (child is Attr || child is Document || child.Contains(this))
            {
                throw new DOMException(ErrorCode.HierarchyRequestError);
            }
            else
            {
                if (child.ParentNode != null)
                    throw new DOMException(ErrorCode.InUse);

                child._parent = this;
                child.OwnerDocument = _owner ?? (this as Document);
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
        [DOM("insertBefore")]
        public virtual Node InsertBefore(Node newElement, Node referenceElement)
        {
            if (newElement is Attr || newElement is Document || newElement.Contains(this))
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
        [DOM("replaceChild")]
        public virtual Node ReplaceChild(Node newChild, Node oldChild)
        {
            if (newChild is Attr || newChild is Document || newChild.Contains(this))
                throw new DOMException(ErrorCode.HierarchyRequestError);
            else if (newChild == oldChild)
                return oldChild;
            else if (newChild.ParentNode != null)
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
        [DOM("removeChild")]
        public virtual Node RemoveChild(Node child)
        {
            if (_children.Contains(child))
            {
                child._parent = null;
                child.OwnerDocument = null;
                _children.Remove(child);
            }

            return child;
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public virtual Node CloneNode(Boolean deep = true)
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
        [DOM("compareDocumentPosition")]
        public virtual DocumentPosition CompareDocumentPosition(Node otherNode)
        {
            if (this == otherNode)
                return DocumentPosition.Same;

            if(this.OwnerDocument != otherNode.OwnerDocument)
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
        [DOM("contains")]
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
        [DOM("normalize")]
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
                            text.AppendData(t.Data);
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
        [DOM("lookupNamespaceURI")]
        public virtual String LookupNamespaceURI(String prefix)
        {
            if (_parent != null)
                _parent.LookupNamespaceURI(prefix);

            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        [DOM("lookupPrefix")]
        public virtual String LookupPrefix(String namespaceURI)
        {
            if(_parent != null)
                return _parent.LookupPrefix(namespaceURI); 

            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to look for.</param>
        /// <param name="originalElement">The element that originated the request.</param>
        /// <returns>The namespace prefix.</returns>
        [DOM("lookupNamespacePrefix")]
        protected String LookupNamespacePrefix(String namespaceURI, Element originalElement)
        {
            if (!String.IsNullOrEmpty(_ns) && !String.IsNullOrEmpty(Prefix) && _ns == namespaceURI && originalElement.LookupNamespaceURI(Prefix) == namespaceURI)
                return Prefix;
            
            if(_attributes.Length > 0)
            {
                for (int i = 0; i < _attributes.Length; i++)
                {
                    if (_attributes[i].Prefix == "xmlns" && _attributes[i].NodeValue == namespaceURI && originalElement.LookupNamespaceURI(_attributes[i].LocalName) == namespaceURI)
                        return _attributes[i].LocalName;
                }
            }

            if (_parent != null)
                return _parent.LookupNamespacePrefix(namespaceURI, originalElement); 

            return null; 
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        [DOM("isDefaultNamespace")]
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
        [DOM("isEqualNode")]
        public virtual Boolean IsEqualNode(Node otherNode)
        {
            if (this._baseURI != otherNode._baseURI)
                return false;

            if (this._name != otherNode._name)
                return false;

            if (this._ns != otherNode._ns)
                return false;

            if (this._attributes.Length != otherNode._attributes.Length)
                return false;

            if (this._children.Length != otherNode._children.Length)
                return false;

            for (int i = 0; i < this._attributes.Length; i++)
            {
                if(!this._attributes[i].IsEqualNode(otherNode._attributes[i]))
                    return false;
            }

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
            target._ns = source._ns;

            if (deep)
            {
                for (int i = 0; i < source._children.Length; i++)
                    target._children.Add(source._children[i].CloneNode(true));

                for (int i = 0; i < source._attributes.Length; i++)
                    target._attributes.SetNamedItem(source._attributes[i].CloneNode(true) as Attr);
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
            return string.Format("DOM.{0}.{1}", NodeType, _name);
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
            var sb = new StringBuilder();
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

            return sb.ToString();
        }

        #endregion
    }
}
