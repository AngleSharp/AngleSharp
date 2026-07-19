#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

[Flags]
public enum Utf8HtmlStartTagCapture : byte
{
    None = 0,
    Attributes = 1,
}

/// <summary>
/// Synchronous borrowed views over tokenizer-owned or PipeReader-owned UTF-8. Every span is valid only for the duration
/// of its callback. The split start-tag callbacks let a construction sink collect only attributes it needs.
/// </summary>
public interface IUtf8HtmlTokenSink
{
    void Text(ReadOnlySpan<Byte> utf8);

    Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name);

    void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value);

    void StartTagEnd(Boolean selfClosing);

    void EndTag(Utf8HtmlName name);

    void Comment(ReadOnlySpan<Byte> utf8) { }

    void Doctype(ReadOnlySpan<Byte> utf8) { }

    void Doctype(in Utf8DoctypeToken token) => Doctype(token.Name);

    Boolean WantsAttribute(Utf8HtmlName name);

    void EndOfFile() { }
}

/// <summary>
/// Opt-in capability for sinks that need the half-open normalized UTF-8 byte range of each start tag.
/// The range callback immediately precedes <see cref="IUtf8HtmlTokenSink.StartTagEnd"/>.
/// </summary>
public interface IUtf8HtmlStartTagSourceRangeSink
{
    Boolean WantsStartTagSourceRanges { get; }

    void StartTagSourceRange(Int64 sourceStart, Int64 sourceEnd);
}

/// <summary>
/// Internal compatibility-lane capability for comments whose payload should not be materialized
/// in the tokenizer's contiguous scratch buffer.
/// </summary>
internal interface IUtf8HtmlStreamingCommentSink
{
    Boolean BeginComment();

    void CommentChunk(ReadOnlySpan<Byte> utf8);

    void EndComment();
}
