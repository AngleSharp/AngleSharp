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
                args.Length != 0
                && args[0].Equals("--profile-host", StringComparison.OrdinalIgnoreCase)
            )
            {
                TokenizerProfileHost.Run(args[1..]);
                return;
            }

            if (
                args.Length == 1
                && args[0].Equals("--utf8-name-hash-collisions", StringComparison.OrdinalIgnoreCase)
            )
            {
                Utf8NameHashBenchmark.PrintCollisionReport();
                return;
            }

            if (
                args.Length is 1 or 2
                && args[0].Equals("--utf8-adapter-accounting", StringComparison.OrdinalIgnoreCase)
            )
            {
                Utf8AdapterSeamAccounting.RunAsync(args.Length == 2 ? args[1] : "page.html")
                    .GetAwaiter()
                    .GetResult();
                return;
            }
#endif

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
