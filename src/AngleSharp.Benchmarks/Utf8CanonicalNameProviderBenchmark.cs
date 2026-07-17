#if NET10_0

using System;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Parser.Utf8;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

[MemoryDiagnoser, ShortRunJob]
public class Utf8CanonicalNameProviderBenchmark
{
    private Byte[][] _names = null!;

    [Params(CanonicalNameCorpus.SparseHits, CanonicalNameCorpus.SameLengthMisses)]
    public CanonicalNameCorpus Corpus { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var source = Corpus switch
        {
            CanonicalNameCorpus.SparseHits => SparseNames.Select(Encoding.ASCII.GetBytes).ToArray(),
            CanonicalNameCorpus.SameLengthMisses => SparseNames.Select(CreateSameLengthMiss).ToArray(),
            _ => throw new ArgumentOutOfRangeException(),
        };
        _names = Utf8NameHashBenchmark.RepeatToBatch(source);
    }

    [Benchmark(Baseline = true)]
    public Int32 SparseHashSwitch()
    {
        var result = 0;
        foreach (var name in _names)
        {
            if (TryGetSparseTag(name, out var canonical))
            {
                result += canonical.Length;
            }
        }
        return result;
    }

    [Benchmark]
    public Int32 GeneratedByteTree()
    {
        var result = 0;
        foreach (var name in _names)
        {
            if (Utf8CanonicalNameProvider.TryGetHtmlTag(name, out var canonical))
            {
                result += canonical.Length;
            }
        }
        return result;
    }

    internal static Boolean TryGetSparseTag(ReadOnlySpan<Byte> name, out String canonical)
    {
        canonical = Utf8NameHash.ComputeSemantic(name) switch
        {
            0xAF63DC4C8601EC8CUL => TagNames.A,
            0xAF63ED4C8602096FUL => TagNames.P,
            0x0BB51791194B4414UL => TagNames.Code,
            0x08C83407B56AB825UL => TagNames.Td,
            0x77FD511956A1EDC6UL => TagNames.Pre,
            0x08AD3707B553F586UL => TagNames.Li,
            0x08C83E07B56AC923UL => TagNames.Tr,
            0xCAA83A18F46E5888UL => TagNames.Div,
            0x690418194ED15D3EUL => TagNames.Var,
            0x08915407B53BAC15UL => TagNames.Dd,
            0x08A64607B54DF055UL => TagNames.Br,
            0x08914407B53B90E5UL => TagNames.Dt,
            0x08C45607B5670914UL => TagNames.Ul,
            0x08C83807B56ABEF1UL => TagNames.Th,
            0x08BA8607B55F061FUL => TagNames.H2,
            0x08BA8407B55F02B9UL => TagNames.H4,
            0x08BA8307B55F0106UL => TagNames.H5,
            0xA5E9F6D91985A3DAUL => TagNames.Strong,
            0xAF63DF4C8601F1A5UL => TagNames.B,
            0x08BA8507B55F046CUL => TagNames.H3,
            0xF9DAA9910F08943EUL => TagNames.Cite,
            0x088E3707B53944F7UL => TagNames.Em,
            0x8B7DC019093CD0E1UL => TagNames.Span,
            0xCA972418F45FBFF3UL => TagNames.Dfn,
            0x888BB1CC15EF7930UL => TagNames.Html,
            0x0A8F12CC5F9A0C03UL => TagNames.Head,
            0xCD4DE79BC6C93295UL => TagNames.Body,
            0x4320E9A2E32EAC38UL => TagNames.Meta,
            0xDA31296C0C1B6029UL => TagNames.Title,
            0xACFC82293C04634AUL => TagNames.Script,
            0xBF7282ADBC7013F6UL => TagNames.Style,
            0x77203729B376A83FUL => TagNames.Table,
            0xE1CB381F1F501FABUL => TagNames.Tbody,
            0xEB218F725DDD9B79UL => TagNames.Thead,
            0xE4444542747391BBUL => TagNames.Tfoot,
            0xDD1D0F790C2F1BE7UL => TagNames.Form,
            0x1EBBAE8F5810B65BUL => TagNames.Input,
            0x2B9CEE192BD27584UL => TagNames.Img,
            0xBF4B9BAD694F4809UL => TagNames.Link,
            _ => null!,
        };
        return canonical is not null && EqualsAsciiIgnoreCase(name, canonical);
    }

    private static Boolean EqualsAsciiIgnoreCase(ReadOnlySpan<Byte> name, String canonical)
    {
        if (name.Length != canonical.Length)
        {
            return false;
        }
        for (var index = 0; index < name.Length; index++)
        {
            var left = ToLowerAscii(name[index]);
            var right = ToLowerAscii((Byte)canonical[index]);
            if (left != right)
            {
                return false;
            }
        }
        return true;
    }

    private static Byte ToLowerAscii(Byte value) =>
        (UInt32)(value - (Byte)'A') <= 'Z' - 'A' ? (Byte)(value | 0x20) : value;

    private static Byte[] CreateSameLengthMiss(String value)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        bytes[^1] = (Byte)'~';
        return bytes;
    }

    internal static Byte[][] GetAllHtmlTags()
    {
        var excluded = new[]
        {
            TagNames.Doctype,
            TagNames.Math,
            TagNames.Mi,
            TagNames.Mo,
            TagNames.Mn,
            TagNames.Ms,
            TagNames.Mtext,
            TagNames.AnnotationXml,
            TagNames.Svg,
            TagNames.ForeignObject,
            TagNames.Desc,
            TagNames.Circle,
            TagNames.Xml,
        }.ToHashSet(StringComparer.Ordinal);
        return typeof(TagNames)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Select(static field => field.GetValue(null))
            .OfType<String>()
            .Where(value => !excluded.Contains(value))
            .Select(Encoding.ASCII.GetBytes)
            .ToArray();
    }

    private static readonly String[] SparseNames =
    [
        "a", "p", "code", "td", "pre", "li", "tr", "div", "var", "dd", "br", "dt", "ul", "th", "h2",
        "h4", "h5", "strong", "b", "h3", "cite", "em", "span", "dfn", "html", "head", "body", "meta",
        "title", "script", "style", "table", "tbody", "thead", "tfoot", "form", "input", "img", "link",
    ];

    public enum CanonicalNameCorpus
    {
        SparseHits,
        SameLengthMisses,
    }
}

[MemoryDiagnoser, ShortRunJob]
public class Utf8CanonicalNameFallbackBenchmark
{
    private Byte[][] _names = null!;

    [GlobalSetup]
    public void Setup() =>
        _names = Utf8NameHashBenchmark.RepeatToBatch(Utf8CanonicalNameProviderBenchmark.GetAllHtmlTags());

    [Benchmark(Baseline = true)]
    public Int32 SparseHashSwitchWithDecodeFallback()
    {
        var result = 0;
        foreach (var name in _names)
        {
            result += Utf8CanonicalNameProviderBenchmark.TryGetSparseTag(name, out var canonical)
                ? canonical.Length
                : Encoding.UTF8.GetString(name).Length;
        }
        return result;
    }

    [Benchmark]
    public Int32 GeneratedByteTreeWithDecodeFallback()
    {
        var result = 0;
        foreach (var name in _names)
        {
            result += Utf8CanonicalNameProvider.TryGetHtmlTag(name, out var canonical)
                ? canonical.Length
                : Encoding.UTF8.GetString(name).Length;
        }
        return result;
    }
}

#endif
