namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests21.dat
    /// </summary>
    [TestFixture]
    public class CDataTests
    {
        [Test]
        public void CDataInSvgElement()
        {
            var doc = (@"<svg><![CDATA[foo]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataInMathElement()
        {
            var doc = (@"<math><![CDATA[foo]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0Text0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1math0Text0.TextContent);
        }

        [Test]
        public void CDataInDivElement()
        {
            var doc = (@"<div><![CDATA[foo]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Comment0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0body1div0Comment0.NodeType);
            Assert.AreEqual(@"[CDATA[foo]]", dochtml0body1div0Comment0.TextContent);
        }

        [Test]
        public void CDataUnclosedInSvgElement()
        {
            var doc = (@"<svg><![CDATA[foo").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataUnclosedWithoutTextInSvgElement()
        {
            var doc = (@"<svg><![CDATA[").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);
        }

        [Test]
        public void CDataWithoutTextInSvgElement()
        {
            var doc = (@"<svg><![CDATA[]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);
        }

        [Test]
        public void CDataClosedWrongInSvgElement()
        {
            var doc = (@"<svg><![CDATA[]] >]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]] >", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataMissingClosingBracketInSvgElement()
        {
            var doc = (@"<svg><![CDATA[]]").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]]", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataMissingClosingBracketsInSvgElement()
        {
            var doc = (@"<svg><![CDATA[]").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataMissingClosingSquareBracketInSvgElement()
        {
            var doc = (@"<svg><![CDATA[]>a").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("]>a", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithAdditionalClosingSquareBracketInSvgElementWithStandardDoctype()
        {
            var doc = (@"<!DOCTYPE html><svg><![CDATA[foo]]]>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]", dochtml1body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithManyClosingSquareBracketsInSvgElementWithStandardDoctype()
        {
            var doc = (@"<!DOCTYPE html><svg><![CDATA[foo]]]]>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]]", dochtml1body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithManyMoreClosingSquareBracketsInSvgElementWithStandardDoctype()
        {
            var doc = (@"<!DOCTYPE html><svg><![CDATA[foo]]]]]>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("foo]]]", dochtml1body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataInDivLocatedInForeignObjectWhichIsPlacedInSvgElement()
        {
            var doc = (@"<svg><foreignObject><div><![CDATA[foo]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0foreignObject0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0foreignObject0).Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0foreignObject0.NodeType);

            var dochtml0body1svg0foreignObject0div0 = dochtml0body1svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0foreignObject0div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1svg0foreignObject0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0foreignObject0div0.NodeType);

            var dochtml0body1svg0foreignObject0div0Comment0 = dochtml0body1svg0foreignObject0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0body1svg0foreignObject0div0Comment0.NodeType);
            Assert.AreEqual(@"[CDATA[foo]]", dochtml0body1svg0foreignObject0div0Comment0.TextContent);
        }

        [Test]
        public void CDataWithSvgTagInSvgElement()
        {
            var doc = (@"<svg><![CDATA[<svg>]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithClosingSvgTagInSvgElement()
        {
            var doc = (@"<svg><![CDATA[</svg>a]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("</svg>a", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithSvgTagUnclosedInSvgElement()
        {
            var doc = (@"<svg><![CDATA[<svg>a").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>a", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithClosingSvgTagUnclosedInSvgElement()
        {
            var doc = (@"<svg><![CDATA[</svg>a").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("</svg>a", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithSvgTagInSvgElementFollowedByPath()
        {
            var doc = (@"<svg><![CDATA[<svg>]]><path>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

            var dochtml0body1svg0path1 = dochtml0body1svg0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0path1).Attributes.Length);
            Assert.AreEqual("path", dochtml0body1svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0path1.NodeType);
        }

        [Test]
        public void CDataWithSvgTagInSvgElementFollowedByClosingPath()
        {
            var doc = (@"<svg><![CDATA[<svg>]]></path>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithSvgTagInSvgElementFollowedByComment()
        {
            var doc = (@"<svg><![CDATA[<svg>]]><!--path-->").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>", dochtml0body1svg0Text0.TextContent);

            var dochtml0body1svg0Comment1 = dochtml0body1svg0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1svg0Comment1.NodeType);
            Assert.AreEqual(@"path", dochtml0body1svg0Comment1.TextContent);
        }

        [Test]
        public void CDataWithSvgTagInSvgElementFollowedByText()
        {
            var doc = (@"<svg><![CDATA[<svg>]]>path").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<svg>path", dochtml0body1svg0Text0.TextContent);
        }

        [Test]
        public void CDataWithCommentInSvgElement()
        {
            var doc = (@"<svg><![CDATA[<!--svg-->]]>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0Text0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1svg0Text0.NodeType);
            Assert.AreEqual("<!--svg-->", dochtml0body1svg0Text0.TextContent);
        }
    }
}