using System;
using AngleSharp.Dom;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Maps standard HTML semantic names to AngleSharp's existing canonical strings.
/// Generated tag lookups confirm the complete ASCII name without allocating or hashing.
/// </summary>
internal static partial class Utf8CanonicalNameProvider
{
    public static Boolean TryGetTag(Utf8HtmlName name, out String canonical) =>
        TryGetHtmlTag(name, out canonical);

    public static Boolean TryGetAttribute(ReadOnlySpan<Byte> name, out String canonical)
    {
        var cache = default(Utf8HtmlNameHashCache);
        return TryGetAttribute(new Utf8HtmlName(name, ref cache), out canonical);
    }

    public static Boolean TryGetAttribute(Utf8HtmlName name, out String canonical)
    {
        canonical = name.SemanticHash switch
        {
            0xD11655952FCBAB9FUL => AttributeNames.Class,
            0x9AB8EDCC20799138UL => AttributeNames.Href,
            0xC4BCADBA8E631B86UL => AttributeNames.Name,
            0xDA31296C0C1B6029UL => AttributeNames.Title,
            0xE6F0A3190519E83CUL => AttributeNames.Alt,
            0x825994195CFB21C9UL => AttributeNames.Src,
            0xAFDCEBFFFA777F55UL => AttributeNames.ColSpan,
            0xBF7282ADBC7013F6UL => AttributeNames.Style,
            0x08B72E07B55C3AC0UL => AttributeNames.Id,
            0xA79439EF7BFA9C2DUL => AttributeNames.Type,
            0x17720BF67D347222UL => AttributeNames.Height,
            0x0460DFAD9060B275UL => AttributeNames.Lang,
            0xDBDACD932FD1E9BFUL => AttributeNames.Width,
            0x89E9C61960F4CFB4UL => AttributeNames.Rel,
            0x7CE4FD9430E80CEAUL => AttributeNames.Value,
            0x420C75B526B35282UL => AttributeNames.Content,
            _ => null!,
        };

        return canonical is not null && SemanticEquals(name.Verbatim, canonical);
    }

    private static Boolean SemanticEquals(ReadOnlySpan<Byte> verbatim, String semantic)
    {
        if (verbatim.Length != semantic.Length)
        {
            return false;
        }

        for (var index = 0; index < verbatim.Length; index++)
        {
            if (
                Utf8NameHash.ToLowerAscii(verbatim[index])
                != Utf8NameHash.ToLowerAscii((Byte)semantic[index])
            )
            {
                return false;
            }
        }

        return true;
    }
}
