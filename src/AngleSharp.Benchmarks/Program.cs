using System;
using System.Text;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Html;
    using Html.Parser;
    using Html.Parser.Tokens.Struct;
    using Text;

    static class Program
    {
        private static ReadOnlySpan<char> id => "id";
        private static ReadOnlySpan<char> @class => "class";

        static void Main(String[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (args.Length > 0)
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            var file = @"..\..\..\temp\w3.html";
            // var file = @"..\..\..\page.html";

            var readOnlyMemory = File.ReadAllText(file);

            var options = new HtmlParserOptions()
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

            var context = BrowsingContext.New(Configuration.Default);

            for (int i = 0; i < 50; i++)
            {
                var filter = new ParserBenchmark.TokenFilter("p", "some-magical-id");
                var parser = new HtmlParser(options, context);
                using var source = new PrefetchedTextSource(readOnlyMemory);
                using var document = parser.ParseDocument(source, filter.Loop);
                var result = document.QuerySelector("p#some-magical-id");
                // Console.WriteLine(result?.ToHtml());
            }


            // using var source = new PrefetchedTextSource(readOnlyMemory);
            // using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
            // StructHtmlToken token;
            // int sum = 0;
            // do
            // {
            //     token = tokenizer.Get();
            //     if (token.Type == HtmlTokenType.StartTag)
            //     {
            //         sum+=token.Attributes.Count;
            //     }
            //
            // } while (token.Type != HtmlTokenType.EndOfFile);
            //
            // Console.WriteLine(sum);



            // return new
            // {
            //     file,
            //     avg = atrs.Count > 0 ? atrs.Average() : -1,
            //     max = atrs.Count > 0 ? atrs.Max() : -1,
            //     count = atrs.Count
            // };


            /*
            var stats = Directory.EnumerateFiles(@"..\..\..\temp\", "*.html", SearchOption.AllDirectories)
                .Select(file =>
                {
                    var readOnlyMemory = File.ReadAllText(file).AsMemory();
                    using var source = new PrefetchedTextSource(readOnlyMemory);
                    using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
                    StructHtmlToken token;
                    List<int> atrs = new();
                    do
                    {
                        token = tokenizer.Get();
                        if (token.Type == HtmlTokenType.StartTag)
                        {
                            atrs.Add(token.Attributes.Count);
                        }

                    } while (token.Type != HtmlTokenType.EndOfFile);

                    return new
                    {
                        file,
                        avg = atrs.Count > 0 ? atrs.Average() : -1,
                        max = atrs.Count > 0 ? atrs.Max() : -1,
                        count = atrs.Count
                    };
                });

            // foreach (var stat in stats)
            // {
            //     Console.WriteLine(stat);
            // }
*/

        }
    }
}
