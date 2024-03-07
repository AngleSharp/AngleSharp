#nullable disable

namespace AngleSharp.Text
{
    using AngleSharp.Common;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A stream abstraction to handle encoding and more.
    /// </summary>
    public sealed class TextSource : ITextSource
    {
        private readonly WritableTextSource _writableSource;
        private readonly IReadOnlyTextSource _readOnlyTextSource;

        /// <summary>
        /// Creates a new text source from a string. No underlying stream will
        /// be used.
        /// </summary>
        /// <param name="source">The data source.</param>
        public TextSource(String source)
        {
            _writableSource = new WritableTextSource(source);
            _readOnlyTextSource = _writableSource;
        }

        /// <summary>
        /// Creates a new text source from a stream. The underlying stream is
        /// used as an unknown data source.
        /// </summary>
        /// <param name="baseStream">
        /// The underlying stream as data source.
        /// </param>
        /// <param name="encoding">
        /// The initial encoding. Otherwise UTF-8.
        /// </param>
        public TextSource(Stream baseStream, Encoding encoding = null)
        {
            _writableSource = new WritableTextSource(baseStream, encoding);
            _readOnlyTextSource = _writableSource;
        }

        /// <summary>
        /// Creates a new immutable text source from a <see cref="ReadOnlyMemoryTextSource"/>. No underlying stream will be used
        /// </summary>
        public TextSource(ReadOnlyMemoryTextSource source)
        {
            _writableSource = null;
            _readOnlyTextSource = source;
        }

        /// <summary>
        /// Creates a new immutable text source from a <see cref="CharArrayTextSource"/>. No underlying stream will be used
        /// </summary>
        public TextSource(CharArrayTextSource source)
        {
            _writableSource = null;
            _readOnlyTextSource = source;
        }

        /// <summary>
        /// Creates a new immutable text source from a <see cref="StringTextSource"/>. No underlying stream will be used
        /// </summary>
        public TextSource(StringTextSource source)
        {
            _writableSource = null;
            _readOnlyTextSource = source;
        }

        /// <summary>
        /// Gets the full text buffer.
        /// </summary>
        public String Text => _readOnlyTextSource.Text;

        /// <summary>
        /// Gets the length of the text buffer.
        /// </summary>
        public Int32 Length => _readOnlyTextSource.Length;

        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
        public Encoding CurrentEncoding
        {
            get => _readOnlyTextSource.CurrentEncoding;
            set
            {
                if (_writableSource is not null)
                {
                    _writableSource.CurrentEncoding = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current index of the insertation and read point.
        /// </summary>
        public Int32 Index
        {
            get => _readOnlyTextSource.Index;
            set => _readOnlyTextSource.Index = value;
        }

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        public Char this[Int32 index] => _readOnlyTextSource[index];

        /// <summary>
        /// Reads the next character from the buffer or underlying stream, if
        /// any.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char ReadCharacter() => _readOnlyTextSource.ReadCharacter();

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or
        /// underlying stream, if any.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <returns>The string with the next characters.</returns>
        public String ReadCharacters(Int32 characters) => _readOnlyTextSource.ReadCharacters(characters);

        /// <inheritdoc/>
        public StringOrMemory ReadMemory(Int32 characters) => _readOnlyTextSource.ReadMemory(characters);

        /// <summary>
        /// Prefetches the number of bytes by expanding the internal buffer.
        /// </summary>
        /// <param name="length">The number of bytes to prefetch.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken) => _readOnlyTextSource.PrefetchAsync(length, cancellationToken);

        /// <summary>
        /// Prefetches the whole stream by expanding the internal buffer.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAllAsync(CancellationToken cancellationToken) => _readOnlyTextSource.PrefetchAllAsync(cancellationToken);

        /// <summary>
        /// Gets the content length, if known.
        /// </summary>
        /// <param name="length">Found length if known</param>
        /// <returns>True if length is available</returns>
        public Boolean TryGetContentLength(out Int32 length) => _readOnlyTextSource.TryGetContentLength(out length);

        /// <summary>
        /// Inserts the given content at the current insertation mark. Moves the
        /// insertation mark.
        /// </summary>
        /// <param name="content">The content to insert.</param>
        public void InsertText(String content)
        {
            if (_writableSource is null)
            {
                throw new InvalidOperationException("Cannot insert text into a read-only text source.");
            }

            _writableSource.InsertText(content);
        }

        /// <summary>
        /// Disposes the text source by freeing the underlying stream, if any.
        /// </summary>
        public void Dispose() => _readOnlyTextSource.Dispose();

        /// <summary>
        /// Gets underlying text source.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyTextSource GetUnderlyingTextSource() => _readOnlyTextSource;
    }
}
