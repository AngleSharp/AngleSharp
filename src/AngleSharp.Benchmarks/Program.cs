using System;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    static class Program
    {
        static void Main(String[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
