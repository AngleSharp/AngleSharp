namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests14.dat
    /// </summary>
    [TestFixture]
    public class NamespaceTests
    {
        private static readonly String HtmlWithNestedSvgElement = @"<!DOCTYPE html>
<div><span><svg xmlns=""http://www.w3.org/2000/svg""><svg><circle /></svg></svg></span></div>";

        [Test]
        public void UnknownElementWithUnknownNamespaceInBody()
        {
            var doc = (@"<!DOCTYPE html><html><body><xyz:abc></xyz:abc>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1xyzabc0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1xyzabc0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1xyzabc0).Attributes.Length);
            Assert.AreEqual("xyz:abc", dochtml1body1xyzabc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1xyzabc0.NodeType);
        }

        [Test]
        public void UnknownElementWithUnknownNamespaceInBodyBeforeSpan()
        {
            var doc = (@"<!DOCTYPE html><html><body><xyz:abc></xyz:abc><span></span>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1xyzabc0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1xyzabc0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1xyzabc0).Attributes.Length);
            Assert.AreEqual("xyz:abc", dochtml1body1xyzabc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1xyzabc0.NodeType);

            var dochtml1body1span1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1span1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1span1).Attributes.Length);
            Assert.AreEqual("span", dochtml1body1span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1span1.NodeType);
        }

        [Test]
        public void UnknownElementWithUnknownNamespaceInHtmlWithUnknownAttribute()
        {
            var doc = (@"<!DOCTYPE html><html><html abc:def=gh><xyz:abc></xyz:abc>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("abc:def"));
            Assert.AreEqual("gh", ((Element)dochtml1).GetAttribute("abc:def"));

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1xyzabc0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1xyzabc0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1xyzabc0).Attributes.Length);
            Assert.AreEqual("xyz:abc", dochtml1body1xyzabc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1xyzabc0.NodeType);
        }

        [Test]
        public void DuplicatedHtmlTagWithMultipleXmlLangAttributes()
        {
            var doc = (@"<!DOCTYPE html><html xml:lang=bar><html xml:lang=foo>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("xml:lang"));
            Assert.AreEqual("bar", ((Element)dochtml1).GetAttribute("xml:lang"));

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void NumericAttributeWithNumericValue()
        {
            var doc = (@"<!DOCTYPE html><html 123=456>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("123"));
            Assert.AreEqual("456", ((Element)dochtml1).GetAttribute("123"));

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void DuplicatedHtmlTagWithDifferentNumericAttributes()
        {
            var doc = (@"<!DOCTYPE html><html 123=456><html 789=012>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("123"));
            Assert.AreEqual("456", ((Element)dochtml1).GetAttribute("123"));

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("789"));
            Assert.AreEqual("012", ((Element)dochtml1).GetAttribute("789"));

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BodyTagWithNumericAttribute()
        {
            var doc = (@"<!DOCTYPE html><html><body 789=012>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1).GetAttribute("789"));
            Assert.AreEqual("012", ((Element)dochtml1body1).GetAttribute("789"));
        }

        [Test]
        public void HtmlElementsAreUppercaseSvgElementsLowercase()
        {
            var doc = (HtmlWithNestedSvgElement).ToHtmlDocument();
            var body = doc.Body;
            var div = body.FirstElementChild;
            var span = div.FirstElementChild;
            var svg = span.FirstElementChild;
            Assert.AreEqual("BODY", body.TagName);
            Assert.AreEqual("DIV", div.TagName);
            Assert.AreEqual("SPAN", span.TagName);
            Assert.AreEqual("svg", svg.TagName);
        }

        [Test]
        public void NestedSvgElementHasSameNameAsNormalSvgElement()
        {
            var doc = (HtmlWithNestedSvgElement).ToHtmlDocument();
            var svg = doc.Body.FirstElementChild.FirstElementChild.FirstElementChild;
            var nestedSvg = svg.FirstElementChild;
            var circle = nestedSvg.FirstElementChild;
            Assert.AreEqual("svg", nestedSvg.TagName);
            Assert.AreEqual("circle", circle.TagName);
        }
    }
}