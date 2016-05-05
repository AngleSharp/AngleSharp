using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// These tests are taken from http://www.quirksmode.org/dom/tests/.
    /// More information: http://www.quirksmode.org/dom/w3c_core.html
    /// </summary>
    [TestFixture]
    public class QuirksmodeTests
    {
        IDocument document;

        [SetUp]
        public void Setup()
        {
            document = Assets.quirksmode.ToHtmlDocument();
        }

        [Test]
        public void CreateElementInUppercase()
        {
            var x = document.CreateElement("P");
            Assert.IsNotNull(x);
            Assert.IsTrue(x is HtmlParagraphElement);
            Assert.AreEqual(document, x.Owner);
        }

        [Test]
        public void CreateTextNode()
        {
            var text = " textNode";
            var test = document.CreateTextNode(text);
            var testEl = document.GetElementById("test");

            for (var i = testEl.ChildNodes.Length - 1; i >= 0; i--)
                testEl.RemoveChild(testEl.ChildNodes[i]);

            Assert.AreEqual(0, testEl.Children.Length);
            testEl.AppendChild(test as TextNode);
            Assert.AreEqual(text, testEl.InnerHtml);
            Assert.AreEqual(document, test.Owner);
        }

        [Test]
        public void GetElementById()
        {
            var x = document.GetElementById("test");
            Assert.AreEqual("p", x.LocalName);
            Assert.AreEqual(document, x.Owner);
        }

        [Test]
        public void GetElementByIdWithAName()
        {
            var x = document.GetElementById("test3");
            Assert.IsNull(x);
        }

        [Test]
        public void GetElementsByClassNameSingle()
        {
            var cn = document.GetElementsByClassName("testClass");
            Assert.AreEqual(2, cn.Length);
            Assert.AreEqual("p", cn[0].LocalName);
        }

        [Test]
        public void GetElementsByClassNameMultiple()
        {
            var cn = document.GetElementsByClassName("testClass nonsense");
            Assert.AreEqual(1, cn.Length);
            Assert.AreEqual("p", cn[0].LocalName);
        }

        [Test]
        public void GetElementsByTagNameUsual()
        {
            var result = document.GetElementsByTagName("P");
            Assert.AreEqual(4, result.Length);
        }

        [Test]
        public void GetElementsByTagNameAll()
        {
            var result = document.GetElementsByTagName("*");
            Assert.AreEqual(23, result.Length);
        }

        [Test]
        public void GetElementsByTagNameCustom()
        {
            var result = document.GetElementsByTagName("ppk");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void QuerySelectorAllClass()
        {
            var qsa = document.QuerySelectorAll(".testClass");
            Assert.AreEqual(2, qsa.Length);
        }

        [Test]
        public void QuerySelectorAllCompound()
        {
            var qsa = document.QuerySelectorAll(".testClass + p");
            Assert.AreEqual(2, qsa.Length);
        }
    }
}
