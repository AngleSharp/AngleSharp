namespace AngleSharp.Html.Parser.Utf8;

using System;

#pragma warning disable CS1591 // Experimental diagnostics surface; not proposed as final API.

public readonly record struct Utf8HtmlTokenizerCounters(
    Int64 BytesConsumed,
    Int64 InputSegments,
    Int64 Reconsumes,
    Int32 MaximumSourceLookbehind,
    Int32 MaximumBufferedTokenBytes
);
