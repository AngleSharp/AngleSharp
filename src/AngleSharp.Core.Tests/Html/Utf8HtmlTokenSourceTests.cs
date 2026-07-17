#if NET8_0_OR_GREATER
namespace AngleSharp.Core.Tests.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Construction;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Utf8;
    using AngleSharp.Text;
    using NUnit.Framework;

    [TestFixture]
    public class Utf8HtmlTokenSourceTests
    {
        [Test]
        public async Task ArenaBackedTokensBuildEquivalentMutableDomAcrossSegments()
        {
            const String html = "<!doctype html><title>x&amp;y</title><body><main DATA-X='a&amp;b' data-x='ignored'>"
                + "hé &amp; <b>bold</b><textarea>a&amp;b</textarea>"
                + "<svg><title><b>x</b></title></svg><!--c--></main>";
            using var expected = new HtmlParser().ParseDocument(html);
            using var actual = await ParseUtf8Async(SegmentUtf8(html, 3));

            Assert.That(actual.DocumentElement.OuterHtml, Is.EqualTo(expected.DocumentElement.OuterHtml));
        }

        [TestCase("text<!--unfinished")]
        [TestCase("text<!DOCTYPE html")]
        [TestCase("text<article data-value='unfinished")]
        [TestCase("text<")]
        [TestCase("text&amp")]
        public async Task FixedTokenBufferHandlesMalformedEofAcrossSegments(String html)
        {
            using var expected = new HtmlParser().ParseDocument(html);

            for (var segmentSize = 1; segmentSize <= Math.Max(1, Encoding.UTF8.GetByteCount(html)); segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize));
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}");
            }
        }

        [Test]
        public async Task FixedTokenBufferHandlesDenseMarkupAcrossSegments()
        {
            var html = String.Concat(System.Linq.Enumerable.Repeat("text<b a='1'>x</b><!--c-->", 64));
            using var expected = new HtmlParser().ParseDocument(html);

            for (var segmentSize = 1; segmentSize <= 17; segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize));
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}");
            }
        }

        [Test]
        public async Task ContiguousWindowYieldsBeforeTreeBuilderControlledContent()
        {
            const String html = "<title>&amp;<b>x</b></title>"
                + "<style>.x::before{content:'<b>&amp;</b>'}</style>"
                + "<script>if (a < b) document.write('<b>&amp;</b>')</script>"
                + "<textarea>&amp;<b>x</b></textarea><p>after</p>";
            using var expected = new HtmlParser().ParseDocument(html);
            using var actual = await ParseUtf8Async(SegmentUtf8(html, Encoding.UTF8.GetByteCount(html)));

            Assert.That(actual.DocumentElement.OuterHtml, Is.EqualTo(expected.DocumentElement.OuterHtml));
        }

        [Test]
        public async Task UppercaseRawTextEndTagsMatchMatureParser()
        {
            const String html = "<TITLE>x&amp;y</TiTlE><STYLE>x<y</StYlE>"
                + "<SCRIPT>if (a < b) x = '</not-script>';</ScRiPt>"
                + "<TEXTAREA>x&amp;y</TeXtArEa><p>after</p>";
            using var expected = new HtmlParser().ParseDocument(html);

            for (var segmentSize = 1; segmentSize <= 11; segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize));
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}");
            }
        }

        [Test]
        public async Task PreserveAttributeNamesMatchesMatureParser()
        {
            const String html = "<DIV *ngIf='condition' DATA-Custom='x' CLASS='hero'></DIV>";
            var options = new HtmlParserOptions { IsPreservingAttributeNames = true };
            using var expected = new HtmlParser(options).ParseDocument(html);
            using var actual = await ParseUtf8Async(SegmentUtf8(html, 2), options);

            Assert.That(actual.DocumentElement.OuterHtml, Is.EqualTo(expected.DocumentElement.OuterHtml));
        }

        [Test]
        public void BorrowedNamesExposeVerbatimBytesAndSharedSemanticIdentity()
        {
            var sink = new NameRecordingSink();
            var tokenizer = new Utf8HtmlTokenizer(sink);
            tokenizer.Write("<DiV DaTa-X='1'></dIv>"u8);
            tokenizer.Complete();

            Assert.That(sink.StartTagVerbatim, Is.EqualTo("DiV"));
            Assert.That(sink.AttributeWanted, Is.EqualTo("DaTa-X"));
            Assert.That(sink.AttributeVerbatim, Is.EqualTo("DaTa-X"));
            Assert.That(sink.EndTagVerbatim, Is.EqualTo("dIv"));
            Assert.That(sink.StartTagHash, Is.EqualTo(Utf8NameHash.ComputeSemantic("div"u8)));
            Assert.That(sink.AttributeWantedHash, Is.EqualTo(Utf8NameHash.ComputeSemantic("data-x"u8)));
            Assert.That(sink.AttributeHash, Is.EqualTo(sink.AttributeWantedHash));
            Assert.That(sink.EndTagHash, Is.EqualTo(sink.StartTagHash));
        }

        [Test]
        public void SemanticHashIsLazyAndCachedInSingleField()
        {
            var cache = default(Utf8HtmlNameHashCache);
            var name = new Utf8HtmlName("DaTa-X"u8, ref cache);

            Assert.That(cache.Value, Is.Zero);
            var hash = name.SemanticHash;
            Assert.That(hash, Is.EqualTo(Utf8NameHash.ComputeSemantic("data-x"u8)));
            Assert.That(cache.Value, Is.EqualTo(hash));
            Assert.That(name.SemanticHash, Is.EqualTo(hash));
        }

        [Test]
        public async Task ValidatedAsciiPrefixSurvivesRepeatedYieldsBeforeNonAsciiInput()
        {
            var html = String.Concat(System.Linq.Enumerable.Repeat("<div><b>x</b></div>", 256)) + "<p>héllø</p>";
            using var expected = new HtmlParser().ParseDocument(html);
            using var actual = await ParseUtf8Async(SegmentUtf8(html, Encoding.UTF8.GetByteCount(html)));

            Assert.That(actual.DocumentElement.OuterHtml, Is.EqualTo(expected.DocumentElement.OuterHtml));
        }

        [Test]
        public async Task MultibyteTextSplitAtEveryByteBoundaryMatchesMatureParser()
        {
            const String html = "<main>Ж🙂é界</main>";
            using var expected = new HtmlParser().ParseDocument(html);

            for (var segmentSize = 1; segmentSize <= Encoding.UTF8.GetByteCount(html); segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize));
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}"
                );
            }
        }

        [Test]
        public void CanonicalNameProviderReusesKnownTagAndRejectsUnknownName()
        {
            var cache = default(Utf8HtmlNameHashCache);
            var div = new Utf8HtmlName("DiV"u8, ref cache);

            Assert.That(Utf8CanonicalNameProvider.TryGetTag(div, out var canonical), Is.True);
            Assert.That(canonical, Is.SameAs(TagNames.Div));

            cache.Reset();
            var custom = new Utf8HtmlName("custom"u8, ref cache);
            Assert.That(Utf8CanonicalNameProvider.TryGetTag(custom, out _), Is.False);
        }

        [Test]
        public void CanonicalNameProviderReusesKnownAttributeAndRejectsUnknownName()
        {
            Assert.That(Utf8CanonicalNameProvider.TryGetAttribute("class"u8, out var canonical), Is.True);
            Assert.That(canonical, Is.SameAs(AttributeNames.Class));
            Assert.That(Utf8CanonicalNameProvider.TryGetAttribute("data-custom"u8, out _), Is.False);
        }

        [Test]
        public void CanonicalNameProviderCoversEveryHtmlTagWithEquivalentLookupPaths()
        {
            foreach (var field in GetTagFields().Where(static field => !NonHtmlTagFields.Contains(field.Name)))
            {
                var canonical = (String)field.GetValue(null)!;
                var mixedCase = MixedCaseUtf8(canonical);
                Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(mixedCase, out var direct), Is.True, field.Name);
                Assert.That(direct, Is.SameAs(canonical), field.Name);

                var cache = default(Utf8HtmlNameHashCache);
                var name = new Utf8HtmlName(mixedCase, ref cache);
                _ = name.SemanticHash;
                Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(name, out var prehashed), Is.True, field.Name);
                Assert.That(prehashed, Is.SameAs(direct), field.Name);
            }
        }

        [Test]
        public void CanonicalNameProviderKeepsForeignTagSetsSeparate()
        {
            AssertCategory(MathMlTagFields, Utf8CanonicalNameProvider.TryGetMathMlTag);
            AssertCategory(SvgTagFields, Utf8CanonicalNameProvider.TryGetSvgTag);

            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag("foreignobject"u8, out _), Is.False);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag("annotation-xml"u8, out _), Is.False);
        }

        [Test]
        public void CanonicalNameProviderRejectsMissesWrongHashesAndNonAsciiBytes()
        {
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(ReadOnlySpan<Byte>.Empty, out _), Is.False);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag("sixteen-byte-name"u8, out _), Is.False);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag("custom-element"u8, out _), Is.False);

            var cache = default(Utf8HtmlNameHashCache);
            Unsafe.As<Utf8HtmlNameHashCache, UInt64>(ref cache) = Utf8NameHash.ComputeSemantic("div"u8);
            var wrongHash = new Utf8HtmlName("dix"u8, ref cache);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(wrongHash, out _), Is.False);

            cache.Reset();
            Unsafe.As<Utf8HtmlNameHashCache, UInt64>(ref cache) = Utf8NameHash.ComputeSemantic("dir"u8);
            var hashIndependent = new Utf8HtmlName("div"u8, ref cache);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(hashIndependent, out var div), Is.True);
            Assert.That(div, Is.SameAs(TagNames.Div));

            cache.Reset();
            Unsafe.As<Utf8HtmlNameHashCache, UInt64>(ref cache) = Utf8NameHash.ComputeSemantic("div"u8);
            ReadOnlySpan<Byte> nonAscii = [(Byte)'d', 0xFF, (Byte)'v'];
            var invalidName = new Utf8HtmlName(nonAscii, ref cache);
            Assert.That(Utf8CanonicalNameProvider.TryGetHtmlTag(invalidName, out _), Is.False);
        }

        private static async Task<IDocument> ParseUtf8Async(
            IAsyncEnumerable<ReadOnlyMemory<Byte>> input,
            HtmlParserOptions? options = null)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = new HtmlDocument(context, new TextSource(String.Empty));
            var factory = context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
            await using var tokenSource = new Utf8HtmlTokenSource(input);
            using var builder = new HtmlDomBuilder(factory, document, tokenSource: tokenSource);
            return await builder.ParseAsync(options ?? new HtmlParserOptions());
        }

        private static readonly HashSet<String> NonHtmlTagFields =
        [
            nameof(TagNames.Doctype),
            nameof(TagNames.Math),
            nameof(TagNames.Mi),
            nameof(TagNames.Mo),
            nameof(TagNames.Mn),
            nameof(TagNames.Ms),
            nameof(TagNames.Mtext),
            nameof(TagNames.AnnotationXml),
            nameof(TagNames.Svg),
            nameof(TagNames.ForeignObject),
            nameof(TagNames.Desc),
            nameof(TagNames.Circle),
            nameof(TagNames.Xml),
        ];

        private static readonly String[] MathMlTagFields =
        [
            nameof(TagNames.Math),
            nameof(TagNames.Mi),
            nameof(TagNames.Mo),
            nameof(TagNames.Mn),
            nameof(TagNames.Ms),
            nameof(TagNames.Mtext),
            nameof(TagNames.AnnotationXml),
        ];

        private static readonly String[] SvgTagFields =
        [
            nameof(TagNames.Svg),
            nameof(TagNames.ForeignObject),
            nameof(TagNames.Desc),
            nameof(TagNames.Circle),
        ];

        private static FieldInfo[] GetTagFields() =>
            typeof(TagNames).GetFields(BindingFlags.Public | BindingFlags.Static);

        private static void AssertCategory(
            IEnumerable<String> fieldNames,
            CanonicalLookup lookup)
        {
            foreach (var fieldName in fieldNames)
            {
                var canonical = (String)typeof(TagNames).GetField(fieldName)!.GetValue(null)!;
                var mixedCase = MixedCaseUtf8(canonical);
                Assert.That(lookup(mixedCase, out var actual), Is.True, fieldName);
                Assert.That(actual, Is.SameAs(canonical), fieldName);
            }
        }

        private static Byte[] MixedCaseUtf8(String value)
        {
            var bytes = Encoding.ASCII.GetBytes(value.ToLowerInvariant());
            for (var index = 0; index < bytes.Length; index += 2)
            {
                if (bytes[index] is >= (Byte)'a' and <= (Byte)'z')
                {
                    bytes[index] &= 0xDF;
                }
            }
            return bytes;
        }

        private delegate Boolean CanonicalLookup(ReadOnlySpan<Byte> name, out String canonical);

        private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SegmentUtf8(String html, Int32 segmentSize)
        {
            var utf8 = Encoding.UTF8.GetBytes(html);
            for (var offset = 0; offset < utf8.Length; offset += segmentSize)
            {
                await Task.Yield();
                yield return utf8.AsMemory(offset, Math.Min(segmentSize, utf8.Length - offset));
            }
        }

        private sealed class NameRecordingSink : IUtf8HtmlTokenSink
        {
            public String StartTagVerbatim { get; private set; } = null!;

            public UInt64 StartTagHash { get; private set; }

            public String AttributeWanted { get; private set; } = null!;

            public UInt64 AttributeWantedHash { get; private set; }

            public String AttributeVerbatim { get; private set; } = null!;

            public UInt64 AttributeHash { get; private set; }

            public String EndTagVerbatim { get; private set; } = null!;

            public UInt64 EndTagHash { get; private set; }

            public void Text(ReadOnlySpan<Byte> utf8) { }

            public void StartTag(Utf8HtmlName name)
            {
                StartTagVerbatim = Encoding.UTF8.GetString(name.Verbatim);
                StartTagHash = name.SemanticHash;
            }

            public Boolean WantsAttribute(Utf8HtmlName name)
            {
                AttributeWanted = Encoding.UTF8.GetString(name.Verbatim);
                AttributeWantedHash = name.SemanticHash;
                return true;
            }

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value)
            {
                AttributeVerbatim = Encoding.UTF8.GetString(name.Verbatim);
                AttributeHash = name.SemanticHash;
            }

            public void StartTagEnd(Boolean selfClosing) { }

            public void EndTag(Utf8HtmlName name)
            {
                EndTagVerbatim = Encoding.UTF8.GetString(name.Verbatim);
                EndTagHash = name.SemanticHash;
            }
        }
    }
}
#endif
