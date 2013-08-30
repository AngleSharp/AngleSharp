using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class HtmlFormatting
    {
        [TestMethod]
        public void FormattingEightFontTagsWithParagraph()
        {
            var doc = DocumentBuilder.Html(@"<p><font size=4><font color=red><font size=4><font size=4><font size=4><font size=4><font size=4><font color=red><p>X");

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes["color"]);
            Assert.AreEqual("color", dochtml0body1p0font0font0.Attributes["color"].Name);
            Assert.AreEqual("red", dochtml0body1p0font0font0.Attributes["color"].Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes["color"]);
            Assert.AreEqual("color", dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes["color"].Name);
            Assert.AreEqual("red", dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes["color"].Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes["color"]);
            Assert.AreEqual("color", dochtml0body1p1font0.Attributes["color"].Name);
            Assert.AreEqual("red", dochtml0body1p1font0.Attributes["color"].Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0font0 = dochtml0body1p1font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0font0font0.Attributes["color"]);
            Assert.AreEqual("color", dochtml0body1p1font0font0font0font0font0.Attributes["color"].Name);
            Assert.AreEqual("red", dochtml0body1p1font0font0font0font0font0.Attributes["color"].Value);

            var dochtml0body1p1font0font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0font0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingThreeFontTagsWithParagraph()
        {
            var doc = DocumentBuilder.Html(@"<p><font size=4><font size=4><font size=4><font size=4><p>X");

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0.Attributes["size"].Value);
            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0];

            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0Text0 = dochtml0body1p1font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingFiveFontTagsWithParagraph()
        {
            var doc = DocumentBuilder.Html(@"<p><font size=4><font size=4><font size=4><font size=""5""><font size=4><p>X");

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("5", dochtml0body1p0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("5", dochtml0body1p1font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingFourFontTagsWithParagraph()
        {
            var doc = DocumentBuilder.Html(@"<p><font size=4 id=a><font size=4 id=b><font size=4><font size=4><p>X");

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0font0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0font0.Attributes["id"].Value);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0font0font0.Attributes["id"].Name);
            Assert.AreEqual("b", dochtml0body1p0font0font0.Attributes["id"].Value);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p1font0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p1font0.Attributes["id"].Value);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p1font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p1font0font0.Attributes["id"].Name);
            Assert.AreEqual("b", dochtml0body1p1font0font0.Attributes["id"].Value);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0font0.Attributes["size"]);
            Assert.AreEqual("size", dochtml0body1p1font0font0font0font0.Attributes["size"].Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0font0font0.Attributes["size"].Value);

            var dochtml0body1p1font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingMultipleBoldTagsWithObject()
        {
            var doc = DocumentBuilder.Html(@"<p><b id=a><b id=a><b id=a><b><object><b id=a><b id=a>X</object><p>Y");

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0b0.Attributes["id"].Value);

            var dochtml0body1p0b0b0 = dochtml0body1p0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0b0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0.Attributes["id"].Value);

            var dochtml0body1p0b0b0b0 = dochtml0body1p0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0b0b0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0.Attributes["id"].Value);

            var dochtml0body1p0b0b0b0b0 = dochtml0body1p0b0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0b0b0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0.NodeType);

            var dochtml0body1p0b0b0b0b0object0 = dochtml0body1p0b0b0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0b0b0b0b0object0.Attributes.Length);
            Assert.AreEqual("object", dochtml0body1p0b0b0b0b0object0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0.NodeType);

            var dochtml0body1p0b0b0b0b0object0b0 = dochtml0body1p0b0b0b0b0object0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0object0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0b0object0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0b0b0b0b0object0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0b0object0b0.Attributes["id"].Value);

            var dochtml0body1p0b0b0b0b0object0b0b0 = dochtml0body1p0b0b0b0b0object0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0object0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0b0object0b0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p0b0b0b0b0object0b0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0b0object0b0b0.Attributes["id"].Value);

            var dochtml0body1p0b0b0b0b0object0b0b0Text0 = dochtml0body1p0b0b0b0b0object0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0b0b0b0b0object0b0b0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p0b0b0b0b0object0b0b0Text0.TextContent);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1b0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p1b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p1b0.Attributes["id"].Value);

            var dochtml0body1p1b0b0 = dochtml0body1p1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p1b0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p1b0b0.Attributes["id"].Value);

            var dochtml0body1p1b0b0b0 = dochtml0body1p1b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0b0b0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1p1b0b0b0.Attributes["id"].Name);
            Assert.AreEqual("a", dochtml0body1p1b0b0b0.Attributes["id"].Value);

            var dochtml0body1p1b0b0b0b0 = dochtml0body1p1b0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1b0b0b0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0b0b0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0b0b0.NodeType);

            var dochtml0body1p1b0b0b0b0Text0 = dochtml0body1p1b0b0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1b0b0b0b0Text0.NodeType);
            Assert.AreEqual("Y", dochtml0body1p1b0b0b0b0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingMultipleTagsWithXInDivSurroundedByAnchor()
        {
            var doc = DocumentBuilder.Html(@"<a><b><big><em><strong><div>X</a>");

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1a0b0big0 = dochtml0body1a0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0b0big0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0.Attributes.Length);
            Assert.AreEqual("big", dochtml0body1a0b0big0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0.NodeType);

            var dochtml0body1a0b0big0em0 = dochtml0body1a0b0big0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0b0big0em0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0em0.Attributes.Length);
            Assert.AreEqual("em", dochtml0body1a0b0big0em0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0em0.NodeType);

            var dochtml0body1a0b0big0em0strong0 = dochtml0body1a0b0big0em0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0b0big0em0strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0em0strong0.Attributes.Length);
            Assert.AreEqual("strong", dochtml0body1a0b0big0em0strong0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0em0strong0.NodeType);

            var dochtml0body1big1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1big1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1.Attributes.Length);
            Assert.AreEqual("big", dochtml0body1big1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1big1.NodeType);

            var dochtml0body1big1em0 = dochtml0body1big1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1big1em0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0.Attributes.Length);
            Assert.AreEqual("em", dochtml0body1big1em0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0.NodeType);

            var dochtml0body1big1em0strong0 = dochtml0body1big1em0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1big1em0strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0.Attributes.Length);
            Assert.AreEqual("strong", dochtml0body1big1em0strong0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0.NodeType);

            var dochtml0body1big1em0strong0div0 = dochtml0body1big1em0strong0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1big1em0strong0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1big1em0strong0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0div0.NodeType);

            var dochtml0body1big1em0strong0div0a0 = dochtml0body1big1em0strong0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1big1em0strong0div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0div0a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1big1em0strong0div0a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0div0a0.NodeType);

            var dochtml0body1big1em0strong0div0a0Text0 = dochtml0body1big1em0strong0div0a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1big1em0strong0div0a0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1big1em0strong0div0a0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingEightDivsInBoldAndAnchor()
        {
            var doc = DocumentBuilder.Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8>A</a>");

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);

            Assert.IsNotNull(dochtml0body1b1div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes["id"].Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes["id"].Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes["id"].Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes["id"].Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingNineDivsInBoldAndAnchor()
        {
            var doc = DocumentBuilder.Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8><div id=9>A</a>");

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes["id"].Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes["id"].Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes["id"].Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes["id"].Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"].Name);
            Assert.AreEqual("9", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingTenDivsInBoldAndAnchor()
        {
            var doc = DocumentBuilder.Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8><div id=9><div id=10>A</a>");

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);

            Assert.IsNotNull(dochtml0body1b1div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes["id"].Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes["id"].Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes["id"].Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes["id"].Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"].Name);
            Assert.AreEqual("9", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes["id"]);
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes["id"].Name);
            Assert.AreEqual("10", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes["id"].Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0.TextContent);
        }

        [TestMethod]
        public void FormattingCiteBoldCiteItalicCiteItalicCiteItalicDivWithText()
        {
            var doc = DocumentBuilder.Html(@"<cite><b><cite><i><cite><i><cite><i><div>X</b>TEST");

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

            var dochtml0body1cite0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0.Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1cite0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0.NodeType);

            var dochtml0body1cite0b0 = dochtml0body1cite0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1cite0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0.NodeType);

            var dochtml0body1cite0b0cite0 = dochtml0body1cite0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0.Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0 = dochtml0body1cite0b0cite0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0 = dochtml0body1cite0b0cite0i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0.Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0i0cite0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0 = dochtml0body1cite0b0cite0i0cite0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0cite0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0cite0 = dochtml0body1cite0b0cite0i0cite0i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0i0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0.Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0i0cite0i0cite0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0cite0i0 = dochtml0body1cite0b0cite0i0cite0i0cite0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0cite0i0cite0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0cite0i0.NodeType);

            var dochtml0body1cite0i1 = dochtml0body1cite0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1cite0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml0body1cite0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1.NodeType);

            var dochtml0body1cite0i1i0 = dochtml0body1cite0i1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0i1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml0body1cite0i1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1i0.NodeType);

            var dochtml0body1cite0i1i0div0 = dochtml0body1cite0i1i0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1cite0i1i0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1cite0i1i0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1i0div0.NodeType);

            var dochtml0body1cite0i1i0div0b0 = dochtml0body1cite0i1i0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1cite0i1i0div0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0div0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1cite0i1i0div0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1i0div0b0.NodeType);

            var dochtml0body1cite0i1i0div0b0Text0 = dochtml0body1cite0i1i0div0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1cite0i1i0div0b0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1cite0i1i0div0b0Text0.TextContent);

            var dochtml0body1cite0i1i0div0Text1 = dochtml0body1cite0i1i0div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1cite0i1i0div0Text1.NodeType);
            Assert.AreEqual("TEST", dochtml0body1cite0i1i0div0Text1.TextContent);
 

        }
    }
}
