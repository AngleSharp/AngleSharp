using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// The abstract template class for most DOM collections.
    /// </summary>
    /// <typeparam name="T">The type of elements to contain.</typeparam>
    public abstract class BaseCollection<T> : IHTMLObject, IEnumerable<T> where T : Node
    {
        #region Members

        /// <summary>
        /// The contained entries.
        /// </summary>
        protected List<T> _entries;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new list of nodes.
        /// </summary>
        internal BaseCollection()
        {
            _entries = new List<T>();
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets or sets a node within the list of nodes.
        /// </summary>
        /// <param name="index">The 0-based index of the node.</param>
        /// <returns>The node at the specified index.</returns>
        [DOM("item")]
        public T this[Int32 index]
        {
            get { return index >= 0 && index < _entries.Count ? _entries[index] : null; }
            set { _entries[index] = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of nodes in the list.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _entries.Count; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Clears the list of nodes.
        /// </summary>
        internal protected void Clear()
        {
            _entries.Clear();
        }

        /// <summary>
        /// Adds a node to the list of nodes.
        /// </summary>
        /// <param name="node">The node to add.</param>
        /// <returns>The modified collection.</returns>
        internal protected void Add(T node)
        {
            _entries.Add(node);
        }

        /// <summary>
        /// Inserts a node at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the node should be inserted.</param>
        /// <param name="node">The node to add.</param>
        /// <returns>The modified collection.</returns>
        internal protected void Insert(int index, T node)
        {
            _entries.Insert(index, node);
        }

        /// <summary>
        /// Removes the specified node from the list.
        /// </summary>
        /// <param name="node">The node to remove.</param>
        /// <returns>The modified collection.</returns>
        internal protected void Remove(T node)
        {
            if (_entries.Contains(node))
            {
                _entries.Remove(node);
            }
        }

        /// <summary>
        /// Removes the specified node from the list.
        /// </summary>
        /// <param name="index">The 0-based index of the node to remove.</param>
        /// <returns>The modified collection.</returns>
        internal protected virtual void RemoveAt(int index)
        {
            var entry = _entries[index];
            _entries.RemoveAt(index);
        }

        /// <summary>
        /// Looks for the specified node in the list.
        /// </summary>
        /// <param name="node">The node to look for.</param>
        /// <returns>True if such a node exists, otherwise false.</returns>
        internal bool Contains(T node)
        {
            return IndexOf(node) != -1;
        }

        /// <summary>
        /// Returns the index of node in the list.
        /// </summary>
        /// <param name="node">The node to look for.</param>
        /// <returns>The index [0, Count - 1] of the element if its exists, otherwise -1.</returns>
        internal int IndexOf(T node)
        {
            for (var i = 0; i < _entries.Count; i++)
                if (_entries[i] == node)
                    return i;

            return -1;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An IEnumerator for NodeList.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the list.
        /// </summary>
        /// <returns>An IEnumerator for NodeList.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_entries).GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the nodelist.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public virtual String ToHtml()
        {
            var sb = new StringBuilder();

            foreach (var entry in _entries)
                sb.AppendLine(entry.ToHtml());

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string representing the collection.
        /// </summary>
        /// <returns>A string describing the collection.</returns>
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append("DOM.").Append(GetType().Name).Append(':');

            if (Length == 0)
                sb.AppendLine().Append("\t--No elements available--");

            foreach (var element in this)
                sb.AppendLine().Append("\t").Append(element.ToString());

            return sb.ToString();
        }

        #endregion
    }
}
