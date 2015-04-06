using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/main-element.dat
    /// </summary>
    [TestFixture]
    public class MainElementTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void MainElementClosesOpenParagraph()
        {
            var doc = Html(@"<!doctype html><p>foo<main>bar<p>baz");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0Text0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1p0Text0.TextContent);

            var dochtml1body1main1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1main1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1main1).Attributes.Count);
            Assert.AreEqual("main", dochtml1body1main1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1main1.NodeType);

            var dochtml1body1main1Text0 = dochtml1body1main1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1main1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1main1Text0.TextContent);

            var dochtml1body1main1p1 = dochtml1body1main1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1main1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1main1p1).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1main1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1main1p1.NodeType);

            var dochtml1body1main1p1Text0 = dochtml1body1main1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1main1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1main1p1Text0.TextContent);
        }

        [Test]
        public void MainClosesNestedParagraph()
        {
            var doc = Html(@"<!doctype html><main><p>foo</main>bar");

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

            var dochtml1body1main0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1main0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1main0).Attributes.Count);
            Assert.AreEqual("main", dochtml1body1main0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1main0.NodeType);

            var dochtml1body1main0p0 = dochtml1body1main0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1main0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1main0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1main0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1main0p0.NodeType);

            var dochtml1body1main0p0Text0 = dochtml1body1main0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1main0p0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1main0p0Text0.TextContent);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("bar", dochtml1body1Text1.TextContent);
        }

        [Test]
        public void MainElementInForeignSvgElement()
        {
            var doc = Html(@"<!DOCTYPE html>xxx<svg><x><g><a><main><b>");

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
            Assert.AreEqual("xxx", dochtml1body1Text0.TextContent);

            var dochtml1body1svg1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1svg1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg1).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg1.NodeType);

            var dochtml1body1svg1x0 = dochtml1body1svg1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg1x0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg1x0).Attributes.Count);
            Assert.AreEqual("x", dochtml1body1svg1x0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg1x0.NodeType);

            var dochtml1body1svg1x0g0 = dochtml1body1svg1x0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg1x0g0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg1x0g0).Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg1x0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg1x0g0.NodeType);

            var dochtml1body1svg1x0g0a0 = dochtml1body1svg1x0g0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg1x0g0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg1x0g0a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1svg1x0g0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg1x0g0a0.NodeType);

            var dochtml1body1svg1x0g0a0main0 = dochtml1body1svg1x0g0a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg1x0g0a0main0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg1x0g0a0main0).Attributes.Count);
            Assert.AreEqual("main", dochtml1body1svg1x0g0a0main0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg1x0g0a0main0.NodeType);

            var dochtml1body1b2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1b2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b2).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b2.NodeType);

        }

    }
}