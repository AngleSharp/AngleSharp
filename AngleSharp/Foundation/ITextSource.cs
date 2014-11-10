namespace AngleSharp
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The interface for streaming through source codes.
    /// </summary>
    interface ITextSource : IDisposable
    {
        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
        Encoding CurrentEncoding { get; set; }

        /// <summary>
        /// Gets or sets the current index of the insertation and read point.
        /// </summary>
        Int32 Index { get; set; }

        /// <summary>
        /// Gets the length of the text buffer.
        /// </summary>
        Int32 Length { get; }

        /// <summary>
        /// Reads the next character from the buffer or underlying stream, if any.
        /// </summary>
        /// <returns>The next character.</returns>
        Char ReadCharacter();

        /// <summary>
        /// Reads the next character from the buffer or underlying stream asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task resulting in the next character.</returns>
        Task<Char> ReadCharacterAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Inserts the given content at the current insertation mark.
        /// The insertation mark won't be changed.
        /// </summary>
        /// <param name="content">The content to insert.</param>
        void InsertText(String content);

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        Char this[Int32 index] { get; }

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or underlying stream, if any.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <returns>The string with the next characters.</returns>
        String ReadCharacters(Int32 characters);

        /// <summary>
        /// Reads the upcoming numbers of characters from the buffer or underlying stream asynchronously.
        /// </summary>
        /// <param name="characters">The number of characters to read.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task that returns the string with the next characters.</returns>
        Task<String> ReadCharactersAsync(Int32 characters, CancellationToken cancellationToken);

        /// <summary>
        /// Prefetches the given number of characters, without changing the index.
        /// </summary>
        /// <param name="length">The number of bytes to prefetch.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The running prefetch task.</returns>
        Task Prefetch(Int32 length, CancellationToken cancellationToken);
    }
}
