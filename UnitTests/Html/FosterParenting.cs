using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;

namespace UnitTests
{
    [TestClass]
    public class FosterParentingTests
    {
        [TestMethod]
        public void FosterDivInDivMisclosedSpan()
        {
            var doc = DocumentBuilder.Html(@"<div>
<div></div>
</span>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("\nx", dochtml0body1div0Text2.TextContent);
        }

        [TestMethod]
        public void FosterTextAndDivInDivMisclosedSpan()
        {
            var doc = DocumentBuilder.Html(@"<div>x<div></div>
</span>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("\nx", dochtml0body1div0Text2.TextContent);
        }

        [TestMethod]
        public void FosterTextAndDivInDivMisclosedSpanWithText()
        {
            var doc = DocumentBuilder.Html(@"<div>x<div></div>x</span>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("xx", dochtml0body1div0Text2.TextContent);
        }

        [TestMethod]
        public void FosterTextAndDivInDivWithTextMisclosedSpan()
        {
            var doc = DocumentBuilder.Html(@"<div>x<div></div>y</span>z");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("yz", dochtml0body1div0Text2.TextContent);
        }

        [TestMethod]
        public void FosterDivAndTextInDivAndTable()
        {
            var doc = DocumentBuilder.Html(@"<table><div>x<div></div>x</span>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("xx", dochtml0body1div0Text2.TextContent);


            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void FosterTextInTable()
        {
            var doc = DocumentBuilder.Html(@"x<table>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void FosterTableInTable()
        {
            var doc = DocumentBuilder.Html(@"x<table><table>x");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("x", dochtml0body1Text2.TextContent);

            var dochtml0body1table3 = dochtml0body1.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml0body1table3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table3.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table3.NodeType);
        }

        [TestMethod]
        public void FosterDivsInBoldFormatting()
        {
            var doc = DocumentBuilder.Html(@"<b>a<div></div><div></b>y");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0div1 = dochtml0body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1b0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1b0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b0div1.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1div1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1Text1 = dochtml0body1div1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1Text1.NodeType);
            Assert.AreEqual("y", dochtml0body1div1Text1.TextContent);
        }

        [TestMethod]
        public void FosterParagraphAndDivInAnchor()
        {
            var doc = DocumentBuilder.Html(@"<a><div><p></a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1a0 = dochtml0body1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1a0.NodeType);

            var dochtml0body1div1p1 = dochtml0body1div1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1div1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1div1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1p1.NodeType);

            var dochtml0body1div1p1a0 = dochtml0body1div1p1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1p1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1div1p1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1p1a0.NodeType);
        }

        [TestMethod]
        public void FosterTitleInBody()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><body><title>X</title>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1title0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1title0.Attributes.Count);
            Assert.AreEqual("title", dochtml1body1title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1title0.NodeType);

            var dochtml1body1title0Text0 = dochtml1body1title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1title0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1title0Text0.TextContent);
        }

        [TestMethod]
        public void FosterTitleInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><title>X</title></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1title0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1title0.Attributes.Count);
            Assert.AreEqual("title", dochtml1body1title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1title0.NodeType);

            var dochtml1body1title0Text0 = dochtml1body1title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1title0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1title0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void FosterTitleOutsideHead()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><head></head><title>X</title>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0title0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml1head0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0title0.NodeType);

            var dochtml1head0title0Text0 = dochtml1head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0title0Text0.NodeType);
            Assert.AreEqual("X", dochtml1head0title0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void FosterMisclosedHeadAndTitle()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html></head><title>X</title>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0title0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml1head0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0title0.NodeType);

            var dochtml1head0title0Text0 = dochtml1head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0title0Text0.NodeType);
            Assert.AreEqual("X", dochtml1head0title0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void FosterMetaInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><meta></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1meta0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1meta0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1meta0.Attributes.Count);
            Assert.AreEqual("meta", dochtml1body1meta0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1meta0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void FosterMetaInTableInTableCell()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table>X<tr><td><table> <meta></table></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);

            var dochtml1body1table1tbody0tr0td0 = dochtml1body1table1tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table1tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0td0.NodeType);

            var dochtml1body1table1tbody0tr0td0meta0 = dochtml1body1table1tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0td0meta0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0td0meta0.Attributes.Count);
            Assert.AreEqual("meta", dochtml1body1table1tbody0tr0td0meta0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0td0meta0.NodeType);

            var dochtml1body1table1tbody0tr0td0table1 = dochtml1body1table1tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0td0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0td0table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1tbody0tr0td0table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0td0table1.NodeType);

            var dochtml1body1table1tbody0tr0td0table1Text0 = dochtml1body1table1tbody0tr0td0table1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table1tbody0tr0td0table1Text0.NodeType);
            Assert.AreEqual(" ", dochtml1body1table1tbody0tr0td0table1Text0.TextContent);
        }

        [TestMethod]
        public void FosterSpaceBeforeHead()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><html> <head>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void FosterSpaceBeforeHtml()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html> <head>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void FosterStyleInTableWithSpaces()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><style> <tr>x </style> </table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0style0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml1body1table0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0style0.NodeType);

            var dochtml1body1table0style0Text0 = dochtml1body1table0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0style0Text0.NodeType);
            Assert.AreEqual(" <tr>x ", dochtml1body1table0style0Text0.TextContent);

            var dochtml1body1table0Text1 = dochtml1body1table0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0Text1.NodeType);
            Assert.AreEqual(" ", dochtml1body1table0Text1.TextContent);
        }

        [TestMethod]
        public void FosterScriptInTbodyInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><TBODY><script> <tr>x </script> </table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0script0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1body1table0tbody0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0script0.NodeType);

            var dochtml1body1table0tbody0script0Text0 = dochtml1body1table0tbody0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0script0Text0.NodeType);
            Assert.AreEqual(" <tr>x ", dochtml1body1table0tbody0script0Text0.TextContent);

            var dochtml1body1table0tbody0Text1 = dochtml1body1table0tbody0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0Text1.NodeType);
            Assert.AreEqual(" ", dochtml1body1table0tbody0Text1.TextContent);
        }

        [TestMethod]
        public void FosterAppletInParagraph()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><p><applet><p>X</p></applet>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0applet0 = dochtml1body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0applet0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0applet0.Attributes.Count);
            Assert.AreEqual("applet", dochtml1body1p0applet0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p0applet0.NodeType);

            var dochtml1body1p0applet0p0 = dochtml1body1p0applet0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0applet0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0applet0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0applet0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p0applet0p0.NodeType);

            var dochtml1body1p0applet0p0Text0 = dochtml1body1p0applet0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0applet0p0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1p0applet0p0Text0.TextContent);
        }

        [TestMethod]
        public void FosterListingBeforeHtml()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><listing>
X</listing>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1listing0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1listing0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1listing0.Attributes.Count);
            Assert.AreEqual("listing", dochtml1body1listing0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1listing0.NodeType);

            var dochtml1body1listing0Text0 = dochtml1body1listing0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1listing0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1listing0Text0.TextContent);
        }

        [TestMethod]
        public void FosterSelectWithMisnestedInput()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><select><input>X");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1input1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1input1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1input1.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1input1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1input1.NodeType);

            var dochtml1body1Text2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text2.NodeType);
            Assert.AreEqual("X", dochtml1body1Text2.TextContent);
        }

        [TestMethod]
        public void FosterSelectInSelect()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><select><select>X");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("X", dochtml1body1Text1.TextContent);
        }

        [TestMethod]
        public void FosterInputInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><input type=hidDEN></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0input0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table0input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1table0input0.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1table0input0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0input0.NodeType);
            Assert.AreEqual("hidDEN", dochtml1body1table0input0.GetAttribute("type"));
        }

        [TestMethod]
        public void FosterInputAndTextInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table>X<input type=hidDEN></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1input0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1table1input0.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1table1input0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1input0.NodeType);
            Assert.AreEqual("hidDEN", dochtml1body1table1input0.GetAttribute("type"));
        }

        [TestMethod]
        public void FosterSpacesAndInputInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table>  <input type=hidDEN></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0Text0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0Text0.NodeType);
            Assert.AreEqual("  ", dochtml1body1table0Text0.TextContent);

            var dochtml1body1table0input1 = dochtml1body1table0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table0input1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1table0input1.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1table0input1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0input1.NodeType);
            Assert.AreEqual("hidDEN", dochtml1body1table0input1.GetAttribute("type"));
        }

        [TestMethod]
        public void FosterInputAndSpacesWithAttributeInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table>  <input type='hidDEN'></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0Text0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0Text0.NodeType);
            Assert.AreEqual("  ", dochtml1body1table0Text0.TextContent);

            var dochtml1body1table0input1 = dochtml1body1table0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table0input1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1table0input1.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1table0input1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0input1.NodeType);
            Assert.AreEqual("hidDEN", dochtml1body1table0input1.GetAttribute("type"));
        }

        [TestMethod]
        public void FosterTwoInputsInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><input type="" hidden""><input type=hidDEN></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1input0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1input0.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1input0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1input0.NodeType);
            Assert.AreEqual(" hidden", dochtml1body1input0.GetAttribute("type"));

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1input0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1table1input0.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1table1input0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1input0.NodeType);
            Assert.AreEqual("hidDEN", dochtml1body1table1input0.GetAttribute("type"));
        }

        [TestMethod]
        public void FosterSelectInTable()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><table><select>X<tr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1select0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [TestMethod]
        public void FosterTextInSelect()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><select>X</select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1select0Text0.TextContent);
        }

        [TestMethod]
        public void MixingUpperAndLowercaseInDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE hTmL><html></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void PureUppercaseDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE HTML><html></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void ParagraphClosedWrongInDiv()
        {
            var doc = DocumentBuilder.Html(@"<div><p>a</x> b");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0p0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1div0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p0.NodeType);

            var dochtml0body1div0p0Text0 = dochtml0body1div0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p0Text0.NodeType);
            Assert.AreEqual("a b", dochtml0body1div0p0Text0.TextContent);
        }

        [TestMethod]
        public void FosterImplicitCellClosed()
        {
            var doc = DocumentBuilder.Html(@"<table><tr><td><code></code> </table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0code0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0code0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0code0.Attributes.Count);
            Assert.AreEqual("code", dochtml0body1table0tbody0tr0td0code0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0code0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text1 = dochtml0body1table0tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0body1table0tbody0tr0td0Text1.TextContent);
        }

        [TestMethod]
        public void FosterTextInTableAfterRow()
        {
            var doc = DocumentBuilder.Html(@"<table><b><tr><td>aaa</td></tr>bbb</table>ccc");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1b1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1Text0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1Text0.NodeType);
            Assert.AreEqual("bbb", dochtml0body1b1Text0.TextContent);

            var dochtml0body1table2 = dochtml0body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml0body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table2.NodeType);

            var dochtml0body1table2tbody0 = dochtml0body1table2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table2tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table2tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0.NodeType);

            var dochtml0body1table2tbody0tr0 = dochtml0body1table2tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table2tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table2tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0tr0.NodeType);

            var dochtml0body1table2tbody0tr0td0 = dochtml0body1table2tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table2tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table2tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0tr0td0.NodeType);

            var dochtml0body1table2tbody0tr0td0Text0 = dochtml0body1table2tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table2tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("aaa", dochtml0body1table2tbody0tr0td0Text0.TextContent);

            var dochtml0body1b3 = dochtml0body1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml0body1b3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b3.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1b3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b3.NodeType);

            var dochtml0body1b3Text0 = dochtml0body1b3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b3Text0.NodeType);
            Assert.AreEqual("ccc", dochtml0body1b3Text0.TextContent);
        }

        [TestMethod]
        public void FosterSpacesAndTextAfterRow()
        {
            var doc = DocumentBuilder.Html(@"A<table><tr> B</tr> B</table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("A B B", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);
        }

        [TestMethod]
        public void FosterSpacesTextAndFormattingAfterRow()
        {
            var doc = DocumentBuilder.Html(@"A<table><tr> B</tr> </em>C</table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("A BC", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0Text1 = dochtml0body1table1tbody0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1table1tbody0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0body1table1tbody0Text1.TextContent);
        }

        [TestMethod]
        public void FosterKeygenInSelect()
        {
            var doc = DocumentBuilder.Html(@"<select><keygen>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1keygen1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1keygen1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1keygen1.Attributes.Count);
            Assert.AreEqual("keygen", dochtml0body1keygen1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1keygen1.NodeType);
        }
    }
}
