namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Depth-first pre-order enumerator over an element and all of its element
    /// descendants.
    /// </summary>
    /// <remarks>
    /// The obvious implementation pushes every element child onto a stack, but a
    /// <see cref="System.Collections.Generic.Stack{T}"/> of a reference type pays an array
    /// covariance check on every push, and the stack grows to the width of the tree even
    /// when the very first element matches. This walks parent links instead and only keeps
    /// a stack of child indices, which stores value types and is bounded by the depth of
    /// the tree rather than its width.
    /// </remarks>
    internal struct ElementTreeEnumerator
    {
        private const Int32 InitialDepth = 16;

        private Element _current;
        private Int32[]? _indices;
        private Int32 _depth;
        private Boolean _started;
        private Boolean _finished;

        public ElementTreeEnumerator(Element root)
        {
            _current = root;
            _indices = null;
            _depth = 0;
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
                Push(childIndex);
                _current = child;
                return true;
            }

            while (_depth > 0)
            {
                // Only elements are ever descended into, so the parent is always an element.
                var parent = (Element)_current.Parent!;
                var sibling = GetElementAtOrAfter(parent, _indices![_depth - 1] + 1, out var siblingIndex);

                if (sibling is not null)
                {
                    _indices[_depth - 1] = siblingIndex;
                    _current = sibling;
                    return true;
                }

                _depth--;
                _current = parent;
            }

            _finished = true;
            return false;
        }

        private void Push(Int32 index)
        {
            var indices = _indices;

            if (indices is null)
            {
                indices = _indices = new Int32[InitialDepth];
            }
            else if (_depth == indices.Length)
            {
                Array.Resize(ref indices, indices.Length * 2);
                _indices = indices;
            }

            indices[_depth++] = index;
        }

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
