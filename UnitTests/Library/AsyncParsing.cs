using AngleSharp;
using AngleSharp.Parser.Css;
using AngleSharp.Parser.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AsyncParsing
    {
        [TestMethod]
        public void TestAsyncCssParsing()
        {
            var source = "h1 { color: red; } h2 { color: blue; } p { font-family: Arial; } div { margin: 10 }";
            var parser = new CssParser(source, Configuration.Default);
            var task = parser.ParseAsync();
            Assert.IsFalse(task.IsCompleted);
            Assert.IsNotNull(parser.Result);
            Assert.IsFalse(task.IsCompleted);
            task.Wait();
            Assert.IsTrue(task.IsCompleted);
            Assert.IsNotNull(parser.Result);
            Assert.AreEqual(4, parser.Result.CssRules.Length);
        }

        [TestMethod]
        public void TestAsyncHtmlParsing()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser(source, Configuration.Default);
            var task = parser.ParseAsync();
            Assert.IsFalse(task.IsCompleted);
            Assert.IsNotNull(parser.Result);
            Assert.IsFalse(task.IsCompleted);
            task.Wait();
            Assert.IsTrue(task.IsCompleted);
            Assert.IsNotNull(parser.Result);
            Assert.AreEqual("My test", parser.Result.Title);
            Assert.AreEqual(1, parser.Result.Body.ChildElementCount);
            Assert.AreEqual("Some text", parser.Result.Body.Children[0].TextContent);
        }
    }
}
