namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Useful methods for node objects.
    /// </summary>
    [DebuggerStepThrough]
    static class NodeExtensions
    {
        /// <summary>
        /// Gets the root of the given node, which is the node itself, if it has
        /// no parent, or the root of the parent.
        /// </summary>
        /// <param name="node">The node to get the root of.</param>
        /// <returns>The root node.</returns>
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
        public static Boolean IsInclusiveAncestorOf(this INode parent, INode node)
        {
            return node == parent || node.IsDescendentOf(parent);
        }

        /// <summary>
        /// Gets the associated host object, if any. This is mostly interesting for the HTML5 template tag.
        /// </summary>
        /// <param name="node">The node that probably has an host object</param>
        /// <returns>The host object or null.</returns>
        public static INode GetAssociatedHost(this INode node)
        {
            if (node is IDocumentFragment && node.Owner != null)
                return node.Owner.All.OfType<IHtmlTemplateElement>().FirstOrDefault(m => m.Content == node);

            return null;
        }

        /// <summary>
        /// Checks for an inclusive ancestor relationship or if the host (if any) has such a relationship.
        /// </summary>
        /// <param name="parent">The possible parent to use.</param>
        /// <param name="node">The node to check for being descendent.</param>
        /// <returns>True if the given parent is actually an inclusive ancestor (including the host) of the provided node.</returns>
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
        public static Boolean IsInclusiveDescendentOf(this INode node, INode parent)
        {
            return node == parent || node.IsDescendentOf(parent);
        }

        /// <summary>
        /// Pre-inserts the given node at the parent before the provided child.
        /// </summary>
        /// <param name="parent">The origin that will be mutated.</param>
        /// <param name="node">The node to be inserted.</param>
        /// <param name="child">The reference node of the insertation.</param>
        /// <returns>The inserted node, which is node.</returns>
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

        /// <summary>
        /// Ensures the validity for inserting the given node at parent before the
        /// provided child. Throws an error is the insertation is invalid.
        /// </summary>
        /// <param name="parent">The origin that will be mutated.</param>
        /// <param name="node">The node to be inserted.</param>
        /// <param name="child">The reference node of the insertation.</param>
        public static void EnsurePreInsertionValidity(this INode parent, INode node, INode child)
        {
            if ((parent is IDocument == false && parent is IDocumentFragment == false && parent is IElement == false) || node.IsHostIncludingInclusiveAncestor(parent))
                throw new DomException(ErrorCode.HierarchyRequest);

            if (child != null && child.Parent != parent)
                throw new DomException(ErrorCode.NotFound);

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
        public static String ToCss(this Object obj)
        {
            var css = obj as ICssObject;

            if (css == null)
                return String.Empty;

            return css.ToCss();
        }
    }
}
