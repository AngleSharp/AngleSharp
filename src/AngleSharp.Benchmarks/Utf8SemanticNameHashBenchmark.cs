#if NET10_0

using System;
using System.Linq;
using System.Numerics;
using AngleSharp.Html.Parser.Utf8;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

[MemoryDiagnoser, ShortRunJob]
public class Utf8SemanticNameHashBenchmark
{
    private Byte[][] _names = null!;

    [Params(
        Utf8NameHashBenchmark.HashCorpus.RealPageTags,
        Utf8NameHashBenchmark.HashCorpus.StandardVocabulary,
        Utf8NameHashBenchmark.HashCorpus.SharedPrefixNames
    )]
    public Utf8NameHashBenchmark.HashCorpus Corpus { get; set; }

    [Params(SemanticNameCasing.Lowercase, SemanticNameCasing.SparseMixedCase, SemanticNameCasing.MixedCase)]
    public SemanticNameCasing Casing { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var source = Corpus switch
        {
            Utf8NameHashBenchmark.HashCorpus.RealPageTags => Utf8NameHashBenchmark.ExtractPageTags(),
            Utf8NameHashBenchmark.HashCorpus.StandardVocabulary => Utf8NameHashBenchmark.StandardVocabulary(),
            Utf8NameHashBenchmark.HashCorpus.SharedPrefixNames => CreateSharedPrefixNames(),
            _ => throw new ArgumentOutOfRangeException(),
        };
        _names = ApplyCasing(Utf8NameHashBenchmark.RepeatToBatch(source), Casing);

        Console.WriteLine(
            $"Semantic hash corpus {Corpus}/{Casing}: {_names.Length:N0} names, "
                + $"{_names.Sum(static name => name.Length):N0} bytes."
        );
    }

    [Benchmark(Baseline = true)]
    public UInt64 ScalarBranchlessFold()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ Utf8NameHash.ComputeSemantic(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 UppercasePrescan()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ Utf8NameHash.ComputeSemanticWithUppercasePrescan(name);
        }
        return result;
    }

    private static Byte[][] ApplyCasing(Byte[][] source, SemanticNameCasing casing)
    {
        var result = new Byte[source.Length][];
        for (var nameIndex = 0; nameIndex < source.Length; nameIndex++)
        {
            var name = source[nameIndex].ToArray();
            if (casing == SemanticNameCasing.SparseMixedCase && nameIndex % 32 == 0)
            {
                UppercaseFirstLetter(name);
            }
            else if (casing == SemanticNameCasing.MixedCase)
            {
                for (var index = 0; index < name.Length; index += 2)
                {
                    name[index] = ToUpperAscii(name[index]);
                }
            }
            result[nameIndex] = name;
        }
        return result;
    }

    private static void UppercaseFirstLetter(Span<Byte> name)
    {
        for (var index = 0; index < name.Length; index++)
        {
            var upper = ToUpperAscii(name[index]);
            if (upper != name[index])
            {
                name[index] = upper;
                return;
            }
        }
    }

    private static Byte ToUpperAscii(Byte value) =>
        (UInt32)(value - (Byte)'a') <= 'z' - 'a' ? (Byte)(value & ~0x20) : value;

    private static Byte[][] CreateSharedPrefixNames() =>
        Enumerable
            .Range(0, 256)
            .Select(static index => System.Text.Encoding.ASCII.GetBytes($"data-field-{index:x8}"))
            .ToArray();

    public enum SemanticNameCasing
    {
        Lowercase,
        SparseMixedCase,
        MixedCase,
    }
}

#endif
