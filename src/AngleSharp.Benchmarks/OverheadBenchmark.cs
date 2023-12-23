#nullable enable
namespace AngleSharp.Benchmarks;

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Html;
using Html.Dom;
using Html.Parser;
using Html.Parser.Tokens.Struct;
using Io;
using ReadOnly.Html;
using Text;

[Config(typeof(Config))]
[MemoryDiagnoser]
public class OverheadBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            AddJob(Job.ShortRun
                .WithRuntime(CoreRuntime.Core80)
                .WithStrategy(RunStrategy.Throughput)
            );
        }
    }

    public class HtmlTask
    {
        public required string Display { get; init; }
        public required string Html { get; init; }
        public required HtmlParserOptions Options {get; init;}
        public override string ToString() => Display;
    }

    private HtmlParser parser = new HtmlParser();

    [GlobalSetup]
    public void Setup()
    {
        HtmlEntityProvider.Resolver.GetSymbol("test");
        MimeTypeNames.FromExtension(".txt");
        parser = new HtmlParser(It!.Options);
    }

    [ParamsSource(nameof(GetTasks))] public HtmlTask? It { get; set; }

    public IEnumerable<HtmlTask> GetTasks()
    {
        // yield return new HtmlTask { Display = "br", Html = "<br/>", Options = default};
        // yield return new HtmlTask { Display = "table", Html = StaticHtml.HtmlTable, Options = default};
        // yield return new HtmlTask { Display = "table tabbed", Html = StaticHtml.HtmlTableTabbed, Options = default};
        yield return new HtmlTask { Display = "table TABBED", Html = StaticHtml.HtmlTableTabbedSoMuch, Options = default};
        yield return new HtmlTask { Display = "github", Html = StaticHtml.Github, Options = default};

        // yield return new HtmlTask { Display = "br *", Html = "<br/>", Options = Custom };
        // yield return new HtmlTask { Display = "table *", Html = StaticHtml.HtmlTable, Options = Custom };
        // yield return new HtmlTask { Display = "table tabbed *", Html = StaticHtml.HtmlTableTabbed, Options = Custom };
        yield return new HtmlTask { Display = "table TABBED *", Html = StaticHtml.HtmlTableTabbedSoMuch, Options = Custom };
        yield return new HtmlTask { Display = "github *", Html = StaticHtml.Github, Options = Custom };
    }

    [Benchmark(Baseline = true)]
    public void V1()
    {
        var doc = parser.ParseDocument(It!.Html);
        doc.Dispose();
    }

    [Benchmark]
    public void V2()
    {
        var source = new PrefetchedTextSource(It!.Html);
        var doc = parser.ParseReadOnlyDocument(source);
        doc.Dispose();
    }

    public static readonly HtmlParserOptions Custom = new HtmlParserOptions()
    {
        IsStrictMode = false,
        IsScripting = false,
        IsNotConsumingCharacterReferences = true,
        IsNotSupportingFrames = true,
        IsSupportingProcessingInstructions = false,
        IsEmbedded = false,

        IsKeepingSourceReferences = false,
        IsPreservingAttributeNames = false,
        IsAcceptingCustomElementsEverywhere = false,

        SkipScriptText = true,
        SkipRawText = true,
        SkipDataText = false,
        SkipComments = true,
        SkipPlaintext = true,
        SkipCDATA = true,
        SkipRCDataText = true,
        SkipProcessingInstructions = true,
        DisableElementPositionTracking = true,
        ShouldEmitAttribute = static (ref StructHtmlToken _, ReadOnlyMemory<Char> n) =>
        {
            var s = n.Span;
            return s.Length switch
            {
                2 => s[0] == 'i' && s[1] == 'd',
                _ => false
            };
        },
    };
}