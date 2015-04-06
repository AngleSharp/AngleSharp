using System;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests22.dat,
    /// tree-construction/tests23.dat
    /// </summary>
    [TestFixture]
    public class HtmlFormattingTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void FormattingEightFontTagsWithParagraph()
        {
            var doc = Html(@"<p><font size=4><font color=red><font size=4><font size=4><font size=4><font size=4><font size=4><font color=red><p>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.GetAttribute("size"));
            Assert.AreEqual("4", dochtml0body1p0font0.GetAttribute("size"));

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.GetAttribute("color"));
            Assert.AreEqual("red", dochtml0body1p0font0font0.GetAttribute("color"));

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);

            var attr1 = dochtml0body1p0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("size", attr1.Name);
            Assert.AreEqual("4", attr1.Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);

            var attr2 = dochtml0body1p0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("size", attr2.Name);
            Assert.AreEqual("4", attr2.Value);

            var dochtml0body1p0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0.NodeType);

            var attr3 = dochtml0body1p0font0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr3);
            Assert.AreEqual("size", attr3.Name);
            Assert.AreEqual("4", attr3.Value);

            var dochtml0body1p0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0.NodeType);

            var attr4 = dochtml0body1p0font0font0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr4);
            Assert.AreEqual("size", attr4.Name);
            Assert.AreEqual("4", attr4.Value);

            var dochtml0body1p0font0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0font0.NodeType);

            var attr5 = dochtml0body1p0font0font0font0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr5);
            Assert.AreEqual("size", attr5.Name);
            Assert.AreEqual("4", attr5.Value);

            var dochtml0body1p0font0font0font0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0font0font0font0.NodeType);

            var attr6 = dochtml0body1p0font0font0font0font0font0font0font0font0.Attributes.Get("color");
            Assert.IsNotNull(attr6);
            Assert.AreEqual("color", attr6.Name);
            Assert.AreEqual("red", attr6.Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var attr7 = dochtml0body1p1font0.Attributes.Get("color");
            Assert.IsNotNull(attr7);
            Assert.AreEqual("color", attr7.Name);
            Assert.AreEqual("red", attr7.Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);

            var attr8 = dochtml0body1p1font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr8);
            Assert.AreEqual("size", attr8.Name);
            Assert.AreEqual("4", attr8.Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);

            var attr9 = dochtml0body1p1font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr9);
            Assert.AreEqual("size", attr9.Name);
            Assert.AreEqual("4", attr9.Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);

            var attr10 = dochtml0body1p1font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr10);
            Assert.AreEqual("size", attr10.Name);
            Assert.AreEqual("4", attr10.Value);

            var dochtml0body1p1font0font0font0font0font0 = dochtml0body1p1font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0font0.NodeType);

            var attr11 = dochtml0body1p1font0font0font0font0font0.Attributes.Get("color");
            Assert.IsNotNull(attr11);
            Assert.AreEqual("color", attr11.Name);
            Assert.AreEqual("red", attr11.Value);

            var dochtml0body1p1font0font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0font0Text0.TextContent);
        }

        [Test]
        public void FormattingThreeFontTagsWithParagraph()
        {
            var doc = Html(@"<p><font size=4><font size=4><font size=4><font size=4><p>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);

            var attr1 = dochtml0body1p0font0.Attributes.Get("size");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("size", attr1.Name);
            Assert.AreEqual("4", attr1.Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);

            var attr2 = dochtml0body1p0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("size", attr2.Name);
            Assert.AreEqual("4", attr2.Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);

            var attr3 = dochtml0body1p0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr3);
            Assert.AreEqual("size", attr3.Name);
            Assert.AreEqual("4", attr3.Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);

            var attr4 = dochtml0body1p0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr4);
            Assert.AreEqual("size", attr4.Name);
            Assert.AreEqual("4", attr4.Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var attr5 = dochtml0body1p1font0.Attributes.Get("size");
            Assert.IsNotNull(attr5);
            Assert.AreEqual("size", attr5.Name);
            Assert.AreEqual("4", attr5.Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);

            var attr6 = dochtml0body1p1font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr6);
            Assert.AreEqual("size", attr6.Name);
            Assert.AreEqual("4", attr6.Value);
            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0] as Element;

            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);

            var attr7 = dochtml0body1p1font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr7);
            Assert.AreEqual("size", attr7.Name);
            Assert.AreEqual("4", attr7.Value);

            var dochtml0body1p1font0font0font0Text0 = dochtml0body1p1font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0Text0.TextContent);
        }

        [Test]
        public void FormattingFiveFontTagsWithParagraph()
        {
            var doc = Html(@"<p><font size=4><font size=4><font size=4><font size=""5""><font size=4><p>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p0font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p0font0.Attributes.Get("size").Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p0font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0.Attributes.Get("size").Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p0font0font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0.Attributes.Get("size").Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("5", dochtml0body1p0font0font0font0font0.Attributes.Get("size").Value);

            var dochtml0body1p0font0font0font0font0font0 = dochtml0body1p0font0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p0font0font0font0font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p0font0font0font0font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p0font0font0font0font0font0.Attributes.Get("size").Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p1font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p1font0.Attributes.Get("size").Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p1font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("4", dochtml0body1p1font0font0.Attributes.Get("size").Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);
            Assert.IsNotNull(dochtml0body1p1font0font0font0.Attributes.Get("size"));
            Assert.AreEqual("size", dochtml0body1p1font0font0font0.Attributes.Get("size").Name);
            Assert.AreEqual("5", dochtml0body1p1font0font0font0.Attributes.Get("size").Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);

            var attr9 = dochtml0body1p1font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr9);
            Assert.AreEqual("size", attr9.Name);
            Assert.AreEqual("4", attr9.Value);

            var dochtml0body1p1font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0Text0.TextContent);
        }

        [Test]
        public void FormattingFourFontTagsWithParagraph()
        {
            var doc = Html(@"<p><font size=4 id=a><font size=4 id=b><font size=4><font size=4><p>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0font0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0.NodeType);

            var attr1 = dochtml0body1p0font0.Attributes.Get("id");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("id", attr1.Name);
            Assert.AreEqual("a", attr1.Value);

            var attr2 = dochtml0body1p0font0.Attributes.Get("size");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("size", attr2.Name);
            Assert.AreEqual("4", attr2.Value);

            var dochtml0body1p0font0font0 = dochtml0body1p0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0.NodeType);

            var attr3 = dochtml0body1p0font0font0.Attributes.Get("id");
            Assert.IsNotNull(attr3);
            Assert.AreEqual("id", attr3.Name);
            Assert.AreEqual("b", attr3.Value);

            var attr4 = dochtml0body1p0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr4);
            Assert.AreEqual("size", attr4.Name);
            Assert.AreEqual("4", attr4.Value);

            var dochtml0body1p0font0font0font0 = dochtml0body1p0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0.NodeType);

            var attr5 = dochtml0body1p0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr5);
            Assert.AreEqual("size", attr5.Name);
            Assert.AreEqual("4", attr5.Value);

            var dochtml0body1p0font0font0font0font0 = dochtml0body1p0font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p0font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p0font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0font0font0font0font0.NodeType);

            var attr6 = dochtml0body1p0font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr6);
            Assert.AreEqual("size", attr6.Name);
            Assert.AreEqual("4", attr6.Value);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p1font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var attr7 = dochtml0body1p1font0.Attributes.Get("id");
            Assert.IsNotNull(attr7);
            Assert.AreEqual("id", attr7.Name);
            Assert.AreEqual("a", attr7.Value);

            var attr8 = dochtml0body1p1font0.Attributes.Get("size");
            Assert.IsNotNull(attr8);
            Assert.AreEqual("size", attr8.Name);
            Assert.AreEqual("4", attr8.Value);

            var dochtml0body1p1font0font0 = dochtml0body1p1font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p1font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0.NodeType);

            var attr9 = dochtml0body1p1font0font0.Attributes.Get("id");
            Assert.IsNotNull(attr9);
            Assert.AreEqual("id", attr9.Name);
            Assert.AreEqual("b", attr9.Value);

            var attr10 = dochtml0body1p1font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr10);
            Assert.AreEqual("size", attr10.Name);
            Assert.AreEqual("4", attr10.Value);

            var dochtml0body1p1font0font0font0 = dochtml0body1p1font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0.NodeType);

            var attr11 = dochtml0body1p1font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr11);
            Assert.AreEqual("size", attr11.Name);
            Assert.AreEqual("4", attr11.Value);

            var dochtml0body1p1font0font0font0font0 = dochtml0body1p1font0font0font0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1font0font0font0font0.Attributes.Count());
            Assert.AreEqual("font", dochtml0body1p1font0font0font0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0font0font0font0.NodeType);

            var attr12 = dochtml0body1p1font0font0font0font0.Attributes.Get("size");
            Assert.IsNotNull(attr12);
            Assert.AreEqual("size", attr12.Name);
            Assert.AreEqual("4", attr12.Value);

            var dochtml0body1p1font0font0font0font0Text0 = dochtml0body1p1font0font0font0font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0font0font0font0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1font0font0font0font0Text0.TextContent);
        }

        [Test]
        public void FormattingMultipleBoldTagsWithObject()
        {
            var doc = Html(@"<p><b id=a><b id=a><b id=a><b><object><b id=a><b id=a>X</object><p>Y");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p0b0.Attributes.Get("id").Value);

            var dochtml0body1p0b0b0 = dochtml0body1p0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p0b0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0.Attributes.Get("id").Value);

            var dochtml0body1p0b0b0b0 = dochtml0body1p0b0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p0b0b0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0.Attributes.Get("id").Value);

            var dochtml0body1p0b0b0b0b0 = dochtml0body1p0b0b0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0b0b0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0.NodeType);

            var dochtml0body1p0b0b0b0b0object0 = dochtml0body1p0b0b0b0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0b0b0b0b0object0.Attributes.Count());
            Assert.AreEqual("object", dochtml0body1p0b0b0b0b0object0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0.NodeType);

            var dochtml0body1p0b0b0b0b0object0b0 = dochtml0body1p0b0b0b0b0object0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0object0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0b0object0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p0b0b0b0b0object0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0b0object0b0.Attributes.Get("id").Value);

            var dochtml0body1p0b0b0b0b0object0b0b0 = dochtml0body1p0b0b0b0b0object0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0b0b0b0b0object0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0object0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0object0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p0b0b0b0b0object0b0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p0b0b0b0b0object0b0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p0b0b0b0b0object0b0b0.Attributes.Get("id").Value);

            var dochtml0body1p0b0b0b0b0object0b0b0Text0 = dochtml0body1p0b0b0b0b0object0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0b0b0b0b0object0b0b0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p0b0b0b0b0object0b0b0Text0.TextContent);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count());
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1b0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p1b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p1b0.Attributes.Get("id").Value);

            var dochtml0body1p1b0b0 = dochtml0body1p1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p1b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p1b0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p1b0b0.Attributes.Get("id").Value);

            var dochtml0body1p1b0b0b0 = dochtml0body1p1b0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1b0b0b0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p1b0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p1b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0b0.NodeType);
            Assert.IsNotNull(dochtml0body1p1b0b0b0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1p1b0b0b0.Attributes.Get("id").Name);
            Assert.AreEqual("a", dochtml0body1p1b0b0b0.Attributes.Get("id").Value);

            var dochtml0body1p1b0b0b0b0 = dochtml0body1p1b0b0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p1b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1b0b0b0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1p1b0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0b0b0.NodeType);

            var dochtml0body1p1b0b0b0b0Text0 = dochtml0body1p1b0b0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1b0b0b0b0Text0.NodeType);
            Assert.AreEqual("Y", dochtml0body1p1b0b0b0b0Text0.TextContent);
        }

        [Test]
        public void FormattingMultipleTagsWithXInDivSurroundedByAnchor()
        {
            var doc = Html(@"<a><b><big><em><strong><div>X</a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1a0b0big0 = dochtml0body1a0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0b0big0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0.Attributes.Count());
            Assert.AreEqual("big", dochtml0body1a0b0big0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0.NodeType);

            var dochtml0body1a0b0big0em0 = dochtml0body1a0b0big0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0b0big0em0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0em0.Attributes.Count());
            Assert.AreEqual("em", dochtml0body1a0b0big0em0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0em0.NodeType);

            var dochtml0body1a0b0big0em0strong0 = dochtml0body1a0b0big0em0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0b0big0em0strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0big0em0strong0.Attributes.Count());
            Assert.AreEqual("strong", dochtml0body1a0b0big0em0strong0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0big0em0strong0.NodeType);

            var dochtml0body1big1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1big1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1.Attributes.Count());
            Assert.AreEqual("big", dochtml0body1big1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1big1.NodeType);

            var dochtml0body1big1em0 = dochtml0body1big1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1big1em0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0.Attributes.Count());
            Assert.AreEqual("em", dochtml0body1big1em0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0.NodeType);

            var dochtml0body1big1em0strong0 = dochtml0body1big1em0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1big1em0strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0.Attributes.Count());
            Assert.AreEqual("strong", dochtml0body1big1em0strong0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0.NodeType);

            var dochtml0body1big1em0strong0div0 = dochtml0body1big1em0strong0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1big1em0strong0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1big1em0strong0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0div0.NodeType);

            var dochtml0body1big1em0strong0div0a0 = dochtml0body1big1em0strong0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1big1em0strong0div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1big1em0strong0div0a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1big1em0strong0div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1big1em0strong0div0a0.NodeType);

            var dochtml0body1big1em0strong0div0a0Text0 = dochtml0body1big1em0strong0div0a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1big1em0strong0div0a0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1big1em0strong0div0a0Text0.TextContent);
        }

        [Test]
        public void FormattingEightDivsInBoldAndAnchor()
        {
            var doc = Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8>A</a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);

            Assert.IsNotNull(dochtml0body1b1div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes.Get("id").Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes.Get("id").Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0Text0.TextContent);
        }

        [Test]
        public void FormattingNineDivsInBoldAndAnchor()
        {
            var doc = Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8><div id=9>A</a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes.Get("id").Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes.Get("id").Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id").Name);
            Assert.AreEqual("9", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0Text0.TextContent);
        }

        [Test]
        public void FormattingTenDivsInBoldAndAnchor()
        {
            var doc = Html(@"<a><b><div id=1><div id=2><div id=3><div id=4><div id=5><div id=6><div id=7><div id=8><div id=9><div id=10>A</a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1div0 = dochtml0body1b1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0.NodeType);

            Assert.IsNotNull(dochtml0body1b1div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0.Attributes.Get("id").Name);
            Assert.AreEqual("1", dochtml0body1b1div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0a0 = dochtml0body1b1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0a0.NodeType);

            var dochtml0body1b1div0div1 = dochtml0body1b1div0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1.Attributes.Get("id").Name);
            Assert.AreEqual("2", dochtml0body1b1div0div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1a0 = dochtml0body1b1div0div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1a0.NodeType);

            var dochtml0body1b1div0div1div1 = dochtml0body1b1div0div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("3", dochtml0body1b1div0div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1a0 = dochtml0body1b1div0div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1 = dochtml0body1b1div0div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("4", dochtml0body1b1div0div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1a0 = dochtml0body1b1div0div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1 = dochtml0body1b1div0div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("5", dochtml0body1b1div0div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("6", dochtml0body1b1div0div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("7", dochtml0body1b1div0div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1 = dochtml0body1b1div0div1div1div1div1div1div1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Name);
            Assert.AreEqual("8", dochtml0body1b1div0div1div1div1div1div1div1div1.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0 = dochtml0body1b1div0div1div1div1div1div1div1div1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1b1div0div1div1div1div1div1div1div1a0.Attributes.Count());
            Assert.AreEqual("a", dochtml0body1b1div0div1div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id").Name);
            Assert.AreEqual("9", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.NodeType);
            Assert.IsNotNull(dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes.Get("id"));
            Assert.AreEqual("id", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes.Get("id").Name);
            Assert.AreEqual("10", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.Attributes.Get("id").Value);

            var dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0 = dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b1div0div1div1div1div1div1div1div1a0div0div0Text0.TextContent);
        }

        [Test]
        public void FormattingCiteBoldCiteItalicCiteItalicCiteItalicDivWithText()
        {
            var doc = Html(@"<cite><b><cite><i><cite><i><cite><i><div>X</b>TEST");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count());
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count());
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count());
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1cite0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0.Attributes.Count());
            Assert.AreEqual("cite", dochtml0body1cite0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0.NodeType);

            var dochtml0body1cite0b0 = dochtml0body1cite0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1cite0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0.NodeType);

            var dochtml0body1cite0b0cite0 = dochtml0body1cite0b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0.Attributes.Count());
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0 = dochtml0body1cite0b0cite0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0.Attributes.Count());
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0 = dochtml0body1cite0b0cite0i0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0.Attributes.Count());
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0i0cite0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0 = dochtml0body1cite0b0cite0i0cite0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0.Attributes.Count());
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0cite0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0cite0 = dochtml0body1cite0b0cite0i0cite0i0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0b0cite0i0cite0i0cite0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0.Attributes.Count());
            Assert.AreEqual("cite", dochtml0body1cite0b0cite0i0cite0i0cite0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0cite0.NodeType);

            var dochtml0body1cite0b0cite0i0cite0i0cite0i0 = dochtml0body1cite0b0cite0i0cite0i0cite0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0b0cite0i0cite0i0cite0i0.Attributes.Count());
            Assert.AreEqual("i", dochtml0body1cite0b0cite0i0cite0i0cite0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0b0cite0i0cite0i0cite0i0.NodeType);

            var dochtml0body1cite0i1 = dochtml0body1cite0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1cite0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1.Attributes.Count());
            Assert.AreEqual("i", dochtml0body1cite0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1.NodeType);

            var dochtml0body1cite0i1i0 = dochtml0body1cite0i1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0i1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0.Attributes.Count());
            Assert.AreEqual("i", dochtml0body1cite0i1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1i0.NodeType);

            var dochtml0body1cite0i1i0div0 = dochtml0body1cite0i1i0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1cite0i1i0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0div0.Attributes.Count());
            Assert.AreEqual("div", dochtml0body1cite0i1i0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1cite0i1i0div0.NodeType);

            var dochtml0body1cite0i1i0div0b0 = dochtml0body1cite0i1i0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1cite0i1i0div0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1cite0i1i0div0b0.Attributes.Count());
            Assert.AreEqual("b", dochtml0body1cite0i1i0div0b0.GetTagName());
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
