using System.Collections.Generic;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace AngleSharp.Benchmarks
{
    using System;
    using System.Collections.Frozen;
    using BenchmarkDotNet.Engines;
    using BenchmarkDotNet.Environments;
    using BenchmarkDotNet.Jobs;
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
                    .WithStrategy(RunStrategy.Throughput)
                    .WithLaunchCount(1)
                );
            }
        }

        public static readonly FrozenDictionary<string, FrozenSet<string>> AllowedAttributes =
            new Dictionary<String, FrozenSet<String>>
            {
                ["div"] = new[] { "id" }.ToFrozenSet(),
                ["span"] = new[] { "id", "class" }.ToFrozenSet(),
                ["table"] = new[] { "id", "class" }.ToFrozenSet(),
                ["meta"] = new [] { "charset" }.ToFrozenSet()
            }.ToFrozenDictionary();

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
                AllowedAttributes.TryGetValue(token.Name, out var allowed)
                && allowed.Contains(attributeName),
        };

        public static readonly HtmlTokenizerOptions HtmlTokenizerOptions = new HtmlTokenizerOptions(HtmlParserOptions);

        public IEnumerable<UrlTest> GetSources()
        {
            var websites = new UrlTests(
                ".html",
                true);

            websites.Include(
                "http://www.amazon.com",
                "http://www.nbc.com",
                "http://www.nytimes.com",
                "http://www.spiegel.de",
                "http://www.vk.com",
                "http://www.mail.ru",
                "http://www.netflix.com",
                "http://en.wikipedia.org/wiki/South_African_labour_law").GetAwaiter().GetResult();

            return websites.Tests;
        }

        [ParamsSource(nameof(GetSources))] public UrlTest UrlTest { get; set; }

        [Benchmark]
        public IHtmlDocument AngleSharpPrefetched()
        {
            var parser = new HtmlParser(HtmlParserOptions);
            var source = new PrefetchedTextSource(UrlTest.Source.AsMemory());
            var document = parser.ParseDocument(source);
            return document;
        }

        [Benchmark]
        public Int32 AngleSharpTokensPrefetched()
        {
            int line = 0, count = 0;

            var source = new PrefetchedTextSource(UrlTest.Source.AsMemory());
            foreach (var token in source.Tokenize(options: HtmlTokenizerOptions))
            {
                line = token.Position.Line;
                count++;
            }

            return count + line;
        }
    }
}