using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/tests_innerHTML_1.dat
    /// </summary>
    [TestClass]
    public class HtmlFragments
    {
        [TestMethod]
        public void FragmentBodyContextDoubleBodyAndSpanElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<body><span>", HTMLElement.Factory("body"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [TestMethod]
        public void FragmentBodyContextSpanAndDoubleBodyElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<span><body>", HTMLElement.Factory("body"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [TestMethod]
        public void FragmentDivContextSpanAndDoubleBodyElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<span><body>", HTMLElement.Factory("div"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [TestMethod]
        public void FragmentHtmlContextBodyAndSpanElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<body><span>", HTMLElement.Factory("html"));

            var dochead0 = doc[0];
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.NodeName);
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1];
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.NodeName);
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1span0 = docbody1.ChildNodes[0];
            Assert.AreEqual(0, docbody1span0.ChildNodes.Length);
            Assert.AreEqual(0, docbody1span0.Attributes.Length);
            Assert.AreEqual("span", docbody1span0.NodeName);
            Assert.AreEqual(NodeType.Element, docbody1span0.NodeType);
        }

        [TestMethod]
        public void FragmentBodyContextFramesetAndSpanElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<frameset><span>", HTMLElement.Factory("body"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);

        }

        [TestMethod]
        public void FragmentBodyContextSpanAndFramesetElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<span><frameset>", HTMLElement.Factory("body"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [TestMethod]
        public void FragmentDivContextSpanAndFramesetElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<span><frameset>", HTMLElement.Factory("div"));

            var docspan0 = doc[0];
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.NodeName);
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [TestMethod]
        public void FragmentHtmlContextEmpty()
        {
            var doc = DocumentBuilder.HtmlFragment(@"", HTMLElement.Factory("html"));
            var dochead0 = doc[0];
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.NodeName);
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1];
            Assert.AreEqual(0, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.NodeName);
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);
        }

        [TestMethod]
        public void FragmentHtmlContextFramesetAndSpanElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<frameset><span>", HTMLElement.Factory("html"));

            var dochead0 = doc[0];
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.NodeName);
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docframeset1 = doc[1];
            Assert.AreEqual(0, docframeset1.ChildNodes.Length);
            Assert.AreEqual(0, docframeset1.Attributes.Length);
            Assert.AreEqual("frameset", docframeset1.NodeName);
            Assert.AreEqual(NodeType.Element, docframeset1.NodeType);
        }

        [TestMethod]
        public void FragmentTableContextOpeningTableAndTrElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"<table><tr>", HTMLElement.Factory("table"));

            var doctbody0 = doc[0];
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctbody0.NodeName);
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0];
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [TestMethod]
        public void FragmentTableContextClosingTableAndTrElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"</table><tr>", HTMLElement.Factory("table"));

            var doctbody0 = doc[0];
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctbody0.NodeName);
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0];
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [TestMethod]
        public void FragmentFramesetContextClosingFramesetAndFrameElement()
        {
            var doc = DocumentBuilder.HtmlFragment(@"</frameset><frame>", HTMLElement.Factory("frameset"));

            var docframe0 = doc[0];
            Assert.AreEqual(0, docframe0.ChildNodes.Length);
            Assert.AreEqual(0, docframe0.Attributes.Length);
            Assert.AreEqual("frame", docframe0.NodeName);
            Assert.AreEqual(NodeType.Element, docframe0.NodeType);
        }
    }
}
