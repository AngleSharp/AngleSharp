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
        static void Main(String[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            var file = @"..\..\..\temp\en.wikipedia.html";
            var readOnlyMemory = File.ReadAllText(file);

            for (int i = 0; i < 175; i++)
            {
                using var source = new PrefetchedTextSource(readOnlyMemory);
                using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
                StructHtmlToken token;
                int sum = 0;
                do
                {
                    token = tokenizer.Get();
                    if (token.Type == HtmlTokenType.StartTag)
                    {
                        sum+=token.Attributes.Count;
                    }

                } while (token.Type != HtmlTokenType.EndOfFile);

                Console.WriteLine(sum);
            }


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
