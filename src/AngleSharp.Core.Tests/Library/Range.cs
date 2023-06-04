namespace AngleSharp.Core.Tests.Library
{
    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void NewRangeIsCollapsedInDocument()
        {
            var document = "<body></body>".ToHtmlDocument();
            var range = document.CreateRange();

            Assert.AreEqual(0, range.Start);
            Assert.AreEqual(document, range.Head);
            Assert.AreEqual(0, range.End);
            Assert.AreEqual(document, range.Tail);
            Assert.AreEqual(document, range.CommonAncestor);
            Assert.IsTrue(range.IsCollapsed);
        }

        [Test]
        public void CanSelectRangeWithinTextNode_Issue1118()
        {
            var document = "<body></body>".ToHtmlDocument();
            var text = document.Body.AppendChild(document.CreateTextNode("this is a test"));
            var range = document.CreateRange();
            range.StartWith(text, 5);

            Assert.AreEqual(5, range.Start);
            Assert.AreEqual(text, range.Head);
            Assert.AreEqual(5, range.End);
            Assert.AreEqual(text, range.Tail);
            Assert.AreEqual(text, range.CommonAncestor);
            Assert.IsTrue(range.IsCollapsed);
        }

        [Test]
        public void CanSelectSomeRange_Issue1119()
        {
            var document = "<body><p><em>Text1</em>Text2</p></body>".ToHtmlDocument();
            var common = document.QuerySelector("p");
            var text1 = document.QuerySelector("em").FirstChild;
            var text2 = common.LastChild;
            var range = document.CreateRange();
            range.StartBefore(text1);
            range.EndAfter(text2);

            Assert.AreEqual(0, range.Start);
            Assert.AreEqual(text1.Parent, range.Head);
            Assert.AreEqual(1, range.End);
            Assert.AreEqual(text2.Parent, range.Tail);
            Assert.AreEqual(common, range.CommonAncestor);
            Assert.IsFalse(range.IsCollapsed);
        }
    }
}
