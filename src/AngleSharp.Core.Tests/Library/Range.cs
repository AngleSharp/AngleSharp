namespace AngleSharp.Core.Tests.Library
{
    using NUnit.Framework;

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
            range.EndBefore(text2);

            Assert.AreEqual(0, range.Start);
            Assert.AreEqual(text1.Parent, range.Head);
            Assert.AreEqual(1, range.End);
            Assert.AreEqual(text2.Parent, range.Tail);
            Assert.AreEqual(common, range.CommonAncestor);
            Assert.IsFalse(range.IsCollapsed);
        }

        [Test]
        public void CanSelectSomeRange_Issue1147()
        {
            var document = "<body></body>".ToHtmlDocument();
            var text1 = document.Body.AppendChild(document.CreateTextNode("Text1"));
            var text2 = document.Body.AppendChild(document.CreateTextNode("TextLonger2"));
            var range1 = document.CreateRange();
            var range2 = document.CreateRange();
            range1.StartWith(text1, "Text".Length);
            range1.EndWith(text2, "TextLonger".Length);
            range2.StartWith(text1, "Text".Length);
            range2.EndWith(text2, "TextLonger".Length);
            range2.StartWith(text2, "TextLonger2".Length);

            Assert.AreEqual("Text".Length, range1.Start);
            Assert.AreEqual(text1, range1.Head);
            Assert.AreEqual("TextLonger".Length, range1.End);
            Assert.AreEqual(text2, range1.Tail);
            Assert.AreEqual(document.Body, range1.CommonAncestor);
            Assert.IsFalse(range1.IsCollapsed);
            Assert.AreEqual("TextLonger2".Length, range2.Start);
            Assert.AreEqual("TextLonger2".Length, range2.End);
            Assert.IsTrue(range2.IsCollapsed);
        }

        [Test]
        public void CheckCommonAncestor()
        {
            var document = "<body></body>".ToHtmlDocument();
            var p1 = document.Body.AppendChild(document.CreateElement("p"));
            var p2 = document.Body.AppendChild(document.CreateElement("p"));
            var p11 = p1.AppendChild(document.CreateElement("p"));
            var p12 = p1.AppendChild(document.CreateElement("p"));
            var p21 = p2.AppendChild(document.CreateElement("p"));

            var range1 = document.CreateRange();
            var range2 = document.CreateRange();

            range1.StartAfter(p11);
            range1.EndBefore(p12);
            range2.StartAfter(p11);
            range2.EndBefore(p21);

            Assert.AreEqual(p1, range1.CommonAncestor);
            Assert.AreEqual(document.Body, range2.CommonAncestor);
        }

        [Test]
        public void CanIntersects()
        {
            var document = "<body></body>".ToHtmlDocument();
            var p1 = document.Body.AppendChild(document.CreateElement("p"));
            var p2 = document.Body.AppendChild(document.CreateElement("p"));
            var p3 = document.Body.AppendChild(document.CreateElement("p"));

            var range1 = document.CreateRange();
            var range2 = document.CreateRange();

            range1.StartAfter(p1);
            range1.EndBefore(p3);
            range2.StartWith(p1, 0);
            range2.EndWith(p3, 0);

            Assert.IsFalse(range1.Intersects(p1));
            Assert.IsTrue(range1.Intersects(p2));
            Assert.IsFalse(range1.Intersects(p3));
            Assert.IsTrue(range1.Intersects(document.Body));

            Assert.IsTrue(range2.Intersects(p1));
            Assert.IsTrue(range2.Intersects(p2));
            Assert.IsTrue(range2.Intersects(p3));
            Assert.IsTrue(range2.Intersects(document.Body));
        }

        [Test]
        public void CanContains()
        {
            var document = "<body></body>".ToHtmlDocument();
            var p1 = document.Body.AppendChild(document.CreateElement("p"));
            var p2 = document.Body.AppendChild(document.CreateElement("p"));
            var p3 = document.Body.AppendChild(document.CreateElement("p"));

            var range1 = document.CreateRange();
            var range2 = document.CreateRange();
            var range3 = document.CreateRange();

            range1.StartAfter(p1);
            range1.EndBefore(p3);
            range2.StartWith(p1, 0);
            range2.EndWith(p3, 0);
            range3.Select(document.Body);

            Assert.IsFalse(range1.Contains(p1, 0));
            Assert.IsTrue(range1.Contains(p2, 0));
            Assert.IsFalse(range1.Contains(p3, 0));
            Assert.IsFalse(range1.Contains(document.Body, 0));

            Assert.IsFalse(range2.Contains(p1, 0));
            Assert.IsTrue(range2.Contains(p2, 0));
            Assert.IsFalse(range2.Contains(p3, 0));
            Assert.IsFalse(range2.Contains(document.Body, 0));

            Assert.IsTrue(range3.Contains(p1, 0));
            Assert.IsTrue(range3.Contains(p2, 0));
            Assert.IsTrue(range3.Contains(p3, 0));
            Assert.IsTrue(range3.Contains(document.Body, 0));
        }

        [Test]
        public void CanClearContent()
        {
            var document = @"
<html>
<head></head>
<body>
<p> No deletion before start <span id=""start""></span> This should be cleared </p>
<p> <span id=""toDelete""></span>This should be deleted too <span id=""end""></span> This is not to be deleted</p>
<p> This should not be deleted either</p>
</body>
</html>".ToHtmlDocument();
            var start = document.QuerySelector("#start");
            var end = document.QuerySelector("#end");
            var toDelete = document.QuerySelector("#toDelete");

            var range = document.CreateRange();

            range.StartWith(start, 0);
            range.EndWith(end, 0);

            range.ClearContent();

            var htmlRaw = document.DocumentElement.OuterHtml;
            Assert.IsTrue(htmlRaw.Contains("No deletion before start"));
            Assert.IsTrue(htmlRaw.Contains("This is not to be deleted"));
            Assert.IsTrue(htmlRaw.Contains("This should not be deleted either"));
            Assert.IsFalse(htmlRaw.Contains("This should be cleared"));
            Assert.IsFalse(htmlRaw.Contains("This should be deleted too"));
            Assert.IsFalse(document.Contains(toDelete));
        }
    }
}
