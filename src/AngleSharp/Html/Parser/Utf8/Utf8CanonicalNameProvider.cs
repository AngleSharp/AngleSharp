using System;
using AngleSharp.Dom;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Maps common tokenizer-normalized ASCII names to AngleSharp's existing canonical strings.
/// Hash matches are always confirmed against the original bytes.
/// </summary>
internal static class Utf8CanonicalNameProvider
{
    public static Boolean TryGetTag(ReadOnlySpan<Byte> name, UInt64 hash, out String canonical)
    {
        canonical = hash switch
        {
            0xAF63DC4C8601EC8CUL when name.SequenceEqual("a"u8) => TagNames.A,
            0xAF63ED4C8602096FUL when name.SequenceEqual("p"u8) => TagNames.P,
            0x0BB51791194B4414UL when name.SequenceEqual("code"u8) => TagNames.Code,
            0x08C83407B56AB825UL when name.SequenceEqual("td"u8) => TagNames.Td,
            0x77FD511956A1EDC6UL when name.SequenceEqual("pre"u8) => TagNames.Pre,
            0x08AD3707B553F586UL when name.SequenceEqual("li"u8) => TagNames.Li,
            0x08C83E07B56AC923UL when name.SequenceEqual("tr"u8) => TagNames.Tr,
            0xCAA83A18F46E5888UL when name.SequenceEqual("div"u8) => TagNames.Div,
            0x690418194ED15D3EUL when name.SequenceEqual("var"u8) => TagNames.Var,
            0x08915407B53BAC15UL when name.SequenceEqual("dd"u8) => TagNames.Dd,
            0x08A64607B54DF055UL when name.SequenceEqual("br"u8) => TagNames.Br,
            0x08914407B53B90E5UL when name.SequenceEqual("dt"u8) => TagNames.Dt,
            0x08C45607B5670914UL when name.SequenceEqual("ul"u8) => TagNames.Ul,
            0x08C83807B56ABEF1UL when name.SequenceEqual("th"u8) => TagNames.Th,
            0x08BA8607B55F061FUL when name.SequenceEqual("h2"u8) => TagNames.H2,
            0x08BA8407B55F02B9UL when name.SequenceEqual("h4"u8) => TagNames.H4,
            0x08BA8307B55F0106UL when name.SequenceEqual("h5"u8) => TagNames.H5,
            0xA5E9F6D91985A3DAUL when name.SequenceEqual("strong"u8) => TagNames.Strong,
            0xAF63DF4C8601F1A5UL when name.SequenceEqual("b"u8) => TagNames.B,
            0x08BA8507B55F046CUL when name.SequenceEqual("h3"u8) => TagNames.H3,
            0xF9DAA9910F08943EUL when name.SequenceEqual("cite"u8) => TagNames.Cite,
            0x088E3707B53944F7UL when name.SequenceEqual("em"u8) => TagNames.Em,
            0x8B7DC019093CD0E1UL when name.SequenceEqual("span"u8) => TagNames.Span,
            0xCA972418F45FBFF3UL when name.SequenceEqual("dfn"u8) => TagNames.Dfn,
            0x888BB1CC15EF7930UL when name.SequenceEqual("html"u8) => TagNames.Html,
            0x0A8F12CC5F9A0C03UL when name.SequenceEqual("head"u8) => TagNames.Head,
            0xCD4DE79BC6C93295UL when name.SequenceEqual("body"u8) => TagNames.Body,
            0x4320E9A2E32EAC38UL when name.SequenceEqual("meta"u8) => TagNames.Meta,
            0xDA31296C0C1B6029UL when name.SequenceEqual("title"u8) => TagNames.Title,
            0xACFC82293C04634AUL when name.SequenceEqual("script"u8) => TagNames.Script,
            0xBF7282ADBC7013F6UL when name.SequenceEqual("style"u8) => TagNames.Style,
            0x77203729B376A83FUL when name.SequenceEqual("table"u8) => TagNames.Table,
            0xE1CB381F1F501FABUL when name.SequenceEqual("tbody"u8) => TagNames.Tbody,
            0xEB218F725DDD9B79UL when name.SequenceEqual("thead"u8) => TagNames.Thead,
            0xE4444542747391BBUL when name.SequenceEqual("tfoot"u8) => TagNames.Tfoot,
            0xDD1D0F790C2F1BE7UL when name.SequenceEqual("form"u8) => TagNames.Form,
            0x1EBBAE8F5810B65BUL when name.SequenceEqual("input"u8) => TagNames.Input,
            0x2B9CEE192BD27584UL when name.SequenceEqual("img"u8) => TagNames.Img,
            0xBF4B9BAD694F4809UL when name.SequenceEqual("link"u8) => TagNames.Link,
            _ => null!,
        };

        return canonical is not null;
    }

    public static Boolean TryGetAttribute(ReadOnlySpan<Byte> name, out String canonical)
    {
        var hash = Utf8NameHash.Compute(name);
        canonical = hash switch
        {
            0xD11655952FCBAB9FUL when name.SequenceEqual("class"u8) => AttributeNames.Class,
            0x9AB8EDCC20799138UL when name.SequenceEqual("href"u8) => AttributeNames.Href,
            0xC4BCADBA8E631B86UL when name.SequenceEqual("name"u8) => AttributeNames.Name,
            0xDA31296C0C1B6029UL when name.SequenceEqual("title"u8) => AttributeNames.Title,
            0xE6F0A3190519E83CUL when name.SequenceEqual("alt"u8) => AttributeNames.Alt,
            0x825994195CFB21C9UL when name.SequenceEqual("src"u8) => AttributeNames.Src,
            0xAFDCEBFFFA777F55UL when name.SequenceEqual("colspan"u8) => AttributeNames.ColSpan,
            0xBF7282ADBC7013F6UL when name.SequenceEqual("style"u8) => AttributeNames.Style,
            0x08B72E07B55C3AC0UL when name.SequenceEqual("id"u8) => AttributeNames.Id,
            0xA79439EF7BFA9C2DUL when name.SequenceEqual("type"u8) => AttributeNames.Type,
            0x17720BF67D347222UL when name.SequenceEqual("height"u8) => AttributeNames.Height,
            0x0460DFAD9060B275UL when name.SequenceEqual("lang"u8) => AttributeNames.Lang,
            0xDBDACD932FD1E9BFUL when name.SequenceEqual("width"u8) => AttributeNames.Width,
            0x89E9C61960F4CFB4UL when name.SequenceEqual("rel"u8) => AttributeNames.Rel,
            0x7CE4FD9430E80CEAUL when name.SequenceEqual("value"u8) => AttributeNames.Value,
            0x420C75B526B35282UL when name.SequenceEqual("content"u8) => AttributeNames.Content,
            _ => null!,
        };

        return canonical is not null;
    }
}
