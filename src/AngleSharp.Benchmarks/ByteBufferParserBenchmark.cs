namespace AngleSharp.Benchmarks;

using System;
using System.IO;
using AngleSharp.Html.Parser;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser, ShortRunJob]
public class ByteBufferParserBenchmark
{
    private readonly HtmlParser _parser = new();
    private Byte[] _utf8 = null!;

    [GlobalSetup]
    public void Setup() => _utf8 = File.ReadAllBytes(FindPagePath());

    [Benchmark(Baseline = true)]
    public Int32 MemoryStream()
    {
        using var stream = new MemoryStream(_utf8, writable: false);
        using var document = _parser.ParseDocument(stream);
        return document.All.Length;
    }

    [Benchmark]
    public Int32 ReadOnlyMemory()
    {
        using var document = _parser.ParseDocument(_utf8.AsMemory());
        return document.All.Length;
    }

    private static String FindPagePath()
    {
        for (var directory = new DirectoryInfo(AppContext.BaseDirectory);
             directory is not null;
             directory = directory.Parent)
        {
            var path = Path.Combine(directory.FullName, "page.html");

            if (File.Exists(path))
            {
                return path;
            }
        }

        throw new FileNotFoundException("Could not locate the page.html benchmark fixture.");
    }
}
