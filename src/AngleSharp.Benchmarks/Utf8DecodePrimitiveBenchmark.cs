#if NET10_0

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AngleSharp.Benchmarks;

public abstract class Utf8PrimitiveBenchmarkBase
{
    private const Int32 BufferSize = 4096;

    protected Byte[] Utf8 { get; private set; } = null!;

    protected Char[] Utf16 { get; private set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        const String fragment =
            "<article class=\"card\" data-id=\"42\">"
            + "<h2>Как устроен быстрый HTML-парсер 🚀</h2>"
            + "<p>Сервер получил страницу, проверил UTF-8 и продолжил обработку без лишних копий. "
            + "Пользователь написал: «Работает удивительно быстро» 😄🔥</p>"
            + "<a href=\"/articles/streaming?lang=ru&amp;page=2\">Читать продолжение →</a>"
            + "<script type=\"application/json\">"
            + "{\"author\":\"Дмитрий\",\"tags\":[\"C#\",\"парсинг\",\"⚡\"],\"score\":98}"
            + "</script></article>";

        var fragmentBytes = Encoding.UTF8.GetBytes(fragment);
        var bytes = new List<Byte>(BufferSize);
        while (bytes.Count + fragmentBytes.Length <= BufferSize)
        {
            bytes.AddRange(fragmentBytes);
        }

        while (bytes.Count < BufferSize)
        {
            bytes.Add((Byte)' ');
        }

        Utf8 = bytes.ToArray();
        Utf16 = new Char[Encoding.UTF8.GetMaxCharCount(BufferSize)];

        if (Utf8.Length != BufferSize || !System.Text.Unicode.Utf8.IsValid(Utf8))
        {
            throw new InvalidOperationException("The benchmark fixture must be exactly 4 KB of valid UTF-8.");
        }

        Console.WriteLine(
            $"UTF-8 primitive corpus: {Utf8.Length:N0} bytes, "
                + $"{Encoding.UTF8.GetCharCount(Utf8):N0} UTF-16 chars."
        );
    }
}

[MemoryDiagnoser, ShortRunJob]
public class Utf8ValidationPrimitiveBenchmark : Utf8PrimitiveBenchmarkBase
{
    [Benchmark(Baseline = true)]
    public Boolean ScalarRuneValidate()
    {
        var index = 0;
        while (index < Utf8.Length)
        {
            var status = Rune.DecodeFromUtf8(Utf8.AsSpan(index), out _, out var consumed);
            if (status != OperationStatus.Done)
            {
                return false;
            }

            index += consumed;
        }

        return true;
    }

    [Benchmark]
    public Boolean BulkValidate() => System.Text.Unicode.Utf8.IsValid(Utf8);
}

[MemoryDiagnoser, ShortRunJob]
public class Utf8DecodePrimitiveBenchmark : Utf8PrimitiveBenchmarkBase
{
    [Benchmark(Baseline = true)]
    public Int32 ScalarRuneDecodeToUtf16()
    {
        var sourceIndex = 0;
        var destinationIndex = 0;
        while (sourceIndex < Utf8.Length)
        {
            var status = Rune.DecodeFromUtf8(
                Utf8.AsSpan(sourceIndex),
                out var rune,
                out var consumed
            );
            if (status != OperationStatus.Done)
            {
                return -1;
            }

            sourceIndex += consumed;
            destinationIndex += rune.EncodeToUtf16(Utf16.AsSpan(destinationIndex));
        }

        return destinationIndex;
    }

    [Benchmark]
    public Int32 BulkDecodeToUtf16() => Encoding.UTF8.GetChars(Utf8, Utf16);

    [Benchmark]
    public Int32 BulkValidateThenDecodeToUtf16() =>
        System.Text.Unicode.Utf8.IsValid(Utf8) ? Encoding.UTF8.GetChars(Utf8, Utf16) : -1;
}

#endif
