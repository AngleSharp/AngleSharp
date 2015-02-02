namespace AngleSharp.Core.Tests
{
    using AngleSharp;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using NUnit.Framework;
    using System.Text;

    [TestFixture]
    public class AsyncParsingTests
    {
        [Test]
        public void TestAsyncCssParsingFromStream()
        {
            var text = "h1 { color: red; } h2 { color: blue; } p { font-family: Arial; } div { margin: 10 }";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var parser = new CssParser(source, Configuration.Default);

            using (var task = parser.ParseAsync())
            {
                Assert.IsFalse(task.IsCompleted);
                Assert.IsNotNull(parser.Result);
                task.Wait();
                Assert.IsTrue(task.IsCompleted);
                Assert.IsNotNull(parser.Result);

                Assert.AreEqual(4, parser.Result.Rules.Length);
            }
        }

        [Test]
        public void TestAsyncHtmlParsingFromStream()
        {
            var text = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var parser = new HtmlParser(source, Configuration.Default);

            using (var task = parser.ParseAsync())
            {
                Assert.IsFalse(task.IsCompleted);
                Assert.IsNotNull(parser.Result);
                task.Wait();
                Assert.IsTrue(task.IsCompleted);
                Assert.IsNotNull(parser.Result);

                Assert.AreEqual("My test", parser.Result.Title);
                Assert.AreEqual(1, parser.Result.Body.ChildElementCount);
                Assert.AreEqual("Some text", parser.Result.Body.Children[0].TextContent);
            }
        }

        [Test]
        public void TestAsyncCssParsingFromString()
        {
            var source = "h1 { color: red; } h2 { color: blue; } p { font-family: Arial; } div { margin: 10 }";
            var parser = new CssParser(source, Configuration.Default);

            using (var task = parser.ParseAsync())
            {
                Assert.IsTrue(task.IsCompleted);
                Assert.IsNotNull(parser.Result);

                Assert.AreEqual(4, parser.Result.Rules.Length);
            }
        }

        [Test]
        public void TestAsyncHtmlParsingFromString()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser(source, Configuration.Default);

            using (var task = parser.ParseAsync())
            {
                Assert.IsTrue(task.IsCompleted);
                Assert.IsNotNull(parser.Result);

                Assert.AreEqual("My test", parser.Result.Title);
                Assert.AreEqual(1, parser.Result.Body.ChildElementCount);
                Assert.AreEqual("Some text", parser.Result.Body.Children[0].TextContent);
            }
        }
    }
}
