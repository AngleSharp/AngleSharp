using System.Collections.Generic;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    using System;
    using System.Collections.Frozen;
    using System.Linq;
    using BenchmarkDotNet.Engines;
    using BenchmarkDotNet.Environments;
    using BenchmarkDotNet.Jobs;
    using Html;
    using Html.Dom;
    using Text;

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
                    .WithStrategy(RunStrategy.Monitoring)
                    .WithLaunchCount(1)
                );
            }
        }

        public static readonly FrozenDictionary<ReadOnlyMemory<Char>, FrozenSet<ReadOnlyMemory<Char>>>
            AllowedAttributes =
                new Dictionary<String, String[]>
                {
                    ["div"] = new[] { "id" },
                    ["span"] = new[] { "id", "class" },
                    ["table"] = new[] { "id", "class" },
                    ["meta"] = new[] { "charset" }
                }.ToFrozenDictionary(
                    kvp => kvp.Key.AsMemory(),
                    kvp => kvp.Value
                        .Select(v => v.AsMemory())
                        .ToFrozenSet(MemStringEqualityComparer.Instance),
                    MemStringEqualityComparer.Instance);

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
            SkipRawText = false,
            SkipDataText = true,

            ShouldEmitAttribute = (token, attributeName) =>
                AllowedAttributes.TryGetValue(token.Name.AsMemory(), out var allowed)
                && allowed.Contains(attributeName),
        };

        public static readonly HtmlTokenizerOptions HtmlTokenizerOptions = new HtmlTokenizerOptions(HtmlParserOptions);

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(".html", true);

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
                "http://en.wikipedia.org/wiki/South_African_labour_law"

                ).GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

        // [Benchmark]
        // public IHtmlDocument AngleSharpPrefetched()
        // {
        //     var parser = new HtmlParser(HtmlParserOptions);
        //     var source = new PrefetchedTextSource(UrlTest.Source);
        //     var document = parser.ParseDocument(source);
        //     return document;
        // }

        [Benchmark]
        public Int32 AngleSharpTokensPrefetched()
        {
            int line = 0, count = 0;

            var source = new PrefetchedTextSource(UrlTest.Source.AsMemory());

            Console.WriteLine($"Source length: {UrlTest.Source.Length}");
            foreach (var token in source.Tokenize(options: HtmlTokenizerOptions))
            {
                line = token.Position.Line;
                count++;
            }

            return count + line;
        }
    }
}