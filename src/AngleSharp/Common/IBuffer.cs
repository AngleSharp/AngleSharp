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
    IBuffer Clear();

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
    /// <param name="offset"></param>
    /// <param name="dest"></param>
    /// <param name="length"></param>
    void CopyTo(Int32 offset, Span<Char> dest, Int32 length);

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
    StringOrMemory GetData();

    /// <summary>
    ///
    /// </summary>
    /// <param name="test"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison);

    /// <summary>
    ///
    /// </summary>
    /// <param name="test"></param>
    /// <param name="comparison"></param>
    /// <returns></returns>
    public Boolean Has(CheckBuffer test, StringComparison comparison);
}

/// <summary>
///
/// </summary>
public delegate Boolean CheckBuffer(ReadOnlySpan<Char> arg);