using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class HtmlWithMathML
    {
        [TestMethod]
        public void MathMLSingleElement()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [TestMethod]
        public void MathMLSingleElementInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><math></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [TestMethod]
        public void MathMLSingleElementWithChild()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math><mi>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);
        }

        [TestMethod]
        public void MathMLAnnotationXmlWithSvgInside()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math><annotation-xml><svg><u>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            var dochtml1body1math0annotationxml0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0.Attributes.Length);
            Assert.AreEqual("annotation-xml", dochtml1body1math0annotationxml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0.NodeType);

            var dochtml1body1math0annotationxml0svg0 = dochtml1body1math0annotationxml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1math0annotationxml0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0svg0.NodeType);

            var dochtml1body1u1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1u1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1u1.Attributes.Length);
            Assert.AreEqual("u", dochtml1body1u1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1u1.NodeType);
        }

        [TestMethod]
        public void MathMLElementInSelect()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><select><math></math></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);
        }

        [TestMethod]
        public void MathMLInOptionOfSelect()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><select><option><math></math></option></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0option0.Attributes.Length);
            Assert.AreEqual("option", dochtml1body1select0option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);
        }

        [TestMethod]
        public void MathMLInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLWithChildInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLWithChildrenInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi><mi>bar</mi></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLInTBodySectionOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><math><mi>foo</mi><mi>bar</mi></math></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);
        }

        [TestMethod]
        public void MathMLInRowOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><math><mi>foo</mi><mi>bar</mi></math></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [TestMethod]
        public void MathMLInCellOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math></td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLCompleteExampleInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math><p>baz</td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0p1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0p1.NodeType);

            var dochtml1body1table0tbody0tr0td0p1Text0 = dochtml1body1table0tbody0tr0td0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0tbody0tr0td0p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInCaptionOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi></math><p>baz</caption></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLImplicitlyClosedInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInCaptionImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0math0Text2 = dochtml1body1table0caption0math0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0Text2.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0math0Text2.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInColgroupOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><colgroup><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);

            var dochtml1body1table2colgroup0 = dochtml1body1table2.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table2colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml1body1table2colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table2colgroup0.NodeType);

            var dochtml1body1p3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1p3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p3.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p3.NodeType);

            var dochtml1body1p3Text0 = dochtml1body1p3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p3Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p3Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInSelectInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tr><td><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0td0select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0select0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0Text0 = dochtml1body1table0tbody0tr0td0select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1table0tbody0tr0td0select0Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInSelectInTableImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1select0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1p2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1p2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p2.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p2.NodeType);

            var dochtml1body1p2Text0 = dochtml1body1p2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p2Text0.TextContent);
        }

        [TestMethod]
        public void MathMLOutsideDocumentRoot()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body></body></html><math><mi>foo</mi><mi>bar</mi><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLOutsideDocumentImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body></body><math><mi>foo</mi><mi>bar</mi><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><frameset><math><mi></mi><mi></mi><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml1frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [TestMethod]
        public void MathMLOutsideFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><frameset></frameset><math><mi></mi><mi></mi><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml1frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [TestMethod]
        public void MathMLWithXLinkAttributes()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo><math xlink:href=foo></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var attr = dochtml1body1math0.Attributes["href"];
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual("xlink", attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceURI);
        }

        [TestMethod]
        public void MathMLInBodyWithLangAttribute()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo></mi></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual("xlink", attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceURI);

            var attr2 = dochtml1body1math0mi0.Attributes["lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceURI);
        }

        [TestMethod]
        public void MathMLWithMiChild()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo /></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual("xlink", attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceURI);

            var attr2 = dochtml1body1math0mi0.Attributes["lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceURI);
        }

        [TestMethod]
        public void MathMLWithTextNode()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo />bar</math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual("xlink", attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceURI);

            var attr2 = dochtml1body1math0mi0.Attributes["lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceURI);

            var dochtml1body1math0Text1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0Text1.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0Text1.TextContent);
        }
    }
}
