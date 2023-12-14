namespace AngleSharp.Html;

using System;
using System.Collections.Generic;

/// <summary>
///
/// </summary>
public class MemStringEqualityComparer : IEqualityComparer<ReadOnlyMemory<Char>>
{
    /// <summary>
    ///
    /// </summary>
    public static MemStringEqualityComparer Instance { get; } = new MemStringEqualityComparer();

    /// <summary>
    ///
    /// </summary>
    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public Int32 GetHashCode( ReadOnlyMemory<Char> obj ) => String.GetHashCode( obj.Span );
    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Boolean Equals(ReadOnlyMemory<Char> x, ReadOnlyMemory<Char> y) =>
        x.Span.SequenceEqual(y.Span);
}