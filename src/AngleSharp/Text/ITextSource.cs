#nullable disable
namespace AngleSharp.Text;

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Represents a text source that can be read from and written to.
/// </summary>
public interface ITextSource : IReadOnlyTextSource
{
    /// <summary>
    /// Inserts the given content at the current insertation mark. Moves the
    /// insertation mark.
    /// </summary>
    /// <param name="content">The content to insert.</param>
    void InsertText(String content);
}

/// <summary>
/// Represents a text source that can be read from.
/// </summary>
public interface IReadOnlyTextSource : IDisposable
{
    /// <summary>
    /// Gets the full text buffer.
    /// </summary>
    String Text { get; }

    /// <summary>
    /// Gets the length of the text buffer.
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    /// Gets or sets the encoding to use.
    /// </summary>
    Encoding CurrentEncoding { get; set; }

    /// <summary>
    /// Gets or sets the current index of the insertation and read point.
    /// </summary>
    Int32 Index { get; set; }

    /// <summary>
    /// Gets the character at the given position in the text buffer.
    /// </summary>
    /// <param name="index">The index of the character.</param>
    /// <returns>The character.</returns>
    Char this[Int32 index] { get; }

    /// <summary>
    /// Reads the next character from the buffer or underlying stream, if
    /// any.
    /// </summary>
    /// <returns>The next character.</returns>
    Char ReadCharacter();

    /// <summary>
    /// Reads the upcoming numbers of characters from the buffer or
    /// underlying stream, if any.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The string with the next characters.</returns>
    String ReadCharacters(Int32 characters);

    /// <summary>
    /// Reads the upcoming numbers of characters from the buffer or
    /// underlying stream, if any.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The structure which is either materialized string or a reference to Memory of Char</returns>
    StringOrMemory ReadMemory(Int32 characters);

    /// <summary>
    /// Prefetches the number of bytes by expanding the internal buffer.
    /// </summary>
    /// <param name="length">The number of bytes to prefetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The awaitable task.</returns>
    Task PrefetchAsync(Int32 length, CancellationToken cancellationToken);

    /// <summary>
    /// Prefetches the whole stream by expanding the internal buffer.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The awaitable task.</returns>
    Task PrefetchAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the content length, if known.
    /// </summary>
    /// <param name="length">Found length if known</param>
    /// <returns>True if length is available</returns>
    Boolean TryGetContentLength(out Int32 length);
}