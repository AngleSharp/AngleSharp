using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    /// <summary>
    /// Matches :has() selectors against every element of page.html, which is what a style
    /// cascade does with each rule. Rows cover the unanchored subtree walk, the relative
    /// reaches (children, following siblings) and a :has() guarded by cheaper parts of its
    /// selector - the shape of rules such as ".TreeViewRootUlStyles :has(.skeleton):hover".
    /// </summary>
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
    public class HasSelectorBenchmark
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
            ":has(.note)",
            ":has(> p)",
            ":has(+ p)",
            ":has(~ .note)",
            ".toc :has(.tocline4)",
            ":has(.no-such-class):hover",
            "body :has(.no-such-class):first-child",
        };

        [Benchmark]
        public IHtmlCollection<IElement> QuerySelectorAll() => document.QuerySelectorAll(Selector);
    }
}
