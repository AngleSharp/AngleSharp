namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;

    /// <summary>
    /// Gate for the per-mutation bookkeeping on the DOM write paths: the document's mutation
    /// version counter, and the attribute change steps a DOMTokenList write now runs.
    /// </summary>
    /// <remarks>
    /// Every row drives one of the three mutator families the counter is advanced from - the child
    /// list, an attribute, character data - in a tight loop, so a single increment per mutation
    /// shows here if it shows anywhere. <see cref="CreateElements"/> is the baseline and also the
    /// control: it allocates the same elements without ever touching a tree, an attribute or
    /// character data, so it must not move.
    ///
    /// Each mutating row owns its own document, built in <see cref="GlobalSetup"/>, because these
    /// rows are writes: sharing one document would let an earlier row's residue (a longer class
    /// attribute, a deeper tree) decide a later row's cost. The parse path deliberately carries
    /// none of this bookkeeping and is measured by ElementCreationParserBenchmark.ParsePage and
    /// ParseSmallDocument - the pair that says whether any of it leaked back into construction.
    /// </remarks>
    [MemoryDiagnoser]
    public class MutationBenchmark
    {
        // Large enough that the loop, not the BenchmarkDotNet overhead, is what is being timed.
        private const Int32 Count = 1000;

        private static readonly String[] Values = { "a", "b", "c", "d", "e", "f", "g", "h" };

        private IDocument _creationDocument;
        private IDocument _appendDocument;
        private IDocument _attributeDocument;
        private IDocument _classListDocument;
        private IDocument _textDocument;
        private IElement _attributeHost;
        private IElement _classListHost;
        private IText _text;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var parser = new HtmlParser();
            const String Source = "<!doctype html><body><div id=host>text</div></body>";

            _creationDocument = parser.ParseDocument(Source);
            _appendDocument = parser.ParseDocument(Source);
            _attributeDocument = parser.ParseDocument(Source);
            _classListDocument = parser.ParseDocument(Source);
            _textDocument = parser.ParseDocument(Source);

            _attributeHost = _attributeDocument.GetElementById("host");
            _classListHost = _classListDocument.GetElementById("host");
            _text = (IText)_textDocument.GetElementById("host").FirstChild;

            // Warm every row's own path once, so the first measured invocation is not the one that
            // materializes the lazy per-element state behind it.
            CreateElements();
            AppendChild();
            SetAttribute();
            ClassListToggle();
            SetCharacterData();
        }

        /// <summary>
        /// Control: the same allocations, no mutation of a tree, an attribute or character data.
        /// </summary>
        [Benchmark(Baseline = true)]
        public IElement CreateElements()
        {
            var document = _creationDocument;
            var last = default(IElement);

            for (var i = 0; i < Count; i++)
            {
                last = document.CreateElement("span");
            }

            return last;
        }

        [Benchmark]
        public IElement AppendChild()
        {
            var document = _appendDocument;
            var host = document.CreateElement("div");

            for (var i = 0; i < Count; i++)
            {
                host.AppendChild(document.CreateElement("span"));
            }

            return host;
        }

        [Benchmark]
        public IElement SetAttribute()
        {
            var host = _attributeHost;

            for (var i = 0; i < Count; i++)
            {
                host.SetAttribute("data-x", Values[i & 7]);
            }

            return host;
        }

        [Benchmark]
        public Boolean ClassListToggle()
        {
            var list = _classListHost.ClassList;
            var result = false;

            for (var i = 0; i < Count; i++)
            {
                result = list.Toggle("alpha");
            }

            return result;
        }

        [Benchmark]
        public Int32 SetCharacterData()
        {
            var text = _text;

            for (var i = 0; i < Count; i++)
            {
                text.Data = Values[i & 7];
            }

            return text.Length;
        }
    }
}
