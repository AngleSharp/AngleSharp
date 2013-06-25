using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a named collection of nodes.
    /// </summary>
    [DOM("NamedNodeMap")]
    public sealed class NamedNodeMap : IHTMLObject, IEnumerable<Attr>
    {
        #region Members

        List<Attr> _entries;
        Node _parent;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new collection of nodes.
        /// </summary>
        /// <param name="parent">The parent of the NamedNodeMap.</param>
        internal NamedNodeMap(Node parent)
        {
            _parent = parent;
            _entries = new List<Attr>();
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The item or null if the index is higher or equal to the number of nodes.</returns>
        [DOM("item")]
        public Attr this[Int32 index]
        {
            get
            {
                if (index >= _entries.Count || index < 0)
                    return null;

                return _entries[index];
            }
        }

        /// <summary>
        /// Gets the value of an attribute within the collection of attributes.
        /// </summary>
        /// <param name="name">The case-insensitive name of the attribute.</param>
        /// <returns>The value of the NodeAttribute or null if it does not exist.</returns>
        public Attr this[String name]
        {
            get
            {
                if (name != null)
                {
                    for (var i = 0; i < _entries.Count; i++)
                    {
                        if (_entries[i].NodeName.Equals(name, StringComparison.OrdinalIgnoreCase))
                            return _entries[i];
                    }
                }

                return null;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of defined attributes.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _entries.Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a node by name.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <returns>The node or null if nothing found.</returns>
        [DOM("getNamedItem")]
        public Attr GetNamedItem(String nodeName)
        {
            return this[nodeName];
        }

        /// <summary>
        /// Adds (or replaces) a node by its nodeName.
        /// </summary>
        /// <param name="node">The node to be added or inserted.</param>
        /// <returns>The replaced node, if any.</returns>
        [DOM("setNamedItem")]
        public Attr SetNamedItem(Attr node)
        {
            if (node != null)
            {
                if (node.OwnerDocument != _parent.OwnerDocument)
                    node.OwnerDocument = _parent.OwnerDocument;

                if (node.ParentNode != null && node.ParentNode != _parent)
                    throw new DOMException(ErrorCode.InUse);

                node.ParentNode = _parent;

                for (var i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i].NodeName.Equals(node.NodeName, StringComparison.OrdinalIgnoreCase))
                    {
                        var entry = _entries[i];
                        _entries[i] = node;
                        RaiseChanged(node.Name);
                        return entry;
                    }
                }

                _entries.Add(node);
                RaiseChanged(node.Name);
            }

            return null;
        }

        /// <summary>
        /// Removes a node (or if an attribute, may reveal a default if present).
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <returns>The removed node or null if nothing found.</returns>
        [DOM("removeNamedItem")]
        public Attr RemoveNamedItem(String nodeName)
        {
            if (nodeName != null)
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i].NodeName.Equals(nodeName, StringComparison.OrdinalIgnoreCase))
                    {
                        var entry = _entries[i];
                        _entries.RemoveAt(i);
                        RaiseChanged(entry.Name);
                        return entry;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a node by name.
        /// </summary>
        /// <param name="namespaceURI">The namespace of the node.</param>
        /// <param name="localName">The name of the node.</param>
        /// <returns>The node or null if nothing found.</returns>
        [DOM("getNamedItemNS")]
        public Attr GetNamedItemNS(String namespaceURI, String localName)
        {
            if (namespaceURI != null && localName != null)
            {
                for (var i = 0; i < _entries.Count; i++)
                    if (_entries[i].NamespaceURI.Equals(namespaceURI, StringComparison.OrdinalIgnoreCase) && _entries[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase))
                        return _entries[i];
            }

            return null;
        }

        /// <summary>
        /// Adds (or replaces) a node by its localName and namespaceURI.
        /// </summary>
        /// <param name="node">The node to be added or inserted.</param>
        /// <returns>The added node.</returns>
        [DOM("setNamedItemNS")]
        public Attr SetNamedItemNS(Attr node)
        {
            if (node != null)
            {
                if (node.OwnerDocument != _parent.OwnerDocument)
                    node.OwnerDocument = _parent.OwnerDocument;

                if (node.ParentNode != null && node.ParentNode != _parent)
                    throw new DOMException(ErrorCode.InUse);

                node.ParentNode = _parent;

                for (var i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i].NamespaceURI.Equals(node.NamespaceURI, StringComparison.OrdinalIgnoreCase) && _entries[i].LocalName.Equals(node.LocalName, StringComparison.OrdinalIgnoreCase))
                    {
                        _entries[i] = node;
                        RaiseChanged(node.Name);
                        return _entries[i];
                    }
                }

                _entries.Add(node);
                RaiseChanged(node.Name);
            }

            return node;
        }

        /// <summary>
        /// Removes a node (or if an attribute, may reveal a default if present).
        /// </summary>
        /// <param name="namespaceURI">The namespace of the node.</param>
        /// <param name="localName">The name of the node.</param>
        /// <returns>The removed node or null if nothing found.</returns>
        [DOM("removeNamedItemNS")]
        public Attr RemoveNamedItemNS(String namespaceURI, String localName)
        {
            if (namespaceURI != null && localName != null)
            {
                for (var i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i].NamespaceURI.Equals(namespaceURI, StringComparison.OrdinalIgnoreCase) && _entries[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase))
                    {
                        var entry = _entries[i];
                        _entries.RemoveAt(i);
                        RaiseChanged(entry.Name);
                        return entry;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Operator

        /// <summary>
        /// Compares another object to this.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is NamedNodeMap)
            {
                var a = this;
                var b = (NamedNodeMap)obj;

                if (a.Length == b.Length)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        var elA = a[i];
                        var elB = b[elA.NodeName];

                        if (elB == null)
                            return false;

                        if (!elA.IsEqualNode(elB))
                            return false;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the base hashcode for this object.
        /// </summary>
        /// <returns>The hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return Length;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator for NodeAttributeCollection.</returns>
        public IEnumerator<Attr> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator for NodeAttributeCollection</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helpers

        void RaiseChanged(String name)
        {
            if (_parent != null)
                _parent.OnAttributeChanged(name);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the named nodemap.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public String ToHtml()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _entries.Count; i++)
            {
                sb.Append(' ');
                sb.Append(_entries[i].ToHtml());
            }

            return sb.ToString();
        }

        #endregion
    }
}
