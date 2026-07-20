#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

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
        EqualsAsciiIgnoreCase(Verbatim, expected);

    private static Boolean EqualsAsciiIgnoreCase(ReadOnlySpan<Byte> left, ReadOnlySpan<Byte> right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        for (var index = 0; index < left.Length; index++)
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
