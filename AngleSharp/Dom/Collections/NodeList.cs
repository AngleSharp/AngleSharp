namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of Node instances or nodes.
    /// </summary>
    sealed class NodeList : INodeList
    {
        #region Fields

        /// <summary>
        /// The contained entries.
        /// </summary>
        readonly List<Node> _entries;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of nodes.
        /// </summary>
        internal NodeList()
        {
            _entries = new List<Node>();
        }

        /// <summary>
        /// Creates a copy of the list of existing nodes.
        /// </summary>
        /// <param name="existing">The list to copy.</param>
        internal NodeList(NodeList existing)
        {
            _entries = new List<Node>(existing._entries);
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets or sets a node within the list of nodes.
        /// </summary>
        /// <param name="index">The 0-based index of the node.</param>
        /// <returns>The node at the specified index.</returns>
        public Node this[Int32 index]
        {
            get { return index >= 0 && index < _entries.Count ? _entries[index] : null; }
            set { _entries[index] = value; }
        }

        /// <summary>
        /// Gets a node within the list of nodes.
        /// </summary>
        /// <param name="index">The 0-based index of the node.</param>
        /// <returns>The node at the specified index.</returns>
        INode INodeList.this[Int32 index]
        {
            get { return this[index]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of nodes in the list.
        /// </summary>
        public Int32 Length
        {
            get { return _entries.Count; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Adds a node to the list of nodes.
        /// </summary>
        /// <param name="node">The node to add.</param>
        internal void Add(Node node)
        {
            _entries.Add(node);
        }

        /// <summary>
        /// Adds nodes from a list of nodes.
        /// </summary>
        /// <param name="nodeList">The nodes to add.</param>
        internal void AddRange(NodeList nodeList)
        {
            _entries.AddRange(nodeList._entries);
        }

        /// <summary>
        /// Inserts a node at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the node should be inserted.</param>
        /// <param name="node">The node to add.</param>
        internal void Insert(Int32 index, Node node)
        {
            _entries.Insert(index, node);
        }

        /// <summary>
        /// Removes the specified node from the list.
        /// </summary>
        /// <param name="node">The node to remove.</param>
        internal void Remove(Node node)
        {
            _entries.Remove(node);
        }

        /// <summary>
        /// Removes the node at the given index.
        /// </summary>
        /// <param name="index">The index to remove.</param>
        internal void RemoveAt(Int32 index)
        {
            _entries.RemoveAt(index);
        }

        /// <summary>
        /// Looks for the specified node in the list.
        /// </summary>
        /// <param name="node">The node to look for.</param>
        /// <returns>True if such a node exists, otherwise false.</returns>
        internal Boolean Contains(Node node)
        {
            return _entries.Contains(node);
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An IEnumerator for NodeList.</returns>
        public IEnumerator<INode> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An IEnumerator for NodeList.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        #endregion

        #region Methods
        
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
        public String ToHtml(IMarkupFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();

            for (int i = 0; i < _entries.Count; i++)
            {
                var htmlCode = _entries[i].ToHtml(formatter);
                sb.Append(htmlCode);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
