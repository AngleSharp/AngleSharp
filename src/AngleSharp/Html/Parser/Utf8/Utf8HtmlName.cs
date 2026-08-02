#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// A callback-scoped HTML name retaining its source spelling while exposing its
/// ASCII case-insensitive semantic identity.
/// </summary>
public readonly ref struct Utf8HtmlName
{
    private readonly ref Utf8HtmlNameIdentityCache _cache;

    internal Utf8HtmlName(ReadOnlySpan<Byte> verbatim, ref Utf8HtmlNameIdentityCache cache)
    {
        Verbatim = verbatim;
        _cache = ref cache;
    }

    /// <summary>Gets the exact source spelling of the name.</summary>
    public ReadOnlySpan<Byte> Verbatim { get; }

    /// <summary>Gets the cached hash of the ASCII case-folded semantic name.</summary>
    public UInt64 SemanticHash
    {
        get => _cache.GetOrCompute(Verbatim);
    }

    /// <summary>
    /// Tries to encode an ASCII-alpha tag name, optionally containing digits 1 through 6,
    /// into an exact case-insensitive key. Returns false when the name must instead be
    /// compared using its verbatim bytes.
    /// </summary>
    public Boolean TryGetCompactKey(out UInt64 key) => _cache.TryGetCompactKey(Verbatim, out key);

    /// <summary>
    /// Tries to encode an ASCII-alpha tag name, optionally containing digits 1 through 6,
    /// into an exact case-insensitive key.
    /// </summary>
    public static Boolean TryGetCompactKey(ReadOnlySpan<Byte> name, out UInt64 key)
    {
        var value = 0UL;
        if (name.IsEmpty || name.Length > 12 || !IsAsciiAlpha(name[0]))
        {
            key = 0;
            return false;
        }

        for (var index = 0; index < name.Length; index++)
        {
            if ((value >> 59) != 0)
            {
                key = 0;
                return false;
            }

            var input = name[index];
            UInt64 symbol;
            if (IsAsciiAlpha(input))
            {
                symbol = ((UInt64)input & 0x1FUL) + 5;
            }
            else if (input is >= (Byte)'1' and <= (Byte)'6')
            {
                symbol = (UInt64)(input - (Byte)'1');
            }
            else
            {
                key = 0;
                return false;
            }

            value = (value << 5) | symbol;
        }

        key = value;
        return true;
    }

    /// <summary>Compares this name using HTML ASCII case-insensitive semantics.</summary>
    public Boolean SemanticEquals(ReadOnlySpan<Byte> expected) =>
        Verbatim.Length < Vector128<Byte>.Count
            ? EqualsAsciiIgnoreCaseScalar(Verbatim, expected, 0)
            : EqualsAsciiIgnoreCase(Verbatim, expected);

    private static Boolean EqualsAsciiIgnoreCase(ReadOnlySpan<Byte> left, ReadOnlySpan<Byte> right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        var index = 0;
        if (Sse2.IsSupported)
        {
            var leftFirst = left[0];
            var rightFirst = right[0];
            if (leftFirst != rightFirst)
            {
                var leftFirstFolded = (Byte)(leftFirst | 0x20);
                if (
                    (UInt32)(leftFirstFolded - (Byte)'a') > (Byte)'z' - (Byte)'a'
                    || leftFirstFolded != (Byte)(rightFirst | 0x20)
                )
                    return false;
            }

            index = CompareVector128(left, right);
            if (index < 0)
                return false;
        }

        return EqualsAsciiIgnoreCaseScalar(left, right, index);
    }

    private static Boolean EqualsAsciiIgnoreCaseScalar(
        ReadOnlySpan<Byte> left,
        ReadOnlySpan<Byte> right,
        Int32 index
    )
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (; index < left.Length; index++)
        {
            var leftValue = left[index];
            var rightValue = right[index];
            if (leftValue == rightValue)
            {
                continue;
            }

            var leftFolded = (Byte)(leftValue | 0x20);
            if (
                (UInt32)(leftFolded - (Byte)'a') > (Byte)'z' - (Byte)'a'
                || leftFolded != (Byte)(rightValue | 0x20)
            )
            {
                return false;
            }
        }

        return true;
    }

    // Keep the intrinsic block out of the inlineable scalar path used by short HTML names.
    // A negative result denotes a mismatch; otherwise it is the first unprocessed byte.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Int32 CompareVector128(ReadOnlySpan<Byte> left, ReadOnlySpan<Byte> right)
    {
        var exactDifference = Vector128<Byte>.Zero;
        var asciiCaseDifference = Vector128.Create((Byte)0x20);
        var belowA = Vector128.Create((SByte)('a' - 1));
        var aboveZ = Vector128.Create((SByte)('z' + 1));
        ref var leftReference = ref MemoryMarshal.GetReference(left);
        ref var rightReference = ref MemoryMarshal.GetReference(right);
        var vectorEnd = left.Length - Vector128<Byte>.Count;
        var index = 0;

        do
        {
            var leftVector = Vector128.LoadUnsafe(ref leftReference, (UIntPtr)index);
            var rightVector = Vector128.LoadUnsafe(ref rightReference, (UIntPtr)index);
            var difference = Sse2.Xor(leftVector, rightVector);
            var exact = Sse2.CompareEqual(difference, exactDifference);
            var differsOnlyByCase = Sse2.CompareEqual(difference, asciiCaseDifference);
            var foldedLeft = Sse2.Or(leftVector, asciiCaseDifference).AsSByte();
            var atLeastA = Sse2.CompareGreaterThan(foldedLeft, belowA);
            var atMostZ = Sse2.CompareGreaterThan(aboveZ, foldedLeft);
            var validCaseDifference = Sse2.And(
                differsOnlyByCase,
                Sse2.And(atLeastA, atMostZ).AsByte()
            );
            if (Sse2.MoveMask(Sse2.Or(exact, validCaseDifference)) != 0xFFFF)
                return -1;

            index += Vector128<Byte>.Count;
        } while (index <= vectorEnd);

        return index;
    }

    private static Boolean IsAsciiAlpha(Byte value) =>
        (UInt32)((value | 0x20) - (Byte)'a') <= (Byte)'z' - (Byte)'a';
}

internal struct Utf8HtmlNameIdentityCache
{
    private const UInt64 UnavailableCompactKey = UInt64.MaxValue;
    private UInt64 _semanticHash;
    private UInt64 _compactKey;

    public readonly UInt64 Value => _semanticHash;

    internal readonly UInt64 CompactValue => _compactKey;

    public UInt64 GetOrCompute(ReadOnlySpan<Byte> verbatim)
    {
        if (_semanticHash == 0)
        {
            _semanticHash = Utf8NameHash.ComputeSemantic(verbatim);
        }

        return _semanticHash;
    }

    public Boolean TryGetCompactKey(ReadOnlySpan<Byte> verbatim, out UInt64 key)
    {
        if (_compactKey == 0)
        {
            _compactKey = Utf8HtmlName.TryGetCompactKey(verbatim, out key)
                ? key
                : UnavailableCompactKey;
        }

        key = _compactKey;
        return key != UnavailableCompactKey;
    }

    public void Reset()
    {
        _semanticHash = 0;
        _compactKey = 0;
    }
}
