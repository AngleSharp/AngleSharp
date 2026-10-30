namespace AngleSharp.Dom
{
    using System;
    using System.Buffers;

    /// <summary>
    /// The explicit stack of child indices that deep walks use instead of the call stack.
    /// Recursing once per level overflows the call stack on deeply nested documents (a few
    /// thousand unclosed tags), and a stack overflow cannot be caught. It starts on a buffer the
    /// caller allocates on the stack, so ordinary documents cost no allocation, and only rents
    /// from the array pool for deeper trees. Call <see cref="Dispose"/> when done, which returns
    /// a rented array.
    /// </summary>
    /// <remarks>
    /// Each entry is the index to resume at together with the depth of the node it belongs to. A
    /// walk that descends into a node's last child has nothing to resume there and pushes no
    /// entry; when it climbs back, it goes up to the depth of the next entry. Deep documents
    /// mostly nest in the last child (unclosed tags), so their levels mostly cost no entry.
    /// </remarks>
    internal ref struct ChildIndexStack
    {
        /// <summary>
        /// Slots of the stack-allocated buffer, two per entry; deeper trees rent from the array pool.
        /// </summary>
        public const Int32 InitialDepth = 64;

        /// <summary>
        /// Levels a walk may recurse before it continues with this stack. Recursion is the fastest
        /// walk for the shallow trees of ordinary documents, and this many small frames fit in the
        /// stack of any thread; only a deeper document reaches the explicit stack.
        /// </summary>
        public const Int32 RecursionLimit = 128;

        private Span<Int32> _indices;
        private Int32[]? _rented;
        private Int32 _count;

        public ChildIndexStack(Span<Int32> buffer)
        {
            _indices = buffer;
            _rented = null;
            _count = 0;
        }

        public readonly Int32 Count => _count;

        public void Push(Int32 index, Int32 depth)
        {
            if (_count + 2 > _indices.Length)
            {
                Grow();
            }

            _indices[_count] = index;
            _indices[_count + 1] = depth;
            _count += 2;
        }

        public Int32 Pop(out Int32 depth)
        {
            depth = _indices[--_count];
            return _indices[--_count];
        }

        public void Dispose()
        {
            if (_rented is not null)
            {
                ArrayPool<Int32>.Shared.Return(_rented);
                _rented = null;
            }
        }

        private void Grow()
        {
            var larger = ArrayPool<Int32>.Shared.Rent(Math.Max(_indices.Length * 2, InitialDepth));
            _indices.Slice(0, _count).CopyTo(larger);

            if (_rented is not null)
            {
                ArrayPool<Int32>.Shared.Return(_rented);
            }

            _rented = larger;
            _indices = larger;
        }
    }
}
