namespace AngleSharp.Dom.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents a list of Node instances or nodes.
    /// </summary>
    sealed class NodeList : INodeList
    {
        #region Fields

        readonly List<Node> _entries;

        /// <summary>
        /// Gets an empty node-list. Shouldn't be modified.
        /// </summary>
        internal static readonly NodeList Empty = new NodeList();

        #endregion

        #region ctor

        internal NodeList()
        {
            _entries = new List<Node>();
        }

        #endregion

        #region Index

        public Node this[Int32 index]
        {
            get { return _entries[index]; }
            set { _entries[index] = value; }
        }

        INode INodeList.this[Int32 index]
        {
            get { return this[index]; }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _entries.Count; }
        }

        #endregion

        #region Internal Methods

        internal void Add(Node node)
        {
            _entries.Add(node);
        }

        internal void AddRange(NodeList nodeList)
        {
            _entries.AddRange(nodeList._entries);
        }

        internal void Insert(Int32 index, Node node)
        {
            _entries.Insert(index, node);
        }

        internal void Remove(Node node)
        {
            _entries.Remove(node);
        }

        internal void RemoveAt(Int32 index)
        {
            _entries.RemoveAt(index);
        }

        internal Boolean Contains(Node node)
        {
            return _entries.Contains(node);
        }

        #endregion

        #region Methods

        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            for (var i = 0; i < _entries.Count; i++)
            {
                _entries[i].ToHtml(writer, formatter);
            }
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<INode> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        #endregion
    }
}
