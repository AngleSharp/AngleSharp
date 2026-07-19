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
        public async Task ReusedTokenSlotsDoNotExposeDiscardedAttributes()
        {
            const String html = "<main a='1' b='2' c='3' d='4' e='5'></main>"
                + "<p x='6'></p><span></span>";
            using var actual = await ParseUtf8Async(SegmentUtf8(html, 1));

            Assert.That(actual.QuerySelector("main")!.Attributes.Length, Is.EqualTo(5));
            Assert.That(actual.QuerySelector("p")!.Attributes.Length, Is.EqualTo(1));
            Assert.That(actual.QuerySelector("span")!.Attributes.Length, Is.Zero);
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
        public async Task SkippedTextModesMatchMatureParser()
        {
            const String html = "<body><b>data<div>block</div></b><title>rcdata</title>"
                + "after title<textarea>more rcdata</textarea>after textarea"
                + "<style>raw</style>after style<script>script</script>after script"
                + "<plaintext>plain";
            var optionSets = new[]
            {
                new HtmlParserOptions { SkipDataText = true },
                new HtmlParserOptions { SkipRCDataText = true },
                new HtmlParserOptions { SkipRawText = true },
                new HtmlParserOptions { SkipScriptText = true },
                new HtmlParserOptions { SkipPlaintext = true },
            };

            foreach (var options in optionSets)
            {
                using var expected = new HtmlParser(options).ParseDocument(html);
                for (var segmentSize = 1; segmentSize <= 11; segmentSize++)
                {
                    using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize), options);
                    Assert.That(
                        actual.DocumentElement.OuterHtml,
                        Is.EqualTo(expected.DocumentElement.OuterHtml),
                        $"UTF-8 segment size {segmentSize}"
                    );
                }
            }
        }

        [Test]
        public async Task SkippedCommentsRetainMatureParserTreeShape()
        {
            const String html = "<body>before<!--one--><table><!--two--><tr><td>x</td></tr></table>after";
            var options = new HtmlParserOptions { SkipComments = true };
            using var expected = new HtmlParser(options).ParseDocument(html);

            for (var segmentSize = 1; segmentSize <= 11; segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html, segmentSize), options);
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}"
                );
            }
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

        [TestCase(
            "<script/><b>x</b></script>",
            new[] { "S:script:/", "T:<b>x</b>", "E:script", "EOF" }
        )]
        [TestCase(
            "<textarea/><b>x</b></textarea>",
            new[] { "S:textarea:/", "T:<b>x</b>", "E:textarea", "EOF" }
        )]
        [TestCase(
            "<plaintext/><b>x</b>",
            new[] { "S:plaintext:/", "T:<b>x</b>", "EOF" }
        )]
        public void StandaloneTokenizerIgnoresTrailingSolidusForHtmlTextElements(
            String html,
            String[] expected
        ) => Assert.That(TokenizeStandalone(html), Is.EqualTo(expected));

        [Test]
        public void StandaloneTokenizerRejectsFalseRcDataEndTagCandidate()
        {
            var actual = TokenizeStandalone("<title>x</not-title>y</title>");

            Assert.That(
                actual,
                Is.EqualTo(new[] { "S:title", "T:x</not-title>y", "E:title", "EOF" })
            );
        }

        [Test]
        public void StandaloneTokenizerTextModeInferenceIsLexicalWithoutForeignContext()
        {
            var actual = TokenizeStandalone("<svg><title><b>x</b></title></svg>");

            Assert.That(
                actual,
                Is.EqualTo(
                    new[] { "S:svg", "S:title", "T:<b>x</b>", "E:title", "E:svg", "EOF" }
                )
            );
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
        public async Task MalformedUtf8MatchesMatureReplacementAcrossNearbySegmentBoundaries()
        {
            var malformedSequences = new[]
            {
                new Byte[] { 0xFF },
                new Byte[] { 0x80 },
                new Byte[] { 0xC0, 0xAF },
                new Byte[] { 0xE2, 0x82 },
                new Byte[] { 0xED, 0xA0, 0x80 },
                new Byte[] { 0xF4, 0x90, 0x80, 0x80 },
            };
            var contexts = new[]
            {
                ("<main>Ж", "🙂</main>"),
                ("<x data-", "='v'>text</x>"),
                ("<x a='Ж", "🙂'>text</x>"),
                ("<title>Ж", "</not-title>🙂</title>"),
                ("<style>Ж", "</not-style>🙂</style>"),
                ("<script>Ж", "</not-script>🙂</script>"),
                ("<plaintext>Ж", "<b>🙂</b>"),
            };

            foreach (var malformed in malformedSequences)
            {
                foreach (var (prefix, suffix) in contexts)
                {
                    var prefixBytes = Encoding.UTF8.GetBytes(prefix);
                    var utf8 = prefixBytes
                        .Concat(malformed)
                        .Concat(Encoding.UTF8.GetBytes(suffix))
                        .ToArray();
                    using var expected = new HtmlParser().ParseDocument(Encoding.UTF8.GetString(utf8));

                    var firstSplit = Math.Max(0, prefixBytes.Length - 3);
                    var lastSplit = Math.Min(utf8.Length, prefixBytes.Length + malformed.Length + 3);
                    for (var split = firstSplit; split <= lastSplit; split++)
                    {
                        using var actual = await ParseUtf8Async(SplitUtf8(utf8, split));
                        Assert.That(
                            actual.DocumentElement.OuterHtml,
                            Is.EqualTo(expected.DocumentElement.OuterHtml),
                            $"malformed {Convert.ToHexString(malformed)}, split {split}, context {prefix}"
                        );
                    }

                    using var byteByByte = await ParseUtf8Async(SegmentUtf8(utf8, 1));
                    Assert.That(
                        byteByByte.DocumentElement.OuterHtml,
                        Is.EqualTo(expected.DocumentElement.OuterHtml),
                        $"malformed {Convert.ToHexString(malformed)}, byte-by-byte, context {prefix}"
                    );
                }
            }
        }

        [Test]
        public void BorrowedSinkStillReceivesValidatedUtf8WhenInputTextIsMalformed()
        {
            var utf8 = new Byte[]
            {
                (Byte)'a',
                0xFF,
                0xC0,
                0xAF,
                0xE2,
                0x82,
                0xD0,
                0x96,
                (Byte)'z',
            };
            var sink = new ValidatingTextSink();
            var tokenizer = new Utf8HtmlTokenizer(sink);
            var input = new Utf8HtmlTokenizerInput(tokenizer);

            foreach (var value in utf8)
            {
                input.Write(new ReadOnlySpan<Byte>(in value));
            }
            input.Complete();

            Assert.That(sink.DecodedText, Is.EqualTo(Encoding.UTF8.GetString(utf8)));
        }

        [Test]
        public void TrustedTokenizerMatchesValidatingInputAcrossEveryByteBoundary()
        {
            var utf8 = Encoding.UTF8.GetBytes("<div>ASCII Ж 🙂 text</div>");
            var expectedSink = new TokenRecordingSink();
            var actualSink = new TokenRecordingSink();
            var expectedTokenizer = new Utf8HtmlTokenizer(expectedSink);
            var expected = new Utf8HtmlTokenizerInput(expectedTokenizer);
            var actual = new Utf8HtmlTokenizer(actualSink);

            foreach (var value in utf8)
                expected.Write(new ReadOnlySpan<Byte>(in value));
            actual.Write(utf8);
            expected.Complete();
            actual.Complete();

            Assert.That(actualSink.Events, Is.EqualTo(expectedSink.Events));
            Assert.That(actual.Counters.BytesConsumed, Is.EqualTo(utf8.Length));
        }

        [Test]
        public void StructureOnlyCaptureSkipsTextAndPreservesRawTextBoundariesBytewise()
        {
            var html = Encoding.UTF8.GetBytes(
                "outside&amp;\0<main><script>if (a < b) x='&';</script>"
                    + "<textarea>&amp;</textarea><span>x</span></main>tail&amp;"
            );
            var sink = new StructureOnlySink();
            var tokenizer = new Utf8HtmlTokenizer(sink);

            foreach (var value in html)
                tokenizer.Write(new ReadOnlySpan<Byte>(in value));
            tokenizer.Complete();

            Assert.That(
                sink.Events,
                Is.EqualTo(
                    new[]
                    {
                        "S:main",
                        "S:script",
                        "E:script",
                        "S:textarea",
                        "E:textarea",
                        "S:span",
                        "E:span",
                        "E:main",
                        "EOF",
                    }
                )
            );
        }

        [Test]
        public void DiscardedTagTailScannerMatchesFullAttributeLexerAcrossEveryByteBoundary()
        {
            var html = Encoding.UTF8.GetBytes(
                "<main plain weird\"name=x quoted='1>2' unquoted=three c = \"four\" />"
                    + "<x nul=\0 line=one\r\ntwo></x ignored='>'>"
                    + "<script data-x='>'>if (a < b) x='&';</script><tail/>"
            );
            var expected = TokenizeStructuralEvents(html, captureTagAttributes: true, split: -1);

            for (var split = 0; split <= html.Length; split++)
            {
                var actual = TokenizeStructuralEvents(html, captureTagAttributes: false, split);
                Assert.That(actual, Is.EqualTo(expected), $"split={split}");
            }

            var bytewiseSink = new StructuralEventSink(captureTagAttributes: false);
            var bytewiseTokenizer = new Utf8HtmlTokenizer(bytewiseSink);
            foreach (var value in html)
                bytewiseTokenizer.Write(new ReadOnlySpan<Byte>(in value));
            bytewiseTokenizer.Complete();
            Assert.That(bytewiseSink.Events, Is.EqualTo(expected), "bytewise");
        }

        private static IReadOnlyList<String> TokenizeStructuralEvents(
            Byte[] html,
            Boolean captureTagAttributes,
            Int32 split
        )
        {
            var sink = new StructuralEventSink(captureTagAttributes);
            var tokenizer = new Utf8HtmlTokenizer(sink);
            if (split < 0)
            {
                tokenizer.Write(html);
            }
            else
            {
                tokenizer.Write(html.AsSpan(0, split));
                tokenizer.Write(html.AsSpan(split));
            }
            tokenizer.Complete();
            return sink.Events;
        }

        [Test]
        public async Task PromotedAttributeIndexPreservesFirstMixedCaseAttributeAcrossSegments()
        {
            var html = new StringBuilder("<x");
            for (var index = 0; index < 40; index++)
            {
                var name = index % 2 == 0 ? $"DATA-{index:D2}" : $"data-{index:D2}";
                html.Append(' ').Append(name).Append("='").Append(index).Append("'");
            }
            html.Append(" data-00='duplicate' DATA-17='duplicate'></x><y");
            for (var index = 39; index >= 0; index--)
            {
                html.Append(" item-").Append(index.ToString("D2")).Append("='").Append(index).Append("'");
            }
            html.Append(" ITEM-39='duplicate'></y>");

            using var expected = new HtmlParser().ParseDocument(html.ToString());
            for (var segmentSize = 1; segmentSize <= 17; segmentSize++)
            {
                using var actual = await ParseUtf8Async(SegmentUtf8(html.ToString(), segmentSize));
                Assert.That(
                    actual.DocumentElement.OuterHtml,
                    Is.EqualTo(expected.DocumentElement.OuterHtml),
                    $"UTF-8 segment size {segmentSize}"
                );
                Assert.That(actual.QuerySelector("x")!.Attributes.Length, Is.EqualTo(40));
                Assert.That(actual.QuerySelector("x")!.GetAttribute("data-00"), Is.EqualTo("0"));
                Assert.That(actual.QuerySelector("x")!.GetAttribute("data-17"), Is.EqualTo("17"));
                Assert.That(actual.QuerySelector("y")!.Attributes.Length, Is.EqualTo(40));
                Assert.That(actual.QuerySelector("y")!.GetAttribute("item-39"), Is.EqualTo("39"));
            }
        }

        [Test]
        public void PromotedAttributeIndexTracksNamesRejectedByConsumer()
        {
            var html = new StringBuilder("<x");
            for (var index = 0; index < 20; index++)
            {
                html.Append(" a").Append(index.ToString("D2")).Append("='rejected'");
            }
            html.Append(" A00='duplicate' fresh='kept'>");

            var sink = new RejectThenAcceptAttributeSink(rejectedCount: 20);
            var tokenizer = new Utf8HtmlTokenizer(sink);
            tokenizer.Write(Encoding.UTF8.GetBytes(html.ToString()));
            tokenizer.Complete();

            Assert.That(sink.WantsCalls, Is.EqualTo(22));
            Assert.That(sink.Attributes, Is.EqualTo(new[] { "fresh=kept" }));
        }

        [Test]
        public void AttributeIndexConfirmsBytesWhenSemanticHashesCollide()
        {
            var seen = "alpha\0beta\0"u8.ToArray();
            Utf8AttributeNameIndex.Entry[] index = null;
            try
            {
                Utf8AttributeNameIndex.Initialize(ref index, seen, 1);

                var alphaCache = default(Utf8HtmlNameHashCache);
                var alpha = new Utf8HtmlName("alpha"u8, ref alphaCache);
                Assert.That(Utf8AttributeNameIndex.Contains(index, alpha, seen), Is.True);

                var betaCache = default(Utf8HtmlNameHashCache);
                Unsafe.As<Utf8HtmlNameHashCache, UInt64>(ref betaCache) = alpha.SemanticHash;
                var betaWithForcedCollision = new Utf8HtmlName("beta"u8, ref betaCache);
                Assert.That(
                    Utf8AttributeNameIndex.Contains(index, betaWithForcedCollision, seen),
                    Is.False
                );

                Utf8AttributeNameIndex.Add(ref index, betaWithForcedCollision.SemanticHash, 6);
                Assert.That(Utf8AttributeNameIndex.Contains(index, alpha, seen), Is.True);
                Assert.That(
                    Utf8AttributeNameIndex.Contains(index, betaWithForcedCollision, seen),
                    Is.True
                );

                var uppercaseCache = default(Utf8HtmlNameHashCache);
                var uppercaseAlpha = new Utf8HtmlName("ALPHA"u8, ref uppercaseCache);
                Assert.That(
                    Utf8AttributeNameIndex.Contains(index, uppercaseAlpha, seen),
                    Is.True
                );
            }
            finally
            {
                Utf8AttributeNameIndex.Reset(ref index);
            }
            Assert.That(index, Is.Null);
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

        private static IAsyncEnumerable<ReadOnlyMemory<Byte>> SegmentUtf8(String html, Int32 segmentSize) =>
            SegmentUtf8(Encoding.UTF8.GetBytes(html), segmentSize);

        private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SegmentUtf8(Byte[] utf8, Int32 segmentSize)
        {
            for (var offset = 0; offset < utf8.Length; offset += segmentSize)
            {
                await Task.Yield();
                yield return utf8.AsMemory(offset, Math.Min(segmentSize, utf8.Length - offset));
            }
        }

        private static async IAsyncEnumerable<ReadOnlyMemory<Byte>> SplitUtf8(Byte[] utf8, Int32 split)
        {
            if (split != 0)
            {
                await Task.Yield();
                yield return utf8.AsMemory(0, split);
            }
            if (split != utf8.Length)
            {
                await Task.Yield();
                yield return utf8.AsMemory(split);
            }
        }

        private static IReadOnlyList<String> TokenizeStandalone(String html)
        {
            var sink = new TokenRecordingSink();
            var tokenizer = new Utf8HtmlTokenizer(sink);
            tokenizer.Write(Encoding.UTF8.GetBytes(html));
            tokenizer.Complete();
            return sink.Events;
        }

        private sealed class TokenRecordingSink : IUtf8HtmlTokenSink
        {
            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.Text;

            private readonly List<String> _events = [];
            private String _pendingStartTag = null!;

            public IReadOnlyList<String> Events => _events;

            public void Text(ReadOnlySpan<Byte> utf8)
            {
                var text = Encoding.UTF8.GetString(utf8);
                if (_events.Count != 0 && _events[^1].StartsWith("T:", StringComparison.Ordinal))
                {
                    _events[^1] += text;
                }
                else
                {
                    _events.Add("T:" + text);
                }
            }

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
            {
                _pendingStartTag = Encoding.UTF8.GetString(name.Verbatim);
                return Utf8HtmlStartTagCapture.None;
            }

            public Boolean WantsAttribute(Utf8HtmlName name) => false;

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) { }

            public void StartTagEnd(Boolean selfClosing)
            {
                _events.Add("S:" + _pendingStartTag + (selfClosing ? ":/" : String.Empty));
                _pendingStartTag = null!;
            }

            public void EndTag(Utf8HtmlName name) =>
                _events.Add("E:" + Encoding.UTF8.GetString(name.Verbatim));

            public void EndOfFile() => _events.Add("EOF");
        }

        private sealed class ValidatingTextSink : IUtf8HtmlTokenSink
        {
            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.Text;

            private readonly StringBuilder _text = new();

            public String DecodedText => _text.ToString();

            public void Text(ReadOnlySpan<Byte> utf8)
            {
                Assert.That(System.Text.Unicode.Utf8.IsValid(utf8), Is.True);
                _text.Append(Encoding.UTF8.GetString(utf8));
            }

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name) =>
                Utf8HtmlStartTagCapture.None;

            public Boolean WantsAttribute(Utf8HtmlName name) => false;

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) { }

            public void StartTagEnd(Boolean selfClosing) { }

            public void EndTag(Utf8HtmlName name) { }
        }

        private sealed class StructureOnlySink : IUtf8HtmlTokenSink
        {
            private readonly List<String> _events = [];
            private String _pendingStartTag = null!;

            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.None;

            public IReadOnlyList<String> Events => _events;

            public void Text(ReadOnlySpan<Byte> utf8) =>
                throw new InvalidOperationException("Text must not be emitted without capture interest.");

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
            {
                _pendingStartTag = Encoding.UTF8.GetString(name.Verbatim);
                return Utf8HtmlStartTagCapture.None;
            }

            public Boolean WantsAttribute(Utf8HtmlName name) => false;

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) { }

            public void StartTagEnd(Boolean selfClosing) =>
                _events.Add("S:" + _pendingStartTag);

            public void EndTag(Utf8HtmlName name) =>
                _events.Add("E:" + Encoding.UTF8.GetString(name.Verbatim));

            public void EndOfFile() => _events.Add("EOF");
        }

        private sealed class StructuralEventSink(Boolean captureTagAttributes) : IUtf8HtmlTokenSink
        {
            private readonly List<String> _events = [];
            private String _pendingStartTag = null!;

            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.None;

            public IReadOnlyList<String> Events => _events;

            public void Text(ReadOnlySpan<Byte> utf8) =>
                throw new InvalidOperationException("Text must not be emitted without capture interest.");

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
            {
                _pendingStartTag = Encoding.UTF8.GetString(name.Verbatim);
                return captureTagAttributes
                    ? Utf8HtmlStartTagCapture.Attributes
                    : Utf8HtmlStartTagCapture.None;
            }

            public Boolean WantsAttribute(Utf8HtmlName name) => false;

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) { }

            public void StartTagEnd(Boolean selfClosing) =>
                _events.Add($"S:{_pendingStartTag}:{selfClosing}");

            public void EndTag(Utf8HtmlName name) =>
                _events.Add("E:" + Encoding.UTF8.GetString(name.Verbatim));

            public void EndOfFile() => _events.Add("EOF");
        }

        private sealed class NameRecordingSink : IUtf8HtmlTokenSink
        {
            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.None;

            public String StartTagVerbatim { get; private set; } = null!;

            public UInt64 StartTagHash { get; private set; }

            public String AttributeWanted { get; private set; } = null!;

            public UInt64 AttributeWantedHash { get; private set; }

            public String AttributeVerbatim { get; private set; } = null!;

            public UInt64 AttributeHash { get; private set; }

            public String EndTagVerbatim { get; private set; } = null!;

            public UInt64 EndTagHash { get; private set; }

            public void Text(ReadOnlySpan<Byte> utf8) { }

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name)
            {
                StartTagVerbatim = Encoding.UTF8.GetString(name.Verbatim);
                StartTagHash = name.SemanticHash;
                return Utf8HtmlStartTagCapture.Attributes;
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

        private sealed class RejectThenAcceptAttributeSink(Int32 rejectedCount) : IUtf8HtmlTokenSink
        {
            public Utf8HtmlTokenCapture Capture => Utf8HtmlTokenCapture.None;

            private readonly List<String> _attributes = [];

            public Int32 WantsCalls { get; private set; }

            public IReadOnlyList<String> Attributes => _attributes;

            public void Text(ReadOnlySpan<Byte> utf8) { }

            public Utf8HtmlStartTagCapture StartTag(Utf8HtmlName name) =>
                Utf8HtmlStartTagCapture.Attributes;

            public Boolean WantsAttribute(Utf8HtmlName name) => ++WantsCalls > rejectedCount;

            public void Attribute(Utf8HtmlName name, ReadOnlySpan<Byte> value) =>
                _attributes.Add(
                    Encoding.UTF8.GetString(name.Verbatim) + "=" + Encoding.UTF8.GetString(value)
                );

            public void StartTagEnd(Boolean selfClosing) { }

            public void EndTag(Utf8HtmlName name) { }
        }
    }
}
#endif
