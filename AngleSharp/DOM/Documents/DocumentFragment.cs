using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a document fragment.
    /// </summary>
    [DOM("DocumentFragment")]
    public sealed class DocumentFragment : Node, IDocumentFragment
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

        #endregion

        #region Methods

        /// <summary>
        /// Prepends nodes to the current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        /// <returns>The current fragment.</returns>
        [DOM("prepend")]
        public DocumentFragment Prepend(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }

            return this;
        }

        /// <summary>
        /// Appends nodes to current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        /// <returns>The current fragment.</returns>
        [DOM("append")]
        public DocumentFragment Append(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }

            return this;
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        [DOM("querySelector")]
        public Element QuerySelector(String selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors"></param>
        /// <returns></returns>
        [DOM("querySelectorAll")]
        public HTMLCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        [DOM("getElementsByClassName")]
        public HTMLCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        [DOM("getElementsByTagName")]
        public HTMLCollection GetElementsByTagName(String tagName)
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
        [DOM("getElementsByTagNameNS")]
        public HTMLCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
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
