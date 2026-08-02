using System;
using AngleSharp.Dom;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Maps standard HTML semantic names to AngleSharp's existing canonical strings.
/// Generated tag lookups confirm the complete ASCII name without allocating or hashing.
/// </summary>
internal static partial class Utf8CanonicalNameProvider
{
    private const UInt64 Alt = 0x0000000000001A39UL;
    private const UInt64 Class = 0x0000000000889B18UL;
    private const UInt64 ColSpan = 0x00000002291C54D3UL;
    private const UInt64 Content = 0x00000002293CAA79UL;
    private const UInt64 Height = 0x000000001AA731B9UL;
    private const UInt64 Href = 0x000000000006DD4BUL;
    private const UInt64 Id = 0x00000000000001C9UL;
    private const UInt64 Lang = 0x0000000000089A6CUL;
    private const UInt64 Name = 0x0000000000099A4AUL;
    private const UInt64 Rel = 0x0000000000005D51UL;
    private const UInt64 Src = 0x00000000000062E8UL;
    private const UInt64 Style = 0x00000000018CFA2AUL;
    private const UInt64 Title = 0x000000000197662AUL;
    private const UInt64 Type = 0x00000000000CFAAAUL;
    private const UInt64 Value = 0x0000000001B3474AUL;
    private const UInt64 Width = 0x0000000001C7272DUL;

    public static Boolean TryGetTag(Utf8HtmlName name, out String canonical) =>
        TryGetHtmlTag(name, out canonical);

    public static Boolean TryGetAttribute(ReadOnlySpan<Byte> name, out String canonical)
    {
        var cache = default(Utf8HtmlNameIdentityCache);
        return TryGetAttribute(new Utf8HtmlName(name, ref cache), out canonical);
    }

    public static Boolean TryGetAttribute(Utf8HtmlName name, out String canonical)
    {
        if (!name.TryGetCompactKey(out var key))
        {
            canonical = null!;
            return false;
        }

        canonical = key switch
        {
            Class => AttributeNames.Class,
            Href => AttributeNames.Href,
            Name => AttributeNames.Name,
            Title => AttributeNames.Title,
            Alt => AttributeNames.Alt,
            Src => AttributeNames.Src,
            ColSpan => AttributeNames.ColSpan,
            Style => AttributeNames.Style,
            Id => AttributeNames.Id,
            Type => AttributeNames.Type,
            Height => AttributeNames.Height,
            Lang => AttributeNames.Lang,
            Width => AttributeNames.Width,
            Rel => AttributeNames.Rel,
            Value => AttributeNames.Value,
            Content => AttributeNames.Content,
            _ => null!,
        };

        return canonical is not null;
    }
}
