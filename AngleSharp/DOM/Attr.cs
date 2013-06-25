using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    [DOM("Attr")]
    public sealed class Attr : Node, IAttr
    {
        #region Members

        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new NodeAttribute with empty name and value.
        /// </summary>
        internal Attr()
            : this(String.Empty, String.Empty, null)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute with empty value.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        internal Attr(String name)
            : this(name, String.Empty, null)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        internal Attr(String name, String value)
            : this(name, value, null)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="ns">The namespace of the attribute.</param>
        internal Attr(String name, String value, String ns)
        {
            _type = NodeType.Attribute;
            _name = name;
            _value = value;
            _ns = ns;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        [DOM("nodeValue")]
        public override String NodeValue
        {
            get { return _value; }
            set { _value = value; RaiseChanged(); }
        }

        /// <summary>
        /// Gets whether the attribute is an ID attribute.
        /// </summary>
        public Boolean IsId
        {
            get { return _name.Equals("id", StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Gets if this attribute was explicitly given a value in the document.
        /// </summary>
        [DOM("specified")]
        public Boolean Specified
        {
            get { return !string.IsNullOrEmpty(_value); }
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return _name; }
            internal set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        [DOM("value")]
        public String Value
        {
            get { return _value; }
            set { _value = value; RaiseChanged(); }
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
            throw new DOMException(ErrorCode.HierarchyRequestError);
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        [DOM("insertChild")]
        public override Node InsertChild(Int32 index, Node child)
        {
            throw new DOMException(ErrorCode.HierarchyRequestError);
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
            throw new DOMException(ErrorCode.HierarchyRequestError);
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
            throw new DOMException(ErrorCode.HierarchyRequestError);
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        [DOM("removeChild")]
        public override Node RemoveChild(Node child)
        {
            throw new DOMException(ErrorCode.HierarchyRequestError);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = new Attr();
            CopyProperties(this, node, deep);
            node._value = _value;
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
            if (_parent != null)
                _parent.LookupNamespaceURI(prefix);

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
            if (_parent != null)
                _parent.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Tests whether two attributes are equal.
        /// </summary>
        /// <param name="otherNode">The node to compare equality with.</param>
        /// <returns>True if they are equal, otherwise false.</returns>
        [DOM("isEqualNode")]
        public override Boolean IsEqualNode(Node otherNode)
        {
            if (otherNode is Attr)
            {
                var a = this;
                var b = (Attr)otherNode;

                if (a._name == b._name && a._ns == b._ns && a._value == b._value)
                    return true;
            }

            return false;
        }

        #endregion

        #region Helpers

        void RaiseChanged()
        {
            if (_parent != null)
                _parent.OnAttributeChanged(_name);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the attribute.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            if (_value.Contains(Specification.DQ))
                _value = _value.Replace(Specification.DQ.ToString(), "&quot;");

            return string.Format("{0}={2}{1}{2}", _name, _value, Specification.DQ);
        }

        #endregion
    }
}
