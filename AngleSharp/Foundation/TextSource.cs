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
    sealed class TextSource : ITextSource
    {
        #region Fields

        const Int32 BufferSize = 4096;

        readonly Stream _baseStream;
        readonly StringBuilder _content;
        readonly MemoryStream _raw;
        readonly Byte[] _buffer;
        readonly Char[] _chars;

        EncodingConfidence _confidence;
        Boolean _finished;
        Encoding _encoding;
        Int32 _index;

        #endregion

        #region ctor

        TextSource(Encoding encoding)
        {
            _buffer = new Byte[BufferSize];
            _chars = new Char[BufferSize];
            _raw = new MemoryStream();
            _index = 0;
            _encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// Creates a new text source from a string. No
        /// underlying stream will be used.
        /// </summary>
        /// <param name="source">The data source.</param>
        public TextSource(String source)
            : this(null, Encoding.UTF8)
        {
            _finished = true;
            _content.Append(source.Replace("\r\n", "\n"));
            _confidence = EncodingConfidence.Certain;
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
            _content = Pool.NewStringBuilder();
            _confidence = EncodingConfidence.Tentative;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the full text buffer.
        /// </summary>
        public String Text
        {
            get { return _content.ToString(); }
        }

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
                if (_confidence != EncodingConfidence.Tentative)
                    return;

                if (_encoding.IsUnicode())
                {
                    _confidence = EncodingConfidence.Certain;
                    return;
                }

                if (value.IsUnicode())
                    value = DocumentEncoding.UTF8;

                if (value == _encoding)
                {
                    _confidence = EncodingConfidence.Certain;
                    return;
                }

                _encoding = value;

                var raw = _raw.ToArray();
                var content = _encoding.GetString(raw, 0, raw.Length);

                //If everything seems to fit up to this point, do an instant switch
                if (content.Substring(0, _index).Equals(_content.ToString(0, _index)))
                {
                    _content.Remove(_index, _content.Length - _index);
                    _content.Append(content.Substring(_index));
                }

                //Otherwise consider restart from beginning ...
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
            _raw.Dispose();
            _content.Clear().ToPool();
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
            size -= offset;
            _finished = size == 0;
            var charLength = _encoding.GetChars(_buffer, offset, size, _chars, 0);

            /*for (int i = 0, n = charLength - 1; i < n; ++i)
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
                charLength--;*/

            _raw.Write(_buffer, offset, size);
            _content.Append(_chars, 0, charLength);
        }

        #endregion

        #region Confidence

        enum EncodingConfidence
        {
            Tentative,
            Certain,
            Irrelevant
        }

        #endregion
    }
}
