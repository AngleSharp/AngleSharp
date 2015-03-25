namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
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
        /// Gets the hyperreference of the given URL - transforming the given
        /// (relative) URL to an absolute URL if required.
        /// </summary>
        /// <param name="node">The node that spawns the hyper reference.</param>
        /// <param name="url">The given URL.</param>
        /// <returns>The absolute URL.</returns>
        public static Url HyperReference(this INode node, String url)
        {
            if (url == null)
                return null;

            return new Url(node.BaseUrl, url);
        }

        /// <summary>
        /// Checks if the node is an descendant of the given parent.
        /// </summary>
        /// <param name="node">The descendant node to use.</param>
        /// <param name="parent">The possible parent to use.</param>
        /// <returns>True if the given parent is actually an ancestor of the provided node.</returns>
        public static Boolean IsDescendantOf(this INode node, INode parent)
        {
            if (node.Parent == null)
                return false;
            else if (Object.ReferenceEquals(node.Parent, parent))
                return true;

            return node.Parent.IsDescendantOf(parent);
        }

        /// <summary>
        /// Gets the descendant nodes of the provided parent, in tree order.
        /// </summary>
        /// <param name="parent">The parent of the descendants.</param>
        /// <returns>An iterator over all descendants.</returns>
        public static IEnumerable<INode> GetDescendants(this INode parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                yield return child;

                foreach (var subchild in child.GetDescendants())
                    yield return subchild;
            }
        }

        /// <summary>
        /// Checks if the node is an inclusive descendant of the given parent.
        /// </summary>
        /// <param name="node">The descendant node to use.</param>
        /// <param name="parent">The possible parent to use.</param>
        /// <returns>True if the given parent is actually an inclusive ancestor of the provided node.</returns>
        public static Boolean IsInclusiveDescendantOf(this INode node, INode parent)
        {
            return node == parent || node.IsDescendantOf(parent);
        }

        /// <summary>
        /// Checks if the parent is an ancestor of the given node.
        /// </summary>
        /// <param name="parent">The possible parent to use.</param>
        /// <param name="node">The node to check for being descendent.</param>
        /// <returns>True if the given parent is actually an ancestor of the provided node.</returns>
        public static Boolean IsAncestorOf(this INode parent, INode node)
        {
            return node.IsDescendantOf(parent);
        }

        /// <summary>
        /// Gets the ancestor nodes of the provided node, in tree order.
        /// </summary>
        /// <param name="node">The child of the ancestors.</param>
        /// <returns>An iterator over all ancestors.</returns>
        public static IEnumerable<INode> GetAncestors(this INode node)
        {
            while ((node = node.Parent) != null)
                yield return node;
        }

        /// <summary>
        /// Gets the inclusive ancestor nodes of the provided node, in tree order.
        /// </summary>
        /// <param name="node">The child of the ancestors.</param>
        /// <returns>An iterator over all ancestors including the given node.</returns>
        public static IEnumerable<INode> GetInclusiveAncestors(this INode node)
        {
            do
                yield return node;
            while ((node = node.Parent) != null);
        }

        /// <summary>
        /// Checks if the parent is an inclusive ancestor of the given node.
        /// </summary>
        /// <param name="parent">The possible parent to use.</param>
        /// <param name="node">The node to check for being descendent.</param>
        /// <returns>True if the given parent is actually an inclusive ancestor of the provided node.</returns>
        public static Boolean IsInclusiveAncestorOf(this INode parent, INode node)
        {
            return node == parent || node.IsDescendantOf(parent);
        }

        /// <summary>
        /// Gets the first ancestor node that is of the specified type.
        /// </summary>
        /// <param name="node">The child of the potential ancestor.</param>
        /// <returns>The specified ancestor or its default value.</returns>
        public static T GetAncestor<T>(this INode node)
            where T : INode
        {
            while ((node = node.Parent) != null)
            {
                if (node is T)
                    return (T)node;
            }

            return default(T);
        }

        /// <summary>
        /// Checks if any parent is an HTML datalist element..
        /// </summary>
        /// <param name="child">The node to use as starting point.</param>
        /// <returns>True if a datalist element is among the ancestors, otherwise false.</returns>
        public static Boolean HasDataListAncestor(this INode child)
        {
            return child.Ancestors<IHtmlDataListElement>().Any();
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
        /// Gets the index of the provided node in the node's parent's collection.
        /// </summary>
        /// <param name="node">The node which needs to know its index.</param>
        /// <returns>The index of the node or -1 if the node is not a child of a parent.</returns>
        public static Int32 Index(this INode node)
        {
            return node.Parent.IndexOf(node);
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

            if (parent != null)
            {
                foreach (var child in parent.ChildNodes)
                {
                    if (Object.ReferenceEquals(child, node))
                        return i;

                    i++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Checks if the context node is before the provided node.
        /// </summary>
        /// <param name="before">The context node.</param>
        /// <param name="after">The provided reference node.</param>
        /// <returns>True if the context node is preceding the reference node in tree order.</returns>
        public static Boolean IsPreceding(this INode before, INode after)
        {
            var parent = before.Parent;

            if (parent == null)
                return false;
            else if (parent == after)
                return true;
            else if (parent == after.Parent)
                return parent.IndexOf(before) < parent.IndexOf(after);

            return parent.IsPreceding(after);
        }

        /// <summary>
        /// Checks if the context node is after the provided node.
        /// </summary>
        /// <param name="after">The context node.</param>
        /// <param name="before">The provided reference node.</param>
        /// <returns>True if the context node is following the reference node in tree order.</returns>
        public static Boolean IsFollowing(this INode after, INode before)
        {
            return before.IsPreceding(after);
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
                throw new DomException(DomError.HierarchyRequest);
            else if (child != null && child.Parent != parent)
                throw new DomException(DomError.NotFound);
            else if (node is IElement == false && node is ICharacterData == false && node is IDocumentType == false && node is IDocumentFragment == false)
                throw new DomException(DomError.HierarchyRequest);

            var document = parent as IDocument;

            if (document != null)
            {
                var forbidden = false;

                switch (node.NodeType)
                {
                    case NodeType.Element:
                        forbidden = document.DocumentElement != null || child is IDocumentType || child.IsFollowedByDoctype();
                        break;
                    case NodeType.DocumentFragment:
                        var elements = node.GetElementCount();
                        forbidden = elements > 1 || node.HasTextNodes() || (elements == 1 && document.DocumentElement != null) || child is IDocumentType || child.IsFollowedByDoctype();
                        break;
                    case NodeType.DocumentType:
                        forbidden = document.Doctype != null || (child != null && child.IsPrecededByElement()) || (child == null && document.DocumentElement != null);
                        break;
                    case NodeType.Text:
                        forbidden = true;
                        break;
                }

                if (forbidden)
                    throw new DomException(DomError.HierarchyRequest);
            }
            else if (node is IDocumentType)
                throw new DomException(DomError.HierarchyRequest);
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
            var parentNode = parent as Node;
            var newNode = node as Node;

            if (parentNode == null)
                throw new DomException(DomError.NotSupported);

            parent.EnsurePreInsertionValidity(node, child);
            var referenceChild = child as Node;

            if (referenceChild == node)
                referenceChild = newNode.NextSibling;

            var document = parent.Owner ?? parent as IDocument;
            document.AdoptNode(node);
            parentNode.InsertBefore(newNode, referenceChild, false);
            return node;
        }

        /// <summary>
        /// Pre-removes the given child of the parent.
        /// </summary>
        /// <param name="parent">The origin that will be mutated.</param>
        /// <param name="child">The node that will be removed.</param>
        /// <returns>The removed node, which is child.</returns>
        public static INode PreRemove(this INode parent, INode child)
        {
            var parentNode = parent as Node;

            if (parentNode == null)
                throw new DomException(DomError.NotSupported);
            else if (child == null || child.Parent != parent)
                throw new DomException(DomError.NotFound);

            parentNode.RemoveChild(child as Node, false);
            return child;
        }

        /// <summary>
        /// Checks if the node has any text node children.
        /// </summary>
        /// <param name="node">The parent of the potential text nodes.</param>
        /// <returns>True if the node has any text nodes, otherwise false.</returns>
        public static Boolean HasTextNodes(this INode node)
        {
            return node.ChildNodes.OfType<IText>().Any();
        }

        /// <summary>
        /// Checks if the given child is followed by a document type.
        /// </summary>
        /// <param name="child">The child that precedes the doctype.</param>
        /// <returns>True if a doctype node is following the provided child, otherwise false.</returns>
        public static Boolean IsFollowedByDoctype(this INode child)
        {
            if (child == null)
                return false;

            var before = true;

            foreach (var node in child.Parent.ChildNodes)
            {
                if (before)
                    before = node != child;
                else if (node.NodeType == NodeType.DocumentType)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the given child is preceded by an element node.
        /// </summary>
        /// <param name="child">The child that follows any element.</param>
        /// <returns>True if an element node is preceded the provided child, otherwise false.</returns>
        public static Boolean IsPrecededByElement(this INode child)
        {
            foreach (var node in child.Parent.ChildNodes)
            {
                if (node == child)
                    break;
                else if (node.NodeType == NodeType.Element)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the element count of the given node.
        /// </summary>
        /// <param name="parent">The parent of potential element nodes.</param>
        /// <returns>The number of element nodes in the parent.</returns>
        public static Int32 GetElementCount(this INode parent)
        {
            int count = 0;

            foreach (var node in parent.ChildNodes)
            {
                if (node.NodeType == NodeType.Element)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Tries to find a direct child of a certain type.
        /// </summary>
        /// <typeparam name="TNode">The node type to find.</typeparam>
        /// <param name="parent">The parent that contains the elements.</param>
        /// <returns>The instance or null.</returns>
        public static TNode FindChild<TNode>(this INode parent)
            where TNode : class, INode
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as TNode;

                if (child != null)
                    return child;
            }

            return null;
        }

        /// <summary>
        /// Tries to find a descendant of a certain type.
        /// </summary>
        /// <typeparam name="TNode">The node type to find.</typeparam>
        /// <param name="parent">The parent that contains the elements.</param>
        /// <returns>The instance or null.</returns>
        public static TNode FindDescendant<TNode>(this INode parent)
            where TNode : class, INode
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var node = parent.ChildNodes[i];
                var child = node as TNode ?? node.FindDescendant<TNode>();

                if (child != null)
                    return child;
            }

            return null;
        }
    }
}
