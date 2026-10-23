namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System.IO;

    /// <summary>
    /// The DOM members that walk a whole subtree, on an ordinary page. They walk iteratively, so
    /// that deeply nested documents cannot overflow the stack; this measures what that costs on
    /// the documents people actually parse.
    /// </summary>
    [MemoryDiagnoser]
    public class TreeWalkBenchmark
    {
        private static readonly HtmlParser parser = new HtmlParser();
        private IDocument document;
        private IDocument copy;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var source = File.ReadAllText("page.html");
            document = parser.ParseDocument(source);
            copy = parser.ParseDocument(source);
        }

        [Benchmark]
        public int TextContent() => document.Body.TextContent.Length;

        [Benchmark]
        public int GetElementsByTagName() => document.GetElementsByTagName("a").Length;

        [Benchmark]
        public int GetElementsByClassName() => document.GetElementsByClassName("missing").Length;

        [Benchmark]
        public IElement GetElementByIdMiss() => document.GetElementById("missing");

        [Benchmark]
        public IElement ContainsSelectorMiss() => document.QuerySelector("body:contains(NoSuchTextAnywhereInThisDocument)");

        [Benchmark]
        public INode CloneDeep() => document.Body.Clone(true);

        [Benchmark]
        public bool IsEqualNode() => document.Body.Equals((INode)copy.Body);

        [Benchmark]
        public void Normalize() => document.Body.Normalize();

        [Benchmark]
        public IDocumentFragment RangeCopyContent()
        {
            var range = document.CreateRange();
            range.SelectContent(document.Body);
            return range.CopyContent();
        }
    }
}
