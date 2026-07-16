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

#if NET10_0
            if (
                args.Length == 1
                && args[0].Equals("--utf8-name-hash-collisions", StringComparison.OrdinalIgnoreCase)
            )
            {
                Utf8NameHashBenchmark.PrintCollisionReport();
                return;
            }
#endif

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
