using AngleSharp.Css;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of Node instances or nodes.
    /// </summary>
    [DOM("NodeList")]
    public sealed class NodeList : BaseCollection<Node>
    {
        #region ctor

        /// <summary>
        /// Creates a new list of nodes.
        /// </summary>
        internal NodeList()
        {
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        internal Element QuerySelector(String selectors)
        {
            var sg = CssParser.ParseSelector(selectors);
            return QuerySelector(this, sg);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the given selector.
        /// </summary>
        /// <param name="selectors">A selector object.</param>
        /// <returns>An element object.</returns>
        internal Element QuerySelector(Selector selectors)
        {
            return QuerySelector(this, selectors);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the given selector.
        /// </summary>
        /// <param name="selectors">A selector object.</param>
        /// <returns>An element object.</returns>
        internal T QuerySelector<T>(Selector selectors) where T : Element
        {
            return QuerySelector(this, selectors) as T;
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
            var result = new HTMLCollection();
            QuerySelectorAll(this, sg, result);
            return result;
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the selector.
        /// </summary>
        /// <param name="selector">A selector object.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        internal HTMLCollection QuerySelectorAll(Selector selector)
        {
            var result = new HTMLCollection();
            QuerySelectorAll(this, selector, result);
            return result;
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        internal HTMLCollection GetElementsByClassName(String classNames)
        {
            var result = new HTMLCollection();
            var names = classNames.SplitSpaces();

            if(names.Length > 0)
                GetElementsByClassName(this, names, result);

            return result;
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        internal HTMLCollection GetElementsByTagName(String tagName)
        {
            var result = new HTMLCollection();
            GetElementsByTagName(this, tagName, result);
            return result;
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of elements to look for.</param>
        /// <param name="localName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        internal HTMLCollection GetElementsByTagNameNS(String namespaceURI, String localName)
        {
            var result = new HTMLCollection();
            GetElementsByTagNameNS(this, namespaceURI, localName, result);
            return result;
        }

        #endregion

        #region Helpers

        static Element QuerySelector(NodeList elements, Selector selector)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as Element;

                if (element != null)
                {
                    if (selector.Match(element))
                        return element;

                    if (!element.HasChildNodes)
                        continue;

                    element = QuerySelector(element.ChildNodes, selector);

                    if (element != null)
                        return element;
                }
            }

            return null;
        }

        static void QuerySelectorAll(NodeList elements, Selector selector, HTMLCollection result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as Element;

                if (element != null)
                {
                    if (selector.Match(element))
                        result.Add(element);

                    if (element.HasChildNodes)
                        QuerySelectorAll(element.ChildNodes, selector, result);                    
                }
            }
        }

        static void GetElementsByClassName(NodeList elements, string[] classNames, HTMLCollection result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as HTMLElement;

                if (element != null)
                {
                    if (element.ClassList.Contains(classNames))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByClassName(element.ChildNodes, classNames, result);
                }
            }
        }

        static void GetElementsByTagName(NodeList elements, string tagName, HTMLCollection result)
        {
            var takeAll = tagName == "*";

            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as Element;

                if (element != null)
                {
                    if (takeAll || element.NodeName.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByTagName(element.ChildNodes, tagName, result);
                }
            }
        }

        static void GetElementsByTagNameNS(NodeList elements, string namespaceURI, string localName, HTMLCollection result)
        {
            var takeAll = localName == "*";

            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as Element;

                if (element != null)
                {
                    if (element.NamespaceURI == namespaceURI && (takeAll || element.LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase)))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByTagNameNS(element.ChildNodes, namespaceURI, localName, result);
                }
            }
        }

        #endregion
    }
}
