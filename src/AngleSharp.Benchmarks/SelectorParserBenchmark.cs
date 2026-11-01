namespace AngleSharp.Benchmarks
{
    using AngleSharp.Css.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.Globalization;

    /// <summary>
    /// Parses selectors the way a style sheet does, each distinct one for the first time.
    /// </summary>
    /// <remarks>
    /// SelectorBenchmark, QuerySelectorBenchmark and HasSelectorBenchmark repeat one selector, so
    /// after the first call CssSelectorParser serves it from its cache and they time matching
    /// only. Here every operation parses more distinct selectors than that cache holds, in the
    /// same order, so each one has been evicted by the time it comes round again and every call
    /// is a real parse (including the cache's own lookup and insert, as for a style sheet).
    /// </remarks>
    [MemoryDiagnoser]
    public class SelectorParserBenchmark
    {
        // More than the 256 selectors CssSelectorParser keeps
        private const Int32 Count = 300;

        private static readonly String[] SimpleTemplates =
        [
            "div p.c{0}",
            "ul.toc > li.tocline2.c{0}",
            "h1#title + div > p.c{0}",
            "div ~ p.c{0}",
            "a[href][lang][class].c{0}",
            "div[class^=exa][class$=mple].c{0}",
            "div, p, a.c{0}",
            "#title.c{0}",
            "p:nth-child(2n+1).c{0}",
            "ul .tocline2 a:first-child.c{0}",
        ];

        private static readonly String[] FunctionalTemplates =
        [
            ":is(div, p).c{0}",
            "div:not(.example, .c{0})",
            "li:has(> a.c{0})",
            ":where(ul .c{0}) a",
            "p:nth-child(2n+1 of .c{0})",
            ":is(:not(.a), :where(.c{0} > p))",
            "a:is(:hover, :focus):not(.c{0})",
            ":is(h1, h2, h3):has(+ p.c{0})",
            "body :has(.c{0}):first-child",
            ":where(:is(nav, header) :not(.c{0}) > a)",
        ];

        private CssSelectorParser _parser;
        private String[] _simple;
        private String[] _functional;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _parser = new CssSelectorParser();
            _simple = Expand(SimpleTemplates);
            _functional = Expand(FunctionalTemplates);
        }

        [Benchmark]
        public Int32 Simple() => ParseAll(_simple);

        [Benchmark]
        public Int32 Functional() => ParseAll(_functional);

        private Int32 ParseAll(String[] selectors)
        {
            var valid = 0;

            foreach (var selector in selectors)
            {
                if (_parser.ParseSelector(selector) is not null)
                {
                    valid++;
                }
            }

            return valid;
        }

        private static String[] Expand(String[] templates)
        {
            var selectors = new String[Count];
            var check = new CssSelectorParser();

            for (var i = 0; i < Count; i++)
            {
                selectors[i] = String.Format(CultureInfo.InvariantCulture, templates[i % templates.Length], i);

                // An invalid selector stops early and would time something else
                if (check.ParseSelector(selectors[i]) is null)
                {
                    throw new InvalidOperationException($"Not a valid selector: {selectors[i]}");
                }
            }

            return selectors;
        }
    }
}
