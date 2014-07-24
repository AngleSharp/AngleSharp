namespace AngleSharp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    partial class TextStream
    {
        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            if (!_finished)
                await _baseStream.FlushAsync(cancellationToken);
        }

        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            await ExpandBufferAsync(Position + count, cancellationToken);
            return await _content.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            return _content.WriteAsync(buffer, offset, count, cancellationToken);
        }
    }
}
