namespace AngleSharp.Common;

using System;
using System.Collections.Generic;
using System.IO;
internal static class Shims
{
    public static HashSet<StringOrMemory> ToHashSet(this IEnumerable<StringOrMemory> items, IEqualityComparer<StringOrMemory>? comparer = null)
    {
        return new HashSet<StringOrMemory>(items, comparer);
    }

#if !NETSTANDARD2_1_OR_GREATER && !NETCOREAPP2_1_OR_GREATER
    public static void Write(this TextWriter writer, ReadOnlySpan<Char> buffer)
    {
        for (var i = 0; i < buffer.Length; i++)
        {
            writer.Write(buffer[i]);
        }
    }
#endif
}
