namespace AngleSharp.Common;

using System;

/// <summary>
///
/// </summary>
public interface IBuffer : IDisposable
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    IBuffer Append(Char c);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    void Discard();

    /// <summary>
    ///
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    ///
    /// </summary>
    Int32 Capacity { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    IBuffer Remove(Int32 start, Int32 length);

    /// <summary>
    ///
    /// </summary>
    void ReturnToPool();

    /// <summary>
    ///
    /// </summary>
    /// <param name="index"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    IBuffer Insert(Int32 index, Char c);

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    IBuffer Append(ReadOnlySpan<Char> str);

    /// <summary>
    /// Gets the character at the specified index.
    /// </summary>
    /// <param name="i"></param>
    Char this[Int32 i] { get; }

    /// <summary>
    /// Gets the data as a memory.
    /// </summary>
    /// <returns></returns>
    StringOrMemory GetDataAndClear();

    /// <summary>
    ///
    /// </summary>
    /// <param name="test"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison = StringComparison.Ordinal);

    public Boolean HasTextAt(ReadOnlySpan<Char> test, int offset, int length, StringComparison comparison = StringComparison.Ordinal);

    public String ToString();
}