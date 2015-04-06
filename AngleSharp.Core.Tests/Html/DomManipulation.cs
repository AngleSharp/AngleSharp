using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests (automatically modified and adjusted originally) taken from
    /// http://w3c-test.org/html/dom/documents/dom-tree-accessors/document.body-getter.html
    /// </summary>
    [TestFixture]
    public class DomManipulation
    {
        static IDocument CreateDocument()
        {
            var doc = Html("");
            doc.RemoveChild(doc.DocumentElement);
            return doc;
        }

        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ChildlessDocument()
        {
            var doc = CreateDocument();
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void ChildlessHtmlElement()
        {
            var doc = CreateDocument();
            doc.AppendChild(doc.CreateElement("html"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void BodyFollowedByFramesetInsideTheHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            var b = html.AppendChild(doc.CreateElement("body"));
            html.AppendChild(doc.CreateElement("frameset"));
            Assert.AreEqual(b, doc.Body);
        }

        [Test]
        public void FramesetFollowedByBodyInsideTheHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            var f = html.AppendChild(doc.CreateElement("frameset"));
            html.AppendChild(doc.CreateElement("body"));
            Assert.AreEqual(f, doc.Body);
        }

        [Test]
        public void BodyFollowedByFramesetInsideAnonHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("http://example.org/test", "html"));
            html.AppendChild(doc.CreateElement("body"));
            html.AppendChild(doc.CreateElement("frameset"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void FramesetFollowedByBodyInsideAnonHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("http://example.org/test", "html"));
            html.AppendChild(doc.CreateElement("frameset"));
            html.AppendChild(doc.CreateElement("body"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void NonHtmlBodyFollowedByBodyInsideTheHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            html.AppendChild(doc.CreateElement("http://example.org/test", "body"));
            var b = html.AppendChild(doc.CreateElement("body"));
            Assert.AreEqual(b, doc.Body);
        }

        [Test]
        public void NonHtmlFramesetFollowedByBodyInsideTheHtmlElement()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            html.AppendChild(doc.CreateElement("http://example.org/test", "frameset"));
            var b = html.AppendChild(doc.CreateElement("body"));
            Assert.AreEqual(b, doc.Body);
        }

        [Test]
        public void BodyInsideAnxElementFollowedByBody()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            var x = html.AppendChild(doc.CreateElement("x"));
            x.AppendChild(doc.CreateElement("body"));
            var body = html.AppendChild(doc.CreateElement("body"));
            Assert.AreEqual(body, doc.Body);
        }

        [Test]
        public void FramesetInsideAnXElementFollowedByFrameset()
        {
            var doc = CreateDocument();
            var html = doc.AppendChild(doc.CreateElement("html"));
            var x = html.AppendChild(doc.CreateElement("x"));
            x.AppendChild(doc.CreateElement("frameset"));
            var frameset = html.AppendChild(doc.CreateElement("frameset"));
            Assert.AreEqual(frameset, doc.Body);
        }

        [Test]
        public void BodyAsTheRootNode()
        {
            var doc = CreateDocument();
            doc.AppendChild(doc.CreateElement("body"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void FramesetAsTheRootNode()
        {
            var doc = CreateDocument();
            doc.AppendChild(doc.CreateElement("frameset"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void BodyAsTheRootNodeWithAFramesetChild()
        {
            var doc = CreateDocument();
            var body = doc.AppendChild(doc.CreateElement("body"));
            body.AppendChild(doc.CreateElement("frameset"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void FramesetAsTheRootNodeWithABodyChild()
        {
            var doc = CreateDocument();
            var frameset = doc.AppendChild(doc.CreateElement("frameset"));
            frameset.AppendChild(doc.CreateElement("body"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void NonHtmlBodyAsTheRootNode()
        {
            var doc = CreateDocument();
            doc.AppendChild(doc.CreateElement("http://example.org/test", "body"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void NonHtmlFramesetAsTheRootNode()
        {
            var doc = CreateDocument();
            doc.AppendChild(doc.CreateElement("http://example.org/test", "frameset"));
            Assert.IsNull(doc.Body);
        }

        [Test]
        public void DocumentTitleExactMatch()
        {
            var doc = Html("<title>document.title with head blown away</title>");
            Assert.AreEqual("document.title with head blown away", doc.Title);
        }

        [Test]
        public void DocumentRemoveHeadAndReadOutTitle()
        {
            var doc = Html("<title>document.title with head blown away</title>");
            var head = doc.GetElementsByTagName("head")[0];
            Assert.IsNotNull(head);
            head.Parent.RemoveChild(head);
            Assert.IsNull(doc.GetElementsByTagName("head")[0]);
            doc.Title = "FAIL";
            Assert.AreEqual("", doc.Title);
        }
        
        [Test]
        public void DocumentFreshTitleAppendedAfterHeadRemoved()
        {
            var doc = Html("<title>document.title with head blown away</title>");
            var head = doc.GetElementsByTagName("head")[0];
            Assert.IsNotNull(head);
            head.Parent.RemoveChild(head);
            var title2 = doc.CreateElement("title");
            title2.AppendChild(doc.CreateTextNode("PASS"));
            doc.Body.AppendChild(title2);
            Assert.AreEqual("PASS", doc.Title);
        }
        
        [Test]
        public void DocumentInsertTitleBeforePreviouslyInsertedTitle()
        {
            var doc = Html("<title>document.title with head blown away</title>");
            var head = doc.GetElementsByTagName("head")[0];
            Assert.IsNotNull(head);
            head.Parent.RemoveChild(head);
            var title2 = doc.CreateElement("title");
            title2.AppendChild(doc.CreateTextNode("PASS"));
            doc.Body.AppendChild(title2);
            Assert.AreEqual("PASS", doc.Title);
            var title3 = doc.CreateElement("title");
            title3.AppendChild(doc.CreateTextNode("PASS2"));
            doc.DocumentElement.InsertBefore(title3, doc.Body);
            Assert.AreEqual("PASS2", doc.Title);
        }
    }
}
