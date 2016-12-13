namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class AsyncParsingTests
    {
        [Test]
        public async Task TestAsyncHtmlParsingFromStream()
        {
            var text = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var source = new DelayedStream(Encoding.UTF8.GetBytes(text));
            var parser = new HtmlParser();

            using (var task = parser.ParseDocumentAsync(source))
            {
                Assert.IsFalse(task.IsCompleted);
                var result = await task;

                Assert.IsTrue(task.IsCompleted);
                Assert.AreEqual("My test", result.Title);
                Assert.AreEqual(1, result.Body.ChildElementCount);
                Assert.AreEqual("Some text", result.Body.Children[0].TextContent);
            }
        }

        [Test]
        public async Task TestAsyncHtmlParsingFromString()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser();

            using (var task = parser.ParseDocumentAsync(source))
            {
                Assert.IsTrue(task.IsCompleted);
                var result = await task;

                Assert.AreEqual("My test", result.Title);
                Assert.AreEqual(1, result.Body.ChildElementCount);
                Assert.AreEqual("Some text", result.Body.Children[0].TextContent);
            }
        }
    }
}
