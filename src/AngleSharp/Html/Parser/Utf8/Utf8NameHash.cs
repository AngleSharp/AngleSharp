using System;

namespace AngleSharp.Html.Parser.Utf8;

#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

public static class Utf8NameHash
{
    public const UInt64 Offset = 14695981039346656037;
    private const UInt64 Prime = 1099511628211;

    public static UInt64 Append(UInt64 hash, Byte value) => (hash ^ value) * Prime;

    public static UInt64 Append(UInt64 hash, ReadOnlySpan<Byte> value)
    {
        foreach (var character in value)
        {
            hash = Append(hash, character);
        }

        return hash;
    }

    public static UInt64 Compute(ReadOnlySpan<Byte> value) => Append(Offset, value);
}
