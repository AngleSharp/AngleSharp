using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class FosterParenting
    {
        [TestMethod]
        public void FosterDivInDivMisclosedSpan()
        {
            var doc = DocumentBuilder.Html(@"<div>
<div></div>
</span>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Length);
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

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Length);
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

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Length);
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

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Length);
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

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0div1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0div1.NodeType);

            var dochtml0body1div0Text2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text2.NodeType);
            Assert.AreEqual("xx", dochtml0body1div0Text2.TextContent);


            var dochtml0body1table1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void FosterTextInTable()
        {
            var doc = DocumentBuilder.Html(@"x<table>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void FosterTableInTable()
        {
            var doc = DocumentBuilder.Html(@"x<table><table>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("x", dochtml0body1Text2.TextContent);

            var dochtml0body1table3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(0, dochtml0body1table3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table3.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table3.NodeType);
        }

        [TestMethod]
        public void FosterDivsInBoldFormatting()
        {
            var doc = DocumentBuilder.Html(@"<b>a<div></div><div></b>y");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0div1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1b0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b0div1.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0.Attributes.Length);
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

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1a0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1a0.NodeType);

            var dochtml0body1div1p1 = dochtml0body1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1p1.NodeType);

            var dochtml0body1div1p1a0 = dochtml0body1div1p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1p1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1p1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1p1a0.NodeType); 
        }
    }
}
