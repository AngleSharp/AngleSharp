#pragma warning disable CS1591 // Experimental API surface; shape is intentionally unsettled.

using System;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>Bounds resources retained or consumed by a streaming HTML execution.</summary>
public sealed class HtmlStreamingLimits
{
    public const Int32 DefaultMaximumBufferedTokenBytes = 1024 * 1024;
    public const Int32 DefaultMaximumNestingDepth = 4096;
    public const Int64 DefaultMaximumInputBytes = 128L * 1024 * 1024;
    public const Int64 DefaultMaximumQueryCaptureBytes = 64L * 1024 * 1024;

    public static HtmlStreamingLimits Default { get; } = new();

    public static HtmlStreamingLimits Unlimited { get; } =
        new(Int32.MaxValue, Int32.MaxValue, Int64.MaxValue, Int64.MaxValue);

    public HtmlStreamingLimits(
        Int32 maximumBufferedTokenBytes = DefaultMaximumBufferedTokenBytes,
        Int32 maximumNestingDepth = DefaultMaximumNestingDepth,
        Int64 maximumInputBytes = DefaultMaximumInputBytes,
        Int64 maximumQueryCaptureBytes = DefaultMaximumQueryCaptureBytes
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumBufferedTokenBytes);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumNestingDepth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumInputBytes);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumQueryCaptureBytes);
        MaximumBufferedTokenBytes = maximumBufferedTokenBytes;
        MaximumNestingDepth = maximumNestingDepth;
        MaximumInputBytes = maximumInputBytes;
        MaximumQueryCaptureBytes = maximumQueryCaptureBytes;
    }

    public Int32 MaximumBufferedTokenBytes { get; }

    public Int32 MaximumNestingDepth { get; }

    public Int64 MaximumInputBytes { get; }

    public Int64 MaximumQueryCaptureBytes { get; }
}
