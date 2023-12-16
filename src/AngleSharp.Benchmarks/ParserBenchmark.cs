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
    using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
    using Html.Dom;
    using Html.Parser.Tokens;
    using Html.Parser.Tokens.Struct;
    using Perfolizer.Horology;

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
                    // .WithToolchain(InProcessNoEmitToolchain.Instance)
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
                        .ToFrozenSet(ReadOnlyMemoryComparer.Instance),
                    ReadOnlyMemoryComparer.Instance);

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
            SkipComments = true,
            SkipPlaintext = true,
            SkipCDATA = true,
            SkipRCDataText = true,
            SkipProcessingInstructions = true,

            DisableElementPositionTracking = true,

            ShouldEmitAttribute = (token, attributeName) =>
                AllowedAttributes.TryGetValue(token.Name, out var allowed)
                && allowed.Contains(attributeName),
        };

        public static readonly HtmlTokenizerOptions HtmlTokenizerOptions = new HtmlTokenizerOptions(HtmlParserOptions);

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(".html", true);

            websites.Include(
                // "http://www.amazon.com",
                // "http://www.blogspot.com",
                // "http://www.smashing.com",
                // "http://www.youtube.com",
                // "http://www.weibo.com",
                // "http://www.yahoo.com",
                // "http://www.google.com",
                // "http://www.linkedin.com",
                // "http://www.pinterest.com",
                // "http://news.google.com",
                // "http://www.baidu.com",
                // "http://www.codeproject.com",
                // "http://www.ebay.com",
                // "http://www.msn.com",
                //
                // "http://www.qq.com",
                // "http://www.florian-rappl.de",
                // "http://www.stackoverflow.com",
                //
                // "http://www.live.com",
                // "http://www.taobao.com",
                // "http://www.huffingtonpost.com",
                // "http://www.wordpress.org",
                // "http://www.myspace.com",
                // "http://www.flickr.com",
                // "http://www.godaddy.com",
                // "http://www.reddit.com",
                //
                // "http://peacekeeper.futuremark.com",
                // "http://www.pcmag.com",
                // "http://www.sitepoint.com",
                // "http://html5test.com",
                // "http://www.spiegel.de",
                // "http://www.tmall.com",
                // "http://www.sohu.com",
                // "http://www.vk.com",
                // "http://www.wordpress.com",
                // "http://www.bing.com",
                //
                // "http://www.nbc.com",
                // "http://www.ask.com",
                // "http://www.mail.ru",
                // "http://www.imdb.com",
                // "http://www.kickass.to",
                // "http://www.360.cn",
                // "http://www.163.com",
                // "http://www.tumblr.com",
                // "http://www.html5rocks.com/en",
                // "http://www.neobux.com",
                "http://www.nytimes.com",
                "http://www.aliexpress.com",
                "http://www.netflix.com",
                "http://www.w3.org/TR/html5/single-page.html",
                "http://en.wikipedia.org/wiki/South_African_labour_law"
                ).GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

        [Benchmark]
        public IHtmlDocument AngleSharpPrefetched()
        {
            var parser = new HtmlParser();
            using var source = new PrefetchedTextSource(UrlTest.Source);
            using var document = parser.ParseDocument(source);
            return document;
        }

        [Benchmark(Baseline = true)]
        public IHtmlDocument AngleSharp()
        {
            var parser = new HtmlParser();
            using var source = new TextSource(UrlTest.Source);
            using var document = parser.ParseDocument(source);
            return document;
        }

        // [Benchmark(Baseline = true)]
        // public Int32 Classes()
        // {
        //     int line = 0, count = 0;
        //     using var source = new TextSource(UrlTest.Source);
        //     using var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.Resolver);
        //
        //     HtmlToken token;
        //     do
        //     {
        //         token = tokenizer.Get();
        //         line = token.Position.Line;
        //         count++;
        //     }
        //     while (token.Type != HtmlTokenType.EndOfFile);
        //     return count + line;
        // }
        //
        // [Benchmark]
        // public Int32 Structs()
        // {
        //     int line = 0, count = 0;
        //     using var source = new PrefetchedTextSource(UrlTest.Source);
        //     using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
        //
        //     StructHtmlToken token;
        //     do
        //     {
        //         token = tokenizer.Get();
        //         line = token.Position.Line;
        //         count++;
        //     }
        //     while (token.Type != HtmlTokenType.EndOfFile);
        //     return count + line;
        // }
    }
}