namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Depth-first pre-order walk over an element and all of its element descendants, without
    /// recursion. The same walk as <see cref="ElementTreeEnumerator"/>, but its stack of child
    /// indices starts on a buffer the caller allocates on the stack, so a walk over an ordinary
    /// document allocates nothing. Call <see cref="Dispose"/> when done.
    /// </summary>
    internal ref struct ElementWalker
    {
        private ChildIndexStack _resume;
        private Element _current;
        private Boolean _started;
        private Boolean _finished;

        /// <param name="root">The first element of the walk.</param>
        /// <param name="buffer">Stack space for the indices, see <see cref="ChildIndexStack.InitialDepth"/>.</param>
        public ElementWalker(Element root, Span<Int32> buffer)
        {
            _resume = new ChildIndexStack(buffer);
            _current = root;
            _started = false;
            _finished = false;
        }

        public readonly Element Current => _current;

        public Boolean MoveNext()
        {
            if (_finished)
            {
                return false;
            }

            if (!_started)
            {
                _started = true;
                return true;
            }

            var child = GetElementAtOrAfter(_current, 0, out var childIndex);

            if (child is not null)
            {
                _resume.Push(childIndex);
                _current = child;
                return true;
            }

            while (_resume.Count > 0)
            {
                // Only elements are ever descended into, so the parent is always an element.
                var parent = (Element)_current.Parent!;
                var sibling = GetElementAtOrAfter(parent, _resume.Pop() + 1, out var siblingIndex);

                if (sibling is not null)
                {
                    _resume.Push(siblingIndex);
                    _current = sibling;
                    return true;
                }

                _current = parent;
            }

            _finished = true;
            return false;
        }

        public void Dispose() => _resume.Dispose();

        private static Element? GetElementAtOrAfter(Element element, Int32 start, out Int32 index)
        {
            var children = element.ChildNodes;
            var n = children.Length;

            for (var i = start; i < n; i++)
            {
                var node = children[i];

                if (node.NodeType == NodeType.Element)
                {
                    index = i;
                    return (Element)node;
                }
            }

            index = -1;
            return null;
        }
    }
}
