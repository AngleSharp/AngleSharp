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
        public void GetPositionViaSourceReferenceWithCrLf()
        {
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsKeepingSourceReferences = true,
            });
            var document = parser.ParseDocument("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Invoice</title>\r\n    <style>\r\n");
            var meta = document.Head.QuerySelector("meta");
            var title = document.Head.QuerySelector("title");
            var style = document.Head.QuerySelector("style");

            Assert.AreEqual(new TextPosition(4, 1, 38), document.Head.SourceReference.Position);
            Assert.AreEqual(new TextPosition(5, 5, 50), meta.SourceReference.Position);
            Assert.AreEqual(new TextPosition(6, 5, 78), title.SourceReference.Position);
            Assert.AreEqual(new TextPosition(7, 5, 106), style.SourceReference.Position);
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
