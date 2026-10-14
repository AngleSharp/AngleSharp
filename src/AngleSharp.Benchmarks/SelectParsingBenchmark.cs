namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Globalization;
    using System.Text;

    [MemoryDiagnoser, ShortRunJob]
    public class SelectParsingBenchmark
    {
        private readonly HtmlParser _parser = new();
        private String _markup = null!;
        private IDocument _lastDocument = null!;

        [Params(1000, 2000, 4000)]
        public Int32 OptionCount { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var markup = new StringBuilder("<!doctype html><select>");

            for (var i = 0; i < OptionCount; i++)
            {
                var value = i.ToString(CultureInfo.InvariantCulture);
                markup.Append("<option value=\"").Append(value).Append("\">")
                    .Append(value).Append("</option>");
            }

            _markup = markup.Append("</select>").ToString();
            using var initial = _parser.ParseDocument(_markup);
            Validate(initial);
        }

        [Benchmark]
        public IDocument ParseDocument() => _lastDocument = _parser.ParseDocument(_markup);

        [GlobalCleanup]
        public void Cleanup()
        {
            using var final = _lastDocument;
            Validate(final);
        }

        private void Validate(IDocument document)
        {
            if (document is null || document.QuerySelector("select") is not IHtmlSelectElement select)
            {
                throw new InvalidOperationException("The benchmark did not produce a select element.");
            }

            var options = select.Options;
            var lastValue = (OptionCount - 1).ToString(CultureInfo.InvariantCulture);

            if (options.Length != OptionCount || select.SelectedIndex != 0 ||
                select.Value != "0" || options[OptionCount - 1].Value != lastValue)
            {
                throw new InvalidOperationException("The parsed select does not match the benchmark input.");
            }
        }
    }
}
