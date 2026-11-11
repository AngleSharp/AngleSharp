namespace AngleSharp.Benchmarks;

using System;
using System.IO;
using System.Text;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using BenchmarkDotNet.Attributes;

/// <summary>
/// Parses the synthetic page of <see cref="ComplexDocument"/> through every public entry point
/// of the HTML parser. Works offline, so it is the benchmark to iterate on for parser changes.
/// </summary>
[MemoryDiagnoser]
public class ComplexDocumentBenchmark
{
    private static readonly HtmlParser Parser = new();

    private String _html;
    private Byte[] _bytes;

    [GlobalSetup]
    public void Setup()
    {
        _html = ComplexDocument.Create();
        _bytes = Encoding.UTF8.GetBytes(_html);
    }

    [Benchmark(Description = "Tokenize only")]
    public Int32 Tokenize()
    {
        // The source the parser itself picks for a string when scripting is off.
        var source = new TextSource(new ReadOnlyMemoryTextSource(_html.AsMemory()));
        using var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.Resolver);
        var tokens = 0;

        while (true)
        {
            ref var token = ref tokenizer.GetStructToken();

            if (token.Type == HtmlTokenType.EndOfFile)
            {
                break;
            }

            tokens++;
        }

        return tokens;
    }

    [Benchmark(Baseline = true, Description = "ParseDocument(String)")]
    public Object ParseString() => Parser.ParseDocument(_html);

    [Benchmark(Description = "ParseDocument(ReadOnlyMemory)")]
    public void ParseMemory()
    {
        using var _ = Parser.ParseDocument(_html.AsMemory());
    }

    [Benchmark(Description = "ParseDocument(Stream)")]
    public Object ParseStream()
    {
        using var stream = new MemoryStream(_bytes, false);
        return Parser.ParseDocument(stream);
    }
}
