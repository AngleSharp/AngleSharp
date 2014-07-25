namespace AngleSharp
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The interface for streaming through source codes.
    /// </summary>
    interface ITextSource
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
        /// Reads the next character from the buffer or underlying stream asynchronously, if any.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task resulting in the next character.</returns>
        Task<Char> ReadCharacterAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the character at the given position in the text buffer.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character.</returns>
        Char this[Int32 index] { get; }
    }
}
