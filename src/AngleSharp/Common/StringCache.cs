namespace AngleSharp.Common;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// Hands out one string for equal character runs, for as long as a tree is constructed.
/// </summary>
/// <remarks>
/// A document repeats itself: the same class lists, the same "\n" between two tags, the same
/// data-* names - about two thirds of the attribute values and of the whitespace runs of a
/// real page have already been seen earlier in it. Every one of them would otherwise become a
/// string of its own that lives exactly as long as the tree does, which is what a parse pays
/// the collector for. This is a direct-mapped table rather than a dictionary: a lookup is a
/// hash over at most 20 characters and one comparison, a collision simply replaces the entry,
/// and so a value that never repeats costs little more than the string it needs anyway.
/// </remarks>
internal sealed class StringCache
{
    /// <summary>
    /// The longest run worth looking up. Longer values are rarely repeated.
    /// </summary>
    public const Int32 MaximumLength = 128;

    private const Int32 InitialSize = 64;
    private const Int32 MaximumSize = 4096;

    private String?[] _entries = new String?[InitialSize];
    private Int32 _misses;

    /// <summary>
    /// Gets the string of the given characters, which must be 1 to
    /// <see cref="MaximumLength"/> of them.
    /// </summary>
    public String Get(ReadOnlySpan<Char> chars)
    {
        var entries = _entries;
        var index = Hash(chars) & (entries.Length - 1);
        var cached = entries[index];

        if (cached is not null && cached.Length == chars.Length && chars.SequenceEqual(cached.AsSpan()))
        {
            return cached;
        }

        var created = chars.ToString();

        // A table that has missed as often as it has slots is too small for the document: the
        // entries are dropped rather than rehashed, they are re-added as they come up again.
        if (++_misses == entries.Length && entries.Length < MaximumSize)
        {
            _misses = 0;
            _entries = entries = new String?[entries.Length * 8];
            index = Hash(chars) & (entries.Length - 1);
        }

        entries[index] = created;
        return created;
    }

    private static Int32 Hash(ReadOnlySpan<Char> chars)
    {
        const UInt64 prime = 0x9E3779B97F4A7C15;
        var length = chars.Length;
        var hash = (UInt64)length * prime;

        if (length >= 4)
        {
            // The first and the last 8 characters and 4 from the middle, as overlapping 64-bit
            // reads: all of a run of up to 16, and enough of a longer one to tell apart the
            // values that share a prefix (URLs) or a suffix (file names).
            var bytes = MemoryMarshal.AsBytes(chars);
            var end = bytes.Length - 8;
            hash = (hash ^ MemoryMarshal.Read<UInt64>(bytes)) * prime;
            hash = (hash ^ MemoryMarshal.Read<UInt64>(bytes.Slice(end))) * prime;

            if (length > 8)
            {
                hash = (hash ^ MemoryMarshal.Read<UInt64>(bytes.Slice(8))) * prime;
                hash = (hash ^ MemoryMarshal.Read<UInt64>(bytes.Slice(end - 8))) * prime;

                if (length > 16)
                {
                    hash = (hash ^ MemoryMarshal.Read<UInt64>(bytes.Slice(length & ~1))) * prime;
                }
            }
        }
        else
        {
            for (var i = 0; i < length; i++)
            {
                hash = (hash ^ chars[i]) * prime;
            }
        }

        return (Int32)(hash >> 40);
    }
}
