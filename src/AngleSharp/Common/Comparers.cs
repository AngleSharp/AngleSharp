namespace AngleSharp.Html;

using System;
using System.Collections.Generic;
using Common;

internal class OrdinalStringOrMemoryComparer : IEqualityComparer<StringOrMemory>
{
    /// <summary>
    ///
    /// </summary>
    public static OrdinalStringOrMemoryComparer Instance { get; } = new();

    /// <summary>
    ///
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
    public Int32 GetHashCode(StringOrMemory obj) => String.GetHashCode(obj.Memory.Span);
#else
    public Int32 GetHashCode(StringOrMemory obj) => obj.String.GetHashCode();
#endif

    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Boolean Equals(StringOrMemory x, StringOrMemory y) => x.Memory.Span.SequenceEqual(y.Memory.Span);
}