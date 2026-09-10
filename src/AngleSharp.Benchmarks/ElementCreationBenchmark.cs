namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;

    [MemoryDiagnoser]
    public class ElementCreationBenchmark
    {
        private IDocument _document;
        private IElement _element;

        [Params("div", "title", "unknown", "DiV", "ABC")]
        public String Name { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _document = new HtmlParser().ParseDocument("<!doctype html><body></body>");
            _element = _document.CreateElement(NamespaceNames.HtmlUri, Name);
        }

        [Benchmark]
        public IElement CreateElement() => _document.CreateElement(Name);

        [Benchmark]
        public IElement CreateElementNS() => _document.CreateElement(NamespaceNames.HtmlUri, Name);

        [Benchmark]
        public INode CloneElement() => _element.Clone(false);
    }

    [MemoryDiagnoser]
    public class ElementCreationParserBenchmark
    {
        private readonly HtmlParser _parser = new HtmlParser();
        private String _page;

        [GlobalSetup]
        public void Setup()
        {
            _page = File.ReadAllText("page.html");
        }

        [Benchmark]
        public IDocument ParsePage() => _parser.ParseDocument(_page);

        [Benchmark]
        public IDocument ParseSmallDocument() => _parser.ParseDocument("<!doctype html><title>Title</title><div><span>Text</span><input><unknown>Custom</unknown></div>");
    }
}
