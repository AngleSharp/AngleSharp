namespace AngleSharp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    partial class TextStream
    {
        public Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            ExpandBuffer(Position + count);
            return _content.ReadAsync(buffer, offset, count, cancellationToken);
        }
    }
}
