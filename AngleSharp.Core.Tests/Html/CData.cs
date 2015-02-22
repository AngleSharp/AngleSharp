using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests21.dat
    /// </summary>
    [TestFixture]
    public class CDataTests
    {
        [Test]
        public void TestMethod0()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[foo]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void TestMethod1()
        {
            var doc = DocumentBuilder.Html(@"<math><![CDATA[foo]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0Text0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1math0Text0.TextContent);

        }

        [Test]
        public void TestMethod2()
        {
            var doc = DocumentBuilder.Html(@"<div><![CDATA[foo]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Comment0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0body1div0Comment0.NodeType);
            Assert.AreEqual(@"[CDATA[foo]]", dochtml0body1div0Comment0.TextContent);

        }

        [Test]
        public void TestMethod3()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[foo");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod4()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[foo");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod5()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

        }

        [Test]
        public void TestMethod6()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

        }

        [Test]
        public void TestMethod7()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]] >]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]] >", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod8()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]] >]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]] >", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod9()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]]");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]]", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod10()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod11()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[]>a");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]>a", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod12()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><svg><![CDATA[foo]]]>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]", dochtml1body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod13()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><svg><![CDATA[foo]]]]>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]]", dochtml1body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod14()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><svg><![CDATA[foo]]]]]>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]]]", dochtml1body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod15()
        {
            var doc = DocumentBuilder.Html(@"<svg><foreignObject><div><![CDATA[foo]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0foreignObject0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1svg0foreignObject0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0foreignObject0.NodeType);

            var dochtml0body1svg0foreignObject0div0 = dochtml0body1svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0foreignObject0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1svg0foreignObject0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0foreignObject0div0.NodeType);

            var dochtml0body1svg0foreignObject0div0Comment0 = dochtml0body1svg0foreignObject0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0body1svg0foreignObject0div0Comment0.NodeType);
            Assert.AreEqual(@"[CDATA[foo]]", dochtml0body1svg0foreignObject0div0Comment0.TextContent);

        }

        [Test]
        public void TestMethod16()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod17()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[</svg>a]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("</svg>a", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod18()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>a");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>a", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod19()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[</svg>a");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("</svg>a", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod20()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>]]><path>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

            var dochtml0body1svg0path1 = dochtml0body1svg0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0path1).Attributes.Count);
            Assert.AreEqual("path", dochtml0body1svg0path1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0path1.NodeType);

        }

        [Test]
        public void TestMethod21()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>]]></path>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod22()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>]]><!--path-->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

            var dochtml0body1svg0Comment1 = dochtml0body1svg0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1svg0Comment1.NodeType);
            Assert.AreEqual(@"path", dochtml0body1svg0Comment1.TextContent);

        }

        [Test]
        public void TestMethod23()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<svg>]]>path");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>path", dochtml0body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod24()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[<!--svg-->]]>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<!--svg-->", dochtml0body1svg0Text0.TextContent);

        }

    }
}