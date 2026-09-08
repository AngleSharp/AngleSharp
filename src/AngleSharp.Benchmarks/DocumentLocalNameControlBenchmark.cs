namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;

    [MemoryDiagnoser]
    public class DocumentLocalNameControlBenchmark
    {
        private readonly HtmlParser _parser = new HtmlParser();
        private String _html;
        private IDocument _document;

        [GlobalSetup]
        public void Setup()
        {
            _html = File.ReadAllText("page.html");
            _document = _parser.ParseDocument(_html);
        }

        [Benchmark]
        public IDocument ParseHtml() => _parser.ParseDocument(_html);

        [Benchmark]
        public IHtmlCollection<IElement> QuerySelector() => _document.QuerySelectorAll("div.example, div.note");
    }
}
