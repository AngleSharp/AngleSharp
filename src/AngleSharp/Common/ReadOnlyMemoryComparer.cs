namespace AngleSharp.Html;

using System;
using System.Collections.Generic;
using Common;

/// <summary>
///
/// </summary>
public class ReadOnlyMemoryComparer : IEqualityComparer<ReadOnlyMemory<Char>>
{
    /// <summary>
    ///
    /// </summary>
    public static ReadOnlyMemoryComparer Ordinal { get; } = new ReadOnlyMemoryComparer();

    /// <summary>
    ///
    /// </summary>
    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Int32 GetHashCode(ReadOnlyMemory<Char> obj ) => String.GetHashCode(obj.Span);
    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Boolean Equals(ReadOnlyMemory<Char> x, ReadOnlyMemory<Char> y) => x.Span.SequenceEqual(y.Span);
}

public class StringOrMemoryComparer : IEqualityComparer<StringOrMemory>
{
    /// <summary>
    ///
    /// </summary>
    public static StringOrMemoryComparer Ordinal { get; } = new StringOrMemoryComparer();

    /// <summary>
    ///
    /// </summary>
    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Int32 GetHashCode(StringOrMemory obj ) => String.GetHashCode(obj.Memory.Span);
    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Boolean Equals(StringOrMemory x, StringOrMemory y) => x.Memory.Span.SequenceEqual(y.Memory.Span);
}