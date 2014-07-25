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
    sealed partial class TextStream : IDisposable
    {
        #region Fields

        const Int32 BufferSize = 4096;

        readonly Stream _baseStream;
        readonly StringBuilder _content;
        readonly Byte[] _buffer;
        readonly Char[] _chars;

        Boolean _finished;
        Encoding _encoding;
        Int32 _index;

        #endregion

        #region ctor

        TextStream(Encoding encoding)
        {
            _buffer = new Byte[BufferSize];
            _chars = new Char[BufferSize];
            _index = 0;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public TextStream(String source, Encoding encoding = null)
            : this(encoding)
        {
            _finished = true;
            _content = new StringBuilder(source.Replace("\r\n", "\n"));
        }

        public TextStream(Stream baseStream, Encoding encoding = null)
            : this(encoding)
        {
            _baseStream = baseStream;
            _content = new StringBuilder();
        }

        #endregion

        #region Properties

        public Char this[Int32 index]
        {
            get { return _content[index]; }
        }

        public Encoding CurrentEncoding
        {
            get { return _encoding; }
            set 
            {
                if (value != _encoding)
                {
                    //Get previous interpretation of rest
                    var length = _content.Length - _index;
                    var rest = _content.ToString(_index, length);
                    //Get raw bytes from old encoding
                    var raw = _encoding.GetBytes(rest);
                    //Set new encoding
                    _encoding = value;
                    //Remove former rest
                    _content.Remove(_index, length);
                    //Add former rest re-interpreted with new encoding
                    _content.Append(_encoding.GetString(raw, 0, raw.Length));
                }
            }
        }

        public Boolean IsFinished
        {
            get { return _finished; }
        }

        public Int32 Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public Int32 Length
        {
            get { return _content.Length; }
        }

        #endregion

        #region Text Methods

        public Char Read()
        {
            if (_index < _content.Length)
                return _content[_index++];

            ExpandBuffer(BufferSize);
            return _index >= _content.Length ? Specification.EndOfFile : _content[_index++];
        }

        public async Task<Char> ReadAsync(CancellationToken cancellationToken)
        {
            if (_index < _content.Length)
                return _content[_index++];

            await ExpandBufferAsync(BufferSize, cancellationToken);
            return _index >= _content.Length ? Specification.EndOfFile : _content[_index++];
        }

        #endregion

        #region Helpers

        public void Dispose()
        {
            if (_baseStream != null)
                _baseStream.Dispose();

            _content.Clear();
        }

        async Task ExpandBufferAsync(Int64 size, CancellationToken cancellationToken)
        {
            while (size + _index > _content.Length && !_finished)
                await ReadIntoBufferAsync(cancellationToken);
        }

        async Task ReadIntoBufferAsync(CancellationToken cancellationToken)
        {
            var returned = await _baseStream.ReadAsync(_buffer, 0, BufferSize, cancellationToken);
            AppendContentFromBuffer(returned);
        }

        void ExpandBuffer(Int64 size)
        {
            while (size + _index > _content.Length && !_finished)
                ReadIntoBuffer();
        }

        void ReadIntoBuffer()
        {
            var returned = _baseStream.ReadAsync(_buffer, 0, BufferSize).Result;
            AppendContentFromBuffer(returned);
        }

        void AppendContentFromBuffer(Int32 size)
        {
            _finished = size == 0;
            var charLength = _encoding.GetChars(_buffer, 0, size, _chars, 0);

            for (int i = 0, n = charLength - 1; i < n; ++i)
            {
                if (_chars[i] == Specification.CarriageReturn && _chars[i + 1] == Specification.LineFeed)
                {
                    for (int j = i; j < n;  ++j)
                        _chars[j] = _chars[j + 1];

                    charLength--;
                    n--;
                }
            }

            if (charLength > 0 && _chars[charLength - 1] == Specification.CarriageReturn)
                charLength--;

            _content.Insert(_index, _chars, 0, charLength);
        }

        #endregion
    }
}
