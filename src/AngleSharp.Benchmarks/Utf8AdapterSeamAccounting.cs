#if NET10_0
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Construction;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Parser.Tokens;
using AngleSharp.Html.Parser.Tokens.Struct;
using AngleSharp.Html.Parser.Utf8;
using AngleSharp.Text;

namespace AngleSharp.Benchmarks;

internal static class Utf8AdapterSeamAccounting
{
    private const Int32 NetworkBufferSize = 4096;

    public static async Task RunAsync(String corpus)
    {
        var utf8 = await File.ReadAllBytesAsync(corpus).ConfigureAwait(false);
        var html = Encoding.UTF8.GetString(utf8);
        var context = BrowsingContext.New(Configuration.Default);
        var factory = context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;

        var mature = await ParseMatureAsync(context, factory, html).ConfigureAwait(false);
        var native = await ParseNativeAsync(context, factory, utf8, NetworkBufferSize).ConfigureAwait(false);
        var native32K = await ParseNativeAsync(context, factory, utf8, 32 * 1024).ConfigureAwait(false);
        var nativeContiguous = await ParseNativeAsync(context, factory, utf8, utf8.Length).ConfigureAwait(false);

        if (
            !String.Equals(mature.Markup, native.Markup, StringComparison.Ordinal)
            || !String.Equals(mature.Markup, native32K.Markup, StringComparison.Ordinal)
            || !String.Equals(mature.Markup, nativeContiguous.Markup, StringComparison.Ordinal)
        )
        {
            throw new InvalidOperationException("Native UTF-8 and mature token sources produced different mutable DOMs.");
        }

        Console.WriteLine($"Input bytes: {utf8.Length:N0}; network chunks: {native.InputChunks:N0}");
        Console.WriteLine(
            $"Carrier sizes: StructHtmlToken={Unsafe.SizeOf<StructHtmlToken>()} B, "
            + $"StructAttributes={Unsafe.SizeOf<StructAttributes>()} B, "
            + $"MemoryHtmlAttributeToken={Unsafe.SizeOf<MemoryHtmlAttributeToken>()} B"
        );
        Console.WriteLine("DOM output: equivalent");
        Console.WriteLine();
        PrintTable(mature.Metrics, native.Metrics);
        Console.WriteLine();
        PrintBufferTable(native, native32K, nativeContiguous);
        Console.WriteLine();
        Console.WriteLine("Native tokenizer kernel:");
        Console.WriteLine($"  bytes consumed              {native.Tokenizer.BytesConsumed,12:N0}");
        Console.WriteLine($"  reconsumes                  {native.Tokenizer.Reconsumes,12:N0}");
        Console.WriteLine($"  max buffered token bytes    {native.Tokenizer.MaximumBufferedTokenBytes,12:N0}");
        Console.WriteLine();
        Console.WriteLine("Notes:");
        Console.WriteLine("  * Counts are collected outside the timed benchmarks.");
        Console.WriteLine("  * Payload backing objects include shared canonical/static strings; they are not allocation counts.");
        Console.WriteLine("  * A successful move exposes a token by ref. Carrier size x token count is not proof of bytes copied.");
    }

    private static async Task<ParseResult> ParseMatureAsync(
        IBrowsingContext context,
        IHtmlElementConstructionFactory factory,
        String html)
    {
        using var document = new HtmlDocument(context, new TextSource(html));
        using var tokenizer = new HtmlTokenizer(document.Source, HtmlEntityProvider.ResolverExtended);
        var inner = new HtmlTokenizerTokenSource(tokenizer);
        var metrics = new SeamMetrics(html);
        var source = new CountingTokenSource(inner, metrics);
        using var builder = new HtmlDomBuilder(factory, document, tokenSource: source);
        await builder.ParseAsync(new HtmlParserOptions(), metrics.Middleware).ConfigureAwait(false);
        return new ParseResult(document.DocumentElement.OuterHtml, metrics, default, 0);
    }

    private static async Task<ParseResult> ParseNativeAsync(
        IBrowsingContext context,
        IHtmlElementConstructionFactory factory,
        Byte[] utf8,
        Int32 bufferSize)
    {
        using var document = new HtmlDocument(context, new TextSource(String.Empty));
        var chunks = new CountingChunks(utf8, bufferSize);
        await using var inner = new Utf8HtmlTokenSource(chunks.Read());
        var metrics = new SeamMetrics();
        var source = new CountingTokenSource(inner, metrics);
        using var builder = new HtmlDomBuilder(factory, document, tokenSource: source);
        await builder.ParseAsync(new HtmlParserOptions(), metrics.Middleware).ConfigureAwait(false);
        return new ParseResult(
            document.DocumentElement.OuterHtml,
            metrics,
            inner.TokenizerCounters,
            chunks.Count,
            bufferSize
        );
    }

    private static void PrintBufferTable(params ParseResult[] results)
    {
        Console.WriteLine("Native adapter by input buffer:");
        Console.WriteLine(
            $"{"Buffer",12} {"Chunks",9} {"Moves",9} {"Empty",9} {"Waits",9} {"Tokens",9} {"Fresh strings",14}"
        );
        Console.WriteLine(new String('-', 76));
        foreach (var result in results)
        {
            var buffer = result.InputChunks == 1 ? "contiguous" : $"{result.BufferSize / 1024}K";
            Console.WriteLine(
                $"{buffer,12} {result.InputChunks,9:N0} {result.Metrics.MoveCalls,9:N0} "
                + $"{result.Metrics.EmptyMoves,9:N0} {result.Metrics.WaitCalls,9:N0} "
                + $"{result.Metrics.SuccessfulMoves,9:N0} {result.Metrics.FreshPayloadStrings,14:N0}"
            );
        }
    }

    private static void PrintTable(SeamMetrics mature, SeamMetrics native)
    {
        Console.WriteLine($"{"Seam metric",-34} {"Mature",12} {"Native UTF-8",12} {"Delta",12}");
        Console.WriteLine(new String('-', 74));
        Row("TryMoveNext calls", mature.MoveCalls, native.MoveCalls);
        Row("successful moves / tokens", mature.SuccessfulMoves, native.SuccessfulMoves);
        Row("empty moves", mature.EmptyMoves, native.EmptyMoves);
        Row("Current ref accesses", mature.CurrentAccesses, native.CurrentAccesses);
        Row("WaitForInput calls", mature.WaitCalls, native.WaitCalls);
        Row("SetState calls", mature.SetStateCalls, native.SetStateCalls);
        Row("RCData state calls", mature.RcDataStateCalls, native.RcDataStateCalls);
        Row("Rawtext state calls", mature.RawTextStateCalls, native.RawTextStateCalls);
        Row("Script state calls", mature.ScriptStateCalls, native.ScriptStateCalls);
        Row("middleware callbacks", mature.MiddlewareCalls, native.MiddlewareCalls);
        Row("start tags", mature.StartTags, native.StartTags);
        Row("end tags", mature.EndTags, native.EndTags);
        Row("character tokens", mature.CharacterTokens, native.CharacterTokens);
        Row("comments", mature.Comments, native.Comments);
        Row("doctypes", mature.Doctypes, native.Doctypes);
        Row("EOF tokens", mature.EndOfFiles, native.EndOfFiles);
        Row("attributes", mature.Attributes, native.Attributes);
        Row("payload values", mature.PayloadValues, native.PayloadValues);
        Row("payload UTF-16 chars", mature.PayloadCharacters, native.PayloadCharacters);
        Row("string-backed values", mature.StringBackedValues, native.StringBackedValues);
        Row("array-backed values", mature.ArrayBackedValues, native.ArrayBackedValues);
        Row("distinct backing objects", mature.BackingObjects.Count, native.BackingObjects.Count);
        Row("fresh payload string objects", mature.FreshPayloadStrings, native.FreshPayloadStrings);

        static void Row(String name, Int64 mature, Int64 native) =>
            Console.WriteLine($"{name,-34} {mature,12:N0} {native,12:N0} {native - mature,12:+#,0;-#,0;0}");
    }

    private readonly record struct ParseResult(
        String Markup,
        SeamMetrics Metrics,
        Utf8HtmlTokenizerCounters Tokenizer,
        Int32 InputChunks,
        Int32 BufferSize = 0
    );

    private sealed class CountingTokenSource(IHtmlTokenSource inner, SeamMetrics metrics) :
        IHtmlTokenSource,
        IHtmlTokenAvailability
    {
        public Boolean TryMoveNext()
        {
            metrics.MoveCalls++;
            if (!inner.TryMoveNext())
            {
                metrics.EmptyMoves++;
                return false;
            }

            metrics.SuccessfulMoves++;
            metrics.Record(ref inner.Current);
            return true;
        }

        public ref StructHtmlToken Current
        {
            get
            {
                metrics.CurrentAccesses++;
                return ref inner.Current;
            }
        }

        public void Configure(
            HtmlTokenizerOptions options,
            Action<HtmlToken, TextRange> onToken,
            Action<HtmlParseError, TextPosition> reportError) =>
            inner.Configure(options, onToken, reportError);

        public void SetState(HtmlParseMode state)
        {
            metrics.SetStateCalls++;
            switch (state)
            {
                case HtmlParseMode.RCData:
                    metrics.RcDataStateCalls++;
                    break;
                case HtmlParseMode.Rawtext:
                    metrics.RawTextStateCalls++;
                    break;
                case HtmlParseMode.Script:
                    metrics.ScriptStateCalls++;
                    break;
            }
            inner.SetState(state);
        }

        public void SetAcceptingCharacterData(Boolean value) => inner.SetAcceptingCharacterData(value);

        public Task WaitForInputAsync(CancellationToken cancellationToken)
        {
            metrics.WaitCalls++;
            return inner is IHtmlTokenAvailability availability
                ? availability.WaitForInputAsync(cancellationToken)
                : throw new InvalidOperationException("The wrapped token source cannot wait for input.");
        }
    }

    private sealed class SeamMetrics(Object sourceText = null)
    {
        private static readonly HashSet<Object> KnownStaticStrings = CreateKnownStaticStrings();

        public Int64 MoveCalls;
        public Int64 SuccessfulMoves;
        public Int64 EmptyMoves;
        public Int64 CurrentAccesses;
        public Int64 WaitCalls;
        public Int64 SetStateCalls;
        public Int64 RcDataStateCalls;
        public Int64 RawTextStateCalls;
        public Int64 ScriptStateCalls;
        public Int64 MiddlewareCalls;
        public Int64 StartTags;
        public Int64 EndTags;
        public Int64 CharacterTokens;
        public Int64 Comments;
        public Int64 Doctypes;
        public Int64 EndOfFiles;
        public Int64 Attributes;
        public Int64 PayloadValues;
        public Int64 PayloadCharacters;
        public Int64 StringBackedValues;
        public Int64 ArrayBackedValues;
        public HashSet<Object> BackingObjects { get; } = new(ReferenceEqualityComparer.Instance);

        public Int32 FreshPayloadStrings
        {
            get
            {
                var count = 0;
                foreach (var backing in BackingObjects)
                {
                    if (
                        backing is String
                        && !ReferenceEquals(backing, String.Empty)
                        && !ReferenceEquals(backing, sourceText)
                        && !KnownStaticStrings.Contains(backing)
                    )
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        public TokenConsumptionResult Middleware(ref StructHtmlToken token, TokenConsumer next)
        {
            MiddlewareCalls++;
            next(ref token);
            return TokenConsumptionResult.Continue;
        }

        public void Record(ref StructHtmlToken token)
        {
            switch (token.Type)
            {
                case HtmlTokenType.StartTag:
                    StartTags++;
                    RecordPayload(token.Name);
                    var attributes = token.Attributes;
                    Attributes += attributes.Count;
                    for (var index = 0; index < attributes.Count; index++)
                    {
                        var attribute = attributes[index];
                        RecordPayload(attribute.Name);
                        RecordPayload(attribute.Value);
                    }
                    break;
                case HtmlTokenType.EndTag:
                    EndTags++;
                    RecordPayload(token.Name);
                    break;
                case HtmlTokenType.Character:
                    CharacterTokens++;
                    RecordPayload(token.Data);
                    break;
                case HtmlTokenType.Comment:
                    Comments++;
                    RecordPayload(token.Data);
                    break;
                case HtmlTokenType.Doctype:
                    Doctypes++;
                    RecordPayload(token.Name);
                    if (!token.IsPublicIdentifierMissing)
                    {
                        RecordPayload(token.PublicIdentifier);
                    }
                    if (!token.IsSystemIdentifierMissing)
                    {
                        RecordPayload(token.SystemIdentifier);
                    }
                    break;
                case HtmlTokenType.EndOfFile:
                    EndOfFiles++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RecordPayload(StringOrMemory value)
        {
            PayloadValues++;
            PayloadCharacters += value.Length;
            var memory = value.Memory;
            if (MemoryMarshal.TryGetString(memory, out var text, out _, out _))
            {
                StringBackedValues++;
                if (text is not null)
                {
                    BackingObjects.Add(text);
                }
            }
            else if (MemoryMarshal.TryGetArray(memory, out ArraySegment<Char> segment))
            {
                ArrayBackedValues++;
                if (segment.Array is not null)
                {
                    BackingObjects.Add(segment.Array);
                }
            }
        }

        private static HashSet<Object> CreateKnownStaticStrings()
        {
            var result = new HashSet<Object>(ReferenceEqualityComparer.Instance) { String.Empty };
            Add(typeof(TagNames));
            Add(typeof(AttributeNames));
            return result;

            void Add(Type type)
            {
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (field.FieldType == typeof(String) && field.GetValue(null) is String value)
                    {
                        result.Add(value);
                    }
                }
            }
        }
    }

    private sealed class CountingChunks(Byte[] source, Int32 chunkSize)
    {
        public Int32 Count { get; private set; }

        public async IAsyncEnumerable<ReadOnlyMemory<Byte>> Read(
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var buffer = ArrayPool<Byte>.Shared.Rent(chunkSize);
            try
            {
                for (var offset = 0; offset < source.Length; offset += chunkSize)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var length = Math.Min(chunkSize, source.Length - offset);
                    source.AsSpan(offset, length).CopyTo(buffer);
                    Count++;
                    yield return buffer.AsMemory(0, length);
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }
    }
}
#endif
