namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Text;

    [MemoryDiagnoser]
    [SimpleJob(warmupCount: 5, iterationCount: 10)]
    public class ClassNameMatchingBenchmark
    {
        private readonly HtmlParser _parser = new HtmlParser();
        private IDocument _standards;
        private IDocument _quirks;
        private String _html;

        [GlobalSetup]
        public void Setup()
        {
            var html = new StringBuilder("<html><body>");

            for (var i = 0; i < 1000; i++)
            {
                html.Append(i % 2 == 0 ? "<div class='note selected'>text</div>" : "<div class='other'>text</div>");
            }

            html.Append("</body></html>");
            _html = "<!doctype html>" + html;
            _standards = _parser.ParseDocument(_html);
            _quirks = _parser.ParseDocument(html.ToString());

            // Measure repeated queries, with lazy class lists already initialized.
            _standards.GetElementsByClassName("note");
            _quirks.GetElementsByClassName("note");
        }

        [Benchmark]
        public Int32 StandardsSingleClass() => _standards.GetElementsByClassName("note").Length;

        [Benchmark]
        public Int32 StandardsMultipleClasses() => _standards.GetElementsByClassName("note selected").Length;

        [Benchmark]
        public Int32 StandardsMissingClass() => _standards.GetElementsByClassName("missing").Length;

        [Benchmark]
        public Int32 QuirksExactCase() => _quirks.GetElementsByClassName("note").Length;

        [Benchmark]
        public Int32 QuirksDifferentCase() => _quirks.GetElementsByClassName("NOTE SELECTED").Length;

        [Benchmark]
        public Int32 QuirksElementDifferentCase() => _quirks.Body.GetElementsByClassName("NOTE SELECTED").Length;

        [Benchmark]
        public Int32 StandardsCssSelector() => _standards.QuerySelectorAll(".note").Length;

        [Benchmark]
        public IDocument ParseStandardsDocument() => _parser.ParseDocument(_html);
    }
}
