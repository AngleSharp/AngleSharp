using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    /// <summary>
    /// Matches :nth-child() and :nth-last-child() against every element of page.html. Rows
    /// cover the plain An+B count, the of-clause that counts only siblings matching a selector,
    /// and an of-clause nested in another, which has to know where each sibling stands among
    /// those matching the inner one.
    /// </summary>
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
    public class NthChildBenchmark
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
            "p:nth-child(2n+1)",
            "li:nth-last-child(odd)",
            ":nth-child(2n+1 of .note)",
            "li:nth-last-child(odd of .tocline2)",
            ":nth-child(2n+1 of p)",
            ":nth-child(odd of :nth-child(n of p))",
            ":nth-last-child(2n of :nth-child(odd of li, p))",
        };

        [Benchmark]
        public IHtmlCollection<IElement> QuerySelectorAll() => document.QuerySelectorAll(Selector);
    }
}
