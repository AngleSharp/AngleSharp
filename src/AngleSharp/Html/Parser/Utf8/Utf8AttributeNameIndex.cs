#pragma warning disable CS1591 // Experimental implementation detail; shape is intentionally unsettled.

using System;
using System.Buffers;

namespace AngleSharp.Html.Parser.Utf8;

internal static class Utf8AttributeNameIndex
{
    private const Int32 MinimumSlotCount = 31;

    public static void Initialize(
        ref Entry[]? entries,
        ReadOnlySpan<Byte> seenNames,
        Int32 count
    )
    {
        Reset(ref entries);
        entries = RentCleared(CapacityForCount(count + 1));

        var offset = 0;
        for (var index = 0; index < count; index++)
        {
            var remaining = seenNames.Slice(offset);
            var length = remaining.IndexOf((Byte)0);
            if (length < 0)
            {
                throw new InvalidOperationException("Attribute-name index source is incomplete.");
            }

            Insert(entries, Utf8NameHash.ComputeSemantic(remaining.Slice(0, length)), offset);
            offset += length + 1;
        }
    }

    public static Boolean Contains(
        Entry[] entries,
        Utf8HtmlName name,
        ReadOnlySpan<Byte> seenNames
    )
    {
        var hash = name.SemanticHash;
        var slotCount = entries.Length - 1;
        var slot = Slot(hash, slotCount);
        while (true)
        {
            ref readonly var entry = ref entries[slot];
            if (entry.OffsetPlusOne == 0)
            {
                return false;
            }

            if (entry.Hash == hash)
            {
                var remaining = seenNames.Slice(entry.OffsetPlusOne - 1);
                var length = remaining.IndexOf((Byte)0);
                if (length >= 0 && name.SemanticEquals(remaining.Slice(0, length)))
                {
                    return true;
                }
            }

            slot = NextSlot(slot, slotCount);
        }
    }

    public static void Add(ref Entry[]? entries, UInt64 hash, Int32 offset)
    {
        var current = entries ?? throw new InvalidOperationException(
            "Attribute-name index is not initialized."
        );
        var count = current[0].OffsetPlusOne;
        var slotCount = current.Length - 1;
        if ((Int64)(count + 1) * 4 > (Int64)slotCount * 3)
        {
            Grow(ref entries);
        }

        Insert(entries!, hash, offset);
    }

    public static void Reset(ref Entry[]? entries)
    {
        var rented = entries;
        if (rented is not null)
        {
            ArrayPool<Entry>.Shared.Return(rented);
            entries = null;
        }
    }

    private static void Grow(ref Entry[]? entries)
    {
        var oldEntries = entries!;
        var newEntries = RentCleared(checked(oldEntries.Length * 2));

        for (var index = 1; index < oldEntries.Length; index++)
        {
            var entry = oldEntries[index];
            if (entry.OffsetPlusOne != 0)
            {
                Insert(newEntries, entry.Hash, entry.OffsetPlusOne - 1);
            }
        }

        entries = newEntries;
        ArrayPool<Entry>.Shared.Return(oldEntries);
    }

    private static void Insert(Entry[] entries, UInt64 hash, Int32 offset)
    {
        var slotCount = entries.Length - 1;
        var slot = Slot(hash, slotCount);
        while (entries[slot].OffsetPlusOne != 0)
        {
            slot = NextSlot(slot, slotCount);
        }

        entries[slot] = new Entry(hash, checked(offset + 1));
        entries[0].OffsetPlusOne++;
    }

    private static Entry[] RentCleared(Int32 minimumLength)
    {
        var entries = ArrayPool<Entry>.Shared.Rent(minimumLength);
        entries.AsSpan().Clear();
        return entries;
    }

    private static Int32 CapacityForCount(Int32 count)
    {
        var requiredSlots = Math.Max(
            MinimumSlotCount,
            (Int32)(((Int64)count * 4 + 2) / 3)
        );
        return checked(requiredSlots + 1);
    }

    private static Int32 Slot(UInt64 hash, Int32 slotCount) =>
        1 + (Int32)((hash ^ (hash >> 32)) % (UInt64)slotCount);

    private static Int32 NextSlot(Int32 slot, Int32 slotCount) =>
        slot == slotCount ? 1 : slot + 1;

    internal struct Entry(UInt64 hash, Int32 offsetPlusOne)
    {
        public readonly UInt64 Hash = hash;

        public Int32 OffsetPlusOne = offsetPlusOne;
    }
}
