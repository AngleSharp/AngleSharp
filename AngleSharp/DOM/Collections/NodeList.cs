namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of Node instances or nodes.
    /// </summary>
    public sealed class NodeList : IHtmlObject, INodeList, IEnumerable<Node>
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
        /// <returns>The modified collection.</returns>
        internal void Add(Node node)
        {
            _entries.Add(node);
        }

        /// <summary>
        /// Inserts a node at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the node should be inserted.</param>
        /// <param name="node">The node to add.</param>
        /// <returns>The modified collection.</returns>
        internal void Insert(Int32 index, Node node)
        {
            _entries.Insert(index, node);
        }

        /// <summary>
        /// Removes the specified node from the list.
        /// </summary>
        /// <param name="node">The node to remove.</param>
        /// <returns>The modified collection.</returns>
        internal void Remove(Node node)
        {
            _entries.Remove(node);
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
        public IEnumerator<Node> GetEnumerator()
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

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the nodelist.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public String ToHtml()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var entry in _entries)
                sb.Append(entry.ToHtml());

            return sb.ToPool();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        internal IElement QuerySelector(String selectors)
        {
            var sg = CssParser.ParseSelector(selectors);
            return this.QuerySelector(sg);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        internal HTMLCollection QuerySelectorAll(String selectors)
        {
            var sg = CssParser.ParseSelector(selectors);
            var result = new List<IElement>();
            this.QuerySelectorAll(sg, result);
            return new HTMLCollection(result);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the selector.
        /// </summary>
        /// <param name="selector">A selector object.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        internal HTMLCollection QuerySelectorAll(ISelector selector)
        {
            var result = new List<IElement>();
            this.QuerySelectorAll(selector, result);
            return new HTMLCollection(result);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        internal HTMLCollection GetElementsByClassName(String classNames)
        {
            var result = new List<IElement>();
            var names = classNames.SplitSpaces();

            if (names.Length > 0)
                this.GetElementsByClassName(names, result);

            return new HTMLCollection(result);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        internal HTMLCollection GetElementsByTagName(String tagName)
        {
            var result = new List<IElement>();
            this.GetElementsByTagName(tagName != "*" ? tagName : null, result);
            return new HTMLCollection(result);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceUri">The namespace URI of elements to look for.</param>
        /// <param name="localName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        internal HTMLCollection GetElementsByTagNameNS(String namespaceUri, String localName)
        {
            var result = new List<IElement>();
            this.GetElementsByTagNameNS(namespaceUri, localName != "*" ? localName : null, result);
            return new HTMLCollection(result);
        }

        #endregion

        #region INodeList

        INode INodeList.this[Int32 index]
        {
            get { return this[index]; }
        }

        Int32 INodeList.Length
        {
            get { return Length; }
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
        {
            var enumerator = GetEnumerator();

            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        #endregion
    }
}
