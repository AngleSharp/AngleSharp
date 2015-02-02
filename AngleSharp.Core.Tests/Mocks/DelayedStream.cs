namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DelayedStream : Stream
    {
        Stream _stream;
        const Int32 delay = 10;

        public DelayedStream(Stream stream)
        {
            _stream = stream;
        }

        public DelayedStream(Byte[] content)
            : this(new MemoryStream(content))
        {
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get
            {
                return _stream.Position;
            }
            set
            {
                _stream.Position = value;
            }
        }

        public override async Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            await _stream.CopyToAsync(destination, bufferSize, cancellationToken).ConfigureAwait(false);
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            return await _stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            await _stream.WriteAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }
    }
}
