namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The DOM members that walk a whole subtree, on an ordinary page and on nested documents of
    /// increasing depth. The depths cover the switch from recursion to the explicit stack (128),
    /// the parser's default maximum depth (512) and deeper trees with that limit lifted, up to
    /// below where a recursive walk overflows the default stack.
    /// </summary>
    [MemoryDiagnoser]
    public class TreeWalkBenchmark
    {
        private static readonly HtmlParser parser = new HtmlParser(new HtmlParserOptions { MaximumDepth = Int32.MaxValue });
        private IDocument document;

        /// <summary>
        /// "page" is page.html; a number is a document nested that many levels deep, where every
        /// level has a text, an attribute and a sibling element.
        /// </summary>
        [Params("page", "16", "128", "129", "256", "512", "1000", "3000")]
        public String Shape { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var source = Shape == "page" ? File.ReadAllText("page.html") : Nested(Int32.Parse(Shape));
            document = parser.ParseDocument(source);
        }

        [Benchmark]
        public Int32 TextContent() => document.Body.TextContent.Length;

        [Benchmark]
        public IElement ContainsSelectorMiss() => document.QuerySelector("body:contains(NoSuchTextAnywhereInThisDocument)");

        [Benchmark]
        public Int32 GetElementsByTagName() => document.GetElementsByTagName("a").Length;

        [Benchmark]
        public Int32 GetElementsByTagNameNamespaced() => document.GetElementsByTagName(NamespaceNames.HtmlUri, "a").Length;

        [Benchmark]
        public Int32 GetElementsByClassName() => document.GetElementsByClassName("missing").Length;

        [Benchmark]
        public IElement GetElementByIdMiss() => document.GetElementById("missing");

        [Benchmark]
        public Int32 GetElementsByNameMiss() => document.GetElementsByName("missing").Length;

        [Benchmark]
        public void Normalize() => document.Body.Normalize();

        private static String Nested(Int32 depth) =>
            "<!DOCTYPE html><body>" + String.Concat(Enumerable.Repeat("<div class=\"level\">text <a href=\"#\">link</a>", depth));
    }
}
