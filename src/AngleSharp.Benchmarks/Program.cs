using System;
using System.Text;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    using System.IO;
    using Html.Parser;
    using Text;

    static class Program
    {
        static void Main(String[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // var source = new PrefetchedTextSource(File.ReadAllText($@"..\..\..\temp\amazon.html").AsMemory());
            // var parser = new HtmlParser();
            // // var parser = new HtmlParser(ParserBenchmark.HtmlParserOptions);
            // var document = parser.ParseDocument(source);
            // Console.WriteLine(document.ToHtml());
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
