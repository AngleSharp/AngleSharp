namespace AngleSharp.Common;

using System;
using System.Buffers;

internal static class ArrayPoolExtensions
{
    internal static Lease<T> Borrow<T>(this ArrayPool<T> pool, Int32 length)
    {
        var arr = ArrayPool<T>.Shared.Rent(length);
        return new Lease<T>(ArrayPool<T>.Shared, arr, length);
    }

    internal readonly struct Lease<T> : IDisposable
    {
        private readonly ArrayPool<T> _owner;
        private readonly T[] _data;
        private readonly Int32 _requestedLength;

        public Lease(ArrayPool<T> owner, T[] data, Int32 requestedLength)
        {
            _owner = owner;
            _data = data;
            _requestedLength = requestedLength;
        }

        public Int32 RequestedLength => _requestedLength;

        public T[] Data => _data;

        public Span<T> Span => Data.AsSpan(0, RequestedLength);

        public Memory<T> Memory => Data.AsMemory(0, RequestedLength);

        public void Dispose()
        {
            _owner.Return(_data, false);
        }
    }
}