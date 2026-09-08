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

            if (
                args.Length == 1
                && args[0].Equals("--text-source-dispatch", StringComparison.OrdinalIgnoreCase)
            )
            {
                BenchmarkRunner.Run<TextSourceDispatchBenchmark>();
                return;
            }

            if (
                args.Length == 1
                && args[0].Equals("--scan-data-text", StringComparison.OrdinalIgnoreCase)
            )
            {
                BenchmarkRunner.Run<ScanDataTextBenchmark>();
                return;
            }

            if (args.Length > 0 && args[0].Equals("--class-name-matching", StringComparison.OrdinalIgnoreCase))
            {
                var benchmarkArgs = new String[args.Length - 1];
                Array.Copy(args, 1, benchmarkArgs, 0, benchmarkArgs.Length);
                BenchmarkSwitcher.FromTypes(new[] { typeof(ClassNameMatchingBenchmark) }).Run(benchmarkArgs);
                return;
            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
