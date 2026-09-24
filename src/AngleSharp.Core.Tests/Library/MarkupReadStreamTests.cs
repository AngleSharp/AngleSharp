namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Parser;
    using AngleSharp.Xhtml;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public sealed class MarkupReadStreamTests
    {
        [TestCase("<!doctype html><html><body><template><p>inside ✨</p></template><pre>\nkeep</pre></body></html>")]
        [TestCase("<!doctype html><html><body><svg><title>π &amp; λ</title></svg><!--tail--></body></html>")]
        [TestCase("<p>one</p><p>two</p><p>three</p>")]
        public void SmallReadsMatchToHtmlForRepresentativeDocuments(String html)
        {
            using var document = new HtmlParser().ParseDocument(html);
            foreach (var formatter in new IMarkupFormatter[] { HtmlMarkupFormatter.Instance, new PrettyMarkupFormatter(), new MinifyMarkupFormatter(), XhtmlMarkupFormatter.Instance })
            {
                using var stream = document.ToHtmlStream(formatter);
                using var output = new MemoryStream();
                stream.CopyTo(output, 1);
                Assert.IsTrue(output.ToArray().SequenceEqual(Encoding.UTF8.GetBytes(document.ToHtml(formatter))));
                Assert.AreEqual(0, stream.Read(new Byte[1], 0, 1));
            }
        }

        [Test]
        public void PrefixReadDoesNotSerializeLaterNodes()
        {
            using var document = new HtmlParser().ParseDocument("<main>" + String.Concat(Enumerable.Range(0, 1000).Select(i => $"<p>later-{i}</p>")) + "</main>");
            var formatter = new CountingFormatter();
            using var stream = document.ToHtmlStream(formatter);
            Assert.AreEqual(0, formatter.TextCalls);
            Assert.Greater(stream.Read(new Byte[16], 0, 16), 0);
            Assert.LessOrEqual(formatter.TextCalls, 1);
        }

        [Test]
        public void Utf8EncoderCarriesSurrogateBetweenNodes()
        {
            using var document = new HtmlParser().ParseDocument("<p></p>");
            var paragraph = document.QuerySelector("p")!;
            paragraph.AppendChild(document.CreateTextNode("\uD83D"));
            paragraph.AppendChild(document.CreateTextNode("\uDE80"));
            using var stream = document.ToHtmlStream();
            using var output = new MemoryStream();
            stream.CopyTo(output, 1);
            Assert.IsTrue(output.ToArray().SequenceEqual(Encoding.UTF8.GetBytes(document.ToHtml())));
        }

        [Test]
        public async Task AsyncCopyMatchesToHtml()
        {
            using var document = new HtmlParser().ParseDocument("<template><p>inside</p></template><p>after ✨</p>");
            using var stream = document.ToHtmlStream();
            using var output = new MemoryStream();
            await stream.CopyToAsync(output);
            Assert.IsTrue(output.ToArray().SequenceEqual(Encoding.UTF8.GetBytes(document.ToHtml())));
        }

        [Test]
        public void FormatterFailureReachesTheReader()
        {
            using var document = new HtmlParser().ParseDocument("<p>failure</p>");
            using var stream = document.ToHtmlStream(new ThrowingFormatter());
            Assert.Throws<InvalidOperationException>(() => stream.CopyTo(Stream.Null));
            Assert.Throws<InvalidOperationException>(() => stream.ReadByte());
        }

        [Test]
        public void CancellationDoesNotPoisonStreamAndDisposalDoesNotDisposeDocument()
        {
            using var document = new HtmlParser().ParseDocument("<p>still readable</p>");
            using var stream = document.ToHtmlStream();
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            // A single-byte read is enough to verify cancellation before consumption.
#pragma warning disable CA2022
            Assert.ThrowsAsync<TaskCanceledException>(async () => await stream.ReadAsync(new Byte[1], 0, 1, cancellation.Token));
#pragma warning restore CA2022
            Assert.Greater(stream.Read(new Byte[1], 0, 1), 0);
            stream.Dispose();
            Assert.Throws<ObjectDisposedException>(() => stream.ReadByte());
            Assert.IsTrue(document.ToHtml().Contains("still readable"));
        }

        private sealed class CountingFormatter : HtmlMarkupFormatter
        {
            public Int32 TextCalls { get; private set; }

            public override String Text(ICharacterData text)
            {
                TextCalls++;
                return base.Text(text);
            }
        }

        private sealed class ThrowingFormatter : HtmlMarkupFormatter
        {
            public override String Text(ICharacterData text) => throw new InvalidOperationException("Formatter failed.");
        }
    }
}
