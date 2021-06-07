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

            using (var task = parser.ParseHeadAsync(source))
            {
                Assert.IsFalse(task.IsCompleted);
                var result = await task;

                Assert.IsTrue(task.IsCompleted);
                Assert.AreEqual("head", result.LocalName);
                Assert.AreEqual(1, result.ChildElementCount);
                Assert.AreEqual("My test", result.Children[0].TextContent);
            }
        }

        [Test]
        public async Task TestAsyncHeadParsingFromString()
        {
            var source = "<html><head><title>My test</title></head><body><p>Some text</p></body></html>";
            var parser = new HtmlParser();

            using (var task = parser.ParseHeadAsync(source))
            {
                Assert.IsTrue(task.IsCompleted);
                var result = await task;

                Assert.AreEqual("head", result.LocalName);
                Assert.AreEqual(1, result.ChildElementCount);
                Assert.AreEqual("My test", result.Children[0].TextContent);
            }
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
    }
}
