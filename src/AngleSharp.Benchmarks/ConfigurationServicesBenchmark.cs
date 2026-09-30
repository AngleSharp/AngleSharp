using System;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;

namespace AngleSharp.Benchmarks;

/// <summary>
/// Measures the cost of building a configuration and resolving its services.
/// Run on both the base and candidate revisions to compare the implementation.
/// </summary>
[MemoryDiagnoser, ShortRunJob]
public class ConfigurationServicesBenchmark
{
    [Benchmark]
    public Boolean ConfigureAndResolveParser()
    {
        IConfiguration configuration = Configuration.Default;

        for (var i = 0; i < 12; i++)
        {
            configuration = configuration.With(new Marker(i));
        }

        for (var i = 0; i < 8; i++)
        {
            configuration = configuration.WithOnly<IMarker>(new Marker(i));
        }

        using var context = BrowsingContext.New(configuration);
        return context.GetService<IHtmlParser>() is not null;
    }

    private interface IMarker { }

    private sealed class Marker(Int32 id) : IMarker
    {
        public Int32 Id { get; } = id;
    }
}
