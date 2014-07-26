using System.Collections.Generic;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            UrlTest.UseBuffer = true;

            var tests = new List<ITest>
            {
                UrlTest.For("http://www.amazon.com"),
                UrlTest.For("http://www.blogspot.com"),
                UrlTest.For("http://www.smashing.com"),
                UrlTest.For("http://www.youtube.com"),
                UrlTest.For("http://www.weibo.com"),
                UrlTest.For("http://en.wikipedia.org"),
                UrlTest.For("http://www.w3.org"),
                UrlTest.For("http://www.yahoo.com"),
                UrlTest.For("http://www.google.com"),
                UrlTest.For("http://www.linkedin.com"),
                UrlTest.For("http://www.pinterest.com"),
                UrlTest.For("http://news.google.com"),
                UrlTest.For("http://www.baidu.com"),
                UrlTest.For("http://www.codeproject.com"),
                UrlTest.For("http://www.ebay.com"),
                UrlTest.For("http://www.msn.com"),
                UrlTest.For("http://www.nbc.com"),
                UrlTest.For("http://www.qq.com"),
                UrlTest.For("http://www.florian-rappl.de"),
                UrlTest.For("http://www.stackoverflow.com"),
                UrlTest.For("http://www.html5rocks.com/en"),
                UrlTest.For("http://www.live.com"),
                UrlTest.For("http://www.taobao.com"),
                UrlTest.For("http://www.huffingtonpost.com"),
                UrlTest.For("http://www.wordpress.org"),
                UrlTest.For("http://www.myspace.com"),
                UrlTest.For("http://www.flickr.com"),
                UrlTest.For("http://www.godaddy.com"),
                UrlTest.For("http://www.reddit.com"),
                UrlTest.For("http://www.nytimes.com")
            };

            var parsers = new List<IHtmlParser>
            {
                new AngleSharpParser(),
                new CsQueryParser(),
                new AgilityPackParser()
            };

            //Majestic is neither HTML5 conform, nor building a realistic DOM structure.
            //Therefore Majestic has been excluded. You could, however, just re-enable
            //it by uncommenting the following line.
            //parsers.Add(new MajesticParser());

            var testsuite = new TestSuite
            {
                Parsers = parsers,
                Tests = tests,
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
