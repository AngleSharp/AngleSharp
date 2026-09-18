using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    /// <summary>
    /// Focused gate for selector matching performance. Unlike <see cref="SelectorBenchmark"/>
    /// - which is a broad short-run sweep - this uses the default job for low noise and covers
    /// both the "all matches" and the "first match" traversal, which are separate code paths.
    /// </summary>
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
    public class QuerySelectorBenchmark
    {
        private static readonly HtmlParser parser = new HtmlParser();
        private IDocument document;

        [GlobalSetup]
        public void GlobalSetup()
        {
            document = parser.ParseDocument(File.ReadAllText("page.html"));
        }

        [ParamsSource(nameof(GetSelectors))]
        public string Selector { get; set; }

        public IEnumerable<string> GetSelectors => new[]
        {
            // Baseline traversal cost: every element is visited, matching is trivial.
            "div",
            // Descendant and child combinators.
            "div p",
            "ul.toc li.tocline2",
            // Sibling combinators - these walk siblings per candidate.
            "div + p",
            "div ~ p",
            // Structural pseudo classes - these walk the parent's child list per candidate.
            "p:nth-child(2n+1)",
            "p:only-child",
            // Text matching - this materializes text content per candidate.
            "p:contains(selectors)",
            // Single-hit selectors: the first match is found without visiting the whole tree.
            "#title",
            ".note",
            // No match at all: forces a complete traversal even for QuerySelector.
            "div.no-such-class-anywhere",
            // A compound selector with several simple-selector parts chained on one element -
            // exercises CompoundSelector.Match/Specificity across all four parts per candidate.
            "a.url.fn[href]",
            // A selector list (ListSelector) with branches of differing specificity - exercises
            // ListSelector.Match plus, for anything that reads it (e.g. IMultiSelector.
            // GetMatchingSelector, or a host cascade), Selectors.Specificity across every branch.
            "h1, h2, h3, .toc, .note"
        };

        [Benchmark]
        public IElement QuerySelector() => document.QuerySelector(Selector);

        [Benchmark]
        public IHtmlCollection<IElement> QuerySelectorAll() => document.QuerySelectorAll(Selector);
    }

    /// <summary>
    /// Focused gate for the DOM collection-read paths touched by the memoized
    /// <see cref="IDocument.Forms"/>, the single-pass id/name lookup behind indexed collection
    /// access (<see cref="CollectionExtensions.GetElementById{T}(System.Collections.Generic.IEnumerable{T}, string)"/>),
    /// and the tag/class-name traversal helpers in <c>QueryExtensions</c>. Kept as its own class
    /// - rather than added to <see cref="QuerySelectorBenchmark"/> - because that class is
    /// parameterized over CSS selector text (<see cref="QuerySelectorBenchmark.Selector"/>),
    /// which none of these rows take; sharing the class would multiply every row across every
    /// unrelated selector value. One <see cref="IDocument"/> per fixture, built once in
    /// <see cref="GlobalSetup"/> and read-only from every benchmark method, so no per-row
    /// isolation is needed.
    /// </summary>
    [MemoryDiagnoser]
    public class CollectionReadBenchmark
    {
        private const Int32 FormCount = 20;
        private const Int32 FieldsPerForm = 10;

        private static readonly HtmlParser parser = new HtmlParser();
        private IDocument document;
        private IDocument formDocument;
        private IHtmlFormElement lookupForm;
        private String lookupName;

        [GlobalSetup]
        public void GlobalSetup()
        {
            document = parser.ParseDocument(File.ReadAllText("page.html"));

            // A realistically large document (the same page.html fixture) with a batch of named
            // forms appended, so the id/name lookup and the memoized Forms read both walk a
            // document of non-trivial size rather than a handful of hand-written elements.
            var markup = new StringBuilder(File.ReadAllText("page.html"));

            for (var i = 0; i < FormCount; i++)
            {
                markup.Append("<form>");

                for (var j = 0; j < FieldsPerForm; j++)
                {
                    markup.Append("<input name=\"field-").Append(i).Append('-').Append(j).Append("\">");
                }

                markup.Append("</form>");
            }

            formDocument = parser.ParseDocument(markup.ToString());
            lookupForm = (IHtmlFormElement)formDocument.Forms[FormCount - 1];
            lookupName = $"field-{FormCount - 1}-{FieldsPerForm - 1}";
        }

        // Control: exercises the selector engine (Css/Dom), which this change set does not
        // touch. Its number should not move.
        [Benchmark(Baseline = true)]
        public Int32 QuerySelectorAllControl() => document.QuerySelectorAll("div").Length;

        [Benchmark]
        public Int32 GetElementsByTagNameSweep() => document.GetElementsByTagName("div").Length;

        [Benchmark]
        public Int32 GetElementsByClassNameSweep() => document.GetElementsByClassName("toc").Length;

        // Isolates the property getter itself (repeated reads, no further use of the
        // collection) rather than the live query a Length or indexed read on it also pays for
        // regardless of getter caching - that live-query cost is unrelated to this change.
        [Benchmark]
        public IHtmlCollection<IHtmlFormElement> DocumentFormsRead()
        {
            IHtmlCollection<IHtmlFormElement> last = null;

            for (var i = 0; i < 1000; i++)
            {
                last = formDocument.Forms;
            }

            return last;
        }

        // Worst case for the id/name lookup: no element in the document has an "id" attribute,
        // so the old two-pass GetElementById exhausted the whole (document-wide, lazily
        // filtered) sequence once looking for an id match before scanning it again for the
        // name match.
        [Benchmark]
        public IHtmlElement FormElementsNamedLookup() => lookupForm.Elements[lookupName];
    }
}
