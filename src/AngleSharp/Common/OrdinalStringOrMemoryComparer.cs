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
    public Int32 GetHashCode(StringOrMemory obj)
    {
        return obj.GetHashCode();
    }

    /// <inheritdoc/>
    public Boolean Equals(StringOrMemory x, StringOrMemory y)
    {
        return x.Equals(y);
    }
}

/// <summary>
/// Compares like <see cref="OrdinalStringOrMemoryComparer"/>, with a hash that looks at four
/// places of a name rather than at all of it. Only for the fixed tables of well-known names:
/// the tree builder asks several of them about every tag, and - unlike a table that takes
/// its keys from a document - nothing can fill them with names made to collide.
/// </summary>
internal sealed class WellKnownNameComparer : IEqualityComparer<StringOrMemory>
{
    /// <summary>
    /// Gets the default instance of the comparer.
    /// </summary>
    public static WellKnownNameComparer Instance { get; } = new();

    /// <inheritdoc/>
    public Int32 GetHashCode(StringOrMemory obj)
    {
        var name = obj.Memory.Span;
        var length = name.Length;

        if (length == 0)
        {
            return 0;
        }

        return length ^ (name[0] << 5) ^ (name[length >> 1] << 11) ^ (name[length - 1] << 17);
    }

    /// <inheritdoc/>
    public Boolean Equals(StringOrMemory x, StringOrMemory y)
    {
        return x.Equals(y);
    }
}
