namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Useful methods for parent node objects.
    /// </summary>
    public static class ParentNodeExtensions
    {
        /// <summary>
        /// Runs the mutation macro as defined in 5.2.2 Mutation methods
        /// of http://www.w3.org/TR/domcore/.
        /// </summary>
        /// <param name="parent">The parent, which invokes the algorithm.</param>
        /// <param name="nodes">The nodes array to add.</param>
        /// <returns>A (single) node.</returns>
        internal static INode MutationMacro(this INode parent, INode[] nodes)
        {
            if (nodes.Length > 1)
            {
                var node = parent.Owner.CreateDocumentFragment();

                for (var i = 0; i < nodes.Length; i++)
                {
                    node.AppendChild(nodes[i]);
                }

                return node;
            }

            return nodes[0];
        }

        /// <summary>
        /// Prepends nodes to the parent node.
        /// </summary>
        /// <param name="parent">The parent, where to prepend to.</param>
        /// <param name="nodes">The nodes to prepend.</param>
        public static void PrependNodes(this INode parent, params INode[] nodes)
        {
            if (nodes.Length > 0)
            {
                var node = parent.MutationMacro(nodes);
                parent.PreInsert(node, parent.FirstChild);
            }
        }

        /// <summary>
        /// Appends nodes to parent node.
        /// </summary>
        /// <param name="parent">The parent, where to append to.</param>
        /// <param name="nodes">The nodes to append.</param>
        public static void AppendNodes(this INode parent, params INode[] nodes)
        {
            if (nodes.Length > 0)
            {
                var node = parent.MutationMacro(nodes);
                parent.PreInsert(node, null);
            }
        }

        /// <summary>
        /// Inserts nodes before the given child.
        /// </summary>
        /// <param name="child">The context object.</param>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        public static void InsertBefore(this INode child, params INode[] nodes)
        {
            var parent = child.Parent;

            if (parent != null && nodes.Length > 0)
            {
                var node = parent.MutationMacro(nodes);
                parent.PreInsert(node, child);
            }
        }

        /// <summary>
        /// Inserts nodes after the given child.
        /// </summary>
        /// <param name="child">The context object.</param>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public static void InsertAfter(this INode child, params INode[] nodes)
        {
            var parent = child.Parent;

            if (parent != null && nodes.Length > 0)
            {
                var node = parent.MutationMacro(nodes);
                parent.PreInsert(node, child.NextSibling);
            }
        }

        /// <summary>
        /// Replaces the given child with the nodes.
        /// </summary>
        /// <param name="child">The context object.</param>
        /// <param name="nodes">The nodes to replace.</param>
        public static void ReplaceWith(this INode child, params INode[] nodes)
        {
            var parent = child.Parent;

            if (parent != null)
            {
                if (nodes.Length != 0)
                {
                    var node = parent.MutationMacro(nodes);
                    parent.ReplaceChild(node, child);
                }
                else
                {
                    parent.RemoveChild(child);
                }
            }
        }

        /// <summary>
        /// Removes the child from its parent.
        /// </summary>
        /// <param name="child">The context object.</param>
        public static void RemoveFromParent(this INode child)
        {
            child.Parent?.PreRemove(child);
        }

        /// <summary>
        /// Inserts a node as the last child node of this element.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="element">The element to be appended.</param>
        /// <returns>The appended element.</returns>
        public static TElement AppendElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.AppendChild(element) as TElement;
        }

        /// <summary>
        /// Inserts the newElement immediately before the referenceElement.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="newElement">The node to be inserted.</param>
        /// <param name="referenceElement">
        /// The existing child element that will succeed the new element.
        /// </param>
        /// <returns>The inserted element.</returns>
        public static TElement InsertElement<TElement>(this INode parent, TElement newElement, INode referenceElement)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.InsertBefore(newElement, referenceElement) as TElement;
        }

        /// <summary>
        /// Removes a child node from the current element, which must be a
        /// child of the current node.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="parent">The parent of the node to remove.</param>
        /// <param name="element">The element to be removed.</param>
        /// <returns>The removed element.</returns>
        public static TElement RemoveElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.RemoveChild(element) as TElement;
        }

        /// <summary>
        /// Returns the first element matching the selectors with the provided
        /// type, or null.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The element, if there is any.</returns>
        public static TElement QuerySelector<TElement>(this IParentNode parent, String selectors)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (selectors == null)
                throw new ArgumentNullException(nameof(selectors));

            return parent.QuerySelector(selectors) as TElement;
        }

        /// <summary>
        /// Returns a list of elements matching the selectors with the
        /// provided type.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>An enumeration with the elements.</returns>
        public static IEnumerable<TElement> QuerySelectorAll<TElement>(this IParentNode parent, String selectors)
            where TElement : IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (selectors == null)
                throw new ArgumentNullException(nameof(selectors));

            return parent.QuerySelectorAll(selectors).OfType<TElement>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<TNode> Descendents<TNode>(this INode parent)
        {
            return parent.Descendents().OfType<TNode>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<INode> Descendents(this INode parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.GetDescendants();
        }

        /// <summary>
        /// Gets the descendent nodes including itself of the given parent.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes including itself.</returns>
        public static IEnumerable<TNode> DescendentsAndSelf<TNode>(this INode parent)
        {
            return parent.DescendentsAndSelf().OfType<TNode>();
        }

        /// <summary>
        /// Gets the descendent nodes including itself of the given parent.
        /// </summary>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes including itself.</returns>
        public static IEnumerable<INode> DescendentsAndSelf(this INode parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.GetDescendantsAndSelf();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<TNode> Ancestors<TNode>(this INode child)
        {
            return child.Ancestors().OfType<TNode>();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<INode> Ancestors(this INode child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            return child.GetAncestors();
        }
    }
}
