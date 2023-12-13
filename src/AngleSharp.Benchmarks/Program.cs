using System;
using System.Text;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    static class Program
    {
        static void Main(String[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var parserBenchmark = new ParserBenchmark();
            parserBenchmark.Setup();

            Console.ReadLine();

            parserBenchmark.AngleSharp();

            Console.ReadLine();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
