namespace AngleSharp
{
    using AngleSharp.DOM;
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
        /// <param name="nodes">The nodes array to add.</param>
        /// <returns>A (single) node.</returns>
        public static INode MutationMacro(this INode[] nodes)
        {
            if (nodes.Length > 1)
            {
                var node = new DocumentFragment();

                for (int i = 0; i < nodes.Length; i++)
                    node.AppendChild(nodes[i]);

                return node;
            }

            return nodes[0];
        }
    }
}
