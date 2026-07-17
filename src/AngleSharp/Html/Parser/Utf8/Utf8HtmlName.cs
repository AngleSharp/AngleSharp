#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// A callback-scoped HTML name retaining its source spelling while exposing its
/// ASCII case-insensitive semantic identity.
/// </summary>
public readonly ref struct Utf8HtmlName
{
    private readonly ref Utf8HtmlNameHashCache _cache;

    internal Utf8HtmlName(ReadOnlySpan<Byte> verbatim, ref Utf8HtmlNameHashCache cache)
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
            if (Utf8NameHash.ToLowerAscii(left[index]) != Utf8NameHash.ToLowerAscii(right[index]))
            {
                return false;
            }
        }

        return true;
    }
}

internal struct Utf8HtmlNameHashCache
{
    private UInt64 _semanticHash;

    public readonly UInt64 Value => _semanticHash;

    public UInt64 GetOrCompute(ReadOnlySpan<Byte> verbatim)
    {
        if (_semanticHash == 0)
        {
            _semanticHash = Utf8NameHash.ComputeSemantic(verbatim);
        }

        return _semanticHash;
    }

    public void Reset() => _semanticHash = 0;
}
