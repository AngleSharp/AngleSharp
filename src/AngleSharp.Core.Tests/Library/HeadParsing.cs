namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class HeadParsingTests
    {
        [Test]
        public async Task TestAsyncHeadParsingFromStream()
        {
            var text = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var parser = new HtmlParser();

            using var task = parser.ParseHeadAsync(source);
            Assert.IsFalse(task.IsCompleted);
            var result = await task;

            Assert.IsTrue(task.IsCompleted);
            Assert.AreEqual("head", result.LocalName);
            Assert.AreEqual(1, result.ChildElementCount);
            Assert.AreEqual("My test", result.Children[0].TextContent);
        }

        [Test]
        public async Task TestAsyncHeadParsingFromString()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser();

            using var task = parser.ParseHeadAsync(source);
            Assert.IsTrue(task.IsCompleted);
            var result = await task;

            Assert.AreEqual("head", result.LocalName);
            Assert.AreEqual(1, result.ChildElementCount);
            Assert.AreEqual("My test", result.Children[0].TextContent);
        }

        [Test]
        public void TestSyncHeadParsingFromStream()
        {
            var text = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var parser = new HtmlParser();
            var result = parser.ParseHead(source);
            Assert.AreEqual("head", result.LocalName);
            Assert.AreEqual(1, result.ChildElementCount);
            Assert.AreEqual("My test", result.Children[0].TextContent);
        }

        [Test]
        public void TestSyncHeadParsingFromString()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser();
            var result = parser.ParseHead(source);
            Assert.AreEqual("head", result.LocalName);
            Assert.AreEqual(1, result.ChildElementCount);
            Assert.AreEqual("My test", result.Children[0].TextContent);
        }

        [Test]
        public void CustomElementsInHeadShouldByDefaultBeMovedToBody_Issue1035()
        {
            var source = @"<html>
  <head>
    <site-include type=""partial"" name=""scripts""></site-include>
  </head>
</html>";
            var parser = new HtmlParser();
            var document = parser.ParseDocument(source);
            var isInHead = document.Head.QuerySelector("site-include");
            var isInBody = document.Body?.QuerySelector("site-include");
            Assert.IsNull(isInHead);
            Assert.IsNotNull(isInBody);
        }

        [Test]
        public void CustomElementsInHeadShouldBeKeptIfActivated_Issue1035()
        {
            var source = @"<html>
  <head>
    <site-include type=""partial"" name=""scripts""></site-include>
  </head>
</html>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsAcceptingCustomElementsEverywhere = true,
            });
            var document = parser.ParseDocument(source);
            var isInHead = document.Head.QuerySelector("site-include");
            var isInBody = document.Body?.QuerySelector("site-include");
            Assert.IsNotNull(isInHead);
            Assert.IsNull(isInBody);
        }

        [Test]
        public void CustomElementsInHeadWithChildrenIfActivated_Issue1035()
        {
            var source = @"<html>
  <head>
  </head>
  <body>
    <site-include type=""partial"" name=""scripts""><div>Yes</div></site-include>
  </body>
</html>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsAcceptingCustomElementsEverywhere = true,
            });
            var document = parser.ParseDocument(source);
            var element = document.Body.QuerySelector("site-include");
            Assert.AreEqual(@"<site-include type=""partial"" name=""scripts""><div>Yes</div></site-include>", element.ToHtml());
        }

        [Test]
        public void CustomElementsInTableWithChildrenIfActivated_Issue1035()
        {
            var source = @"<html>
  <head>
  </head>
  <body>
    <table>
      <my-table-head></my-table-head>
    </table>
  </body>
</html>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsAcceptingCustomElementsEverywhere = true,
            });
            var document = parser.ParseDocument(source);
            var element = document.Body;
            Assert.AreEqual("<body>\n    <table>\n      <my-table-head></my-table-head>\n    </table>\n  \n</body>", element.ToHtml());
        }
    }
}
