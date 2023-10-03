
using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

#if NETFRAMEWORK
using CsQuery;
#endif

namespace AngleSharp.Benchmarks
{
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams), ShortRunJob]
    public class SelectorBenchmark
    {
        private static readonly HtmlParser angleSharpParser = new HtmlParser();
        private IDocument angleSharpDocument;

#if NETFRAMEWORK
        private CQ cqDocument;
#endif

        [GlobalSetup]
        public void GlobalSetup()
        {
            var pageContent = File.ReadAllText("page.html");
            angleSharpDocument = angleSharpParser.ParseDocument(pageContent);
#if NETFRAMEWORK
            cqDocument = CQ.CreateDocument(pageContent);
#endif
        }

        [ParamsSource(nameof(GetSelectors))]
        public string Selector { get; set; }

        public IEnumerable<string> GetSelectors => new[]
        {
            "body",
            "div",
            "body div",
            "div p",
            "div > p",
            "div + p",
            "div ~ p",
            "div[class^=exa][class$=mple]",
            "div p a",
            "div, p, a",
            ".note",
            "div.example",
            "ul .tocline2",
            "div.example, div.note",
            "#title",
            "h1#title",
            "div #title",
            "ul.toc li.tocline2",
            "ul.toc > li.tocline2",
            "h1#title + div > p",
            "h1[id]:contains(Selectors)",
            "a[href][lang][class]",
            "div[class]",
            "div[class=example]",
            "div[class^=exa]",
            "div[class$=mple]",
            "div[class*=e]",
            "div[class|=dialog]",
            "div[class!=made_up]",
            "div[class~=example]",
            "div:not(.example)",
            "p:contains(selectors)",
            "p:nth-child(even)",
            "p:nth-child(2n)",
            "p:nth-child(odd)",
            "p:nth-child(2n+1)",
            "p:nth-child(n)",
            "p:only-child",
            "p:last-child",
            "p:first-child",
            "a[href]",
            "div > p > a"
        };

#if NETFRAMEWORK
        [Benchmark]
        public void CsQuery()
        {
            cqDocument.Select(Selector);
        }
#endif

        [Benchmark]
        public void AngleSharp()
        {
            angleSharpDocument.QuerySelectorAll(Selector);
        }
    }
}