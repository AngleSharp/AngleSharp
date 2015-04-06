namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Extensions for performing QuerySelector operations.
    /// </summary>
    [DebuggerStepThrough]
    static class QueryExtensions
    {
        #region Text Selector

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public static IElement QuerySelector(this INodeList elements, String selectors)
        {
            var sg = CssParser.ParseSelector(selectors);

            if (sg == null)
                throw new DomException(DomError.Syntax);

            return elements.QuerySelector(sg);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static HtmlElementCollection QuerySelectorAll(this INodeList elements, String selectors)
        {
            var sg = CssParser.ParseSelector(selectors);

            if (sg == null)
                throw new DomException(DomError.Syntax);

            var result = new List<IElement>();
            elements.QuerySelectorAll(sg, result);
            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the selector.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selector">A selector object.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static HtmlElementCollection QuerySelectorAll(this INodeList elements, ISelector selector)
        {
            var result = new List<IElement>();
            elements.QuerySelectorAll(selector, result);
            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        public static HtmlElementCollection GetElementsByClassName(this INodeList elements, String classNames)
        {
            var result = new List<IElement>();
            var names = classNames.SplitSpaces();

            if (names.Length > 0)
                elements.GetElementsByClassName(names, result);

            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public static HtmlElementCollection GetElementsByTagName(this INodeList elements, String tagName)
        {
            var result = new List<IElement>();
            elements.GetElementsByTagName(tagName != "*" ? tagName : null, result);
            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="namespaceUri">The namespace URI of elements to look for.</param>
        /// <param name="localName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public static HtmlElementCollection GetElementsByTagName(this INodeList elements, String namespaceUri, String localName)
        {
            var result = new List<IElement>();
            elements.GetElementsByTagName(namespaceUri, localName != "*" ? localName : null, result);
            return new HtmlElementCollection(result);
        }

        #endregion

        #region Object Selector

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the given selector.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selectors">A selector object.</param>
        /// <returns>An element object.</returns>
        public static T QuerySelector<T>(this INodeList elements, ISelector selectors)
            where T : class, IElement
        {
            return elements.QuerySelector(selectors) as T;
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selector">A selector object.</param>
        /// <returns>An element object.</returns>
        public static IElement QuerySelector(this INodeList elements, ISelector selector)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as IElement;

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

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="selector">A selector object.</param>
        /// <param name="result">A reference to the list where to store the results.</param>
        public static void QuerySelectorAll(this INodeList elements, ISelector selector, List<IElement> result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as IElement;

                if (element != null)
                {
                    if (selector.Match(element))
                        result.Add(element);

                    if (element.HasChildNodes)
                        QuerySelectorAll(element.ChildNodes, selector, result);
                }
            }
        }

        /// <summary>
        /// Returns true if the underlying string contains all of the tokens, otherwise false.
        /// </summary>
        /// <param name="list">The list that is considered.</param>
        /// <param name="tokens">The tokens to consider.</param>
        /// <returns>True if the string contained all tokens, otherwise false.</returns>
        public static Boolean Contains(this ITokenList list, String[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (!list.Contains(tokens[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="classNames">An array with class names to consider.</param>
        /// <param name="result">A reference to the list where to store the results.</param>
        public static void GetElementsByClassName(this INodeList elements, String[] classNames, List<IElement> result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as IElement;

                if (element != null)
                {
                    if (element.ClassList.Contains(classNames))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByClassName(element.ChildNodes, classNames, result);
                }
            }
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <param name="result">A reference to the list where to store the results.</param>
        public static void GetElementsByTagName(this INodeList elements, String tagName, List<IElement> result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as IElement;

                if (element != null)
                {
                    if (tagName == null || element.LocalName.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByTagName(element.ChildNodes, tagName, result);
                }
            }
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="elements">The elements to take as source.</param>
        /// <param name="namespaceUri">The namespace URI of elements to look for.</param>
        /// <param name="localName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <param name="result">A reference to the list where to store the results.</param>
        public static void GetElementsByTagName(this INodeList elements, String namespaceUri, String localName, List<IElement> result)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i] as IElement;

                if (element != null)
                {
                    if (element.NamespaceUri == namespaceUri && (localName == null || element.LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase)))
                        result.Add(element);

                    if (element.ChildElementCount != 0)
                        GetElementsByTagName(element.ChildNodes, namespaceUri, localName, result);
                }
            }
        }

        #endregion
    }
}
