#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Synchronous borrowed views over tokenizer-owned or PipeReader-owned UTF-8. Every span is valid only for the duration
/// of its callback. The split start-tag callbacks let a construction sink collect only attributes it needs.
/// </summary>
public interface IUtf8HtmlTokenSink
{
    void Text(ReadOnlySpan<Byte> utf8);

    void StartTag(ReadOnlySpan<Byte> name);

    void Attribute(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> value);

    void StartTagEnd(Boolean selfClosing);

    void EndTag(ReadOnlySpan<Byte> name);

    void Comment(ReadOnlySpan<Byte> utf8) { }

    void Doctype(ReadOnlySpan<Byte> utf8) { }

    void Doctype(in Utf8DoctypeToken token) => Doctype(token.Name);

    void EndOfFile() { }
}
