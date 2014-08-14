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
        /// Checks if the node is an descendent of the given parent.
        /// </summary>
        /// <param name="node">The descendent node to use.</param>
        /// <param name="parent">The possible parent to use.</param>
        /// <returns>True if the given parent is actually an ancestor of the provided node.</returns>
        public static Boolean IsDescendentOf(this INode node, INode parent)
        {
            if (node.Parent == null)
                return false;
            else if (Object.ReferenceEquals(node.Parent, parent))
                return true;

            return node.Parent.IsDescendentOf(parent);
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
        /// Checks if the current node is a sibling of the specified element.
        /// </summary>
        /// <param name="node">The maybe sibling.</param>
        /// <param name="element">The node to check for having the same parent.</param>
        /// <returns>True if the parent is actually non-null and actually the same.</returns>
        public static Boolean IsSiblingOf(this INode node, INode element)
        {
            return node.Parent != null && node.Parent == element.Parent;
        }

        /// <summary>
        /// Finds the index of the given node of the provided parent node.
        /// </summary>
        /// <param name="parent">The parent of the given node.</param>
        /// <param name="node">The node which needs to know its index.</param>
        /// <returns>The index of the node or -1 if the node is not a child of the parent.</returns>
        public static Int32 IndexOf(this INode parent, INode node)
        {
            var i = 0;

            foreach (var child in parent.ChildNodes)
            {
                if (Object.ReferenceEquals(child, node))
                    return i;

                i++;
            }

            return -1;
        }

        /// <summary>
        /// Checks if the context node is before the provided node.
        /// </summary>
        /// <param name="before">The context node.</param>
        /// <param name="after">The provided reference node.</param>
        /// <returns>True if the context node is preceeding the reference node in tree order.</returns>
        public static Boolean IsPreceeding(this INode before, INode after)
        {
            var parent = before.Parent;

            if (parent == null)
                return false;
            else if (parent == after)
                return true;
            else if (parent == after.Parent)
                return parent.IndexOf(before) < parent.IndexOf(after);

            return parent.IsPreceeding(after);
        }

        /// <summary>
        /// Checks if the context node is after the provided node.
        /// </summary>
        /// <param name="after">The context node.</param>
        /// <param name="before">The provided reference node.</param>
        /// <returns>True if the context node is following the reference node in tree order.</returns>
        public static Boolean IsFollowing(this INode after, INode before)
        {
            return before.IsPreceeding(after);
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
                return parent.IsInclusiveAncestorOf(host);

            return false;
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
            else if (child != null && child.Parent != parent)
                throw new DomException(ErrorCode.NotFound);
            else if (node is IElement == false && node is ICharacterData == false && node is IDocumentType == false && node is IDocumentFragment == false)
                throw new DomException(ErrorCode.HierarchyRequest);

            var document = parent as IDocument;

            if (document != null)
            {
                if (node is IElement)
                {
                    if (document.DocumentElement != null || child is IDocumentType || child.IsFollowedByDoctype())
                        throw new DomException(ErrorCode.HierarchyRequest);
                }
                else if (node is IDocumentFragment)
                {
                    var elementChild = node.ChildNodes.OfType<IElement>().Count();

                    if (elementChild > 1 || node.ChildNodes.OfType<IText>().Any())
                        throw new DomException(ErrorCode.HierarchyRequest);
                    else if ((elementChild == 1 && parent.HasElements()) || child is IDocumentType || child.IsFollowedByDoctype())
                        throw new DomException(ErrorCode.HierarchyRequest);
                }
                else if (node is IDocumentType)
                {
                    if (document.Doctype != null || (child != null && document.IndexOf(child) > 0) || (child == null && parent.HasElements()))
                        throw new DomException(ErrorCode.HierarchyRequest);
                }
                else if (node is IText)
                    throw new DomException(ErrorCode.HierarchyRequest);
            }
            else if (node is IDocumentType)
                throw new DomException(ErrorCode.HierarchyRequest);
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

            parent.Owner.AdoptNode(node);
            parent.InsertBefore(node, child);
            return node;
        }

        /// <summary>
        /// Adopts the given node for the provided document context.
        /// </summary>
        /// <param name="document">The new owner of the node.</param>
        /// <param name="node">The node to change its owner.</param>
        public static void AdoptNode(this IDocument document, INode node)
        {
            var adopted = node as Node;

            if (adopted == null)
                return;

            var oldDocument = adopted.Owner;

            if (node.Parent != null)
                node.Parent.RemoveChild(node);

            adopted.Owner = document as Document;
            //Run any adopting steps defined for node in other applicable specifications and pass node and oldDocument as parameters.
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

        static Boolean IsFollowedByDoctype(this INode child)
        {
            if (child == null)
                return false;

            var before = true;

            foreach (var node in child.Parent.ChildNodes)
            {
                if (before)
                    before = node != child;
                else if (node is IDocumentType)
                    return true;
            }

            return false;
        }

        static Boolean HasElements(this INode parent)
        {
            foreach (var node in parent.ChildNodes)
                if (node is IElement)
                    return true;

            return false;
        }
    }
}
