using System;
using System.Runtime.CompilerServices;

namespace AngleSharp.Html.Parser.Utf8;

#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

public static class Utf8NameHash
{
    public const UInt64 Offset = 14695981039346656037;
    private const UInt64 Prime = 1099511628211;

    internal static UInt64 Append(UInt64 hash, Byte value) => (hash ^ value) * Prime;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Byte ToLowerAscii(Byte value) =>
        (UInt32)(value - (Byte)'A') <= 'Z' - 'A' ? (Byte)(value | 0x20) : value;

    internal static UInt64 Append(UInt64 hash, ReadOnlySpan<Byte> value)
    {
        foreach (var character in value)
        {
            hash = (hash ^ character) * Prime;
        }

        return hash;
    }

    internal static UInt64 Compute(ReadOnlySpan<Byte> value) => Append(Offset, value);

    public static UInt64 ComputeSemantic(ReadOnlySpan<Byte> value)
    {
        var hash = Offset;
        foreach (var character in value)
        {
            hash = Append(hash, ToLowerAscii(character));
        }

        return hash;
    }

    internal static UInt64 ComputeSemanticWithUppercasePrescan(ReadOnlySpan<Byte> value)
    {
        if (value.IndexOfAnyInRange((Byte)'A', (Byte)'Z') < 0)
        {
            return Compute(value);
        }

        return ComputeSemantic(value);
    }
}
