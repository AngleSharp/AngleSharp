using System.Collections.Generic;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

#if NETFRAMEWORK
using CsQuery;
using CsQuery.ExtensionMethods.Internal;
using CsQuery.HtmlParser;
#endif

using HtmlAgilityPack;

namespace AngleSharp.Benchmarks
{
    [MemoryDiagnoser, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams), ShortRunJob]
    public class ParserBenchmark
    {
        private static readonly HtmlParser angleSharpParser = new();

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(
                ".html",
                true);

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
                // hangs HTMLAgilityPack "http://www.w3.org/TR/html5/single-page.html",
                "http://en.wikipedia.org/wiki/South_African_labour_law").GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

#if NETFRAMEWORK
        [Benchmark]
        public void CsQuery()
        {
            var factory = new ElementFactory(DomIndexProviders.Simple);

            using var stream = UrlTest.Source.ToStream();
            factory.Parse(stream, System.Text.Encoding.UTF8);
        }
#endif

        [Benchmark]
        public void HTMLAgilityPack()
        {
            var document = new HtmlDocument();
            document.LoadHtml(UrlTest.Source);
        }

        [Benchmark]
        public void AngleSharp()
        {
            angleSharpParser.ParseDocument(UrlTest.Source);
        }
    }
}