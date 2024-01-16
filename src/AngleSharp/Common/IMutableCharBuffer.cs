namespace AngleSharp.Common;

using System;

/// <summary>
/// Represents a sequence of characters.
/// </summary>
public interface ICharBuffer
{
    /// <summary>
    /// Gets the length of the buffer.
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    /// Returns the character at the given index.
    /// </summary>
    Char this[Int32 i] { get; }

    /// <summary>
    ///
    /// </summary>
    ReadOnlyMemory<Char>? TryCopyTo(Char[] buffer);
}

/// <summary>
/// Represents a mutable sequence of characters.
/// </summary>
internal interface IMutableCharBuffer : ICharBuffer, IDisposable
{
    /// <summary>
    /// Appends the given character to the buffer.
    /// </summary>
    void Append(Char c);

    /// <summary>
    /// Clears the buffer.
    /// </summary>
    void Discard();

    /// <summary>
    /// Current capacity of the buffer.
    /// </summary>
    Int32 Capacity { get; }

    /// <summary>
    /// Removes the given amount of characters from the buffer.
    /// </summary>
    IMutableCharBuffer Remove(Int32 start, Int32 length);

    /// <summary>
    /// Returns the buffer to the pool.
    /// </summary>
    void ReturnToPool();

    /// <summary>
    /// Inserts the given character at the specified index.
    /// </summary>
    IMutableCharBuffer Insert(Int32 index, Char c);

    /// <summary>
    /// Appends the given char span to the buffer.
    /// </summary>
    IMutableCharBuffer Append(ReadOnlySpan<Char> str);

    /// <summary>
    /// Materializes the buffer to a <see cref="StringOrMemory"/> instance and resets it
    /// </summary>
    /// <returns></returns>
    StringOrMemory GetDataAndClear();

    /// <summary>
    /// Checks if the buffer contains the given text.
    /// </summary>
    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison = StringComparison.Ordinal);

    /// <summary>
    /// Checks if the buffer contains the given text at the specified offset.
    /// </summary>
    public Boolean HasTextAt(ReadOnlySpan<Char> test, Int32 offset, Int32 length, StringComparison comparison = StringComparison.Ordinal);

    /// <summary>
    /// Creates a CLR String instance from the buffer.
    /// </summary>
    public String ToString();
}