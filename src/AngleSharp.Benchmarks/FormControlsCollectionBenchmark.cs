namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Gate for reading a live HTMLFormControlsCollection - form.elements - which is a whole
    /// document traversal per read, once for the length, again for every indexed or named item.
    /// </summary>
    /// <remarks>
    /// One document for every row, built once in <see cref="GlobalSetup"/>: unlike
    /// <see cref="MutationBenchmark"/> every row here is a read, so nothing a row does can decide
    /// a later row's cost, and sharing the document is what makes the control row comparable -
    /// it walks the same tree.
    ///
    /// The fixture is the page.html fixture with a form of a hundred controls appended, spread
    /// over fieldsets and rows so the walk has real depth as well as real width, and so the bulk
    /// of the document sits ahead of the form in tree order - which is where a document-rooted
    /// walk spends most of its time on a real page.
    ///
    /// Every control in the form is owned by it through its ancestors. A control associated by
    /// form="id" would be realistic too, but resolving that association is a whole-tree
    /// GetElementById per candidate per read inside HtmlFormControlElement.Form, which is a
    /// separate cost on a separate code path; including one here would hide the traversal these
    /// rows are meant to measure behind it.
    ///
    /// <see cref="GetElementsByTagNameControl"/> is the baseline and the control: another live
    /// collection over the same document, read the same number of times, on a code path
    /// (QueryExtensions plus HtmlCollection) that form.elements does not share. It must not move.
    /// </remarks>
    [MemoryDiagnoser]
    public class FormControlsCollectionBenchmark
    {
        private const Int32 ControlCount = 100;
        private const Int32 FieldSetCount = 5;

        // Enough reads that the loop, not the BenchmarkDotNet overhead, is what is being timed,
        // and the shape an embedder actually writes: length once per item, item once per index.
        private const Int32 Reads = ControlCount;

        private IDocument _document;
        private IHtmlFormControlsCollection _elements;
        private String _lastId;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var parser = new HtmlParser();
            var markup = new StringBuilder(File.ReadAllText("page.html"));

            markup.Append("<form id=\"checkout\">");

            for (var i = 0; i < FieldSetCount; i++)
            {
                markup.Append("<fieldset><legend>Section ").Append(i).Append("</legend>");

                for (var j = 0; j < ControlCount / FieldSetCount; j++)
                {
                    var index = (i * ControlCount / FieldSetCount) + j;
                    markup.Append("<div class=\"row\"><label for=\"field-").Append(index).Append("\">Field ").Append(index)
                        .Append("</label><input id=\"field-").Append(index).Append("\" name=\"field-").Append(index).Append("\"></div>");
                }

                markup.Append("</fieldset>");
            }

            markup.Append("</form>");

            _document = parser.ParseDocument(markup.ToString());
            _elements = ((IHtmlFormElement)_document.GetElementById("checkout")).Elements;
            _lastId = "field-" + (ControlCount - 1);

            // Warm every row's own path once, so the first measured invocation is not the one
            // that materializes the lazy per-element state behind it.
            GetElementsByTagNameControl();
            FormElementsLength();
            FormElementsIndexed();
            FormElementsNamed();
            FormElementsEnumerate();
        }

        /// <summary>
        /// Control: a live collection over the same document, on a code path form.elements does
        /// not share.
        /// </summary>
        [Benchmark(Baseline = true)]
        public Int32 GetElementsByTagNameControl()
        {
            var document = _document;
            var total = 0;

            for (var i = 0; i < Reads; i++)
            {
                total += document.GetElementsByTagName("input").Length;
            }

            return total;
        }

        [Benchmark]
        public Int32 FormElementsLength()
        {
            var elements = _elements;
            var total = 0;

            for (var i = 0; i < Reads; i++)
            {
                total += elements.Length;
            }

            return total;
        }

        [Benchmark]
        public IHtmlElement FormElementsIndexed()
        {
            var elements = _elements;
            var last = default(IHtmlElement);

            for (var i = 0; i < ControlCount; i++)
            {
                last = elements[i];
            }

            return last;
        }

        /// <summary>
        /// Worst case for the named read: the id of the last control in tree order, so the walk
        /// runs to the end of the document before it can answer.
        /// </summary>
        [Benchmark]
        public IHtmlElement FormElementsNamed()
        {
            var elements = _elements;
            var id = _lastId;
            var last = default(IHtmlElement);

            for (var i = 0; i < Reads; i++)
            {
                last = elements[id];
            }

            return last;
        }

        /// <summary>
        /// Enumeration goes through IEnumerable&lt;IHtmlElement&gt;, so unlike the rows above it
        /// also pays for whatever the collection hands out as an IEnumerator.
        /// </summary>
        [Benchmark]
        public Int32 FormElementsEnumerate()
        {
            var elements = _elements;
            var count = 0;

            for (var i = 0; i < Reads; i++)
            {
                foreach (var element in elements)
                {
                    count += element.NodeName.Length;
                }
            }

            return count;
        }
    }
}
