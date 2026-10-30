namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// What <see cref="ElementWalker"/> does with each element of a walk. Implemented by structs,
    /// so the walk is compiled once per visitor, with the visitor inlined into its loop.
    /// </summary>
    internal interface IElementVisitor
    {
        /// <returns>True to stop the walk at this element.</returns>
        Boolean Visit(Element element);
    }

    /// <summary>
    /// Depth-first pre-order walk over elements and all of their element descendants, with an
    /// explicit stack instead of recursion. The same walk as <see cref="ElementTreeEnumerator"/>,
    /// but its stack starts on a buffer allocated on the stack, so a walk over an ordinary
    /// document allocates nothing.
    /// </summary>
    internal static class ElementWalker
    {
        /// <summary>
        /// Walks every element in the list and its element descendants.
        /// </summary>
        /// <returns>The element the visitor stopped at, if any.</returns>
        public static Element? Walk<TVisitor>(NodeList nodes, ref TVisitor visitor)
            where TVisitor : struct, IElementVisitor
        {
            for (var i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] is Element root)
                {
                    var stopped = Walk(root, ref visitor);

                    if (stopped is not null)
                    {
                        return stopped;
                    }
                }
            }

            return null;
        }

        private static Element? Walk<TVisitor>(Element element, ref TVisitor visitor)
            where TVisitor : struct, IElementVisitor
        {
            Span<Int32> buffer = stackalloc Int32[ChildIndexStack.InitialDepth];
            var resume = new ChildIndexStack(buffer);
            // The depth of the node whose children are searched next, counted from the root
            var depth = 0;

            // Locals rather than the fields of an enumerator, so the loop keeps them in registers.
            // No finally either: should the visitor throw, a rented array is left to the garbage
            // collector instead of returned to the pool.
            while (!visitor.Visit(element))
            {
                Node parent = element;
                var children = element.ChildNodes;
                var start = 0;

                while (true)
                {
                    var n = children.Length;
                    var i = start;

                    while (i < n && children[i].NodeType != NodeType.Element)
                    {
                        i++;
                    }

                    if (i < n)
                    {
                        // After the last child there is nothing to come back to
                        if (i + 1 < n)
                        {
                            resume.Push(i + 1, depth);
                        }

                        element = (Element)children[i];
                        depth++;
                        break;
                    }

                    if (resume.Count == 0)
                    {
                        resume.Dispose();
                        return null;
                    }

                    start = resume.Pop(out var target);

                    for (; depth > target; depth--)
                    {
                        parent = parent.Parent!;
                    }

                    children = parent.ChildNodes;
                }
            }

            resume.Dispose();
            return element;
        }
    }
}
