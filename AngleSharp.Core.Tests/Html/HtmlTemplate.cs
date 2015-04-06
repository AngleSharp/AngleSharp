using System;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Html
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// encoding/template.dat
    /// </summary>
    [TestFixture]
    public class HtmlTemplateTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void TemplateNodeInBodyWithTextContent()
        {
            var doc = Html(@"<body><template>Hello</template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0ContentText0 = dochtml0body1template0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0ContentText0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1template0ContentText0.TextContent);
        }

        [Test]
        public void TemplateNodeStandaloneWithTextContent()
        {
            var doc = Html(@"<template>Hello</template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0ContentText0 = dochtml0head0template0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0template0ContentText0.NodeType);
            Assert.AreEqual("Hello", dochtml0head0template0ContentText0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeEmptyFollowedByEmptyDiv()
        {
            var doc = Html(@"<template></template><div></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(0, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
        }

        [Test]
        public void TemplateInHtmlWithTextContent()
        {
            var doc = Html(@"<html><template>Hello</template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0ContentText0 = dochtml0head0template0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0template0ContentText0.NodeType);
            Assert.AreEqual("Hello", dochtml0head0template0ContentText0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateInHeadWithDivElement()
        {
            var doc = Html(@"<head><template><div></div></template></head>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentdiv0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0head0template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInDivWithDivAndSpanMisclosed()
        {
            var doc = Html(@"<div><template><div><span></template><b>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0template0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1div0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0.NodeType);

            var dochtml0body1div0template0Content = ((HtmlTemplateElement)dochtml0body1div0template0).Content;
            Assert.AreEqual(1, dochtml0body1div0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1div0template0Content.NodeType);

            var dochtml0body1div0template0Contentdiv0 = dochtml0body1div0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0Contentdiv0.NodeType);

            var dochtml0body1div0template0Contentdiv0span0 = dochtml0body1div0template0Contentdiv0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0template0Contentdiv0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0Contentdiv0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1div0template0Contentdiv0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0Contentdiv0span0.NodeType);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0b1.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);
        }

        [Test]
        public void TemplateNodeInDivMisclosed()
        {
            var doc = Html(@"<div><template></div>Hello");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0template0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1div0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0.NodeType);

            var dochtml0body1div0template0Content = ((HtmlTemplateElement)dochtml0body1div0template0).Content;
            Assert.AreEqual(1, dochtml0body1div0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1div0template0Content.NodeType);

            var dochtml0body1div0template0ContentText0 = dochtml0body1div0template0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0template0ContentText0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0template0ContentText0.TextContent);
        }

        [Test]
        public void TemplateNodeClosedInDivElement()
        {
            var doc = Html(@"<div></template></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
        }

        [Test]
        public void TemplateNodeInTableElement()
        {
            var doc = Html(@"<table><template></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTableElementMisclosed()
        {
            var doc = Html(@"<table><template></template></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInDivUnderTableElement()
        {
            var doc = Html(@"<table><div><template></template></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0template0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1div0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0.NodeType);

            var dochtml0body1div0template0Content = ((HtmlTemplateElement)dochtml0body1div0template0).Content;
            Assert.AreEqual(0, dochtml0body1div0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1div0template0Content.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [Test]
        public void TemplateNodeFollowedByDivInTable()
        {
            var doc = Html(@"<table><template></template><div></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1template0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1template0.NodeType);

            var dochtml0body1table1template0Content = ((HtmlTemplateElement)dochtml0body1table1template0).Content;
            Assert.AreEqual(0, dochtml0body1table1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table1template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTableAfterSpaces()
        {
            var doc = Html(@"<table>   <template></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0Text0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0Text0.NodeType);
            Assert.AreEqual("   ", dochtml0body1table0Text0.TextContent);

            var dochtml0body1table0template1 = dochtml0body1table0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0template1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template1.NodeType);

            var dochtml0body1table0template1Content = ((HtmlTemplateElement)dochtml0body1table0template1).Content;
            Assert.AreEqual(0, dochtml0body1table0template1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template1Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTbody()
        {
            var doc = Html(@"<table><tbody><template></template></tbody>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0template0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0tbody0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0template0.NodeType);

            var dochtml0body1table0tbody0template0Content = ((HtmlTemplateElement)dochtml0body1table0tbody0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0tbody0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTbodyMisclosed()
        {
            var doc = Html(@"<table><tbody><template></tbody></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0template0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0tbody0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0template0.NodeType);

            var dochtml0body1table0tbody0template0Content = ((HtmlTemplateElement)dochtml0body1table0tbody0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0tbody0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTbodyInTable()
        {
            var doc = Html(@"<table><tbody><template></template></tbody></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0template0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0tbody0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0template0.NodeType);

            var dochtml0body1table0tbody0template0Content = ((HtmlTemplateElement)dochtml0body1table0tbody0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0tbody0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0tbody0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInThead()
        {
            var doc = Html(@"<table><thead><template></template></thead>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0thead0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0thead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0.NodeType);

            var dochtml0body1table0thead0template0 = dochtml0body1table0thead0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0thead0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0thead0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0template0.NodeType);

            var dochtml0body1table0thead0template0Content = ((HtmlTemplateElement)dochtml0body1table0thead0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0thead0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0thead0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInTfoot()
        {
            var doc = Html(@"<table><tfoot><template></template></tfoot>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tfoot0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tfoot0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tfoot0.Attributes.Count);
            Assert.AreEqual("tfoot", dochtml0body1table0tfoot0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tfoot0.NodeType);

            var dochtml0body1table0tfoot0template0 = dochtml0body1table0tfoot0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tfoot0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tfoot0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0tfoot0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tfoot0template0.NodeType);

            var dochtml0body1table0tfoot0template0Content = ((HtmlTemplateElement)dochtml0body1table0tfoot0template0).Content;
            Assert.AreEqual(0, dochtml0body1table0tfoot0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0tfoot0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInSelect()
        {
            var doc = Html(@"<select><template></template></select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0template0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template0.NodeType);

            var dochtml0body1select0template0Content = ((HtmlTemplateElement)dochtml0body1select0template0).Content;
            Assert.AreEqual(0, dochtml0body1select0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeWithOptionInSelect()
        {
            var doc = Html(@"<select><template><option></option></template></select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0template0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template0.NodeType);

            var dochtml0body1select0template0Content = ((HtmlTemplateElement)dochtml0body1select0template0).Content;
            Assert.AreEqual(1, dochtml0body1select0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template0Content.NodeType);

            var dochtml0body1select0template0Contentoption0 = dochtml0body1select0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template0Contentoption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template0Contentoption0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0template0Contentoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template0Contentoption0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOptionsAndMisclosedSelect()
        {
            var doc = Html(@"<template><option></option></select><option></option></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(2, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentoption0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentoption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentoption0.Attributes.Count);
            Assert.AreEqual("option", dochtml0head0template0Contentoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentoption0.NodeType);

            var dochtml0head0template0Contentoption1 = dochtml0head0template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentoption1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentoption1.Attributes.Count);
            Assert.AreEqual("option", dochtml0head0template0Contentoption1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentoption1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInSelectFollowedByOption()
        {
            var doc = Html(@"<select><template></template><option></select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0template0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template0.NodeType);

            var dochtml0body1select0template0Content = ((HtmlTemplateElement)dochtml0body1select0template0).Content;
            Assert.AreEqual(0, dochtml0body1select0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template0Content.NodeType);

            var dochtml0body1select0option1 = dochtml0body1select0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1select0option1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option1.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option1.NodeType);
        }

        [Test]
        public void TemplateNodeInOptionOfSelect()
        {
            var doc = Html(@"<select><option><template></template></select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0template0 = dochtml0body1select0option0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0option0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0option0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0template0.NodeType);

            var dochtml0body1select0option0template0Content = ((HtmlTemplateElement)dochtml0body1select0option0template0).Content;
            Assert.AreEqual(0, dochtml0body1select0option0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0option0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInImplicitlyClosed()
        {
            var doc = Html(@"<select><template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0template0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template0.NodeType);

            var dochtml0body1select0template0Content = ((HtmlTemplateElement)dochtml0body1select0template0).Content;
            Assert.AreEqual(0, dochtml0body1select0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template0Content.NodeType);
        }

        [Test]
        public void TemplateNodeInInSelectAfterClosedOption()
        {
            var doc = Html(@"<select><option></option><template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0template1 = dochtml0body1select0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1select0template1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template1.NodeType);

            var dochtml0body1select0template1Content = ((HtmlTemplateElement)dochtml0body1select0template1).Content;
            Assert.AreEqual(0, dochtml0body1select0template1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template1Content.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenOptionInSelectAfterClosedOption()
        {
            var doc = Html(@"<select><option></option><template><option>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0template1 = dochtml0body1select0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1select0template1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1select0template1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template1.NodeType);

            var dochtml0body1select0template1Content = ((HtmlTemplateElement)dochtml0body1select0template1).Content;
            Assert.AreEqual(1, dochtml0body1select0template1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1select0template1Content.NodeType);

            var dochtml0body1select0template1Contentoption0 = dochtml0body1select0template1Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0template1Contentoption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0template1Contentoption0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0template1Contentoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0template1Contentoption0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTdInThead()
        {
            var doc = Html(@"<table><thead><template><td></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0thead0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0thead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0.NodeType);

            var dochtml0body1table0thead0template0 = dochtml0body1table0thead0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0thead0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0thead0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0template0.NodeType);

            var dochtml0body1table0thead0template0Content = ((HtmlTemplateElement)dochtml0body1table0thead0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0thead0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0thead0template0Content.NodeType);

            var dochtml0body1table0thead0template0Contenttd0 = dochtml0body1table0thead0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0thead0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0thead0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTheadInTable()
        {
            var doc = Html(@"<table><template><thead></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contentthead0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contentthead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contentthead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0template0Contentthead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contentthead0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTdAndMisclosedTrInTable()
        {
            var doc = Html(@"<body><table><template><td></tr><div></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contenttd0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttd0.NodeType);

            var dochtml0body1table0template0Contenttd0div0 = dochtml0body1table0template0Contenttd0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttd0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttd0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1table0template0Contenttd0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttd0div0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTheadInTableWithMisclosedThead()
        {
            var doc = Html(@"<table><template><thead></template></thead></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contentthead0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contentthead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contentthead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0template0Contentthead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contentthead0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTrInTheadInTable()
        {
            var doc = Html(@"<table><thead><template><tr></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0thead0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0thead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0.NodeType);

            var dochtml0body1table0thead0template0 = dochtml0body1table0thead0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0thead0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0thead0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0template0.NodeType);

            var dochtml0body1table0thead0template0Content = ((HtmlTemplateElement)dochtml0body1table0thead0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0thead0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0thead0template0Content.NodeType);

            var dochtml0body1table0thead0template0Contenttr0 = dochtml0body1table0thead0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0thead0template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0thead0template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0template0Contenttr0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTrInTable()
        {
            var doc = Html(@"<table><template><tr></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contenttr0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTdInTrInTable()
        {
            var doc = Html(@"<table><tr><template><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0template0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0tbody0tr0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0template0.NodeType);

            var dochtml0body1table0tbody0tr0template0Content = ((HtmlTemplateElement)dochtml0body1table0tbody0tr0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0tbody0tr0template0Content.NodeType);

            var dochtml0body1table0tbody0tr0template0Contenttd0 = dochtml0body1table0tbody0tr0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodesNestedWithClosedElementsInTable()
        {
            var doc = Html(@"<table><template><tr><template><td></template></tr></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contenttr0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0.NodeType);

            var dochtml0body1table0template0Contenttr0template0 = dochtml0body1table0template0Contenttr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0Contenttr0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0template0.NodeType);

            var dochtml0body1table0template0Contenttr0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0Contenttr0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Contenttr0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Contenttr0template0Content.NodeType);

            var dochtml0body1table0template0Contenttr0template0Contenttd0 = dochtml0body1table0template0Contenttr0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0template0Contenttr0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodesNestedWithOpenElementsInTable()
        {
            var doc = Html(@"<table><template><tr><template><td></td></template></tr></template></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contenttr0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0.NodeType);

            var dochtml0body1table0template0Contenttr0template0 = dochtml0body1table0template0Contenttr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0Contenttr0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0template0.NodeType);

            var dochtml0body1table0template0Contenttr0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0Contenttr0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Contenttr0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Contenttr0template0Content.NodeType);

            var dochtml0body1table0template0Contenttr0template0Contenttd0 = dochtml0body1table0template0Contenttr0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttr0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0template0Contenttr0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttr0template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodeWithOpenTdInTable()
        {
            var doc = Html(@"<table><template><td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0template0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0.NodeType);

            var dochtml0body1table0template0Content = ((HtmlTemplateElement)dochtml0body1table0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0template0Content.NodeType);

            var dochtml0body1table0template0Contenttd0 = dochtml0body1table0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodeWithTdInBody()
        {
            var doc = Html(@"<body><template><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);
        }

        [Test]
        public void TemplateNodesMisnestedContent()
        {
            var doc = Html(@"<body><template><template><tr></tr></template><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttemplate0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0.NodeType);

            var dochtml0body1template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0body1template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate0Content.NodeType);

            var dochtml0body1template0Contenttemplate0Contenttr0 = dochtml0body1template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttemplate0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0Contenttr0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeWithColInColgroupInTable()
        {
            var doc = Html(@"<table><colgroup><template><col>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0template0 = dochtml0body1table0colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0colgroup0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0template0.NodeType);

            var dochtml0body1table0colgroup0template0Content = ((HtmlTemplateElement)dochtml0body1table0colgroup0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0colgroup0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0colgroup0template0Content.NodeType);

            var dochtml0body1table0colgroup0template0Contentcol0 = dochtml0body1table0colgroup0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1table0colgroup0template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeWithFrameInFrameset()
        {
            var doc = Html(@"<frameset><template><frame></frame></template></frameset>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0frameset1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);
        }

        [Test]
        public void TemplateWithFrameAndMisclosedFrameset()
        {
            var doc = Html(@"<template><frame></frame></frameset><frame></frame></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(0, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);
        }

        [Test]
        public void TemplateWithDivFramesetAndSpan()
        {
            var doc = Html(@"<template><div><frameset><span></span></div><span></span></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(2, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentdiv0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0head0template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0.NodeType);

            var dochtml0head0template0Contentdiv0span0 = dochtml0head0template0Contentdiv0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0head0template0Contentdiv0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0span0.NodeType);

            var dochtml0head0template0Contentspan1 = dochtml0head0template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentspan1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentspan1.Attributes.Count);
            Assert.AreEqual("span", dochtml0head0template0Contentspan1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentspan1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithDivFramesetSpan()
        {
            var doc = Html(@"<body><template><div><frameset><span></span></div><span></span></template></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentdiv0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv0.NodeType);

            var dochtml0body1template0Contentdiv0span0 = dochtml0body1template0Contentdiv0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1template0Contentdiv0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv0span0.NodeType);

            var dochtml0body1template0Contentspan1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentspan1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentspan1.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1template0Contentspan1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentspan1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithScriptAndTd()
        {
            var doc = Html(@"<body><template><script>var i = 1;</script><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentscript0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentscript0.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1template0Contentscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentscript0.NodeType);

            var dochtml0body1template0Contentscript0Text0 = dochtml0body1template0Contentscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0Contentscript0Text0.NodeType);
            Assert.AreEqual("var i = 1;", dochtml0body1template0Contentscript0Text0.TextContent);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrDiv()
        {
            var doc = Html(@"<body><template><tr><div></div></tr></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contentdiv1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentdiv1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrTd()
        {
            var doc = Html(@"<body><template><tr></tr><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contenttr1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contenttr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr1.NodeType);

            var dochtml0body1template0Contenttr1td0 = dochtml0body1template0Contenttr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr1td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr1td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttr1td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr1td0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTdMisclosedTrAndTd()
        {
            var doc = Html(@"<body><template><td></td></tr><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTdTbodyTd()
        {
            var doc = Html(@"<body><template><td></td><tbody><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTdCaptionTd()
        {
            var doc = Html(@"<body><template><td></td><caption></caption><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTdColgroupTd()
        {
            var doc = Html(@"<body><template><td></td><colgroup></caption><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTdMisclosedTableAndTd()
        {
            var doc = Html(@"<body><template><td></td></table><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttd0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrTbodyTr()
        {
            var doc = Html(@"<body><template><tr></tr><tbody><tr></tr></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contenttr1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrCaptionTr()
        {
            var doc = Html(@"<body><template><tr></tr><caption><tr></tr></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contenttr1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrMisclosedTableTr()
        {
            var doc = Html(@"<body><template><tr></tr></table><tr></tr></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contenttr1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr1.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTheadCaptionTbody()
        {
            var doc = Html(@"<body><template><thead></thead><caption></caption><tbody></tbody></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(3, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentthead0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1template0Contentthead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentthead0.NodeType);

            var dochtml0body1template0Contentcaption1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcaption1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcaption1.Attributes.Count);
            Assert.AreEqual("caption", dochtml0body1template0Contentcaption1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcaption1.NodeType);

            var dochtml0body1template0Contenttbody2 = dochtml0body1template0Content.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttbody2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttbody2.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1template0Contenttbody2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttbody2.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTheadMisclosedTableTbody()
        {
            var doc = Html(@"<body><template><thead></thead></table><tbody></tbody></template></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentthead0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1template0Contentthead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentthead0.NodeType);

            var dochtml0body1template0Contenttbody1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttbody1.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1template0Contenttbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttbody1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithDivTr()
        {
            var doc = Html(@"<body><template><div><tr></tr></div></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentdiv0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithEmAndText()
        {
            var doc = Html(@"<body><template><em>Hello</em></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentem0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentem0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentem0.Attributes.Count);
            Assert.AreEqual("em", dochtml0body1template0Contentem0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentem0.NodeType);

            var dochtml0body1template0Contentem0Text0 = dochtml0body1template0Contentem0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0Contentem0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1template0Contentem0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeInBodyWithComment()
        {
            var doc = Html(@"<body><template><!--comment--></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0ContentComment0 = dochtml0body1template0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0body1template0ContentComment0.NodeType);
            Assert.AreEqual(@"comment", dochtml0body1template0ContentComment0.TextContent);
        }

        [Test]
        public void TemplateNodeInBodyWithStyleTd()
        {
            var doc = Html(@"<body><template><style></style><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentstyle0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentstyle0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentstyle0.Attributes.Count);
            Assert.AreEqual("style", dochtml0body1template0Contentstyle0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentstyle0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithMetaTd()
        {
            var doc = Html(@"<body><template><meta><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentmeta0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentmeta0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentmeta0.Attributes.Count);
            Assert.AreEqual("meta", dochtml0body1template0Contentmeta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentmeta0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithLinkTd()
        {
            var doc = Html(@"<body><template><link><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentlink0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentlink0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentlink0.Attributes.Count);
            Assert.AreEqual("link", dochtml0body1template0Contentlink0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentlink0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedTemplateWithTr()
        {
            var doc = Html(@"<body><template><template><tr></tr></template><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttemplate0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0.NodeType);

            var dochtml0body1template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0body1template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate0Content.NodeType);

            var dochtml0body1template0Contenttemplate0Contenttr0 = dochtml0body1template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttemplate0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0Contenttr0.NodeType);

            var dochtml0body1template0Contenttd1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttd1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttd1.NodeType);
        }

        [Test]
        public void TemplateNodeInColgroupWithCol()
        {
            var doc = Html(@"<body><table><colgroup><template><col></col></template></colgroup></table></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0template0 = dochtml0body1table0colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1table0colgroup0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0template0.NodeType);

            var dochtml0body1table0colgroup0template0Content = ((HtmlTemplateElement)dochtml0body1table0colgroup0template0).Content;
            Assert.AreEqual(1, dochtml0body1table0colgroup0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1table0colgroup0template0Content.NodeType);

            var dochtml0body1table0colgroup0template0Contentcol0 = dochtml0body1table0colgroup0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1table0colgroup0template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithAttrAndDivAndOtherBody()
        {
            var doc = Html(@"<body a=b><template><div></div><body c=d><div></div></body></template></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
            Assert.IsNotNull(dochtml0body1.Attributes.Get("a"));
            Assert.AreEqual("a", dochtml0body1.Attributes.Get("a").Name);
            Assert.AreEqual("b", dochtml0body1.Attributes.Get("a").Value);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentdiv0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv0.NodeType);

            var dochtml0body1template0Contentdiv1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentdiv1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv1.NodeType);
        }

        [Test]
        public void TemplateNodeInHtmlWithAttrWithDivAndOtherHtml()
        {
            var doc = Html(@"<html a=b><template><div><html b=c><span></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
            Assert.IsNotNull(dochtml0.Attributes.Get("a"));
            Assert.AreEqual("a", dochtml0.Attributes.Get("a").Name);
            Assert.AreEqual("b", dochtml0.Attributes.Get("a").Value);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentdiv0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0head0template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0.NodeType);

            var dochtml0head0template0Contentdiv0span0 = dochtml0head0template0Contentdiv0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0head0template0Contentdiv0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0span0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInHtmlWithAttrWithColAndOtherHtml()
        {
            var doc = Html(@"<html a=b><template><col></col><html b=c><col></col></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
            Assert.IsNotNull(dochtml0.Attributes.Get("a"));
            Assert.AreEqual("a", dochtml0.Attributes.Get("a").Name);
            Assert.AreEqual("b", dochtml0.Attributes.Get("a").Value);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(2, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentcol0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0head0template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentcol0.NodeType);

            var dochtml0head0template0Contentcol1 = dochtml0head0template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentcol1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentcol1.Attributes.Count);
            Assert.AreEqual("col", dochtml0head0template0Contentcol1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentcol1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInHtmlWithAttrWithFrameAndOtherHtml()
        {
            var doc = Html(@"<html a=b><template><frame></frame><html b=c><frame></frame></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
            Assert.IsNotNull(dochtml0.Attributes.Get("a"));
            Assert.AreEqual("a", dochtml0.Attributes.Get("a").Name);
            Assert.AreEqual("b", dochtml0.Attributes.Get("a").Value);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(0, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTrAndNestedTemplate()
        {
            var doc = Html(@"<body><template><tr></tr><template></template><td></td></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(3, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttr0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr0.NodeType);

            var dochtml0body1template0Contenttemplate1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate1.NodeType);

            var dochtml0body1template0Contenttemplate1Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate1).Content;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate1Content.NodeType);

            var dochtml0body1template0Contenttr2 = dochtml0body1template0Content.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contenttr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr2.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr2.NodeType);

            var dochtml0body1template0Contenttr2td0 = dochtml0body1template0Contenttr2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttr2td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttr2td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1template0Contenttr2td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttr2td0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithTheadTrTfootNestedTemplateWithTr()
        {
            var doc = Html(@"<body><template><thead></thead><template><tr></tr></template><tr></tr><tfoot></tfoot></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(4, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentthead0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentthead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1template0Contentthead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentthead0.NodeType);

            var dochtml0body1template0Contenttemplate1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate1.NodeType);

            var dochtml0body1template0Contenttemplate1Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate1).Content;
            Assert.AreEqual(1, dochtml0body1template0Contenttemplate1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate1Content.NodeType);

            var dochtml0body1template0Contenttemplate1Contenttr0 = dochtml0body1template0Contenttemplate1Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttemplate1Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate1Contenttr0.NodeType);

            var dochtml0body1template0Contenttbody2 = dochtml0body1template0Content.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contenttbody2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttbody2.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1template0Contenttbody2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttbody2.NodeType);

            var dochtml0body1template0Contenttbody2tr0 = dochtml0body1template0Contenttbody2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttbody2tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttbody2tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1template0Contenttbody2tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttbody2tr0.NodeType);

            var dochtml0body1template0Contenttfoot3 = dochtml0body1template0Content.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttfoot3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttfoot3.Attributes.Count);
            Assert.AreEqual("tfoot", dochtml0body1template0Contenttfoot3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttfoot3.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithNestedTemplateBTemplateAndText()
        {
            var doc = Html(@"<body><template><template><b><template></template></template>text</template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenttemplate0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0.NodeType);

            var dochtml0body1template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0body1template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate0Content.NodeType);

            var dochtml0body1template0Contenttemplate0Contentb0 = dochtml0body1template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contenttemplate0Contentb0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contentb0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1template0Contenttemplate0Contentb0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0Contentb0.NodeType);

            var dochtml0body1template0Contenttemplate0Contentb0template0 = dochtml0body1template0Contenttemplate0Contentb0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contentb0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contentb0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate0Contentb0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate0Contentb0template0.NodeType);

            var dochtml0body1template0Contenttemplate0Contentb0template0Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate0Contentb0template0).Content;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate0Contentb0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate0Contentb0template0Content.NodeType);

            var dochtml0body1template0ContentText1 = dochtml0body1template0Content.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0ContentText1.NodeType);
            Assert.AreEqual("text", dochtml0body1template0ContentText1.TextContent);
        }

        [Test]
        public void TemplateNodeWithColColgroupInBody()
        {
            var doc = Html(@"<body><template><col><colgroup>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeWithColMisclosedColgroupInBody()
        {
            var doc = Html(@"<body><template><col></colgroup>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithColAndColgroup()
        {
            var doc = Html(@"<body><template><col><colgroup></template></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithColDiv()
        {
            var doc = Html(@"<body><template><col><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithColMisclosedDiv()
        {
            var doc = Html(@"<body><template><col></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithColAndText()
        {
            var doc = Html(@"<body><template><col>Hello");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(1, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentcol0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1template0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentcol0.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithItalicAndMenuAndText()
        {
            var doc = Html(@"<body><template><i><menu>Foo</i>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contenti0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenti0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenti0.Attributes.Count);
            Assert.AreEqual("i", dochtml0body1template0Contenti0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenti0.NodeType);

            var dochtml0body1template0Contentmenu1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentmenu1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentmenu1.Attributes.Count);
            Assert.AreEqual("menu", dochtml0body1template0Contentmenu1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentmenu1.NodeType);

            var dochtml0body1template0Contentmenu1i0 = dochtml0body1template0Contentmenu1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentmenu1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentmenu1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml0body1template0Contentmenu1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentmenu1i0.NodeType);

            var dochtml0body1template0Contentmenu1i0Text0 = dochtml0body1template0Contentmenu1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0Contentmenu1i0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1template0Contentmenu1i0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeWithMisclosedDivDivTextAndNestedTemplateInBody()
        {
            var doc = Html(@"<body><template></div><div>Foo</div><template></template><tr></tr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1template0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0.NodeType);

            var dochtml0body1template0Content = ((HtmlTemplateElement)dochtml0body1template0).Content;
            Assert.AreEqual(2, dochtml0body1template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Content.NodeType);

            var dochtml0body1template0Contentdiv0 = dochtml0body1template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contentdiv0.NodeType);

            var dochtml0body1template0Contentdiv0Text0 = dochtml0body1template0Contentdiv0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1template0Contentdiv0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1template0Contentdiv0Text0.TextContent);

            var dochtml0body1template0Contenttemplate1 = dochtml0body1template0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1template0Contenttemplate1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1template0Contenttemplate1.NodeType);

            var dochtml0body1template0Contenttemplate1Content = ((HtmlTemplateElement)dochtml0body1template0Contenttemplate1).Content;
            Assert.AreEqual(0, dochtml0body1template0Contenttemplate1Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1template0Contenttemplate1Content.NodeType);
        }

        [Test]
        public void TemplateNodeInBodyWithMisclosedDivTrTdAndText()
        {
            var doc = Html(@"<body><div><template></div><tr><td>Foo</td></tr></template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0template0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0body1div0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0.NodeType);

            var dochtml0body1div0template0Content = ((HtmlTemplateElement)dochtml0body1div0template0).Content;
            Assert.AreEqual(1, dochtml0body1div0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0body1div0template0Content.NodeType);

            var dochtml0body1div0template0Contenttr0 = dochtml0body1div0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0template0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1div0template0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0Contenttr0.NodeType);

            var dochtml0body1div0template0Contenttr0td0 = dochtml0body1div0template0Contenttr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0template0Contenttr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0template0Contenttr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1div0template0Contenttr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0template0Contenttr0td0.NodeType);

            var dochtml0body1div0template0Contenttr0td0Text0 = dochtml0body1div0template0Contenttr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0template0Contenttr0td0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1div0template0Contenttr0td0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeMisclosedFigcaptionAndSubAndTable()
        {
            var doc = Html(@"<template></figcaption><sub><table></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentsub0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contentsub0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentsub0.Attributes.Count);
            Assert.AreEqual("sub", dochtml0head0template0Contentsub0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentsub0.NodeType);

            var dochtml0head0template0Contentsub0table0 = dochtml0head0template0Contentsub0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentsub0table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentsub0table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0head0template0Contentsub0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentsub0table0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeEmptyNested()
        {
            var doc = Html(@"<template><template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeWithDivStandalone()
        {
            var doc = Html(@"<template><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentdiv0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0head0template0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentdiv0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithDiv()
        {
            var doc = Html(@"<template><template><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentdiv0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentdiv0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentdiv0.Attributes.Count);
            Assert.AreEqual("div", dochtml0head0template0Contenttemplate0Contentdiv0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentdiv0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithTable()
        {
            var doc = Html(@"<template><template><table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contenttable0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttable0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttable0.Attributes.Count);
            Assert.AreEqual("table", dochtml0head0template0Contenttemplate0Contenttable0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttable0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithTbody()
        {
            var doc = Html(@"<template><template><tbody>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contenttbody0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0head0template0Contenttemplate0Contenttbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttbody0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithTr()
        {
            var doc = Html(@"<template><template><tr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contenttr0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0head0template0Contenttemplate0Contenttr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttr0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithTd()
        {
            var doc = Html(@"<template><template><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contenttd0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0head0template0Contenttemplate0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttd0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithCaption()
        {
            var doc = Html(@"<template><template><caption>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentcaption0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcaption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcaption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml0head0template0Contenttemplate0Contentcaption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentcaption0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithColgroup()
        {
            var doc = Html(@"<template><template><colgroup>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentcolgroup0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcolgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcolgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0head0template0Contenttemplate0Contentcolgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentcolgroup0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithCol()
        {
            var doc = Html(@"<template><template><col>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentcol0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcol0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentcol0.Attributes.Count);
            Assert.AreEqual("col", dochtml0head0template0Contenttemplate0Contentcol0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentcol0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithSelectInTbody()
        {
            var doc = Html(@"<template><template><tbody><select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(2, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contenttbody0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0head0template0Contenttemplate0Contenttbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttbody0.NodeType);

            var dochtml0head0template0Contenttemplate0Contentselect1 = dochtml0head0template0Contenttemplate0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentselect1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentselect1.Attributes.Count);
            Assert.AreEqual("select", dochtml0head0template0Contenttemplate0Contentselect1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentselect1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithTextInTable()
        {
            var doc = Html(@"<template><template><table>Foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(2, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0ContentText0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0template0Contenttemplate0ContentText0.NodeType);
            Assert.AreEqual("Foo", dochtml0head0template0Contenttemplate0ContentText0.TextContent);

            var dochtml0head0template0Contenttemplate0Contenttable1 = dochtml0head0template0Contenttemplate0Content.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttable1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contenttable1.Attributes.Count);
            Assert.AreEqual("table", dochtml0head0template0Contenttemplate0Contenttable1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contenttable1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithFrame()
        {
            var doc = Html(@"<template><template><frame>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithScriptUnclosed()
        {
            var doc = Html(@"<template><template><script>var i");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentscript0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Contentscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentscript0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0template0Contenttemplate0Contentscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentscript0.NodeType);

            var dochtml0head0template0Contenttemplate0Contentscript0Text0 = dochtml0head0template0Contenttemplate0Contentscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0template0Contenttemplate0Contentscript0Text0.NodeType);
            Assert.AreEqual("var i", dochtml0head0template0Contenttemplate0Contentscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeNestedWithStyleUnclosed()
        {
            var doc = Html(@"<template><template><style>var i");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttemplate0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contenttemplate0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0.NodeType);

            var dochtml0head0template0Contenttemplate0Content = ((HtmlTemplateElement)dochtml0head0template0Contenttemplate0).Content;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Contenttemplate0Content.NodeType);

            var dochtml0head0template0Contenttemplate0Contentstyle0 = dochtml0head0template0Contenttemplate0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contenttemplate0Contentstyle0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttemplate0Contentstyle0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0template0Contenttemplate0Contentstyle0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttemplate0Contentstyle0.NodeType);

            var dochtml0head0template0Contenttemplate0Contentstyle0Text0 = dochtml0head0template0Contenttemplate0Contentstyle0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0template0Contenttemplate0Contentstyle0Text0.NodeType);
            Assert.AreEqual("var i", dochtml0head0template0Contenttemplate0Contentstyle0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TemplateNodeWithTableBeforeBodySpanText()
        {
            var doc = Html(@"<template><table></template><body><span>Foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttable0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttable0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttable0.Attributes.Count);
            Assert.AreEqual("table", dochtml0head0template0Contenttable0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttable0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1span0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1span0.NodeType);

            var dochtml0body1span0Text0 = dochtml0body1span0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1span0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1span0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeWithTdBeforeBodySpanText()
        {
            var doc = Html(@"<template><td></template><body><span>Foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contenttd0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contenttd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contenttd0.Attributes.Count);
            Assert.AreEqual("td", dochtml0head0template0Contenttd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contenttd0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1span0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1span0.NodeType);

            var dochtml0body1span0Text0 = dochtml0body1span0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1span0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1span0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeWithObjectBeforeBodySpanText()
        {
            var doc = Html(@"<template><object></template><body><span>Foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentobject0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentobject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentobject0.Attributes.Count);
            Assert.AreEqual("object", dochtml0head0template0Contentobject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentobject0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1span0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1span0.NodeType);

            var dochtml0body1span0Text0 = dochtml0body1span0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1span0Text0.NodeType);
            Assert.AreEqual("Foo", dochtml0body1span0Text0.TextContent);
        }

        [Test]
        public void TemplateNodeWithSvgAndNestedTemplate()
        {
            var doc = Html(@"<template><svg><template>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0template0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0.NodeType);

            var dochtml0head0template0Content = ((HtmlTemplateElement)dochtml0head0template0).Content;
            Assert.AreEqual(1, dochtml0head0template0Content.ChildNodes.Length);
            Assert.AreEqual(NodeType.DocumentFragment, dochtml0head0template0Content.NodeType);

            var dochtml0head0template0Contentsvg0 = dochtml0head0template0Content.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0template0Contentsvg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentsvg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0head0template0Contentsvg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentsvg0.NodeType);

            var dochtml0head0template0Contentsvg0template0 = dochtml0head0template0Contentsvg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0template0Contentsvg0template0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0template0Contentsvg0template0.Attributes.Count);
            Assert.AreEqual("template", dochtml0head0template0Contentsvg0template0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0template0Contentsvg0template0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
    }
}
