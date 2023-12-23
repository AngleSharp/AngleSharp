namespace AngleSharp.Benchmarks;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Html.Dom;
using Html.Parser;
using ReadOnly.Html;
using Text;
using UserCode;

[Config(typeof(Config))]
[MemoryDiagnoser]
public class SelectorsOverheadBenchmark
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

    private readonly IHtmlDocument document = new HtmlParser().ParseDocument(StaticHtml.Github);
    private readonly IReadOnlyDocument documentReadonly = new HtmlParser().ParseReadOnlyDocument(new PrefetchedTextSource(StaticHtml.Github));
    private readonly StringBuilder sb = new StringBuilder(512);
    private readonly Stack<IReadOnlyNode> stack = new Stack<IReadOnlyNode>();

    private static readonly Func<IReadOnlyNode, Boolean>[] _selectors = {
        n => n.TagClass("div", "edit-comment-hide"),
        n => n.TagClass("tr", "d-block"),
        n => n.TagClass("td", "comment-body")
    };

    [Benchmark]
    public void Selectors()
    {
        var _ = document
                .QuerySelectorAll("div.edit-comment-hide tr.d-block td.comment-body")
                .Count();
    }

    [Benchmark]
    public void SelectorsReadonly()
    {
        var _ = documentReadonly
                .QueryAll(_selectors)
                .Count();
    }
}