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
        private readonly StringBuilderTextSource _stringBuilderSource;
        private readonly PrefetchedTextSource _prefetchedSource;

        /// <summary>
        /// Creates a new text source from a string. No underlying stream will
        /// be used.
        /// </summary>
        /// <param name="source">The data source.</param>
        public TextSource(String source)
        {
            _prefetchedSource = null;
            _stringBuilderSource = new StringBuilderTextSource(source);
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
            _prefetchedSource = null;
            _stringBuilderSource = new StringBuilderTextSource(baseStream, encoding);
        }

        internal TextSource(PrefetchedTextSource readOnlyTextSource)
        {
            _prefetchedSource = readOnlyTextSource;
            _stringBuilderSource = null;
        }

        /// <summary>
        /// Gets the full text buffer.
        /// </summary>
        public String Text => _stringBuilderSource?.Text ?? _prefetchedSource.Text;

        /// <summary>
        /// Gets the length of the text buffer.
        /// </summary>
        public Int32 Length => _stringBuilderSource?.Length ?? _prefetchedSource.Length;

        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
        public Encoding CurrentEncoding
        {
            get => _stringBuilderSource?.CurrentEncoding ?? _prefetchedSource.CurrentEncoding;
            set
            {
                if (_stringBuilderSource != null)
                {
                    _stringBuilderSource.CurrentEncoding = value;
                }

                if (_prefetchedSource != null)
                {
                    _prefetchedSource.CurrentEncoding = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current index of the insertation and read point.
        /// </summary>
        public Int32 Index
        {
            get => _stringBuilderSource?.Index ?? _prefetchedSource.Index;
            set
            {
                if (_stringBuilderSource != null)
                {
                    _stringBuilderSource.Index = value;
                }
                else
                {
                    _prefetchedSource.Index = value;
                }
            }
        }

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        public Char this[Int32 index] => _stringBuilderSource?[index] ?? _prefetchedSource[index];

        /// <summary>
        /// Reads the next character from the buffer or underlying stream, if
        /// any.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char ReadCharacter()
        {
            return _stringBuilderSource?.ReadCharacter() ?? _prefetchedSource.ReadCharacter();
        }

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or
        /// underlying stream, if any.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <returns>The string with the next characters.</returns>
        public String ReadCharacters(Int32 characters)
        {
            return _stringBuilderSource?.ReadCharacters(characters) ?? _prefetchedSource.ReadCharacters(characters);
        }

        /// <inheritdoc/>
        public StringOrMemory ReadMemory(Int32 characters)
        {
            return _stringBuilderSource?.ReadMemory(characters) ?? _prefetchedSource.ReadMemory(characters);
        }

        /// <summary>
        /// Prefetches the number of bytes by expanding the internal buffer.
        /// </summary>
        /// <param name="length">The number of bytes to prefetch.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken)
        {
            return _stringBuilderSource?.PrefetchAsync(length, cancellationToken) ?? _prefetchedSource.PrefetchAsync(length, cancellationToken);
        }

        /// <summary>
        /// Prefetches the whole stream by expanding the internal buffer.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The awaitable task.</returns>
        public Task PrefetchAllAsync(CancellationToken cancellationToken)
        {
            return _stringBuilderSource?.PrefetchAllAsync(cancellationToken) ?? _prefetchedSource.PrefetchAllAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the content length, if known.
        /// </summary>
        /// <param name="length">Found length if known</param>
        /// <returns>True if length is available</returns>
        public Boolean TryGetContentLength(out Int32 length)
        {
            return _stringBuilderSource?.TryGetContentLength(out length) ?? _prefetchedSource.TryGetContentLength(out length);
        }

        /// <summary>
        /// Inserts the given content at the current insertation mark. Moves the
        /// insertation mark.
        /// </summary>
        /// <param name="content">The content to insert.</param>
        public void InsertText(String content)
        {
            _stringBuilderSource?.InsertText(content);
        }

        /// <summary>
        /// Disposes the text source by freeing the underlying stream, if any.
        /// </summary>
        public void Dispose()
        {
            _stringBuilderSource?.Dispose();
            _prefetchedSource?.Dispose();
        }

        internal IReadOnlyTextSource GetReal()
        {
            return (IReadOnlyTextSource)_prefetchedSource ?? _stringBuilderSource;
        }
    }
}
