namespace AngleSharp.Html;

using System;
using System.Collections.Generic;
using Common;

internal class OrdinalStringOrMemoryComparer : IEqualityComparer<StringOrMemory>
{
    /// <summary>
    /// Gets the default instance of the comparer.
    /// </summary>
    public static OrdinalStringOrMemoryComparer Instance { get; } = new();

    /// <inheritdoc/>
    public Int32 GetHashCode(StringOrMemory obj) => obj.GetHashCode();

    /// <inheritdoc/>
    public Boolean Equals(StringOrMemory x, StringOrMemory y) => x.Equals(y);
}