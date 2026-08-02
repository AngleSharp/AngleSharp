using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    /// <summary>
    /// Focused gate for selector matching performance. Unlike <see cref="SelectorBenchmark"/>
    /// - which is a broad short-run sweep - this uses the default job for low noise and covers
    /// both the "all matches" and the "first match" traversal, which are separate code paths.
    /// </summary>
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
    public class QuerySelectorBenchmark
    {
        private static readonly HtmlParser parser = new HtmlParser();
        private IDocument document;

        [GlobalSetup]
        public void GlobalSetup()
        {
            document = parser.ParseDocument(File.ReadAllText("page.html"));
        }

        [ParamsSource(nameof(GetSelectors))]
        public string Selector { get; set; }

        public IEnumerable<string> GetSelectors => new[]
        {
            // Baseline traversal cost: every element is visited, matching is trivial.
            "div",
            // Descendant and child combinators.
            "div p",
            "ul.toc li.tocline2",
            // Sibling combinators - these walk siblings per candidate.
            "div + p",
            "div ~ p",
            // Structural pseudo classes - these walk the parent's child list per candidate.
            "p:nth-child(2n+1)",
            "p:only-child",
            // Text matching - this materializes text content per candidate.
            "p:contains(selectors)",
            // Single-hit selectors: the first match is found without visiting the whole tree.
            "#title",
            ".note",
            // No match at all: forces a complete traversal even for QuerySelector.
            "div.no-such-class-anywhere"
        };

        [Benchmark]
        public IElement QuerySelector() => document.QuerySelector(Selector);

        [Benchmark]
        public IHtmlCollection<IElement> QuerySelectorAll() => document.QuerySelectorAll(Selector);
    }
}
