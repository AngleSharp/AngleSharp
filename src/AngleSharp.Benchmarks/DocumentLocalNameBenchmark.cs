namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;

    [MemoryDiagnoser]
    public class DocumentLocalNameBenchmark
    {
        private IDocument _document;

        [Params("div", "data-value", "f<oo", "foo}", "1", "@", "a/b", "")]
        public String Name { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _document = new HtmlParser().ParseDocument("");
        }

        // Catch on both revisions so newly accepted inputs remain measurable.
        // Those rows compare rejection with creation, not equivalent operations.
        [Benchmark]
        public Object CreateElement()
        {
            try
            {
                return _document.CreateElement(Name);
            }
            catch (DomException)
            {
                return null;
            }
        }

        [Benchmark]
        public Object CreateAttribute()
        {
            try
            {
                return _document.CreateAttribute(Name);
            }
            catch (DomException)
            {
                return null;
            }
        }
    }
}
