namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for parent node objects.
    /// </summary>
    [DebuggerStepThrough]
    static class ParentNodeExtensions
    {
        /// <summary>
        /// Runs the mutation macro as defined in 5.2.2 Mutation methods
        /// of http://www.w3.org/TR/domcore/.
        /// </summary>
        /// <param name="parent">The parent, which invokes the algorithm.</param>
        /// <param name="nodes">The nodes array to add.</param>
        /// <returns>A (single) node.</returns>
        public static INode MutationMacro(this INode parent, INode[] nodes)
        {
            if (nodes.Length > 1)
            {
                var node = parent.Owner.CreateDocumentFragment();

                for (int i = 0; i < nodes.Length; i++)
                    node.AppendChild(nodes[i]);

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
            var parent = child.Parent;

            if (parent != null)
                parent.PreRemove(child);
        }
    }
}
