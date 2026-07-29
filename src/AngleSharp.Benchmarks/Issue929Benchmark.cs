using System.IO;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    /// <summary>
    /// Reproduces https://github.com/AngleSharp/AngleSharp/issues/929 - looking up a single
    /// element by tag name plus text content. The hand-rolled variant is the baseline the
    /// issue reports as being dramatically faster than the equivalent CSS selector.
    /// </summary>
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
    public class Issue929Benchmark
    {
        private const string Present = "Selectors";
        private const string Absent = "NoSuchTextAnywhereInThisDocument";

        private static readonly HtmlParser parser = new HtmlParser();
        private IDocument document;

        [GlobalSetup]
        public void GlobalSetup()
        {
            document = parser.ParseDocument(File.ReadAllText("page.html"));
        }

        [BenchmarkCategory("Found"), Benchmark(Baseline = true)]
        public IElement HandRolled() =>
            document.GetElementsByTagName("h2").FirstOrDefault(x => x.TextContent.Contains(Present));

        [BenchmarkCategory("Found"), Benchmark]
        public IElement Selector() => document.QuerySelector("h2:contains(" + Present + ")");

        [BenchmarkCategory("NotFound"), Benchmark(Baseline = true)]
        public IElement HandRolledMiss() =>
            document.GetElementsByTagName("h2").FirstOrDefault(x => x.TextContent.Contains(Absent));

        [BenchmarkCategory("NotFound"), Benchmark]
        public IElement SelectorMiss() => document.QuerySelector("h2:contains(" + Absent + ")");
    }
}
