namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Useful methods for node objects.
    /// </summary>
    static class NodeExtensions
    {
        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableElement(this INode node)
        {
            return (node is IHtmlTableElement || node is IHtmlTableSectionElement || node is IHtmlTableRowElement);
        }

        /// <summary>
        /// Gets a list of HTML elements given by their name attribute.
        /// </summary>
        /// <param name="children">The list to investigate.</param>
        /// <param name="name">The name attribute's value.</param>
        /// <param name="result">The result collection.</param>
        [DebuggerStepThrough]
        public static void GetElementsByName(this NodeList children, String name, List<Element> result)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] is HTMLElement)
                {
                    var element = (HTMLElement)children[i];

                    if (element.GetAttribute(AttributeNames.Name) == name)
                        result.Add(element);

                    element.ChildNodes.GetElementsByName(name, result);
                }
            }
        }

        /// <summary>
        /// Gets the root of the given node, which is the node itself, if it has
        /// no parent, or the root of the parent.
        /// </summary>
        /// <param name="node">The node to get the root of.</param>
        /// <returns>The root node.</returns>
        [DebuggerStepThrough]
        public static INode GetRoot(this INode node)
        {
            if (node.Parent == null)
                return node;

            return node.Parent.GetRoot();
        }

        /// <summary>
        /// Checks if the parent is an ancestor of the given node.
        /// </summary>
        /// <param name="parent">The possible parent to use.</param>
        /// <param name="node">The node to check for being descendent.</param>
        /// <returns>True if the given parent is actually an ancestor of the provided node.</returns>
        [DebuggerStepThrough]
        public static Boolean IsAncestorOf(this INode parent, INode node)
        {
            return node.IsDescendentOf(parent);
        }

        /// <summary>
        /// Checks if the parent is an inclusive ancestor of the given node.
        /// </summary>
        /// <param name="parent">The possible parent to use.</param>
        /// <param name="node">The node to check for being descendent.</param>
        /// <returns>True if the given parent is actually an inclusive ancestor of the provided node.</returns>
        [DebuggerStepThrough]
        public static Boolean IsInclusiveAncestorOf(this INode parent, INode node)
        {
            return node == parent || node.IsDescendentOf(parent);
        }

        public static INode GetAssociatedHost(this INode node)
        {
            if (node is IDocumentFragment && node.Owner != null)
                return node.Owner.All.OfType<IHtmlTemplateElement>().FirstOrDefault(m => m.Content == node);

            return null;
        }

        [DebuggerStepThrough]
        public static Boolean IsHostIncludingInclusiveAncestor(this INode parent, INode node)
        {
            if (parent.IsInclusiveAncestorOf(node))
                return true;
            
            var host = node.GetRoot().GetAssociatedHost();
            
            if (host != null)
                return parent.IsHostIncludingInclusiveAncestor(host);

            return false;
        }

        /// <summary>
        /// Checks if the node is an descendent of the given parent.
        /// </summary>
        /// <param name="node">The descendent node to use.</param>
        /// <param name="parent">The possible parent to use.</param>
        /// <returns>True if the given parent is actually an ancestor of the provided node.</returns>
        [DebuggerStepThrough]
        public static Boolean IsDescendentOf(this INode node, INode parent)
        {
            if (parent.ChildNodes.Index(node) != -1)
                return true;

            foreach (var child in parent.ChildNodes)
            {
                if (node.IsDescendentOf(child))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the node is an inclusive descendent of the given parent.
        /// </summary>
        /// <param name="node">The descendent node to use.</param>
        /// <param name="parent">The possible parent to use.</param>
        /// <returns>True if the given parent is actually an inclusive ancestor of the provided node.</returns>
        [DebuggerStepThrough]
        public static Boolean IsInclusiveDescendentOf(this INode node, INode parent)
        {
            return node == parent || node.IsDescendentOf(parent);
        }

        [DebuggerStepThrough]
        public static INode PreInsert(this INode parent, INode node, INode child)
        {
            parent.EnsurePreInsertionValidity(node, child);
            var referenceChild = child;

            if (referenceChild == node)
                referenceChild = node.NextSibling;

            parent.Owner.Adopt(node);
            parent.InsertBefore(node, child);
            return node;
        }

        [DebuggerStepThrough]
        public static void EnsurePreInsertionValidity(this INode parent, INode node, INode child)
        {
            if ((parent is IDocument == false && parent is IDocumentFragment == false && parent is IElement == false) || node.IsHostIncludingInclusiveAncestor(parent))
                throw new DomException(ErrorCode.HierarchyRequest);

            if (child != null && child.Parent != parent)
                throw new DomException(ErrorCode.NotFoundError);

            if (node is IDocumentType == false && node is IDocumentFragment == false && node is IElement == false && node is ICharacterData == false)
                throw new DomException(ErrorCode.HierarchyRequest);

            if (node is IText && parent is IDocument)
                throw new DomException(ErrorCode.HierarchyRequest);

            if (node is IDocumentFragment)
            {
                var elementChild = node.ChildNodes.OfType<IElement>().Count();

                if (elementChild > 1 || node.ChildNodes.OfType<IText>().Any())
                    throw new DomException(ErrorCode.HierarchyRequest);

                if ((elementChild == 1 && parent.ChildNodes.OfType<IElement>().Any()) ||
                    child is IDocumentType ||
                    (child != null && (parent.ChildNodes.OfType<IDocumentType>().SkipWhile(m => m != child).Any())))
                    throw new DomException(ErrorCode.HierarchyRequest);
            }

            if (node is IElement)
            {
                if (parent.ChildNodes.OfType<IElement>().Any() || child is IDocumentType ||
                    (child != null && parent.ChildNodes.OfType<IDocumentType>().SkipWhile(m => m != child).Any()))
                    throw new DomException(ErrorCode.HierarchyRequest);
            }

            if (node is IDocumentType)
            {
                if (parent.ChildNodes.OfType<IDocumentType>().Any() ||
                    parent.ChildNodes.OfType<IElement>().TakeWhile(m => m == child).Any() ||
                    (child != null && parent.ChildNodes.OfType<IElement>().Any()))
                    throw new DomException(ErrorCode.HierarchyRequest);
            }
        }

        /// <summary>
        /// Tries to print the HTML representation of the Object, if any.
        /// Otherwise the an empty string is returned.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The HTML string representation.</returns>
        [DebuggerStepThrough]
        public static String ToHtml(this Object obj)
        {
            var html = obj as IHtmlObject;

            if (html == null)
                return String.Empty;

            return html.ToHtml();
        }

        /// <summary>
        /// Tries to print the CSS representation of the Object, if any.
        /// Otherwise the an empty string is returned.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The CSS string representation.</returns>
        [DebuggerStepThrough]
        public static String ToCss(this Object obj)
        {
            var css = obj as ICssObject;

            if (css == null)
                return String.Empty;

            return css.ToCss();
        }
    }
}
