namespace AngleSharp.Performance.Selector
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(String[] args)
        {
            var tests = new List<ITest>
            {
                new SelectorTest("body"),
                new SelectorTest("div"),
                new SelectorTest("body div"),
                new SelectorTest("div p"),
                new SelectorTest("div > p"),
                new SelectorTest("div + p"),
                new SelectorTest("div ~ p"),
                new SelectorTest("div[class^=exa][class$=mple]"),
                new SelectorTest("div p a"),
                new SelectorTest("div, p, a"),
                new SelectorTest(".note"),
                new SelectorTest("div.example"),
                new SelectorTest("ul .tocline2"),
                new SelectorTest("div.example, div.note"),
                new SelectorTest("#title"),
                new SelectorTest("h1#title"),
                new SelectorTest("div #title"),
                new SelectorTest("ul.toc li.tocline2"),
                new SelectorTest("ul.toc > li.tocline2"),
                new SelectorTest("h1#title + div > p"),
                new SelectorTest("h1[id]:contains(Selectors)"),
                new SelectorTest("a[href][lang][class]"),
                new SelectorTest("div[class]"),
                new SelectorTest("div[class=example]"),
                new SelectorTest("div[class^=exa]"),
                new SelectorTest("div[class$=mple]"),
                new SelectorTest("div[class*=e]"),
                new SelectorTest("div[class|=dialog]"),
                new SelectorTest("div[class!=made_up]"),
                new SelectorTest("div[class~=example]"),
                new SelectorTest("div:not(.example)"),
                new SelectorTest("p:contains(selectors)"),
                new SelectorTest("p:nth-child(even)"),
                new SelectorTest("p:nth-child(2n)"),
                new SelectorTest("p:nth-child(odd)"),
                new SelectorTest("p:nth-child(2n+1)"),
                new SelectorTest("p:nth-child(n)"),
                new SelectorTest("p:only-child"),
                new SelectorTest("p:last-child"),
                new SelectorTest("p:first-child")
            };

            var parsers = new List<ITestee>
            {
                new AngleSharpSelector(Page.Content),
                new CsQuerySelector(Page.Content),
            };

            var testsuite = new TestSuite(parsers, tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 20,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
