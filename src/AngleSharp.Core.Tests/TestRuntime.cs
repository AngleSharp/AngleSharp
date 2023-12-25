namespace AngleSharp.Core.Tests;

using System;

public static class TestRuntime
{
    public static Boolean UserPrefetchedTextSource { get; set; } = Environment.GetEnvironmentVariable("prefetched") == "true";
}