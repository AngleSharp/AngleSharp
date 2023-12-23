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
    using System.Linq;
    using System.Net.Http;
    using System.Numerics;
    using System.Threading.Tasks;
    using Css.Dom;
    using Css.Parser;
    using Dom;
    using Html.Parser;
    using Microsoft.IO;
    using ReadOnly.Html;
    using Text;
    using UserCode;

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

            var html = new PrefetchedTextSource(StaticHtml.Github);
            var parser = new HtmlParser();
            using var doc = parser.ParseReadOnlyDocument(html, new FirstTagAndAllChildren("body").Loop);
            var sb = new StringBuilder();

            var comments = doc
                .QueryAll(
                    n => n.Tag("div") && n.Class("edit-comment-hide"),
                    n => n.Tag("tr") && n.Class("d-block"),
                    n => n.Tag("td") && n.Class("comment-body"))
                .Select(n => n.GetTextContent(sb))
                .ToList();

            foreach (var comment in comments)
            {
                Console.WriteLine(comment);
                Console.WriteLine("========================================");
            }
        }
    }

    public static class ParserExtensions
    {
        public static IReadOnlyDocument ParseWithFilter(this IHtmlParser parser, ReadOnlyMemory<Char> data, Middleware mw)
        {
            var source = new PrefetchedTextSource(data);
            var doc = parser.ParseReadOnlyDocument(source, mw);
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