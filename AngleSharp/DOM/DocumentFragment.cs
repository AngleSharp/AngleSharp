namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a document fragment.
    /// </summary>
    sealed class DocumentFragment : Node, IDocumentFragment
    {
        #region ctor

        /// <summary>
        /// Creates a new document fragment.
        /// </summary>
        internal DocumentFragment()
        {
            _type = NodeType.DocumentFragment;
            _name = "#document-fragment";
        }

        /// <summary>
        /// Creates a new document fragment with the given nodelist as
        /// children.
        /// </summary>
        /// <param name="list">The list to contain.</param>
        internal DocumentFragment(NodeList list)
            : this()
        {
            _children = list;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public Int32 ChildElementCount
        {
            get { return _children.OfType<Element>().Count(); }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        public IHtmlCollection Children
        {
            get { return new HtmlElementCollection(_children.OfType<Element>()); }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        public IElement FirstElementChild
        {
            get
            {
                var n = _children.Length;

                for (int i = 0; i < n; i++)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        public IElement LastElementChild
        {
            get
            {
                for (int i = _children.Length - 1; i >= 0; i--)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
                }

                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepends nodes to the current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            if (Parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }
        }

        /// <summary>
        /// Appends nodes to current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            if (Parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public IElement QuerySelector(String selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public IHtmlCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagName(String tagName)
        {
            return _children.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of elements to look for.</param>
        /// <param name="tagName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the document.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            return ChildNodes.ToHtml();
        }

        #endregion
    }
}
