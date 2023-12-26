#nullable disable

namespace AngleSharp.Text
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;

    /// <summary>
    /// A stream abstraction to handle encoding and more.
    /// </summary>
    public sealed class TextSource : ITextSource
    {
        private readonly StringBuilderTextSource _source;
        private readonly IReadOnlyTextSource _readOnlySource;

        /// <summary>
        /// Creates a new text source from a string. No underlying stream will
        /// be used.
        /// </summary>
        /// <param name="source">The data source.</param>
        public TextSource(String source)
        {
            _readOnlySource = null;
            _source = new StringBuilderTextSource(source);
        }

        /// <summary>
        /// Creates a new text source from a string. The underlying stream is
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
            _readOnlySource = null;
            _source = new StringBuilderTextSource(baseStream, encoding);
        }

        internal TextSource(IReadOnlyTextSource readOnlyTextSource)
        {
            _readOnlySource = readOnlyTextSource;
            _source = null;
        }

        /// <summary>
        /// Gets the full text buffer.
        /// </summary>
        public String Text => _source?.Text ?? _readOnlySource.Text;

        /// <summary>
        /// Gets the length of the text buffer.
        /// </summary>
        public Int32 Length => _source?.Length ?? _readOnlySource.Length;

        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
        public Encoding CurrentEncoding
        {
            get => _source?.CurrentEncoding ?? _readOnlySource.CurrentEncoding;
            set
            {
                if (_source != null)
                {
                    _source.CurrentEncoding = value;
                }

                if (_readOnlySource != null)
                {
                    _readOnlySource.CurrentEncoding = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current index of the insertation and read point.
        /// </summary>
        public Int32 Index
        {
            get => _source?.Index ?? _readOnlySource.Index;
            set
            {
                if (_source != null)
                {
                    _source.Index = value;
                }
                else
                {
                    _readOnlySource.Index = value;
                }
            }
        }

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        public Char this[Int32 index] => _source?[index] ?? _readOnlySource[index];

        /// <summary>
        /// Reads the next character from the buffer or underlying stream, if
        /// any.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char ReadCharacter()
        {
            return _source?.ReadCharacter() ?? _readOnlySource.ReadCharacter();
        }

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or
        /// underlying stream, if any.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <returns>The string with the next characters.</returns>
        public String ReadCharacters(Int32 characters)
        {
            return _source?.ReadCharacters(characters) ?? _readOnlySource.ReadCharacters(characters);
        }

        /// <inheritdoc/>
        public StringOrMemory ReadMemory(Int32 characters)
        {
            return _source?.ReadMemory(characters) ?? _readOnlySource.ReadMemory(characters);
        }

        /// <summary>
        /// Prefetches the number of bytes by expanding the internal buffer.
        /// </summary>
        /// <param name="length">The number of bytes to prefetch.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken)
        {
            return _source?.PrefetchAsync(length, cancellationToken) ?? _readOnlySource.PrefetchAsync(length, cancellationToken);
        }

        /// <summary>
        /// Prefetches the whole stream by expanding the internal buffer.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAllAsync(CancellationToken cancellationToken)
        {
            return _source?.PrefetchAllAsync(cancellationToken) ?? _readOnlySource.PrefetchAllAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the content length, if known.
        /// </summary>
        /// <param name="length">Found length if known</param>
        /// <returns>True if length is available</returns>
        public Boolean TryGetContentLength(out Int32 length)
        {
            return _source?.TryGetContentLength(out length) ?? _readOnlySource.TryGetContentLength(out length);
        }

        /// <summary>
        /// Inserts the given content at the current insertation mark. Moves the
        /// insertation mark.
        /// </summary>
        /// <param name="content">The content to insert.</param>
        public void InsertText(String content)
        {
            _source?.InsertText(content);
        }

        /// <summary>
        /// Disposes the text source by freeing the underlying stream, if any.
        /// </summary>
        public void Dispose()
        {
            _source?.Dispose();
            _readOnlySource?.Dispose();
        }
    }
}
