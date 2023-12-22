namespace AngleSharp.Benchmarks
{
    using System;
    using System.Collections.Frozen;
    using System.Linq;
    using BenchmarkDotNet.Engines;
    using BenchmarkDotNet.Environments;
    using BenchmarkDotNet.Jobs;
    using Html;
    using Text;
    using System.Collections.Generic;
    using Html.Parser;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using Dom;
    using Html.Parser.Tokens.Struct;
    using HtmlAgilityPack;

    [Config(typeof(Config))]
    [MemoryDiagnoser]
    public class ParserBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob(Job.ShortRun
                    .WithRuntime(CoreRuntime.Core80)
                    .WithStrategy(RunStrategy.Throughput)

                );
            }
        }

        public static readonly HtmlParserOptions HtmlParserOptions = new HtmlParserOptions()
        {
            IsStrictMode = false,
            IsScripting = false,
            IsNotConsumingCharacterReferences = true,
            IsNotSupportingFrames = true,
            IsSupportingProcessingInstructions = false,
            IsEmbedded = false,

            IsKeepingSourceReferences = false,
            IsPreservingAttributeNames = false,
            IsAcceptingCustomElementsEverywhere = false,

            SkipScriptText = true,
            SkipRawText = true,
            SkipDataText = false,
            SkipComments = true,
            SkipPlaintext = true,
            SkipCDATA = true,
            SkipRCDataText = true,
            SkipProcessingInstructions = true,
            DisableElementPositionTracking = true,
            ShouldEmitAttribute = static (ref StructHtmlToken _, ReadOnlyMemory<Char> n) =>
            {
                var s = n.Span;
                return s.Length switch
                {
                    2 => s[0] == 'i' && s[1] == 'd',
                    _ => false
                };
            },
        };

        private static readonly HtmlParserOptions DefaultOptions = new();

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(".html", true);

            websites.Include(
                new[]{
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

                "http://www.qq.com",
                "http://www.florian-rappl.de",
                "http://www.stackoverflow.com",

                "http://www.live.com",
                "http://www.taobao.com",
                "http://www.huffingtonpost.com",
                "http://www.wordpress.org",
                "http://www.myspace.com",
                "http://www.flickr.com",
                "http://www.godaddy.com",
                "http://www.reddit.com",

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

                "http://www.nbc.com",
                "http://www.ask.com",
                "http://www.mail.ru",
                "http://www.imdb.com",
                "http://www.kickass.to",
                "http://www.360.cn",
                "http://www.163.com",
                "http://www.tumblr.com",
                "http://www.html5rocks.com/en",
                "http://www.neobux.com",
                "http://www.nytimes.com",
                "http://www.aliexpress.com",
                "http://www.netflix.com",
                "http://www.w3.org/TR/html5/single-page.html",
                "http://en.wikipedia.org/wiki/South_African_labour_law",
                }).GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

        IBrowsingContext context = BrowsingContext.New(Configuration.Default);

        [Benchmark]
        public Boolean ReadonlyFiltered()
        {
            var filter = new FirstTagAndAllChildren("body");
            var parser = new HtmlParser(HtmlParserOptions, context);
            using var source = new PrefetchedTextSource(UrlTest.Source);
            using var document = parser.ParseReadOnlyDocument(source, filter.Loop);
            return false;
        }

        [Benchmark]
        public Boolean Readonly()
        {
            var parser = new HtmlParser(DefaultOptions, context);
            using var source = new PrefetchedTextSource(UrlTest.Source);
            using var document = parser.ParseReadOnlyDocument(source);
            return false;
        }

        [Benchmark(Baseline = true)]
        public Boolean Default()
        {
            var parser = new HtmlParser(DefaultOptions, context);
            using var document = parser.ParseDocument(UrlTest.Source);
            return false;
        }
    }
}