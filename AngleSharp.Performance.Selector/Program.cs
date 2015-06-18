namespace AngleSharp.Performance.Selector
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(String[] args)
        {
            TestPureSelectors();
            TestPageSelectorCombinations();
        }

        static void TestPureSelectors()
        {
            var selectors = new StandardTests();

            selectors.Include(
                ("body"),
                ("div"),
                ("body div"),
                ("div p"),
                ("div > p"),
                ("div + p"),
                ("div ~ p"),
                ("div[class^=exa][class$=mple]"),
                ("div p a"),
                ("div, p, a"),
                (".note"),
                ("div.example"),
                ("ul .tocline2"),
                ("div.example, div.note"),
                ("#title"),
                ("h1#title"),
                ("div #title"),
                ("ul.toc li.tocline2"),
                ("ul.toc > li.tocline2"),
                ("h1#title + div > p"),
                ("h1[id]:contains(Selectors)"),
                ("a[href][lang][class]"),
                ("div[class]"),
                ("div[class=example]"),
                ("div[class^=exa]"),
                ("div[class$=mple]"),
                ("div[class*=e]"),
                ("div[class|=dialog]"),
                ("div[class!=made_up]"),
                ("div[class~=example]"),
                ("div:not(.example)"),
                ("p:contains(selectors)"),
                ("p:nth-child(even)"),
                ("p:nth-child(2n)"),
                ("p:nth-child(odd)"),
                ("p:nth-child(2n+1)"),
                ("p:nth-child(n)"),
                ("p:only-child"),
                ("p:last-child"),
                ("p:first-child")
            );

            var parsers = new List<ITestee>
            {
                new AngleSharpSelector(Page.Content),
                new CsQuerySelector(Page.Content),
            };

            var testsuite = new TestSuite(parsers, selectors.Tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 20,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }

        static void TestPageSelectorCombinations()
        {
            var websites = new UrlTests(
                extension: ".html",
                withBuffer: true);

            websites.Include(
                "http://www.amazon.com",
                "http://www.blogspot.com",
                "http://www.smashing.com",
                "http://www.youtube.com",
                "http://www.weibo.com",
                "http://www.yahoo.com",
                "http://www.google.com",
                "http://www.linkedin.com",
                "http://www.pinterest.com",
                "http://news.google.com",
                "http://www.baidu.com",
                "http://www.codeproject.com",
                "http://www.ebay.com",
                "http://www.msn.com",
                "http://www.nbc.com",
                "http://www.qq.com",
                "http://www.florian-rappl.de",
                "http://www.stackoverflow.com",
                "http://www.html5rocks.com/en",
                "http://www.live.com",
                "http://www.taobao.com",
                "http://www.huffingtonpost.com",
                "http://www.wordpress.org",
                "http://www.myspace.com",
                "http://www.flickr.com",
                "http://www.godaddy.com",
                "http://www.reddit.com",
                "http://www.nytimes.com",
                "http://peacekeeper.futuremark.com",
                "http://www.pcmag.com",
                "http://www.sitepoint.com",
                "http://html5test.com",
                "http://www.spiegel.de",
                "http://www.tmall.com",
                "http://www.sohu.com",
                "http://www.vk.com",
                "http://www.wordpress.com",
                "http://www.bing.com",
                "http://www.tumblr.com",
                "http://www.ask.com",
                "http://www.mail.ru",
                "http://www.imdb.com",
                "http://www.kickass.to",
                "http://www.360.cn",
                "http://www.163.com",
                "http://www.neobux.com",
                "http://www.aliexpress.com",
                "http://www.netflix.com",
                "http://www.w3.org/TR/html5/single-page.html",
                "http://en.wikipedia.org/wiki/South_African_labour_law",
                "http://www.time.com").Wait();

            var parsers = new List<ITestee>
            {
                new AngleSharpParser(),
                new CsQueryParser(),
            };

            var testsuite = new TestSuite(parsers, websites.Tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
