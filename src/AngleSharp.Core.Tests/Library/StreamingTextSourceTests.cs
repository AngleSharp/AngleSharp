namespace AngleSharp.Core.Tests.Library;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using NUnit.Framework;

[TestFixture]
public sealed class StreamingTextSourceTests
{
    private static IEnumerable<TestCaseData> DifferentialCases
    {
        get
        {
            var inputs = new[]
            {
                "<!doctype html><p title='hé😀終'>alpha &amp; Ω</p><script>const x = '</nope>';</script>",
                "<table>before<tr><td>A<td>B</table>after",
                "<svg><foreignObject><p>HTML</foreignObject></svg>",
                "<!-- split -- comment --><template><b>nested</template><i>end",
                "<textarea>&lt;b&gt; raw-ish </textarea><p>x",
                "<script><!-- const closing = '</script-not>'; --></script><p>after",
                "<p a='unterminated &amp; value>text<div>recovery",
                new String('x', 20_000) + "<p>tail 😀</p>",
            };
            var chunkSizes = new[] { 1, 3, 127, 4096 };

            for (var inputIndex = 0; inputIndex < inputs.Length; inputIndex++)
            {
                foreach (var chunkSize in chunkSizes)
                {
                    yield return new TestCaseData(inputs[inputIndex], chunkSize)
                        .SetName($"ParserMatchesStringInput_{inputIndex}_Chunk{chunkSize}");
                }
            }
        }
    }

    [TestCaseSource(nameof(DifferentialCases))]
    [Category("StreamingTextSource")]
    public async Task ParserMatchesStringInputAcrossUtf8Boundaries(String html, Int32 maxReadSize)
    {
        var parser = new HtmlParser();
        using var expected = parser.ParseDocument(html);
        using var stream = new ChunkedReadStream(Encoding.UTF8.GetBytes(html), maxReadSize);
        using var actual = await parser.ParseDocumentAsync(
            stream,
            HtmlStreamSourceMode.Streaming,
            Encoding.UTF8);

        Assert.That(actual.ToHtml(), Is.EqualTo(expected.ToHtml()));
    }

    [Test]
    public async Task ParserSkipsUtf8BomSplitAcrossReads()
    {
        const String html = "<p>ok</p>";
        var payload = new Byte[] { 0xEF, 0xBB, 0xBF }.Concat(Encoding.UTF8.GetBytes(html)).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        var parser = new HtmlParser();
        using var actual = await parser.ParseDocumentAsync(
            stream,
            HtmlStreamSourceMode.Streaming,
            Encoding.UTF8);

        Assert.That(actual!.Body!.TextContent, Is.EqualTo("ok"));
    }

    [Test]
    public async Task ExistingStreamOverloadKeepsBufferedSourceContract()
    {
        const String html = "<p>source remains available</p>";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(html));
        using var document = await new HtmlParser().ParseDocumentAsync(stream, CancellationToken.None);

        Assert.That(document.Source.Text, Does.Contain("source remains available"));
    }

    [Test]
    public async Task BufferedModeDetectsUtf32BomAcrossShortReads()
    {
        const String html = "<p>utf-32 ✓</p>";
        var payload = Encoding.UTF32.GetPreamble().Concat(Encoding.UTF32.GetBytes(html)).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, CancellationToken.None);

        Assert.That(document.Body!.TextContent, Is.EqualTo("utf-32 ✓"));
        Assert.That(document.CharacterSet, Is.EqualTo("utf-32"));
    }

    [Test]
    public async Task AutomaticModeAccumulatesPreludeAcrossShortReads()
    {
        const String html = "<meta charset=utf-8><p>hé😀終</p>";
        using var stream = new ChunkedReadStream(Encoding.UTF8.GetBytes(html), 1);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.Body!.TextContent, Is.EqualTo("hé😀終"));
        Assert.That(document.CharacterSet, Is.EqualTo("utf-8"));
    }

    [Test]
    public async Task AutomaticModeUsesLegacyEncodingDeclaredInPrelude()
    {
        var prefix = Encoding.ASCII.GetBytes("<meta charset=windows-1252><p>caf");
        var suffix = Encoding.ASCII.GetBytes("</p>");
        var payload = prefix.Concat(new Byte[] { 0xe9 }).Concat(suffix).ToArray();
        using var stream = new ChunkedReadStream(payload, 3);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.Body!.TextContent, Is.EqualTo("café"));
        Assert.That(document.CharacterSet, Is.EqualTo("windows-1252"));
    }

    [Test]
    public async Task AutomaticModeRestartsWhenConsumedPrefixChangesEncoding()
    {
        var beforeMeta = Encoding.ASCII.GetBytes("<title>caf");
        var afterLegacyByte = Encoding.ASCII.GetBytes("</title><meta charset=windows-1252><p>ok</p>");
        var payload = beforeMeta.Concat(new Byte[] { 0xe9 }).Concat(afterLegacyByte).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.Title, Is.EqualTo("café"));
        Assert.That(document.Body!.TextContent, Is.EqualTo("ok"));
        Assert.That(document.CharacterSet, Is.EqualTo("windows-1252"));
    }

    [Test]
    public async Task AutomaticModeHonorsBomSplitAcrossReads()
    {
        const String html = "<p>bom ✓</p>";
        var payload = new Byte[] { 0xff, 0xfe }.Concat(Encoding.Unicode.GetBytes(html)).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.Body!.TextContent, Is.EqualTo("bom ✓"));
        Assert.That(document.CharacterSet, Is.EqualTo("utf-16"));
    }

    [Test]
    public async Task AutomaticModeIgnoresMetaInsideComment()
    {
        const String html = "<!-- <meta charset=windows-1252> --><p>café</p>";
        using var stream = new ChunkedReadStream(Encoding.UTF8.GetBytes(html), 7);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.Body!.TextContent, Is.EqualTo("café"));
        Assert.That(document.CharacterSet, Is.EqualTo("utf-8"));
    }

    [Test]
    public async Task AutomaticModeIgnoresLateConflictingDeclaration()
    {
        var prefix = Encoding.ASCII.GetBytes("<p>" + new String('x', 1024));
        var suffix = Encoding.ASCII.GetBytes("<meta charset=windows-1252>caf");
        var payload = prefix.Concat(suffix).Concat(new Byte[] { 0xe9 }).Concat(Encoding.ASCII.GetBytes("</p>")).ToArray();
        using var stream = new ChunkedReadStream(payload, 127);
        using var document = await new HtmlParser().ParseDocumentAsync(stream, HtmlStreamSourceMode.Streaming);

        Assert.That(document.CharacterSet, Is.EqualTo("utf-8"));
        Assert.That(document.Body!.TextContent, Does.EndWith("caf�"));
    }

    [Test]
    public async Task StreamingModeWithScriptingFallsBackToBufferedSource()
    {
        var parser = new HtmlParser(new HtmlParserOptions { IsScripting = true });
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("<p>x</p>"));
        using var document = await parser.ParseDocumentAsync(
            stream,
            HtmlStreamSourceMode.Streaming,
            Encoding.UTF8);

        Assert.That(document.Body!.TextContent, Is.EqualTo("x"));
        Assert.That(document.Source.Text, Does.Contain("<p>x</p>"));
    }

    [TestCase(HtmlStreamSourceMode.Buffered)]
    [TestCase(HtmlStreamSourceMode.Streaming)]
    public async Task ExplicitEncodingIsAuthoritative(HtmlStreamSourceMode sourceMode)
    {
        var beforeMeta = Encoding.ASCII.GetBytes("<title>caf");
        var afterLegacyByte = Encoding.ASCII.GetBytes("</title><meta charset=utf-8>");
        var payload = beforeMeta.Concat(new Byte[] { 0xe9 }).Concat(afterLegacyByte).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        using var document = await new HtmlParser().ParseDocumentAsync(
            stream,
            sourceMode,
            Encoding.GetEncoding(1252));

        Assert.That(document.Title, Is.EqualTo("café"));
        Assert.That(document.CharacterSet, Is.EqualTo("windows-1252"));
    }

    [TestCase(HtmlStreamSourceMode.Buffered)]
    [TestCase(HtmlStreamSourceMode.Streaming)]
    public async Task ExplicitEncodingDoesNotYieldToBom(HtmlStreamSourceMode sourceMode)
    {
        var payload = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes("<p>x</p>")).ToArray();
        using var stream = new ChunkedReadStream(payload, 1);
        using var document = await new HtmlParser().ParseDocumentAsync(
            stream,
            sourceMode,
            Encoding.GetEncoding(1252));

        Assert.That(document.CharacterSet, Is.EqualTo("windows-1252"));
        Assert.That(document.Body!.TextContent, Does.EndWith("x"));
    }

    [Test]
    public async Task StreamingModeLeavesInputStreamOpen()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes("<p>x</p>"));
        using (var document = await new HtmlParser().ParseDocumentAsync(
                   stream,
                   HtmlStreamSourceMode.Streaming,
                   Encoding.UTF8))
        {
            Assert.That(document.Body!.TextContent, Is.EqualTo("x"));
        }

        Assert.That(stream.CanRead, Is.True);
        stream.Dispose();
    }

    [Test]
    public void SourceRetainsOnlyBoundedLookback()
    {
        var payload = Encoding.UTF8.GetBytes(new String('x', 32_000));
        using var stream = new MemoryStream(payload);
        using var source = new StreamingTextSource(stream, 128, 64);

        for (var index = 0; index < 20_000; index++)
        {
            Assert.That(source.ReadCharacter(), Is.EqualTo('x'));
        }

        Assert.That(source.Index, Is.EqualTo(20_000));
        Assert.Throws<ArgumentOutOfRangeException>(() => source.Index = 0);
        source.Index -= 64;
        Assert.That(source.ReadCharacter(), Is.EqualTo('x'));
    }

    [Test]
    public void EndOfFileReadCanBeReconsumed()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("x"));
        using var source = new StreamingTextSource(stream, 128);

        Assert.That(source.ReadCharacter(), Is.EqualTo('x'));
        Assert.That(source.ReadCharacter(), Is.EqualTo(Symbols.EndOfFile));
        Assert.That(source.Index, Is.EqualTo(2));

        source.Index--;
        Assert.That(source.ReadCharacter(), Is.EqualTo(Symbols.EndOfFile));
    }

    private sealed class ChunkedReadStream : Stream
    {
        private readonly Byte[] _source;
        private readonly Int32 _maxReadSize;
        private Int32 _position;

        public ChunkedReadStream(Byte[] source, Int32 maxReadSize)
        {
            _source = source;
            _maxReadSize = maxReadSize;
        }

        public override Boolean CanRead => true;
        public override Boolean CanSeek => false;
        public override Boolean CanWrite => false;
        public override Int64 Length => _source.Length;
        public override Int64 Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            var length = Math.Min(Math.Min(count, _maxReadSize), _source.Length - _position);
            if (length <= 0)
            {
                return 0;
            }

            Array.Copy(_source, _position, buffer, offset, length);
            _position += length;
            return length;
        }

        public override Task<Int32> ReadAsync(
            Byte[] buffer,
            Int32 offset,
            Int32 count,
            CancellationToken cancellationToken) => Task.FromResult(Read(buffer, offset, count));

        public override void Flush() { }
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(Int64 value) => throw new NotSupportedException();
        public override void Write(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
    }
}
