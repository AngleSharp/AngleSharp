namespace AngleSharp.Core.Tests.Html
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Html.Parser.Tokens.Struct;
    using AngleSharp.Text;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncTokenSourceTests
    {
        [Test]
        public async Task TreeBuilderWaitsWheneverSegmentedTokenSourceNeedsInput()
        {
            using var source = new SegmentedTokenSource(
                StructHtmlToken.Open("pre"),
                StructHtmlToken.Character("\nhello", default),
                StructHtmlToken.Close("pre"),
                StructHtmlToken.EndOfFile(default));

            var parse = new HtmlParser().ParseDocumentAsync(source);
            Assert.IsFalse(parse.IsCompleted);
            var document = await parse;

            Assert.AreEqual("hello", document.Body.TextContent);
            Assert.AreEqual(3, source.WaitCount);
        }

        [Test]
        public async Task CallerRetainsOwnershipOfAlternativeTokenSource()
        {
            var source = new SegmentedTokenSource(StructHtmlToken.EndOfFile(default));
            var document = await new HtmlParser().ParseDocumentAsync(source);

            document.Dispose();

            Assert.IsFalse(source.IsDisposed);
            source.Dispose();
            Assert.IsTrue(source.IsDisposed);
        }

        private sealed class SegmentedTokenSource : IHtmlTokenSource, IDisposable
        {
            private readonly Queue<StructHtmlToken> _tokens;
            private Boolean _available = true;

            public SegmentedTokenSource(params StructHtmlToken[] tokens) => _tokens = new(tokens);

            public Int32 WaitCount { get; private set; }
            public Boolean IsDisposed { get; private set; }

            public void Configure(
                HtmlTokenizerOptions options,
                Action<HtmlToken, TextRange> onToken,
                Action<HtmlParseError, TextPosition> reportError)
            {
            }

            public void SetState(HtmlParseMode state)
            {
            }

            public void SetAcceptingCharacterData(Boolean value)
            {
            }

            public Boolean TryGetStructToken(out StructHtmlToken token)
            {
                if (!_available)
                {
                    token = default;
                    return false;
                }
                token = _tokens.Dequeue();
                _available = _tokens.Count == 0;
                return true;
            }

            public async Task WaitForInputAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                WaitCount++;
                await Task.Yield();
                cancellationToken.ThrowIfCancellationRequested();
                _available = true;
            }

            public void Dispose() => IsDisposed = true;
        }
    }
}
