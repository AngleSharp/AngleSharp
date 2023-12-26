namespace AngleSharp.Core.Tests;

using System;

public static class TestRuntime
{
    public static Boolean UsePrefetchedTextSource { get; set; } =
        Environment.GetEnvironmentVariable("prefetched") == "true";
}