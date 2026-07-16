#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

public interface IOptimizedUtf8HtmlTokenSink : IUtf8HtmlTokenSink
{
    void StartTag(ReadOnlySpan<Byte> name, UInt64 hash);

    void EndTag(ReadOnlySpan<Byte> name, UInt64 hash);

    Boolean WantsAttribute(ReadOnlySpan<Byte> name);
}
