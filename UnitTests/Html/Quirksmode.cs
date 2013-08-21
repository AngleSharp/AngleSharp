using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.DOM.Html;
using AngleSharp;

namespace UnitTests
{
    /// <summary>
    /// These tests are taken from http://www.quirksmode.org/dom/tests/.
    /// More information: http://www.quirksmode.org/dom/w3c_core.html
    /// </summary>
    [TestClass]
    public class Quirksmode
    {
        HTMLDocument document;

        [TestInitialize]
        public void Setup()
        {
            document = DocumentBuilder.Html(Assets.QuirksMode);
        }

        [TestMethod]
        public void CreateElementInUppercase()
        {
            var x = document.CreateElement("P");
            Assert.IsNotNull(x);
            Assert.IsTrue(x is HTMLParagraphElement);
            Assert.AreEqual(document, x.OwnerDocument);
        }

        [TestMethod]
        public void CreateTextNode()
        {
            var text = " textNode";
            var test = document.CreateTextNode(text);
            var testEl = document.GetElementById("test");

            for (var i = testEl.ChildNodes.Length - 1; i >= 0; i--)
                testEl.RemoveChild(testEl.ChildNodes[i]);

            Assert.AreEqual(0, testEl.Children.Length);
            testEl.AppendChild(test);
            Assert.AreEqual(text, testEl.InnerHTML);
            Assert.AreEqual(document, test.OwnerDocument);
        }

        [TestMethod]
        public void GetElementById()
        {
            var x = document.GetElementById("test");
            Assert.AreEqual("p", x.TagName);
            Assert.AreEqual(document, x.OwnerDocument);
        }

        [TestMethod]
        public void GetElementByIdWithAName()
        {
            var x = document.GetElementById("test3");
            Assert.IsNull(x);
        }

        [TestMethod]
        public void GetElementsByClassNameSingle()
        {
            var cn = document.GetElementsByClassName("testClass");
            Assert.AreEqual(2, cn.Length);
            Assert.AreEqual("p", cn[0].TagName);
        }

        [TestMethod]
        public void GetElementsByClassNameMultiple()
        {
            var cn = document.GetElementsByClassName("testClass nonsense");
            Assert.AreEqual(1, cn.Length);
            Assert.AreEqual("p", cn[0].TagName);
        }

        [TestMethod]
        public void GetElementsByTagNameUsual()
        {
            var result = document.GetElementsByTagName("P");
            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GetElementsByTagNameAll()
        {
            var result = document.GetElementsByTagName("*");
            Assert.AreEqual(23, result.Length);
        }

        [TestMethod]
        public void GetElementsByTagNameCustom()
        {
            var result = document.GetElementsByTagName("ppk");
            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void QuerySelectorAllClass()
        {
            var qsa = document.QuerySelectorAll(".testClass");
            Assert.AreEqual(2, qsa.Length);
        }

        [TestMethod]
        public void QuerySelectorAllCompound()
        {
            var qsa = document.QuerySelectorAll(".testClass + p");
            Assert.AreEqual(2, qsa.Length);
        }
    }
}
