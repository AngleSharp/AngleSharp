namespace AngleSharp.Dom
{
    using System;
    using System.Buffers;

    /// <summary>
    /// Stack of child indices for walking a tree without recursion. Recursing once per level
    /// overflows the call stack on deeply nested documents (a few thousand unclosed tags), and a
    /// stack overflow cannot be caught. It starts on a buffer the caller allocates on the stack,
    /// so ordinary documents cost no allocation, and only rents from the array pool for deeper
    /// trees. Call <see cref="Dispose"/> when done, which returns a rented array.
    /// </summary>
    internal ref struct ChildIndexStack
    {
        /// <summary>
        /// Levels the stack-allocated buffer should hold; deeper trees rent from the array pool.
        /// </summary>
        public const Int32 InitialDepth = 64;

        /// <summary>
        /// Levels a walk may recurse before it continues with this stack. Recursion is the fastest
        /// walk for the shallow trees of ordinary documents, and this many small frames fit in the
        /// stack of any thread; only a deeper document reaches the iterative walk.
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

        public void Push(Int32 index)
        {
            if (_count == _indices.Length)
            {
                Grow();
            }

            _indices[_count++] = index;
        }

        public Int32 Pop() => _indices[--_count];

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
