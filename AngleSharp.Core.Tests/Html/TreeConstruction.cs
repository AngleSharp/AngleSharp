using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests12.dat,
    /// tree-construction/tests17.dat,
    /// tree-construction/tests24.dat,
    /// tree-construction/tests25.dat,
    /// tree-construction/tests26.dat
    /// </summary>
    [TestFixture]
    public class TreeConstructionTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ForeignElementsMathWithSvgAndImage()
        {
            var doc = Html(@"<!DOCTYPE html><body><p>foo<math><mtext><i>baz</i></mtext><annotation-xml><svg><desc><b>eggs</b></desc><g><foreignObject><P>spam<TABLE><tr><td><img></td></table></foreignObject></g><g>quux</g></svg></annotation-xml></math>bar");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0Text0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1p0Text0.TextContent);

            var dochtml1body1p0math1 = dochtml1body1p0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1p0math1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1.NodeType);

            var dochtml1body1p0math1mtext0 = dochtml1body1p0math1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1mtext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1mtext0).Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1p0math1mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1mtext0.NodeType);

            var dochtml1body1p0math1mtext0i0 = dochtml1body1p0math1mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1mtext0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1mtext0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1p0math1mtext0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1mtext0i0.NodeType);

            var dochtml1body1p0math1mtext0i0Text0 = dochtml1body1p0math1mtext0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0math1mtext0i0Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p0math1mtext0i0Text0.TextContent);

            var dochtml1body1p0math1annotationxml1 = dochtml1body1p0math1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml1body1p0math1annotationxml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1.NodeType);

            var dochtml1body1p0math1annotationxml1svg0 = dochtml1body1p0math1annotationxml1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1p0math1annotationxml1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1p0math1annotationxml1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0desc0 = dochtml1body1p0math1annotationxml1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0desc0).Attributes.Count);
            Assert.AreEqual("desc", dochtml1body1p0math1annotationxml1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0desc0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0desc0b0 = dochtml1body1p0math1annotationxml1svg0desc0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0desc0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0desc0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1p0math1annotationxml1svg0desc0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0desc0b0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0desc0b0Text0 = dochtml1body1p0math1annotationxml1svg0desc0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0math1annotationxml1svg0desc0b0Text0.NodeType);
            Assert.AreEqual("eggs", dochtml1body1p0math1annotationxml1svg0desc0b0Text0.TextContent);

            var dochtml1body1p0math1annotationxml1svg0g1 = dochtml1body1p0math1annotationxml1svg0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1).Attributes.Count);
            Assert.AreEqual("g", dochtml1body1p0math1annotationxml1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0 = dochtml1body1p0math1annotationxml1svg0g1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math1annotationxml1svg0g1foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1p0math1annotationxml1svg0g1foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0Text0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0Text0.NodeType);
            Assert.AreEqual("spam", dochtml1body1p0math1annotationxml1svg0g1foreignObject0p0Text0.TextContent);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0 = dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0).Attributes.Count);
            Assert.AreEqual("img", dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g2 = dochtml1body1p0math1annotationxml1svg0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1p0math1annotationxml1svg0g2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math1annotationxml1svg0g2).Attributes.Count);
            Assert.AreEqual("g", dochtml1body1p0math1annotationxml1svg0g2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math1annotationxml1svg0g2.NodeType);

            var dochtml1body1p0math1annotationxml1svg0g2Text0 = dochtml1body1p0math1annotationxml1svg0g2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0math1annotationxml1svg0g2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p0math1annotationxml1svg0g2Text0.TextContent);

            var dochtml1body1p0Text2 = dochtml1body1p0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0Text2.NodeType);
            Assert.AreEqual("bar", dochtml1body1p0Text2.TextContent);

        }

        [Test]
        public void ForeignElementsMathWithSvgAndTable()
        {
            var doc = Html(@"<!DOCTYPE html><body>foo<math><mtext><i>baz</i></mtext><annotation-xml><svg><desc><b>eggs</b></desc><g><foreignObject><P>spam<TABLE><tr><td><img></td></table></foreignObject></g><g>quux</g></svg></annotation-xml></math>bar");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1Text0.TextContent);

            var dochtml1body1math1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1math1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1.NodeType);

            var dochtml1body1math1mtext0 = dochtml1body1math1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1mtext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1mtext0).Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1math1mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1mtext0.NodeType);

            var dochtml1body1math1mtext0i0 = dochtml1body1math1mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1mtext0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1mtext0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1math1mtext0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1mtext0i0.NodeType);

            var dochtml1body1math1mtext0i0Text0 = dochtml1body1math1mtext0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math1mtext0i0Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1math1mtext0i0Text0.TextContent);

            var dochtml1body1math1annotationxml1 = dochtml1body1math1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml1body1math1annotationxml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1.NodeType);

            var dochtml1body1math1annotationxml1svg0 = dochtml1body1math1annotationxml1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1math1annotationxml1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1math1annotationxml1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0.NodeType);

            var dochtml1body1math1annotationxml1svg0desc0 = dochtml1body1math1annotationxml1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0desc0).Attributes.Count);
            Assert.AreEqual("desc", dochtml1body1math1annotationxml1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0desc0.NodeType);

            var dochtml1body1math1annotationxml1svg0desc0b0 = dochtml1body1math1annotationxml1svg0desc0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0desc0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0desc0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1math1annotationxml1svg0desc0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0desc0b0.NodeType);

            var dochtml1body1math1annotationxml1svg0desc0b0Text0 = dochtml1body1math1annotationxml1svg0desc0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math1annotationxml1svg0desc0b0Text0.NodeType);
            Assert.AreEqual("eggs", dochtml1body1math1annotationxml1svg0desc0b0Text0.TextContent);

            var dochtml1body1math1annotationxml1svg0g1 = dochtml1body1math1annotationxml1svg0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1).Attributes.Count);
            Assert.AreEqual("g", dochtml1body1math1annotationxml1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0 = dochtml1body1math1annotationxml1svg0g1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math1annotationxml1svg0g1foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1math1annotationxml1svg0g1foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0p0 = dochtml1body1math1annotationxml1svg0g1foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1math1annotationxml1svg0g1foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0p0.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0p0Text0 = dochtml1body1math1annotationxml1svg0g1foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math1annotationxml1svg0g1foreignObject0p0Text0.NodeType);
            Assert.AreEqual("spam", dochtml1body1math1annotationxml1svg0g1foreignObject0p0Text0.TextContent);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0table1 = dochtml1body1math1annotationxml1svg0g1foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1foreignObject0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1math1annotationxml1svg0g1foreignObject0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0table1.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0 = dochtml1body1math1annotationxml1svg0g1foreignObject0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0 = dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0 = dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.NodeType);

            var dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0 = dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0).Attributes.Count);
            Assert.AreEqual("img", dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g1foreignObject0table1tbody0tr0td0img0.NodeType);

            var dochtml1body1math1annotationxml1svg0g2 = dochtml1body1math1annotationxml1svg0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1math1annotationxml1svg0g2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math1annotationxml1svg0g2).Attributes.Count);
            Assert.AreEqual("g", dochtml1body1math1annotationxml1svg0g2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math1annotationxml1svg0g2.NodeType);

            var dochtml1body1math1annotationxml1svg0g2Text0 = dochtml1body1math1annotationxml1svg0g2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math1annotationxml1svg0g2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1math1annotationxml1svg0g2Text0.TextContent);

            var dochtml1body1Text2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text2.NodeType);
            Assert.AreEqual("bar", dochtml1body1Text2.TextContent);

        }

        [Test]
        public void SelectElementInTableBodyUsesRightState()
        {
            var doc = Html(@"<!doctype html><table><tbody><select><tr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);

        }

        [Test]
        public void SelectElementInTableRowBeforeTableCell()
        {
            var doc = Html(@"<!doctype html><table><tr><select><td>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);

            var dochtml1body1table1tbody0tr0td0 = dochtml1body1table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0td0.NodeType);

        }

        [Test]
        public void SelectElementBetweenTableCellsInTable()
        {
            var doc = Html(@"<!doctype html><table><tr><td><select><td>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0td0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0select0.NodeType);

            var dochtml1body1table0tbody0tr0td1 = dochtml1body1table0tbody0tr0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td1).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td1.NodeType);

        }

        [Test]
        public void SelectElementBetweenTableHeaderAndTableCellInTable()
        {
            var doc = Html(@"<!doctype html><table><tr><th><select><td>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0th0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0th0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0th0).Attributes.Count);
            Assert.AreEqual("th", dochtml1body1table0tbody0tr0th0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0th0.NodeType);

            var dochtml1body1table0tbody0tr0th0select0 = dochtml1body1table0tbody0tr0th0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0th0select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0th0select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0th0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0th0select0.NodeType);

            var dochtml1body1table0tbody0tr0td1 = dochtml1body1table0tbody0tr0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td1).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td1.NodeType);

        }

        [Test]
        public void SelectElementInTableAfterTableCapationBeforeTableRow()
        {
            var doc = Html(@"<!doctype html><table><caption><select><tr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0).Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0select0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0caption0select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1table0caption0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0select0.NodeType);

            var dochtml1body1table0tbody1 = dochtml1body1table0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody1).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody1.NodeType);

            var dochtml1body1table0tbody1tr0 = dochtml1body1table0tbody1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody1tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody1tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody1tr0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableRowWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><tr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableCellWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><td>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableHeaderWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><th>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableBodyWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><tbody>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableHeadWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><thead>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableFootWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><tfoot>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void SelectElementBeforePlainTableCaptionWithoutTableElement()
        {
            var doc = Html(@"<!doctype html><select><caption>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void TableElementWithASingleNonClosedRowFollowedByACharacter()
        {
            var doc = Html(@"<!doctype html><table><tr></table>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceNotEqualTilde()
        {
            var doc = Html(@"<!DOCTYPE html>&NotEqualTilde;");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("≂̸", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceNotEqualTildeFollowedByACharacter()
        {
            var doc = Html(@"<!DOCTYPE html>&NotEqualTilde;A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("≂̸A", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceThickSpace()
        {
            var doc = Html(@"<!DOCTYPE html>&ThickSpace;");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("  ", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceThickSpaceFollowedByACharacter()
        {
            var doc = Html(@"<!DOCTYPE html>&ThickSpace;A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("  A", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceNotSubset()
        {
            var doc = Html(@"<!DOCTYPE html>&NotSubset;");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("⊂⃒", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceNotSubsetFollowedByACharacter()
        {
            var doc = Html(@"<!DOCTYPE html>&NotSubset;A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("⊂⃒A", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceGopf()
        {
            var doc = Html(@"<!DOCTYPE html>&Gopf;");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("\U0001D53E", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void HtmlNamedCharacterRefenceGopfFollowedByACharacter()
        {
            var doc = Html(@"<!DOCTYPE html>&Gopf;A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("\U0001D53EA", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void BodyWithAnUnknownElementAnCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><foo>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1foo0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1foo0).Attributes.Count);
            Assert.AreEqual("foo", dochtml1body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1foo0.NodeType);

            var dochtml1body1foo0Text0 = dochtml1body1foo0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1foo0Text0.NodeType);
            Assert.AreEqual("A", dochtml1body1foo0Text0.TextContent);

        }

        [Test]
        public void BodyWithAnAreaElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><area>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1area0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1area0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1area0).Attributes.Count);
            Assert.AreEqual("area", dochtml1body1area0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1area0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithTheBaseElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><base>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1base0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1base0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1base0).Attributes.Count);
            Assert.AreEqual("base", dochtml1body1base0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1base0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithTheBasefontElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><basefont>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1basefont0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1basefont0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1basefont0).Attributes.Count);
            Assert.AreEqual("basefont", dochtml1body1basefont0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1basefont0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithTheBgSoundElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><bgsound>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1bgsound0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1bgsound0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1bgsound0).Attributes.Count);
            Assert.AreEqual("bgsound", dochtml1body1bgsound0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1bgsound0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithABreakRowElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><br>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1br0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1br0).Attributes.Count);
            Assert.AreEqual("br", dochtml1body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1br0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAColumnElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><col>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("A", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void BodyWithACommandElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><command>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1command0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1command0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1command0).Attributes.Count);
            Assert.AreEqual("command", dochtml1body1command0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1command0.NodeType);

            var dochtml1body1command0Text0 = dochtml1body1command0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1command0Text0.NodeType);
            Assert.AreEqual("A", dochtml1body1command0Text0.TextContent);

        }

        [Test]
        public void BodyWithAMenuitemElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><menuitem>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1menuitem0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1menuitem0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1menuitem0).Attributes.Count);
            Assert.AreEqual("menuitem", dochtml1body1menuitem0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1menuitem0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAnEmbedElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><embed>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1embed0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1embed0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1embed0).Attributes.Count);
            Assert.AreEqual("embed", dochtml1body1embed0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1embed0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAFrameElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><frame>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("A", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void BodyWithAHorizontalLineElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><hr>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1hr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1hr0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAnImageElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><img>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1img0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1img0).Attributes.Count);
            Assert.AreEqual("img", dochtml1body1img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1img0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAnInputElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><input>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1input0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1input0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1input0).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1input0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1input0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAKeygenElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><keygen>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1keygen0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1keygen0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1keygen0).Attributes.Count);
            Assert.AreEqual("keygen", dochtml1body1keygen0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1keygen0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithALinkElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><link>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1link0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1link0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1link0).Attributes.Count);
            Assert.AreEqual("link", dochtml1body1link0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1link0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAMetaElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><meta>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1meta0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1meta0).Attributes.Count);
            Assert.AreEqual("meta", dochtml1body1meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1meta0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAParamElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><param>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1param0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1param0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1param0).Attributes.Count);
            Assert.AreEqual("param", dochtml1body1param0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1param0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithASourceElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><source>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1source0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1source0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1source0).Attributes.Count);
            Assert.AreEqual("source", dochtml1body1source0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1source0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithATrackElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><track>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1track0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1track0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1track0).Attributes.Count);
            Assert.AreEqual("track", dochtml1body1track0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1track0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAWbrElementAndCharacter()
        {
            var doc = Html(@"<!DOCTYPE html><body><wbr>A");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1wbr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1wbr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1wbr0).Attributes.Count);
            Assert.AreEqual("wbr", dochtml1body1wbr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1wbr0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("A", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void BodyWithAnchorsNobreaksAndBreakElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><a href='#1'><nobr>1<nobr></a><br><a href='#2'><nobr>2<nobr></a><br><a href='#3'><nobr>3<nobr></a>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(5, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1a0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1a0).GetAttribute("href"));
            Assert.AreEqual("#1", ((Element)dochtml1body1a0).GetAttribute("href"));

            var dochtml1body1a0nobr0 = dochtml1body1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr0.NodeType);

            var dochtml1body1a0nobr0Text0 = dochtml1body1a0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1a0nobr0Text0.TextContent);

            var dochtml1body1a0nobr1 = dochtml1body1a0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1br0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1br0).Attributes.Count);
            Assert.AreEqual("br", dochtml1body1nobr1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1br0.NodeType);

            var dochtml1body1nobr1a1 = dochtml1body1nobr1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr1a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1nobr1a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1nobr1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1a1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1nobr1a1).GetAttribute("href"));
            Assert.AreEqual("#2", ((Element)dochtml1body1nobr1a1).GetAttribute("href"));

            var dochtml1body1a2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1a2.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1a2).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1a2).GetAttribute("href"));
            Assert.AreEqual("#2", ((Element)dochtml1body1a2).GetAttribute("href"));

            var dochtml1body1a2nobr0 = dochtml1body1a2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr0.NodeType);

            var dochtml1body1a2nobr0Text0 = dochtml1body1a2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1a2nobr0Text0.TextContent);

            var dochtml1body1a2nobr1 = dochtml1body1a2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a2nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(2, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr3).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3br0 = dochtml1body1nobr3.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr3br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr3br0).Attributes.Count);
            Assert.AreEqual("br", dochtml1body1nobr3br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3br0.NodeType);

            var dochtml1body1nobr3a1 = dochtml1body1nobr3.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr3a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1nobr3a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1nobr3a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3a1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1nobr3a1).GetAttribute("href"));
            Assert.AreEqual("#3", ((Element)dochtml1body1nobr3a1).GetAttribute("href"));

            var dochtml1body1a4 = dochtml1body1.ChildNodes[4];
            Assert.AreEqual(2, dochtml1body1a4.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1a4).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1a4).GetAttribute("href"));
            Assert.AreEqual("#3", ((Element)dochtml1body1a4).GetAttribute("href"));

            var dochtml1body1a4nobr0 = dochtml1body1a4.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a4nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a4nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a4nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr0.NodeType);

            var dochtml1body1a4nobr0Text0 = dochtml1body1a4nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a4nobr0Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1a4nobr0Text0.TextContent);

            var dochtml1body1a4nobr1 = dochtml1body1a4.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a4nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a4nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a4nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr1.NodeType);

        }

        [Test]
        public void BodyWithBoldItalicAndNobreakElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);

            var dochtml1body1i2nobr1 = dochtml1body1i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr3).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3Text0 = dochtml1body1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1nobr3Text0.TextContent);

        }

        [Test]
        public void BodyWithFormattedElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<table><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(5, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0nobr1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1.NodeType);

            var dochtml1body1b0nobr0nobr1i0 = dochtml1body1b0nobr0nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1i0.NodeType);

            var dochtml1body1b0nobr0i2 = dochtml1body1b0nobr0.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1b0nobr0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0i2).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2.NodeType);

            var dochtml1body1b0nobr0i2nobr0 = dochtml1body1b0nobr0i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0i2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr0.NodeType);

            var dochtml1body1b0nobr0i2nobr0Text0 = dochtml1body1b0nobr0i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0i2nobr0Text0.TextContent);

            var dochtml1body1b0nobr0i2nobr1 = dochtml1body1b0nobr0i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0i2nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr1.NodeType);

            var dochtml1body1b0nobr0nobr3 = dochtml1body1b0nobr0.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0nobr3).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr3.NodeType);

            var dochtml1body1b0nobr0nobr3Text0 = dochtml1body1b0nobr0nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0nobr3Text0.TextContent);

            var dochtml1body1b0nobr0table4 = dochtml1body1b0nobr0.ChildNodes[4];
            Assert.AreEqual(0, dochtml1body1b0nobr0table4.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table4).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1b0nobr0table4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table4.NodeType);

        }

        [Test]
        public void BodyWithBoldItalicAndNoBreakElementsWithCharacters()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<table><tr><td><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1b0nobr0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1.NodeType);

            var dochtml1body1b0nobr0table1tbody0 = dochtml1body1b0nobr0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1b0nobr0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0 = dochtml1body1b0nobr0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1b0nobr0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0 = dochtml1body1b0nobr0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1b0nobr0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0 = dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0table1tbody0tr0td0nobr2).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.TextContent);

        }

        [Test]
        public void BodyWithBoldNoBreakDivElementsAndItalics()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<div><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1b0 = dochtml1body1div1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0.NodeType);

            var dochtml1body1div1b0nobr0 = dochtml1body1div1b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr0.NodeType);

            var dochtml1body1div1b0nobr1 = dochtml1body1div1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr1.NodeType);

            var dochtml1body1div1nobr1 = dochtml1body1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1.NodeType);

            var dochtml1body1div1nobr1i0 = dochtml1body1div1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1i0.NodeType);

            var dochtml1body1div1i2 = dochtml1body1div1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1div1i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i2).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2.NodeType);

            var dochtml1body1div1i2nobr0 = dochtml1body1div1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr0.NodeType);

            var dochtml1body1div1i2nobr0Text0 = dochtml1body1div1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i2nobr0Text0.TextContent);

            var dochtml1body1div1i2nobr1 = dochtml1body1div1i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i2nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr1.NodeType);

            var dochtml1body1div1nobr3 = dochtml1body1div1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1div1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr3).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr3.NodeType);

            var dochtml1body1div1nobr3Text0 = dochtml1body1div1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr3Text0.TextContent);

        }

        [Test]
        public void BodyWithDivElementsBoldNoBreakAndItalics()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><div><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1nobr0 = dochtml1body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0.NodeType);

            var dochtml1body1div1nobr0i0 = dochtml1body1div1nobr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1nobr0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0i0.NodeType);

            var dochtml1body1div1i1 = dochtml1body1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1div1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1.NodeType);

            var dochtml1body1div1i1nobr0 = dochtml1body1div1i1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i1nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr0.NodeType);

            var dochtml1body1div1i1nobr0Text0 = dochtml1body1div1i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i1nobr0Text0.TextContent);

            var dochtml1body1div1i1nobr1 = dochtml1body1div1i1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1i1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr1.NodeType);

            var dochtml1body1div1nobr2 = dochtml1body1div1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1div1nobr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div1nobr2).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr2.NodeType);

            var dochtml1body1div1nobr2Text0 = dochtml1body1div1nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr2Text0.TextContent);

        }

        [Test]
        public void BodyWithBoldNoBreakAndModInsertElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr><ins></b><i><nobr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1b0nobr1ins0 = dochtml1body1b0nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr1ins0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1ins0).Attributes.Count);
            Assert.AreEqual("ins", dochtml1body1b0nobr1ins0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1ins0.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

        }

        [Test]
        public void BodyWithBoldNoBreakItalicAndModInsertElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<ins><nobr></b><i>2");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0ins1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0ins1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr0ins1).Attributes.Count);
            Assert.AreEqual("ins", dochtml1body1b0nobr0ins1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0ins1.NodeType);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1nobr1i0Text0 = dochtml1body1nobr1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr1i0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1nobr1i0Text0.TextContent);

        }

        [Test]
        public void BodyWithItalicsNoBreaksAndBoldElements()
        {
            var doc = Html(@"<!DOCTYPE html><body><b>1<nobr></b><i><nobr>2</i>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0Text0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i2nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);

        }

        [Test]
        public void ParagraphWithBrokenCodeElement()
        {
            var doc = Html(@"<p><code x</code></p>
");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0code0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0code0.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml0body1p0code0).Attributes.Count);
            Assert.AreEqual("code", dochtml0body1p0code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0code0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1p0code0).GetAttribute("code"));
            Assert.AreEqual("", ((Element)dochtml0body1p0code0).GetAttribute("code"));

            Assert.IsNotNull(((Element)dochtml0body1p0code0).GetAttribute("x<"));
            Assert.AreEqual("", ((Element)dochtml0body1p0code0).GetAttribute("x<"));

            var dochtml0body1code1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1code1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml0body1code1).Attributes.Count);
            Assert.AreEqual("code", dochtml0body1code1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1code1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1code1).GetAttribute("code"));
            Assert.AreEqual("", ((Element)dochtml0body1code1).GetAttribute("code"));

            Assert.IsNotNull(((Element)dochtml0body1code1).GetAttribute("x<"));
            Assert.AreEqual("", ((Element)dochtml0body1code1).GetAttribute("x<"));

            var dochtml0body1code1Text0 = dochtml0body1code1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1code1Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1code1Text0.TextContent);

        }

        [Test]
        public void ForeignElementSvgWithParagraphAndItalics()
        {
            var doc = Html(@"<!DOCTYPE html><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0foreignObject0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0.NodeType);

            var dochtml1body1svg0foreignObject0p0 = dochtml1body1svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1svg0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0.NodeType);

            var dochtml1body1svg0foreignObject0p0i0 = dochtml1body1svg0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0p0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0i0.NodeType);

            var dochtml1body1svg0foreignObject0i1 = dochtml1body1svg0foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0i1.NodeType);

            var dochtml1body1svg0foreignObject0i1Text0 = dochtml1body1svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0foreignObject0i1Text0.TextContent);

        }

        [Test]
        public void TableWithRowHostingAForeignSvgElement()
        {
            var doc = Html(@"<!DOCTYPE html><table><tr><td><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1table0tbody0tr0td0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0foreignObject0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0foreignObject0i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.TextContent);

        }

        [Test]
        public void ForeignElementMathWithMTextParagraphAndItalics()
        {
            var doc = Html(@"<!DOCTYPE html><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mtext0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0mtext0).Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0.NodeType);

            var dochtml1body1math0mtext0p0 = dochtml1body1math0mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0mtext0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1math0mtext0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0.NodeType);

            var dochtml1body1math0mtext0p0i0 = dochtml1body1math0mtext0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0mtext0p0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1math0mtext0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0i0.NodeType);

            var dochtml1body1math0mtext0i1 = dochtml1body1math0mtext0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0mtext0i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1math0mtext0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0i1.NodeType);

            var dochtml1body1math0mtext0i1Text0 = dochtml1body1math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1math0mtext0i1Text0.TextContent);

        }

        [Test]
        public void TableElementWithForeignMathElement()
        {
            var doc = Html(@"<!DOCTYPE html><table><tr><td><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0math0mtext0).Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1table0tbody0tr0td0math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0math0mtext0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0math0mtext0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0i0 = dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0math0mtext0p0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0math0mtext0i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1Text0 = dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.TextContent);

        }

        [Test]
        public void BodyElementWithWronglyClosedDivElement()
        {
            var doc = Html(@"<!DOCTYPE html><body><div><!/div>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0Comment0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1body1div0Comment0.NodeType);
            Assert.AreEqual(@"/div", dochtml1body1div0Comment0.TextContent);

            var dochtml1body1div0Text1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1div0Text1.TextContent);
        }

        [Test]
        public void ParagraphElementBetweenOpenButtonElements()
        {
            var doc = Html(@"<button><p><button>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1button0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button0).Attributes.Count);
            Assert.AreEqual("button", dochtml0body1button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button0.NodeType);

            var dochtml0body1button0p0 = dochtml0body1button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1button0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1button0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button0p0.NodeType);

            var dochtml0body1button1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1button1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1).Attributes.Count);
            Assert.AreEqual("button", dochtml0body1button1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1.NodeType);
        }
    }
}