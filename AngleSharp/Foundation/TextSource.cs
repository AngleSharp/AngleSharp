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

        #region Disposable

        public void Dispose()
        {
            if (_baseStream != null)
                _baseStream.Dispose();
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
        /// Reads the upcoming numbers of characters from the buffer or underlying stream, if any.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <returns>The string with the next characters.</returns>
        public String ReadCharacters(Int32 characters)
        {
            var start = _index;
            var end = start + characters;

            if (end <= _content.Length)
            {
                _index += characters;
                return _content.ToString(start, characters);
            }

            ExpandBuffer(Math.Max(BufferSize, characters));
            _index += characters;
            characters = Math.Min(characters, _content.Length - start);
            return _content.ToString(start, characters);
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

            await ExpandBufferAsync(BufferSize, cancellationToken).ConfigureAwait(false);
            var index = _index++;
            return index < _content.Length ? _content[index] : Specification.EndOfFile;
        }

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or underlying stream asynchronously.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The string with the next characters.</returns>
        public async Task<String> ReadCharactersAsync(Int32 characters, CancellationToken cancellationToken)
        {
            var start = _index;
            var end = start + characters;

            if (end <= _content.Length)
            {
                _index += characters;
                return _content.ToString(start, characters);
            }

            await ExpandBufferAsync(Math.Max(BufferSize, characters), cancellationToken).ConfigureAwait(false);
            _index += characters;
            characters = Math.Min(characters, _content.Length - start);
            return _content.ToString(start, characters);
        }

        public Task Prefetch(Int32 length, CancellationToken cancellationToken)
        {
            return ExpandBufferAsync(length, cancellationToken);
        }

        /// <summary>
        /// Inserts the given content at the current insertation mark.
        /// The insertation mark won't be changed.
        /// </summary>
        /// <param name="content">The content to insert.</param>
        public void InsertText(String content)
        {
            if (_index < _content.Length)
                _content.Insert(_index, content);
            else
                _content.Append(content);
        }

        #endregion

        #region Helpers

        async Task DetectByteOrderMarkAsync(CancellationToken cancellationToken)
        {
            var count = await _baseStream.ReadAsync(_buffer, 0, BufferSize);
            var offset = 0;

            if (count > 2 && _buffer[0] == 0xef && _buffer[1] == 0xbb && _buffer[2] == 0xbf)
            {
                _encoding = DocumentEncoding.UTF8;
                offset = 3;
            }
            else if (count > 3 && _buffer[0] == 0xff && _buffer[1] == 0xfe && _buffer[2] == 0x0 && _buffer[3] == 0x0)
            {
                _encoding = DocumentEncoding.UTF32LE;
                offset = 4;
            }
            else if (count > 3 && _buffer[0] == 0x0 && _buffer[1] == 0x0 && _buffer[2] == 0xfe && _buffer[3] == 0xff)
            {
                _encoding = DocumentEncoding.UTF32BE;
                offset = 4;
            }
            else if (count > 1 && _buffer[0] == 0xfe && _buffer[1] == 0xff)
            {
                _encoding = DocumentEncoding.UTF16BE;
                offset = 2;
            }
            else if (count > 1 && _buffer[0] == 0xff && _buffer[1] == 0xfe)
            {
                _encoding = DocumentEncoding.UTF16LE;
                offset = 2;
            }
            else if (count > 3 && _buffer[0] == 0x84 && _buffer[1] == 0x31 && _buffer[2] == 0x95 && _buffer[3] == 0x33)
            {
                _encoding = DocumentEncoding.GB18030;
                offset = 4;
            }

            AppendContentFromBuffer(count, offset);
        }

        async Task ExpandBufferAsync(Int64 size, CancellationToken cancellationToken)
        {
            if (!_finished && _content.Length == 0)
                await DetectByteOrderMarkAsync(cancellationToken).ConfigureAwait(false);

            while (size + _index > _content.Length && !_finished)
                await ReadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
        }

        async Task ReadIntoBufferAsync(CancellationToken cancellationToken)
        {
            var returned = await _baseStream.ReadAsync(_buffer, 0, BufferSize, cancellationToken).ConfigureAwait(false);
            AppendContentFromBuffer(returned);
        }

        void ExpandBuffer(Int64 size)
        {
            if (!_finished && _content.Length == 0)
                DetectByteOrderMarkAsync(CancellationToken.None).Wait();

            while (size + _index > _content.Length && !_finished)
                ReadIntoBuffer();
        }

        void ReadIntoBuffer()
        {
            var returned = _baseStream.ReadAsync(_buffer, 0, BufferSize).Result;
            AppendContentFromBuffer(returned);
        }

        void AppendContentFromBuffer(Int32 size, Int32 offset = 0)
        {
            _finished = size == 0;
            var charLength = _encoding.GetChars(_buffer, offset, size, _chars, 0);

            for (int i = 0, n = charLength - 1; i < n; ++i)
            {
                if (_chars[i] == Specification.CarriageReturn && _chars[i + 1] == Specification.LineFeed)
                {
                    for (int j = i; j < n; ++j)
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
