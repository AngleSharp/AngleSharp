namespace AngleSharp.Benchmarks;

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Html;
using Html.Parser;
using Text;

[MemoryDiagnoser, ShortRunJob]
public class ScanDataTextBenchmark
{
    [Params(100, 1_000, 10_000)]
    public Int32 Repeats { get; set; }
    private String _html;
    private Char[] _chars;

    [GlobalSetup]
    public void Setup()
    {
        _html = String.Concat(Enumerable.Repeat("<p>Hello World.<br>How are you?<br/>All good<br /> Ok, bye.</p>", Repeats));
        _chars = _html.ToCharArray();
    }

    [Benchmark]
    public Int32 StringSource() => Go(new TextSource(_html));

    [Benchmark]
    public Int32 CharSource() => Go(new TextSource(new CharArrayTextSource(_chars, _chars.Length)));

    [Benchmark]
    public Int32 MemSource() => Go(new TextSource(new ReadOnlyMemoryTextSource(_html.AsMemory())));

    private static Int32 Go(TextSource source)
    {
        var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.Resolver)
        {
            DisableElementPositionTracking = true
        };

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
}
