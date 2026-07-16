#if NET8_0_OR_GREATER
namespace AngleSharp.Core.Tests.Html
{
    using System;
    using System.Collections.Generic;
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
        public async Task ValidatedAsciiPrefixSurvivesRepeatedYieldsBeforeNonAsciiInput()
        {
            var html = String.Concat(System.Linq.Enumerable.Repeat("<div><b>x</b></div>", 256)) + "<p>héllø</p>";
            using var expected = new HtmlParser().ParseDocument(html);
            using var actual = await ParseUtf8Async(SegmentUtf8(html, Encoding.UTF8.GetByteCount(html)));

            Assert.That(actual.DocumentElement.OuterHtml, Is.EqualTo(expected.DocumentElement.OuterHtml));
        }

        [Test]
        public void CanonicalNameProviderReusesKnownTagAndRejectsHashOnlyMatch()
        {
            var divHash = Utf8NameHash.Compute("div"u8);

            Assert.That(Utf8CanonicalNameProvider.TryGetTag("div"u8, divHash, out var canonical), Is.True);
            Assert.That(canonical, Is.SameAs(TagNames.Div));
            Assert.That(Utf8CanonicalNameProvider.TryGetTag("custom"u8, divHash, out _), Is.False);
        }

        [Test]
        public void CanonicalNameProviderReusesKnownAttributeAndRejectsUnknownName()
        {
            Assert.That(Utf8CanonicalNameProvider.TryGetAttribute("class"u8, out var canonical), Is.True);
            Assert.That(canonical, Is.SameAs(AttributeNames.Class));
            Assert.That(Utf8CanonicalNameProvider.TryGetAttribute("data-custom"u8, out _), Is.False);
        }

        private static async Task<IDocument> ParseUtf8Async(IAsyncEnumerable<ReadOnlyMemory<Byte>> input)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = new HtmlDocument(context, new TextSource(String.Empty));
            var factory = context.GetService<IHtmlElementConstructionFactory>() ?? HtmlDomConstructionFactory.Instance;
            await using var tokenSource = new Utf8HtmlTokenSource(input);
            using var builder = new HtmlDomBuilder(factory, document, tokenSource: tokenSource);
            return await builder.ParseAsync(new HtmlParserOptions());
        }

        private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SegmentUtf8(String html, Int32 segmentSize)
        {
            var utf8 = Encoding.UTF8.GetBytes(html);
            for (var offset = 0; offset < utf8.Length; offset += segmentSize)
            {
                await Task.Yield();
                yield return utf8.AsMemory(offset, Math.Min(segmentSize, utf8.Length - offset));
            }
        }
    }
}
#endif
