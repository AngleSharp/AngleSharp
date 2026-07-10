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
    using System;
    using System.Linq;
    using Html;
    using Text;

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
                "https://www.amazon.com",
                "https://www.blogspot.com",
                "https://www.smashingmagazine.com",
                "https://www.youtube.com",
                "https://www.weibo.com",
                "https://www.yahoo.com",
                "https://www.google.com",
                "https://www.linkedin.com",
                "https://www.pinterest.com",
                "https://news.google.com",
                "https://www.baidu.com",
                "https://www.ebay.com",
                "https://www.msn.com",
                "https://www.nbc.com",
                "https://www.qq.com",
                "https://www.florian-rappl.de",
                "https://www.stackoverflow.com",
                "https://www.html5rocks.com/en",
                "https://www.microsoft.com/en-us/microsoft-365/outlook/email-and-calendar-software-microsoft-outlook",
                "https://www.taobao.com",
                "https://www.huffingtonpost.com",
                "https://www.wordpress.org",
                "https://www.myspace.com",
                "https://www.flickr.com",
                //"https://www.godaddy.com", //possible browser sniff block
                "https://www.reddit.com",
                "https://www.nytimes.com",
                //"https://www.pcmag.com", //possible browser sniff block
                "https://www.sitepoint.com",
                "https://html5test.com",
                "https://www.spiegel.de",
                "https://www.tmall.com",
                "https://www.sohu.com",
                "https://www.vk.com",
                "https://www.wordpress.com",
                "https://www.bing.com",
                "https://www.tumblr.com",
                "https://www.ask.com",
                "https://www.mail.ru",
                "https://www.360.cn",
                "https://www.163.com",
                "https://www.neobux.com",
                "https://www.aliexpress.com",
                "https://www.netflix.com",
                // hangs HTMLAgilityPack "https://www.w3.org/TR/html5/single-page.html",
                "https://en.wikipedia.org/wiki/South_African_labour_law").GetAwaiter().GetResult();

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

        [Benchmark]
        public void ArrayPool()
        {
            using var _ = angleSharpParser.ParseDocument(UrlTest.Source.AsMemory());
        }
    }

    [MemoryDiagnoser, ShortRunJob]
    public class ScanDataTextBenchmark
    {
        [Params(100, 1_000, 10_000)]
        public Int32 Repeats { get; set; }
        private String html;
        private Char[] chars;

        [GlobalSetup]
        public void Setup()
        {
            html = String.Concat(Enumerable.Repeat("<p>Hello World.<br>How are you?<br/>All good<br /> Ok, bye.</p>", Repeats));
            chars = html.ToCharArray();
        }

        [Benchmark]
        public Int32 StringSource() => Go(new TextSource(html));

        [Benchmark]
        public Int32 CharSource() => Go(new TextSource(new CharArrayTextSource(chars, chars.Length)));

        [Benchmark]
        public Int32 MemSource() => Go(new TextSource(new ReadOnlyMemoryTextSource(html.AsMemory())));

        private static Int32 Go(TextSource source)
        {
            var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.Resolver)
            {
                DisableElementPositionTracking = true
            };

            var tokens = 0;
            while (true)
            {
                ref var token = ref tokenizer.GetStructToken();
                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    break;
                }
                tokens++;
            }

            return tokens;
        }

    }
}