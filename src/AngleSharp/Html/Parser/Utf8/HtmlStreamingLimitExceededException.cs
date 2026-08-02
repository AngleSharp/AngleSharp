#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System.IO;

namespace AngleSharp.Html.Parser.Utf8;

using System;

public enum HtmlStreamingLimit
{
    BufferedTokenBytes,
    NestingDepth,
    InputBytes,
    QueryCaptureBytes,
}

/// <summary>Thrown before a streaming HTML execution exceeds a configured resource limit.</summary>
public sealed class HtmlStreamingLimitExceededException(HtmlStreamingLimit limit, Int64 allowed, Int64 observed)
    : IOException($"Streaming HTML {limit} limit exceeded: observed {observed:N0}, allowed {allowed:N0}.")
{
    public HtmlStreamingLimit Limit { get; } = limit;

    public Int64 Allowed { get; } = allowed;

    public Int64 Observed { get; } = observed;
}
