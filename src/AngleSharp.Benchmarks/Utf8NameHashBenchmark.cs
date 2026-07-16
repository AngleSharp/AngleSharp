#if NET10_0

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Hashing;
using System.Linq;
using System.Numerics;
using System.Text;
using AngleSharp.Html.Parser.Utf8;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

[MemoryDiagnoser, ShortRunJob]
public class Utf8NameHashBenchmark
{
    private const Int32 BatchSize = 4096;
    private const UInt64 FnvPrime = 1099511628211;
    private const UInt64 MixPrime1 = 0x9E3779B185EBCA87;
    private const UInt64 MixPrime2 = 0xC2B2AE3D27D4EB4F;
    private Byte[][] _names = null!;

    [Params(HashCorpus.RealPageTags, HashCorpus.StandardVocabulary, HashCorpus.SharedPrefixNames)]
    public HashCorpus Corpus { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _names = Corpus switch
        {
            HashCorpus.RealPageTags => RepeatToBatch(ExtractPageTags()),
            HashCorpus.StandardVocabulary => RepeatToBatch(StandardVocabulary()),
            HashCorpus.SharedPrefixNames => SharedPrefixNames(BatchSize),
            _ => throw new ArgumentOutOfRangeException(),
        };

        var bytes = _names.Sum(static name => name.Length);
        Console.WriteLine(
            $"UTF-8 hash corpus {Corpus}: {_names.Length:N0} names, {bytes:N0} bytes."
        );
    }

    [Benchmark(Baseline = true)]
    public UInt64 IncrementalFnv1A()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            var hash = Utf8NameHash.Offset;
            foreach (var character in name)
            {
                hash = Utf8NameHash.Append(hash, character);
            }
            result = BitOperations.RotateLeft(result, 7) ^ hash;
        }
        return result;
    }

    [Benchmark]
    public UInt64 CurrentFnv1A()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ Utf8NameHash.Compute(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 InlineScalarFnv1A()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ ComputeScalarFnv1A(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 UnrolledFnv1A()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ ComputeUnrolledFnv1A(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 Djb2()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ ComputeDjb2(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 Sdbm()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ ComputeSdbm(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 ShortLoadMix()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ ComputeShortLoadMix(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 XxHash3Algorithm()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ XxHash3.HashToUInt64(name);
        }
        return result;
    }

    [Benchmark]
    public UInt64 XxHash64Algorithm()
    {
        var result = 0UL;
        foreach (var name in _names)
        {
            result = BitOperations.RotateLeft(result, 7) ^ XxHash64.HashToUInt64(name);
        }
        return result;
    }

    public static void PrintCollisionReport()
    {
        var names = SharedPrefixNames(65_536);
        Console.WriteLine(
            $"Collision corpus: {names.Length:N0} unique shared-prefix HTML names; 4,096 low-bit buckets."
        );
        Console.WriteLine("Algorithm             collisions   max bucket");
        PrintCollisionLine("FNV-1a", names, Utf8NameHash.Compute);
        PrintCollisionLine("djb2", names, ComputeDjb2);
        PrintCollisionLine("SDBM", names, ComputeSdbm);
        PrintCollisionLine("short-load-mix", names, ComputeShortLoadMix);
        PrintCollisionLine("xxHash3", names, ComputeXxHash3);
        PrintCollisionLine("xxHash64", names, ComputeXxHash64);
    }

    private static UInt64 ComputeUnrolledFnv1A(ReadOnlySpan<Byte> value)
    {
        var hash = Utf8NameHash.Offset;
        var index = 0;
        for (; index <= value.Length - 4; index += 4)
        {
            hash = (hash ^ value[index]) * FnvPrime;
            hash = (hash ^ value[index + 1]) * FnvPrime;
            hash = (hash ^ value[index + 2]) * FnvPrime;
            hash = (hash ^ value[index + 3]) * FnvPrime;
        }
        for (; index < value.Length; index++)
        {
            hash = (hash ^ value[index]) * FnvPrime;
        }
        return hash;
    }

    private static UInt64 ComputeScalarFnv1A(ReadOnlySpan<Byte> value)
    {
        var hash = Utf8NameHash.Offset;
        foreach (var character in value)
        {
            hash = (hash ^ character) * FnvPrime;
        }
        return hash;
    }

    private static UInt64 ComputeDjb2(ReadOnlySpan<Byte> value)
    {
        var hash = 5381UL;
        foreach (var character in value)
        {
            hash = ((hash << 5) + hash) ^ character;
        }
        return hash;
    }

    private static UInt64 ComputeSdbm(ReadOnlySpan<Byte> value)
    {
        var hash = 0UL;
        foreach (var character in value)
        {
            hash = character + (hash << 6) + (hash << 16) - hash;
        }
        return hash;
    }

    private static UInt64 ComputeXxHash3(ReadOnlySpan<Byte> value) => XxHash3.HashToUInt64(value);

    private static UInt64 ComputeXxHash64(ReadOnlySpan<Byte> value) => XxHash64.HashToUInt64(value);

    private static UInt64 ComputeShortLoadMix(ReadOnlySpan<Byte> value)
    {
        var hash = MixPrime1 ^ ((UInt64)value.Length * MixPrime2);
        while (value.Length >= sizeof(UInt64))
        {
            hash ^= Mix(BinaryPrimitives.ReadUInt64LittleEndian(value));
            hash = BitOperations.RotateLeft(hash, 27) * MixPrime1 + MixPrime2;
            value = value[sizeof(UInt64)..];
        }

        var tail = 0UL;
        for (var index = 0; index < value.Length; index++)
        {
            tail |= (UInt64)value[index] << (index * 8);
        }
        return Mix(hash ^ tail);
    }

    private static UInt64 Mix(UInt64 value)
    {
        value ^= value >> 33;
        value *= 0xFF51AFD7ED558CCD;
        value ^= value >> 33;
        value *= 0xC4CEB9FE1A85EC53;
        return value ^ (value >> 33);
    }

    private static Byte[][] ExtractPageTags()
    {
        var html = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "page.html"));
        var names = new List<Byte[]>();
        for (var index = 0; index < html.Length; index++)
        {
            if (html[index] != (Byte)'<')
            {
                continue;
            }

            index++;
            if (index < html.Length && html[index] == (Byte)'/')
            {
                index++;
            }
            if (index >= html.Length || !IsAsciiLetter(html[index]))
            {
                continue;
            }

            var start = index;
            while (index < html.Length && IsNameByte(html[index]))
            {
                index++;
            }

            var name = html.AsSpan(start, index - start).ToArray();
            for (var nameIndex = 0; nameIndex < name.Length; nameIndex++)
            {
                if (name[nameIndex] is >= (Byte)'A' and <= (Byte)'Z')
                {
                    name[nameIndex] |= 0x20;
                }
            }
            names.Add(name);
        }
        return names.ToArray();
    }

    private static Byte[][] StandardVocabulary() =>
        (
            "a abbr address area article aside audio b base bdi bdo blockquote body br button canvas caption cite "
            + "code col colgroup data datalist dd del details dfn dialog div dl dt em embed fieldset figcaption figure "
            + "footer form h1 h2 h3 h4 h5 h6 head header hgroup hr html i iframe img input ins kbd label legend li "
            + "link main map mark menu meta meter nav noscript object ol optgroup option output p picture pre progress q "
            + "rp rt ruby s samp script search section select slot small source span strong style sub summary sup table "
            + "tbody td template textarea tfoot th thead time title tr track u ul var video wbr accept action alt aria-label "
            + "async autocomplete autofocus charset checked class content data-id disabled download for height hidden href "
            + "http-equiv id lang loading max maxlength media method min multiple name pattern placeholder rel required role "
            + "selected src srcset style tabindex target title type value width"
        )
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(Encoding.ASCII.GetBytes)
            .ToArray();

    private static Byte[][] SharedPrefixNames(Int32 count)
    {
        var names = new Byte[count][];
        for (var index = 0; index < names.Length; index++)
        {
            var prefix = (index & 3) switch
            {
                0 => "data-field-",
                1 => "aria-field-",
                2 => "x-widget-",
                _ => "custom-element-",
            };
            names[index] = Encoding.ASCII.GetBytes(prefix + index.ToString("x8"));
        }
        return names;
    }

    private static Byte[][] RepeatToBatch(Byte[][] source)
    {
        if (source.Length == 0)
        {
            throw new InvalidOperationException("The UTF-8 name corpus is empty.");
        }

        var result = new Byte[BatchSize][];
        for (var index = 0; index < result.Length; index++)
        {
            result[index] = source[index % source.Length];
        }
        return result;
    }

    private static void PrintCollisionLine(String name, Byte[][] values, Utf8Hash hashAlgorithm)
    {
        var seen = new Dictionary<(Int32 Length, UInt64 Hash), Byte[]>();
        var buckets = new Int32[4096];
        var collisions = 0;
        foreach (var value in values)
        {
            var hash = hashAlgorithm(value);
            buckets[hash & (UInt64)(buckets.Length - 1)]++;
            var key = (value.Length, hash);
            if (seen.TryGetValue(key, out var previous))
            {
                if (!value.AsSpan().SequenceEqual(previous))
                {
                    collisions++;
                }
            }
            else
            {
                seen.Add(key, value);
            }
        }

        Console.WriteLine($"{name, -20} {collisions, 10:N0} {buckets.Max(), 12:N0}");
    }

    private static Boolean IsAsciiLetter(Byte value) =>
        value is >= (Byte)'a' and <= (Byte)'z' or >= (Byte)'A' and <= (Byte)'Z';

    private static Boolean IsNameByte(Byte value) =>
        IsAsciiLetter(value) || value is >= (Byte)'0' and <= (Byte)'9' or (Byte)'-' or (Byte)':';

    private delegate UInt64 Utf8Hash(ReadOnlySpan<Byte> value);

    public enum HashCorpus
    {
        RealPageTags,
        StandardVocabulary,
        SharedPrefixNames,
    }
}

#endif
