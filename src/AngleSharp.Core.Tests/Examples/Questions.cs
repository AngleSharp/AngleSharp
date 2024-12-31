namespace AngleSharp.Core.Tests.Examples
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using NUnit.Framework;

    [TestFixture]
    public class Questions
    {
        [Test]
        public void GetPositionViaCallback()
        {
            var bodyPos = TextPosition.Empty;
            var parser = new HtmlParser(new HtmlParserOptions
            {
                OnCreated = (IElement element, TextPosition position) =>
                {
                    if (element.TagName == "BODY")
                    {
                        bodyPos = position;
                    }
                },
            });
            parser.ParseDocument("<!doctype html><body>");
            Assert.AreEqual(new TextPosition(1, 16, 16), bodyPos);
        }

        [Test]
        public void GetPositionViaSourceReference()
        {
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsKeepingSourceReferences = true,
            });
            var document = parser.ParseDocument("<!doctype html><body>");
            var bodyPos = document.Body.SourceReference.Position;
            Assert.AreEqual(new TextPosition(1, 16, 16), bodyPos);
        }

        [Test]
        public void GetPositionViaTokenCallback()
        {
            var bodyStartPos = TextPosition.Empty;
            var bodyEndPos = TextPosition.Empty;
            var parser = new HtmlParser(new HtmlParserOptions
            {
                OnToken = (HtmlToken token, TextRange range) =>
                {
                    if (token.Name == "body")
                    {
                        bodyStartPos = range.Start;
                        bodyEndPos = range.End;
                    }
                },
            });
            parser.ParseDocument("<!doctype html><body>");
            Assert.AreEqual(new TextPosition(1, 16, 16), bodyStartPos);
            Assert.AreEqual(new TextPosition(1, 22, 22), bodyEndPos);
        }
    }
}
