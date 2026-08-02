#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

[Flags]
public enum Utf8HtmlStartTagCapture : byte
{
    None = 0,
    Attributes = 1,
}

[Flags]
public enum Utf8HtmlTokenCapture : byte
{
    None = 0,
    Text = 1,
}

/// <summary>
/// Synchronous borrowed views over tokenizer-owned or PipeReader-owned UTF-8. Every span is valid only for the duration
/// of its callback. The split start-tag callbacks let a construction sink collect only attributes it needs.
/// </summary>
public interface IUtf8HtmlTokenSink
{
    Utf8HtmlTokenCapture Capture { get; }

    void Text(ReadOnlySpan<Byte> utf8);

    Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name);

    void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value);

    void StartTagEnd(Boolean selfClosing);

    void EndTag(Utf8HtmlName name);

    void Comment(ReadOnlySpan<Byte> utf8) { }

    void ProcessingInstruction(ReadOnlySpan<Byte> utf8) => Comment(utf8);

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

    void ObserveNormalizedUtf8(Int64 sourceStart, ReadOnlySpan<Byte> utf8) { }

    void StartTagSourceRange(Int64 sourceStart, Int64 sourceEnd);
}

/// <summary>
/// Optional streaming comment capability. Implement this interface to consume comment payloads incrementally or to
/// decline them from <see cref="BeginComment"/> without materializing the complete payload in tokenizer scratch.
/// </summary>
public interface IUtf8HtmlStreamingCommentSink
{
    /// <summary>Returns whether the payload of the next comment should be delivered.</summary>
    Boolean BeginComment();

    /// <summary>Consumes one complete, callback-scoped UTF-8 comment payload chunk.</summary>
    void CommentChunk(ReadOnlySpan<Byte> utf8);

    /// <summary>Completes the current comment.</summary>
    void EndComment();
}
