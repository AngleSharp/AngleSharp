using System;
using System.Text;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    using System.IO;
    using Html;
    using Html.Parser;
    using Html.Parser.Tokens.Struct;
    using Text;

    static class Program
    {
        static void Main(String[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var readOnlyMemory = File.ReadAllText($@"..\..\..\temp\w3.html").AsMemory();
            HtmlEntityProvider.Resolver.GetSymbol("nbsp");

            for (int i = 0; i < 2; i++)
            {
                int line = 0, count = 0;
                {
                    using var source = new PrefetchedTextSource(readOnlyMemory);
                    using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
                    StructHtmlToken token;
                    do
                    {
                        token = tokenizer.Get();
                        line = token.Position.Line;
                        count++;
                    } while (token.Type != HtmlTokenType.EndOfFile);
                }
                Console.WriteLine(count + line - line);
            }

            Console.ReadLine();

            {
                int line = 0, count = 0;
                {
                    using var source = new PrefetchedTextSource(readOnlyMemory);
                    using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
                    StructHtmlToken token;
                    do
                    {
                        token = tokenizer.Get();
                        line = token.Position.Line;
                        count++;
                    } while (token.Type != HtmlTokenType.EndOfFile);
                    Console.WriteLine(source.Index);

                }
                Console.WriteLine(count);

            }

            Console.ReadLine();


            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
