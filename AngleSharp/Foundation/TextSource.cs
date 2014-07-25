namespace AngleSharp
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A stream abstraction to handle encoding and more.
    /// </summary>
    [DebuggerStepThrough]
    sealed class TextSource : ITextSource, IDisposable
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

        TextSource(Encoding encoding)
        {
            _buffer = new Byte[BufferSize];
            _chars = new Char[BufferSize];
            _index = 0;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// Creates a new text source from a string. No
        /// underlying stream will be used.
        /// </summary>
        /// <param name="source">The data source.</param>
        /// <param name="encoding">The initial encoding. Otherwise UTF-8.</param>
        public TextSource(String source, Encoding encoding = null)
            : this(encoding)
        {
            _finished = true;
            _content = new StringBuilder(source.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Creates a new text source from a string. The underlying stream is used
        /// as an unknown data source.
        /// </summary>
        /// <param name="baseStream">The underlying stream as data source.</param>
        /// <param name="encoding">The initial encoding. Otherwise UTF-8.</param>
        public TextSource(Stream baseStream, Encoding encoding = null)
            : this(encoding)
        {
            _baseStream = baseStream;
            _content = new StringBuilder();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        public Char this[Int32 index]
        {
            get { return _content[index]; }
        }

        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the current index of the insertation and read point.
        /// </summary>
        public Int32 Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        /// Gets the length of the text buffer.
        /// </summary>
        public Int32 Length
        {
            get { return _content.Length; }
        }

        #endregion

        #region Text Methods

        /// <summary>
        /// Reads the next character from the buffer or underlying stream, if any.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char ReadCharacter()
        {
            if (_index < _content.Length)
                return _content[_index++];

            ExpandBuffer(BufferSize);
            var index = _index++;
            return index < _content.Length ? _content[index] : Specification.EndOfFile;
        }

        /// <summary>
        /// Reads the next character from the buffer or underlying stream asynchronously, if any.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task resulting in the next character.</returns>
        public async Task<Char> ReadCharacterAsync(CancellationToken cancellationToken)
        {
            if (_index < _content.Length)
                return _content[_index++];

            await ExpandBufferAsync(BufferSize, cancellationToken);
            var index = _index++;
            return index < _content.Length ? _content[index] : Specification.EndOfFile;
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
