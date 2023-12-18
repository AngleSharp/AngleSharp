#nullable enable
using System;
using System.Text;
using BenchmarkDotNet.Running;

namespace AngleSharp.Benchmarks
{
    using System.Buffers;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Numerics;
    using System.Threading.Tasks;
    using Css.Dom;
    using Css.Parser;
    using Dom;
    using Html;
    using Html.Dom;
    using Html.Parser;
    using Html.Parser.Tokens.Struct;
    using Microsoft.IO;
    using Text;

    static class Program
    {
        static async Task Main(String[] args)
        {
            await Task.CompletedTask;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (args.Length > 0)
            {
                BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
                return;
            }

            var benchmark = new HttpParsingBenchmark();
            benchmark.Id = "81454937594";
            await benchmark.CustomLibLevel();
            await benchmark.CustomLibLevel();
            await benchmark.CustomLibLevel();

            Console.ReadLine();

            await benchmark.CustomLibLevel();

            Console.ReadLine();
            //
            // // var file = @"..\..\..\temp\w3.html";
            // // var file = @"..\..\..\page.html";
            //
            // var files = Directory.EnumerateFiles(@"..\..\..\temp\", "*.html");
            //
            //
            // var options = new HtmlParserOptions()
            // {
            //     IsStrictMode = false,
            //     IsScripting = false,
            //     IsNotConsumingCharacterReferences = true,
            //     IsNotSupportingFrames = true,
            //     IsSupportingProcessingInstructions = false,
            //     IsEmbedded = false,
            //
            //     IsKeepingSourceReferences = false,
            //     IsPreservingAttributeNames = false,
            //     IsAcceptingCustomElementsEverywhere = false,
            //
            //     SkipScriptText = true,
            //     SkipRawText = true,
            //     SkipDataText = false,
            //     SkipComments = true,
            //     SkipPlaintext = true,
            //     SkipCDATA = true,
            //     SkipRCDataText = true,
            //     SkipProcessingInstructions = true,
            //     DisableElementPositionTracking = true,
            //     ShouldEmitAttribute = static (ref StructHtmlToken _, ReadOnlyMemory<Char> n) =>
            //     {
            //         var s = n.Span;
            //         return s.Length switch
            //         {
            //             2 => s[0] == 'i' && s[1] == 'd',
            //             _ => false
            //         };
            //     },
            // };
            //
            // var context = BrowsingContext.New(Configuration.Default);
            // var parser = new HtmlParser(options, context);
            //
            // foreach (var file in files)
            // {
            //     var readOnlyMemory = File.ReadAllText(file);
            //     using var source = new PrefetchedTextSource(readOnlyMemory);
            //     var filter = new ParserBenchmark.TokenFilter("p", "some-magical-id");
            //     using var document = parser.ParseDocument(source, filter.Loop);
            //     var result = document.QuerySelector("p#some-magical-id");
            //     Console.WriteLine(file);
            //     Console.WriteLine(result?.ToHtml());
            // }


            // using var source = new PrefetchedTextSource(readOnlyMemory);
            // using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
            // StructHtmlToken token;
            // int sum = 0;
            // do
            // {
            //     token = tokenizer.Get();
            //     if (token.Type == HtmlTokenType.StartTag)
            //     {
            //         sum+=token.Attributes.Count;
            //     }
            //
            // } while (token.Type != HtmlTokenType.EndOfFile);
            //
            // Console.WriteLine(sum);


            // return new
            // {
            //     file,
            //     avg = atrs.Count > 0 ? atrs.Average() : -1,
            //     max = atrs.Count > 0 ? atrs.Max() : -1,
            //     count = atrs.Count
            // };


            /*
            var stats = Directory.EnumerateFiles(@"..\..\..\temp\", "*.html", SearchOption.AllDirectories)
                .Select(file =>
                {
                    var readOnlyMemory = File.ReadAllText(file).AsMemory();
                    using var source = new PrefetchedTextSource(readOnlyMemory);
                    using var tokenizer = new StructHtmlTokenizer(source, HtmlEntityProvider.Resolver);
                    StructHtmlToken token;
                    List<int> atrs = new();
                    do
                    {
                        token = tokenizer.Get();
                        if (token.Type == HtmlTokenType.StartTag)
                        {
                            atrs.Add(token.Attributes.Count);
                        }

                    } while (token.Type != HtmlTokenType.EndOfFile);

                    return new
                    {
                        file,
                        avg = atrs.Count > 0 ? atrs.Average() : -1,
                        max = atrs.Count > 0 ? atrs.Max() : -1,
                        count = atrs.Count
                    };
                });

            // foreach (var stat in stats)
            // {
            //     Console.WriteLine(stat);
            // }
*/
        }
    }

    public static class ParserExtensions
    {
        public static IDocument ParseWithFilter(this IHtmlParser parser, Memory<Char> data, Middleware mw)
        {
            using var source = new PrefetchedTextSource(data);
            var doc = parser.ParseDocumentStruct(source, mw);
            return doc;
        }
    }

    public class SelectorCache
    {
        public readonly ConcurrentDictionary<String, ISelector?> Selectors = new();
    }

    public class RingBuffer<T> where T : struct, INumber<T>
    {
        private readonly T[] _buffer;
        private Int32 _end;

        public RingBuffer(Int32 capacity)
        {
            _buffer = new T[capacity];
            _end = 0;
        }

        public Int32 Count => Math.Min(_buffer.Length, _end);

        public void Add(T item)
        {
            _buffer[_end % _buffer.Length] = item;
            _end++;
        }

        public T? Avg()
        {
            var count = Count;
            if (count == 0) return null;
            T sum = T.Zero;
            for (int i = 0; i < count; i++)
            {
                sum += _buffer[i];
            }
            return sum / T.CreateChecked(count);
        }

    }

    public static class HttpClientExtensions
    {
        private const int RingBufferSize = 16;

        private static readonly ConcurrentDictionary<String, RingBuffer<Int32>> AvgResponseSize = new();

        private static readonly RecyclableMemoryStreamManager Manager = new RecyclableMemoryStreamManager();

        public static async Task<Lease<Char>> DownloadChars(
            this HttpClient client,
            HttpRequestMessage request,
            String? id = null,
            Int32? expectedResponseSize = null)
        {
            id ??= request.RequestUri!.Host;
            using var postResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var rb = AvgResponseSize.GetOrAdd(id, static _ => new RingBuffer<Int32>(RingBufferSize));
            int size = rb.Avg() ?? (Int32?)postResponse.Content.Headers.ContentLength ?? expectedResponseSize ?? 0;

            await using var htmlStream = await postResponse.Content.ReadAsStreamAsync();
            await using var cachedStream = Manager.GetStream(request.RequestUri!.Host, size);
            await htmlStream.CopyToAsync(cachedStream);

            var totalBytes = (Int32)cachedStream.Length;
            rb.Add(totalBytes);

            var maxTotalChars = Encoding.UTF8.GetMaxCharCount(totalBytes);
            var charsBuffer = ArrayPool<Char>.Shared.Rent(maxTotalChars);
            int writtenChars = Encoding.UTF8.GetChars(cachedStream.GetReadOnlySequence(), charsBuffer.AsSpan());

            return new Lease<Char>(ArrayPool<Char>.Shared, charsBuffer, writtenChars);
        }
    }

    public static class AngleSharpExtensions
    {
        static readonly CssSelectorParser CssSelectorParser = new CssSelectorParser();

        public static IElement? QuerySelector(this INode? element, String? selectors, SelectorCache cache)
        {
            if (element == null)
                return null;

            if (selectors == null)
                return null;

            var selector = cache.Selectors.GetOrAdd(selectors,
                static selectors => CssSelectorParser.ParseSelector(selectors)!);

            if (selector == null)
                return null;

            return element.ChildNodes.QuerySelector(selector);
        }

        private class EmptyHtmlCollection : IHtmlCollection<IElement>
        {
            public static readonly EmptyHtmlCollection Instance = new EmptyHtmlCollection();

            public IEnumerator<IElement> GetEnumerator()
            {
                return Array.Empty<IElement>().AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Int32 Length => 0;

            public IElement this[Int32 index] => throw new IndexOutOfRangeException();

            public IElement? this[String id] => throw new IndexOutOfRangeException();
        }

        public static IHtmlCollection<IElement> QuerySelectorAll(this INode? element, String? selectors,
            SelectorCache cache)
        {
            if (element == null)
                return EmptyHtmlCollection.Instance;

            if (selectors == null)
                return EmptyHtmlCollection.Instance;

            ISelector? selector = cache.Selectors.GetOrAdd(selectors,
                static selectors => CssSelectorParser.ParseSelector(selectors)!);

            if (selector == null)
                return EmptyHtmlCollection.Instance;

            return selector.MatchAll(element.ChildNodes.OfType<IElement>(), null);
        }

        public static String GetText(this INode? element)
        {
            if (element == null)
                return "";

            if (element.ChildNodes is [IText textNode])
                return textNode.Data;

            if (TryFindSingleTextNode(out var fastTextContent))
                return fastTextContent;

            return element.TextContent;

            Boolean TryFindSingleTextNode([NotNullWhen(true)] out String? empty)
            {
                empty = null;

                String? lastVisitedTextNodeData = null;
                var descendantsAndSelf = element.GetDescendantsAndSelf();
                using var enumerator = descendantsAndSelf.GetEnumerator();

                Int32 textNodesVisited = 0;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is IText text)
                    {
                        textNodesVisited++;
                        if (textNodesVisited > 1)
                        {
                            return false;
                        }

                        lastVisitedTextNodeData = text.Data;
                    }
                }

                empty = lastVisitedTextNodeData ?? "";
                return true;
            }
        }
    }
}