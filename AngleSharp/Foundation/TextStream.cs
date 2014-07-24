namespace AngleSharp
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A stream abstraction to handle encoding and more.
    /// </summary>
    sealed partial class TextStream : Stream
    {
        #region Fields

        const Int32 BufferSize = 4096;

        readonly Stream _baseStream;
        readonly MemoryStream _content;
        readonly Byte[] _buffer;
        readonly Char[] _chars;

        Boolean _finished;
        Encoding _encoding;
        Int32 _charIndex;
        Int32 _charLength;

        #endregion

        #region ctor

        TextStream(Encoding encoding)
        {
            _buffer = new Byte[BufferSize];
            _chars = new Char[BufferSize];
            _charIndex = 0;
            _charLength = 0;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public TextStream(String source, Encoding encoding = null)
            : this(encoding)
        {
            _finished = true;
            var content = _encoding.GetBytes(source);
            _content = new MemoryStream();
            _content.Write(content, 0, content.Length);
            _content.Position = 0;
        }

        public TextStream(Stream baseStream, Encoding encoding = null)
            : this(encoding)
        {
            _baseStream = baseStream;
            _content = new MemoryStream();
        }

        #endregion

        #region Properties

        public Encoding CurrentEncoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        public Boolean IsFinished
        {
            get { return _finished; }
        }

        public override Boolean CanRead
        {
            get { return true; }
        }

        public override Boolean CanSeek
        {
            get { return _finished || _baseStream.CanSeek; }
        }

        public override Boolean CanWrite
        {
            get { return true; }
        }

        public override Int64 Length
        {
            get { return _content.Length; }
        }

        public override Int64 Position
        {
            get { return _content.Position; }
            set 
            {
                ExpandBuffer(value + 1);
                _content.Position = value;
                _charIndex = 0;
                _charLength = 0;
            }
        }

        #endregion

        #region Text Methods

        public Char Read()
        {
            if (_charIndex < _charLength)
                return _chars[_charIndex++];

            var contentLength = Read(_buffer, 0, BufferSize);
            _charLength = _encoding.GetChars(_buffer, 0, contentLength, _chars, 0);
            _charIndex = 1;
            return _charIndex > _charLength ? Specification.EndOfFile : _chars[0];
        }

        public async Task<Char> ReadAsync(CancellationToken cancellationToken)
        {
            if (_charIndex < _charLength)
                return _chars[_charIndex++];

            var contentLength = await ReadAsync(_buffer, 0, BufferSize, cancellationToken);
            _charLength = _encoding.GetChars(_buffer, 0, contentLength, _chars, 0);
            _charIndex = 1;

            return _charIndex > _charLength ? Specification.EndOfFile : _chars[0];
        }

        #endregion

        #region Stream Methods

        public override void Flush()
        {
            if (!_finished)
                _baseStream.Flush();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            ExpandBuffer(Position + count);
            return _content.Read(buffer, offset, count);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            var position = origin == SeekOrigin.Current ? Position + offset : (origin == SeekOrigin.Begin ? offset : Length + offset);
            Position = position;
            return position;
        }

        public override void SetLength(Int64 value)
        {
            _content.SetLength(value);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            _content.Write(buffer, offset, count);
        }

        #endregion

        #region Helpers

        protected override void Dispose(Boolean disposing)
        {
            if (_baseStream != null)
                _baseStream.Dispose();

            _content.Dispose();
        }

        async Task ExpandBufferAsync(Int64 minimumSize, CancellationToken cancellationToken)
        {
            while (minimumSize > _content.Length && !_finished)
                await ReadIntoBufferAsync(cancellationToken);
        }

        async Task ReadIntoBufferAsync(CancellationToken cancellationToken)
        {
            if (_finished)
                return;

            var returned = await _baseStream.ReadAsync(_buffer, 0, BufferSize, cancellationToken);

            if (_finished = returned == 0)
                return;

            var position = _content.Position;
            await _content.WriteAsync(_buffer, 0, returned);
            _content.Position = position;
        }

        void ExpandBuffer(Int64 minimumSize)
        {
            while (minimumSize > _content.Length && !_finished)
                ReadIntoBuffer();
        }

        void ReadIntoBuffer()
        {
            if (_finished)
                return;

            var returned = _baseStream.ReadAsync(_buffer, 0, BufferSize).Result;

            if (_finished = returned == 0)
                return;

            var position = _content.Position;
            _content.Write(_buffer, 0, returned);
            _content.Position = position;
        }

        #endregion
    }
}
