namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Remembers the position of the element last asked about among its element siblings, so
    /// that :nth-child() and :nth-last-child() asked about the next sibling only count the
    /// elements in between.
    /// </summary>
    /// <remarks>
    /// Without it every candidate counted its siblings from the start, so a parent with k
    /// matching children cost k^2 / 2 steps per query, and a table of a few thousand rows
    /// millions. Matching runs in document order, and the tree walk descends into the children
    /// of one sibling before reaching the next, so one entry is kept per parent (the ancestors
    /// of the current element, in practice), in a small table hashed by list. An entry holds
    /// its list weakly, so it never keeps a document alive, and is only used while the list's
    /// version is the one it was taken at, so any mutation discards it. The state is per thread,
    /// as Match has no context to carry it.
    /// </remarks>
    static class ChildPositionMemo
    {
        // A power of two; deep enough for the ancestor chain of a typical document
        private const Int32 Size = 16;

        [ThreadStatic]
        private static Entry[]? _entries;

        /// <summary>
        /// Gets the 1-based position of the given child among the elements of the list,
        /// counted from the start or the end, or 0 if it is not in the list.
        /// </summary>
        public static Int32 GetPosition(NodeList children, Node child, Boolean fromEnd)
        {
            var index = children.Index(child);

            if (index < 0)
            {
                return 0;
            }

            var entries = _entries ??= new Entry[Size];
            var slot = RuntimeHelpers.GetHashCode(children) & (Size - 1);
            var entry = entries[slot] ??= new Entry();
            var start = 0;
            var position = 0;

            if (!entry.IsFor(children))
            {
                entry.Reset(children);
            }

            // Resumes from the last answer, unless asked about an earlier child
            if (entry.Index <= index)
            {
                start = entry.Index + 1;
                position = entry.Position;
            }

            for (var i = start; i <= index; i++)
            {
                if (children[i].NodeType == NodeType.Element)
                {
                    position++;
                }
            }

            entry.Index = index;
            entry.Position = position;

            if (fromEnd)
            {
                if (entry.Count < 0)
                {
                    entry.Count = CountElements(children);
                }

                return entry.Count - position + 1;
            }

            return position;
        }

        private static Int32 CountElements(NodeList children)
        {
            var count = 0;

            for (var i = 0; i < children.Length; i++)
            {
                if (children[i].NodeType == NodeType.Element)
                {
                    count++;
                }
            }

            return count;
        }

        private sealed class Entry
        {
            private readonly WeakReference<NodeList?> _list = new(null);
            private Int32 _version;

            // The node index and element position of the child last asked about
            public Int32 Index;
            public Int32 Position;

            // The number of element children, or -1 while not counted
            public Int32 Count = -1;

            public Boolean IsFor(NodeList children) =>
                _version == children.Version && _list.TryGetTarget(out var list) && ReferenceEquals(list, children);

            public void Reset(NodeList children)
            {
                _list.SetTarget(children);
                _version = children.Version;
                Index = -1;
                Position = 0;
                Count = -1;
            }
        }
    }
}
