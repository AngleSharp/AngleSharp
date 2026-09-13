namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;

    /// <summary>
    /// Measures mutation bookkeeping separately from parsing and selector reads.
    /// Each benchmark process has its own document; setup is outside measurement.
    /// </summary>
    [MemoryDiagnoser]
    public class SelectorStateMutationBenchmark
    {
        private const Int32 Count = 1000;
        private IHtmlInputElement _input;
        private IHtmlOptionElement _option;
        private HtmlParser _parser;

        [GlobalSetup]
        public void Setup()
        {
            _parser = new HtmlParser();
            var document = _parser.ParseDocument("<input type=checkbox><option>text</option>");
            _input = (IHtmlInputElement)document.QuerySelector("input");
            _option = (IHtmlOptionElement)document.QuerySelector("option");
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public Boolean SetChecked()
        {
            for (var i = 0; i < Count; i++)
            {
                _input.IsChecked = (i & 1) != 0;
            }

            return _input.IsChecked;
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public Boolean SetSelected()
        {
            for (var i = 0; i < Count; i++)
            {
                _option.IsSelected = (i & 1) != 0;
            }

            return _option.IsSelected;
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public String SetValue()
        {
            for (var i = 0; i < Count; i++)
            {
                _input.Value = (i & 1) != 0 ? "a" : "b";
            }

            return _input.Value;
        }

        [Benchmark]
        public Boolean MatchChecked() => _input.Matches(":checked");

        [Benchmark]
        public IDocument ParseControls() => _parser.ParseDocument("<form><input type=checkbox checked><select><option selected>text</option></select><textarea>value</textarea></form>");
    }
}
