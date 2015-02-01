namespace AngleSharp.Performance.Html
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(String[] args)
        {
            UrlTest.UseBuffer = true;

            var tests = new List<ITest>
            {
                UrlTest.For("http://www.amazon.com").Result,
                UrlTest.For("http://www.blogspot.com").Result,
                UrlTest.For("http://www.smashing.com").Result,
                UrlTest.For("http://www.youtube.com").Result,
                UrlTest.For("http://www.weibo.com").Result,
                UrlTest.For("http://en.wikipedia.org").Result,
                UrlTest.For("http://www.w3.org").Result,
                UrlTest.For("http://www.yahoo.com").Result,
                UrlTest.For("http://www.google.com").Result,
                UrlTest.For("http://www.linkedin.com").Result,
                UrlTest.For("http://www.pinterest.com").Result,
                UrlTest.For("http://news.google.com").Result,
                UrlTest.For("http://www.baidu.com").Result,
                UrlTest.For("http://www.codeproject.com").Result,
                UrlTest.For("http://www.ebay.com").Result,
                UrlTest.For("http://www.msn.com").Result,
                UrlTest.For("http://www.nbc.com").Result,
                UrlTest.For("http://www.qq.com").Result,
                UrlTest.For("http://www.florian-rappl.de").Result,
                UrlTest.For("http://www.stackoverflow.com").Result,
                UrlTest.For("http://www.html5rocks.com/en").Result,
                UrlTest.For("http://www.live.com").Result,
                UrlTest.For("http://www.taobao.com").Result,
                UrlTest.For("http://www.huffingtonpost.com").Result,
                UrlTest.For("http://www.wordpress.org").Result,
                UrlTest.For("http://www.myspace.com").Result,
                UrlTest.For("http://www.flickr.com").Result,
                UrlTest.For("http://www.godaddy.com").Result,
                UrlTest.For("http://www.reddit.com").Result,
                UrlTest.For("http://www.nytimes.com").Result,
                UrlTest.For("http://peacekeeper.futuremark.com").Result,
                UrlTest.For("http://www.pcmag.com").Result,
                UrlTest.For("http://www.sitepoint.com").Result,
                UrlTest.For("http://html5test.com").Result,
                UrlTest.For("http://www.spiegel.de").Result,
                UrlTest.For("http://www.tmall.com").Result,
                UrlTest.For("http://www.sohu.com").Result,
                UrlTest.For("http://www.vk.com").Result,
                UrlTest.For("http://www.wordpress.com").Result,
                UrlTest.For("http://www.bing.com").Result,
                UrlTest.For("http://www.tumblr.com").Result,
                UrlTest.For("http://www.ask.com").Result,
                UrlTest.For("http://www.mail.ru").Result,
                UrlTest.For("http://www.imdb.com").Result,
                UrlTest.For("http://www.kickass.to").Result,
                UrlTest.For("http://www.360.cn").Result,
                UrlTest.For("http://www.163.com").Result,
                UrlTest.For("http://www.neobux.com").Result,
                UrlTest.For("http://www.aliexpress.com").Result,
                UrlTest.For("http://www.netflix.com").Result
            };

            var statistics = new StatisticParser();

            var parsers = new List<ITestee>
            {
                //statistics
                new AngleSharpParser(),
                new CsQueryParser(),
                new AgilityPackParser()
            };

            //Majestic is neither HTML5 conform, nor building a realistic DOM structure.
            //Therefore Majestic has been excluded. You could, however, just re-enable
            //it by uncommenting the following line.
            //parsers.Add(new MajesticParser());

            var testsuite = new TestSuite(parsers, tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();

            //statistics.Print();
        }
    }
}
