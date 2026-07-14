namespace AngleSharp.Core.Tests.Html
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Html.Dom.Events;
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

        private sealed class SegmentedTokenSource : IHtmlTokenSource
        {
            private readonly Queue<StructHtmlToken> _tokens;
            private Boolean _available = true;

            public SegmentedTokenSource(params StructHtmlToken[] tokens) => _tokens = new(tokens);

            public Int32 WaitCount { get; private set; }

            public event EventHandler<HtmlErrorEvent> Error;

            public HtmlParseMode State { get; set; }
            public Boolean IsAcceptingCharacterData { get; set; }
            public Boolean IsStrictMode { get; set; }
            public Boolean IsSupportingProcessingInstructions { get; set; }
            public Boolean IsNotConsumingCharacterReferences { get; set; }
            public Boolean IsPreservingAttributeNames { get; set; }
            public Boolean SkipRawText { get; set; }
            public Boolean SkipScriptText { get; set; }
            public Boolean SkipDataText { get; set; }
            public Boolean SkipComments { get; set; }
            public Boolean SkipPlaintext { get; set; }
            public Boolean SkipRCDataText { get; set; }
            public Boolean SkipCDATA { get; set; }
            public Boolean SkipProcessingInstructions { get; set; }
            public Boolean DisableElementPositionTracking { get; set; }
            public ShouldEmitAttribute ShouldEmitAttribute { get; set; } =
                static (ref StructHtmlToken _, ReadOnlyMemory<Char> _) => true;
            public Action<HtmlToken, TextRange> OnToken { get; set; }

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

            public void RaiseErrorOccurred(HtmlParseError code, TextPosition position) =>
                Error?.Invoke(this, new HtmlErrorEvent(code, position));

            public void Dispose() { }
        }
    }
}
