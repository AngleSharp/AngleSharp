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
                    .WithStrategy(RunStrategy.Monitoring)

                );
            }
        }

        // public static readonly FrozenDictionary<ReadOnlyMemory<Char>, FrozenSet<ReadOnlyMemory<Char>>>
        //     AllowedAttributes =
        //         new Dictionary<String, String[]>
        //         {
        //             ["div"] = new[] { "id" },
        //             ["span"] = new[] { "id", "class" },
        //             ["table"] = new[] { "id", "class" },
        //             ["meta"] = new[] { "charset" }
        //         }.ToFrozenDictionary(
        //             kvp => kvp.Key.AsMemory(),
        //             kvp => kvp.Value
        //                 .Select(v => v.AsMemory())
        //                 .ToFrozenSet(ReadOnlyMemoryComparer.Instance),
        //             ReadOnlyMemoryComparer.Instance);

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
        public static readonly HtmlTokenizerOptions HtmlTokenizerOptions = new HtmlTokenizerOptions(HtmlParserOptions);

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(".html", true);

            websites.Include(
                new[]{
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
                }
                ).GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

        // [Benchmark]
        // public HtmlDocument CreateDocument()
        // {
        //     var htmlDocument = new HtmlDocument();
        //     htmlDocument.CreateComment("This is a comment");
        //     return htmlDocument;
        // }

        IBrowsingContext context = BrowsingContext.New(Configuration.Default);

        [Benchmark]
        public Boolean CustomOptionsFiltered()
        {
            var filter = new OnlyElementWithId("p", "some-magical-id");
            var parser = new HtmlParser(HtmlParserOptions, context);
            using var source = new PrefetchedTextSource(UrlTest.Source);
            using var document = parser.ParseDocumentStruct(source, filter.Loop);
            var result = document.QuerySelector("p#some-magical-id") != null;
            return result;
        }

        [Benchmark]
        public Boolean CustomOptions()
        {
            var parser = new HtmlParser(HtmlParserOptions, context);
            using var source = new PrefetchedTextSource(UrlTest.Source);
            using var document = parser.ParseDocumentStruct(source);
            var result = document.QuerySelector("p#some-magical-id") != null;
            return result;
        }

        [Benchmark(Baseline = true)]
        public Boolean Default()
        {
            var parser = new HtmlParser();
            using var document = parser.ParseDocument(UrlTest.Source);
            var result = document.QuerySelector("p#some-magical-id") != null;
            return result;
        }
        //
        // [Benchmark]
        // public Boolean AngleSharp1()
        // {
        //     var parser = new HtmlParser();
        //     using var source = new PrefetchedTextSource(UrlTest.Source);
        //     using var document = parser.ParseDocument2(source);
        //     var result = document.QuerySelector("p#some-magical-id") != null;
        //     return result;
        // }
        //
        // [Benchmark(Baseline = true)]
        // public Boolean AngleSharp0()
        // {
        //     var parser = new HtmlParser();
        //     using var source = new TextSource(UrlTest.Source);
        //     using var document = parser.ParseDocument(source);
        //     var result = document.QuerySelector("p#some-magical-id") != null;
        //     return result;
        // }

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