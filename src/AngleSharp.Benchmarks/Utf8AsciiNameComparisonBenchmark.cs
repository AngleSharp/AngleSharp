#if NET10_0_OR_GREATER

using System;
using System.Text;
using AngleSharp.Html.Parser.Utf8;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0, launchCount: 1, warmupCount: 10, iterationCount: 10)]
public class Utf8AsciiNameComparisonBenchmark
{
    private const Int32 Repetitions = 4096;

    private static readonly Byte[][] LongLeft =
    {
        Bytes("DaTa-Customer-Record-Id"),
        Bytes("DaTa-Customer-Record-Id"),
        Bytes("DaTa-Customer-Record-Id"),
        Bytes("ArIa-AcTiVeDeScEnDaNt"),
        Bytes("ArIa-MuLtIsElEcTaBlE"),
        Bytes("ArIa-MuLtIsElEcTaBlE"),
    };

    private static readonly Byte[][] LongRight =
    {
        Bytes("aria-activedescendant"),
        Bytes("aria-multiselectable"),
        Bytes("data-customer-record-id"),
        Bytes("aria-activedescendant"),
        Bytes("aria-activedescendant"),
        Bytes("aria-multiselectable"),
    };

    private static readonly Byte[][] ShortLeft =
    {
        Bytes("DaTa-Record"),
        Bytes("ArIa-LaBeL"),
        Bytes("HtTp-EqUiV"),
        Bytes("DaTa-Record"),
        Bytes("ArIa-LaBeL"),
        Bytes("HtTp-EqUiV"),
    };

    private static readonly Byte[][] ShortRight =
    {
        Bytes("data-record"),
        Bytes("aria-label"),
        Bytes("http-equiv"),
        Bytes("data-source"),
        Bytes("aria-level"),
        Bytes("html-equiv"),
    };

    private Byte[][] _left = null!;
    private Byte[][] _right = null!;

    [Params(false, true)]
    public Boolean LongNames { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _left = LongNames ? LongLeft : ShortLeft;
        _right = LongNames ? LongRight : ShortRight;
    }

    [Benchmark(Baseline = true)]
    public Int32 Scalar()
    {
        var matches = 0;
        for (var repetition = 0; repetition < Repetitions; repetition++)
        {
            for (var index = 0; index < _left.Length; index++)
            {
                var cache = default(Utf8HtmlNameIdentityCache);
                var name = new Utf8HtmlName(_left[index], ref cache);
                matches += ScalarEquals(name.Verbatim, _right[index]) ? 1 : 0;
            }
        }

        return matches;
    }

    [Benchmark]
    public Int32 Vector128()
    {
        var matches = 0;
        for (var repetition = 0; repetition < Repetitions; repetition++)
        {
            for (var index = 0; index < _left.Length; index++)
            {
                var cache = default(Utf8HtmlNameIdentityCache);
                var name = new Utf8HtmlName(_left[index], ref cache);
                matches += name.SemanticEquals(_right[index]) ? 1 : 0;
            }
        }

        return matches;
    }

    private static Boolean ScalarEquals(ReadOnlySpan<Byte> left, ReadOnlySpan<Byte> right)
    {
        if (left.Length != right.Length)
            return false;

        for (var index = 0; index < left.Length; index++)
        {
            var leftValue = left[index];
            var rightValue = right[index];
            if (leftValue == rightValue)
                continue;

            var leftFolded = (Byte)(leftValue | 0x20);
            if (
                (UInt32)(leftFolded - (Byte)'a') > (Byte)'z' - (Byte)'a'
                || leftFolded != (Byte)(rightValue | 0x20)
            )
                return false;
        }

        return true;
    }

    private static Byte[] Bytes(String value) => Encoding.ASCII.GetBytes(value);
}

#endif
