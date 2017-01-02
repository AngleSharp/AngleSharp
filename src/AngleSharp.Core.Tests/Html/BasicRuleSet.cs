namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests1.dat
    /// </summary>
    [TestFixture]
    public class BasicRuleSetTests
    {
        [Test]
        public void ParseOnlyText()
        {
            var doc = (@"Test").ToHtmlDocument();

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("Test", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void GenerateImpliedEndTagsForParagraphs()
        {
            var doc = (@"<p>One<p>Two").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0Text0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0Text0.NodeType);
            Assert.AreEqual("One", dochtml0body1p0Text0.TextContent);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1Text0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text0.NodeType);
            Assert.AreEqual("Two", dochtml0body1p1Text0.TextContent);
        }

        [Test]
        public void SelfClosingBreakRowElements()
        {
            var doc = (@"Line1<br>Line2<br>Line3<br>Line4").ToHtmlDocument();

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
            Assert.AreEqual(7, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("Line1", dochtml0body1Text0.TextContent);

            var dochtml0body1br1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1br1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1br1).Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("Line2", dochtml0body1Text2.TextContent);

            var dochtml0body1br3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(0, dochtml0body1br3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1br3).Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br3.NodeType);

            var dochtml0body1Text4 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text4.NodeType);
            Assert.AreEqual("Line3", dochtml0body1Text4.TextContent);

            var dochtml0body1br5 = dochtml0body1.ChildNodes[5];
            Assert.AreEqual(0, dochtml0body1br5.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1br5).Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br5.NodeType);

            var dochtml0body1Text6 = dochtml0body1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text6.NodeType);
            Assert.AreEqual("Line4", dochtml0body1Text6.TextContent);
        }

        [Test]
        public void JustASingleRootElement()
        {
            var doc = (@"<html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void JustASingleHeadElement()
        {
            var doc = (@"<head>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void JustASingleBodyElement()
        {
            var doc = (@"<body>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void OpenHeadInHtmlElement()
        {
            var doc = (@"<html><head>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void ClosedHeadInHtmlElement()
        {
            var doc = (@"<html><head></head>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void OpenBodyInHtmlWithClosedHeadElement()
        {
            var doc = (@"<html><head></head><body>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void BodyInHtmlWithClosedHeadElement()
        {
            var doc = (@"<html><head></head><body></body>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void DocumentWithRootUnclosedHeadAndBody()
        {
            var doc = (@"<html><head><body></body></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void DocumentWithRootUnclosedHeadWronglyClosedBody()
        {
            var doc = (@"<html><head></body></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void HtmlWithOpenHeadAndBody()
        {
            var doc = (@"<html><head><body></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void HtmlWithOpenBody()
        {
            var doc = (@"<html><body></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void BodyClosedByHtml()
        {
            var doc = (@"<body></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void HeadClosedByHtml()
        {
            var doc = (@"<head></html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void JustClosedHead()
        {
            var doc = (@"</head>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void JustClosedBody()
        {
            var doc = (@"</body>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void JustClosedHtml()
        {
            var doc = (@"</html>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void FormattingBoldTableWithCellAndItalic()
        {
            var doc = (@"<b><table><td><i></table>").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0table0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1b0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0.NodeType);

            var dochtml0body1b0table0tbody0 = dochtml0body1b0table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1b0table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0.NodeType);

            var dochtml0body1b0table0tbody0tr0 = dochtml0body1b0table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1b0table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0 = dochtml0body1b0table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1b0table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0i0 = dochtml0body1b0table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0table0tbody0tr0td0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1b0table0tbody0tr0td0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0i0.NodeType);
        }

        [Test]
        public void FormattingBoldClosedInTableWithCellAndItalic()
        {
            var doc = (@"<b><table><td></b><i></table>X").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0table0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1b0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0.NodeType);

            var dochtml0body1b0table0tbody0 = dochtml0body1b0table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1b0table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0.NodeType);

            var dochtml0body1b0table0tbody0tr0 = dochtml0body1b0table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1b0table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0 = dochtml0body1b0table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1b0table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0i0 = dochtml0body1b0table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0table0tbody0tr0td0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1b0table0tbody0tr0td0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0i0.NodeType);

            var dochtml0body1b0Text1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text1.NodeType);
            Assert.AreEqual("X", dochtml0body1b0Text1.TextContent);
        }

        [Test]
        public void HeadingNotClosedFollowedByAnotherHeading()
        {
            var doc = (@"<h1>Hello<h2>World").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1h10 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10).Attributes.Length);
            Assert.AreEqual("h1", dochtml0body1h10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10.NodeType);

            var dochtml0body1h10Text0 = dochtml0body1h10.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1h10Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1h10Text0.TextContent);

            var dochtml0body1h21 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1h21.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h21).Attributes.Length);
            Assert.AreEqual("h2", dochtml0body1h21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h21.NodeType);

            var dochtml0body1h21Text0 = dochtml0body1h21.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1h21Text0.NodeType);
            Assert.AreEqual("World", dochtml0body1h21Text0.TextContent);
        }

        [Test]
        public void AnchorElementsAccumulated()
        {
            var doc = (@"<a><p>X<a>Y</a>Z</p></a>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1a0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a0.NodeType);

            var dochtml0body1p1a0Text0 = dochtml0body1p1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1a0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1p1a0Text0.TextContent);

            var dochtml0body1p1a1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a1.NodeType);

            var dochtml0body1p1a1Text0 = dochtml0body1p1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1a1Text0.NodeType);
            Assert.AreEqual("Y", dochtml0body1p1a1Text0.TextContent);

            var dochtml0body1p1Text2 = dochtml0body1p1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text2.NodeType);
            Assert.AreEqual("Z", dochtml0body1p1Text2.TextContent);
        }

        [Test]
        public void BoldElementsAccumulated()
        {
            var doc = (@"<b><button>foo</b>bar").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1button1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1button1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1).Attributes.Length);
            Assert.AreEqual("button", dochtml0body1button1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1.NodeType);

            var dochtml0body1button1b0 = dochtml0body1button1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1button1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1button1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1b0.NodeType);

            var dochtml0body1button1b0Text0 = dochtml0body1button1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1button1b0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1button1b0Text0.TextContent);

            var dochtml0body1button1Text1 = dochtml0body1button1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1button1Text1.NodeType);
            Assert.AreEqual("bar", dochtml0body1button1Text1.TextContent);
        }

        [Test]
        public void GeneratImpliedEndTagForSpanByButtonElement()
        {
            var doc = (@"<!DOCTYPE html><span><button>foo</span>bar").ToHtmlDocument();

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

            var dochtml1body1span0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1span0).Attributes.Length);
            Assert.AreEqual("span", dochtml1body1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1span0.NodeType);

            var dochtml1body1span0button0 = dochtml1body1span0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1span0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1span0button0).Attributes.Length);
            Assert.AreEqual("button", dochtml1body1span0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1span0button0.NodeType);

            var dochtml1body1span0button0Text0 = dochtml1body1span0button0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1span0button0Text0.NodeType);
            Assert.AreEqual("foobar", dochtml1body1span0button0Text0.TextContent);

        }

        [Test]
        public void LegacyMarqueeElementInDiv()
        {
            var doc = (@"<p><b><div><marquee></p></b></div>X").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1b0marquee0 = dochtml0body1div1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div1b0marquee0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0marquee0).Attributes.Length);
            Assert.AreEqual("marquee", dochtml0body1div1b0marquee0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0marquee0.NodeType);

            var dochtml0body1div1b0marquee0p0 = dochtml0body1div1b0marquee0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1b0marquee0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0marquee0p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div1b0marquee0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0marquee0p0.NodeType);

            var dochtml0body1div1b0marquee0Text1 = dochtml0body1div1b0marquee0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1b0marquee0Text1.NodeType);
            Assert.AreEqual("X", dochtml0body1div1b0marquee0Text1.TextContent);
        }

        [Test]
        public void DivPlacedInScriptElementAndParagraphPlacedInTitleElement()
        {
            var doc = (@"<script><div></script></div><title><p></title><p><p>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0script0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script0).Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);

            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<div>", dochtml0head0script0Text0.TextContent);

            var dochtml0head0title1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0head0title1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title1).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title1.NodeType);

            var dochtml0head0title1Text0 = dochtml0head0title1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title1Text0.NodeType);
            Assert.AreEqual("<p>", dochtml0head0title1Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);
        }

        [Test]
        public void IllegalCommentsAroundDivElement()
        {
            var doc = (@"<!--><div>--<!-->").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"", docComment0.TextContent);


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

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0Text0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0Text0.NodeType);
            Assert.AreEqual("--", dochtml1body1div0Text0.TextContent);

            var dochtml1body1div0Comment1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1body1div0Comment1.NodeType);
            Assert.AreEqual(@"", dochtml1body1div0Comment1.TextContent);
        }

        [Test]
        public void GenerateImpliedEndTagsByTheHorizontalLine()
        {
            var doc = (@"<p><hr></p>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1hr1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1hr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1hr1).Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1hr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1hr1.NodeType);

            var dochtml0body1p2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1p2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p2).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p2.NodeType);
        }

        [Test]
        public void FosterParentingActiveInSelectElement()
        {
            var doc = (@"<select><b><option><select><option></b></select>X").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1select0).Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1select0option0).Attributes.Length);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1option1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1option1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option1).Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1.NodeType);

            var dochtml0body1option1Text0 = dochtml0body1option1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1option1Text0.TextContent);
        }

        [Test]
        public void FormattingElementsInTableWithFosterParenting()
        {
            var doc = (@"<a><table><td><a><table></table><a></tr><a></table><b>X</b>C<a>Y").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0a0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0a0.NodeType);

            var dochtml0body1a0table1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1.NodeType);

            var dochtml0body1a0table1tbody0 = dochtml0body1a0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1a0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0.NodeType);

            var dochtml0body1a0table1tbody0tr0 = dochtml0body1a0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1a0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0 = dochtml0body1a0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1a0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a0 = dochtml0body1a0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0td0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0table1tbody0tr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a0table0 = dochtml0body1a0table1tbody0tr0td0a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0table1tbody0tr0td0a0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1tbody0tr0td0a0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a0table0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a1 = dochtml0body1a0table1tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1a0table1tbody0tr0td0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0table1tbody0tr0td0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a1.NodeType);

            var dochtml0body1a1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1.NodeType);

            var dochtml0body1a1b0 = dochtml0body1a1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1b0.NodeType);

            var dochtml0body1a1b0Text0 = dochtml0body1a1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a1b0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1a1b0Text0.TextContent);

            var dochtml0body1a1Text1 = dochtml0body1a1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1a1Text1.NodeType);
            Assert.AreEqual("C", dochtml0body1a1Text1.TextContent);

            var dochtml0body1a2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1a2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a2).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a2.NodeType);

            var dochtml0body1a2Text0 = dochtml0body1a2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a2Text0.NodeType);
            Assert.AreEqual("Y", dochtml0body1a2Text0.TextContent);
        }

        [Test]
        public void UnknownElementsWithAttributesWithoutValues()
        {
            var doc = (@"<a X>0<b>1<a Y>2").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0).GetAttribute("x"));
            Assert.AreEqual("", ((Element)dochtml0body1a0).GetAttribute("x"));

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("0", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a0b1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b1.NodeType);

            var dochtml0body1a0b1Text0 = dochtml0body1a0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0b1Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0b1Text0.TextContent);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1a0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1b1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1b1a0).GetAttribute("y"));
            Assert.AreEqual("", ((Element)dochtml0body1b1a0).GetAttribute("y"));

            var dochtml0body1b1a0Text0 = dochtml0body1b1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1a0Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1b1a0Text0.TextContent);
        }

        [Test]
        public void CommentsAndTextMixedWithFosterParenting()
        {
            var doc = (@"<!-----><font><div>hello<table>excite!<b>me!<th><i>please!</tr><!--X-->").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"-", docComment0.TextContent);


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

            var dochtml1body1font0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1font0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0).Attributes.Length);
            Assert.AreEqual("font", dochtml1body1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0.NodeType);

            var dochtml1body1font0div0 = dochtml1body1font0.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1font0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0).Attributes.Length);
            Assert.AreEqual("div", dochtml1body1font0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0.NodeType);

            var dochtml1body1font0div0Text0 = dochtml1body1font0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1font0div0Text0.NodeType);
            Assert.AreEqual("helloexcite!", dochtml1body1font0div0Text0.TextContent);

            var dochtml1body1font0div0b1 = dochtml1body1font0div0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1font0div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml1body1font0div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0b1.NodeType);

            var dochtml1body1font0div0b1Text0 = dochtml1body1font0div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1font0div0b1Text0.NodeType);
            Assert.AreEqual("me!", dochtml1body1font0div0b1Text0.TextContent);

            var dochtml1body1font0div0table2 = dochtml1body1font0div0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1font0div0table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0table2).Attributes.Length);
            Assert.AreEqual("table", dochtml1body1font0div0table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0table2.NodeType);

            var dochtml1body1font0div0table2tbody0 = dochtml1body1font0div0table2.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1font0div0table2tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0table2tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1font0div0table2tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0table2tbody0.NodeType);

            var dochtml1body1font0div0table2tbody0tr0 = dochtml1body1font0div0table2tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1font0div0table2tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0table2tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1font0div0table2tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0table2tbody0tr0.NodeType);

            var dochtml1body1font0div0table2tbody0tr0th0 = dochtml1body1font0div0table2tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1font0div0table2tbody0tr0th0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0table2tbody0tr0th0).Attributes.Length);
            Assert.AreEqual("th", dochtml1body1font0div0table2tbody0tr0th0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0table2tbody0tr0th0.NodeType);

            var dochtml1body1font0div0table2tbody0tr0th0i0 = dochtml1body1font0div0table2tbody0tr0th0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1font0div0table2tbody0tr0th0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0div0table2tbody0tr0th0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml1body1font0div0table2tbody0tr0th0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0div0table2tbody0tr0th0i0.NodeType);

            var dochtml1body1font0div0table2tbody0tr0th0i0Text0 = dochtml1body1font0div0table2tbody0tr0th0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1font0div0table2tbody0tr0th0i0Text0.NodeType);
            Assert.AreEqual("please!", dochtml1body1font0div0table2tbody0tr0th0i0Text0.TextContent);

            var dochtml1body1font0div0table2tbody0Comment1 = dochtml1body1font0div0table2tbody0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1body1font0div0table2tbody0Comment1.NodeType);
            Assert.AreEqual(@"X", dochtml1body1font0div0table2tbody0Comment1.TextContent);
        }

        [Test]
        public void ListElementsWithoutListContainer()
        {
            var doc = (@"<!DOCTYPE html><li>hello<li>world<ul>how<li>do</ul>you</body><!--do-->").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
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

            var dochtml1body1li0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1li0).Attributes.Length);
            Assert.AreEqual("li", dochtml1body1li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1li0.NodeType);

            var dochtml1body1li0Text0 = dochtml1body1li0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1li0Text0.NodeType);
            Assert.AreEqual("hello", dochtml1body1li0Text0.TextContent);

            var dochtml1body1li1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1li1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1li1).Attributes.Length);
            Assert.AreEqual("li", dochtml1body1li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1li1.NodeType);

            var dochtml1body1li1Text0 = dochtml1body1li1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1li1Text0.NodeType);
            Assert.AreEqual("world", dochtml1body1li1Text0.TextContent);

            var dochtml1body1li1ul1 = dochtml1body1li1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1li1ul1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1li1ul1).Attributes.Length);
            Assert.AreEqual("ul", dochtml1body1li1ul1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1li1ul1.NodeType);

            var dochtml1body1li1ul1Text0 = dochtml1body1li1ul1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1li1ul1Text0.NodeType);
            Assert.AreEqual("how", dochtml1body1li1ul1Text0.TextContent);

            var dochtml1body1li1ul1li1 = dochtml1body1li1ul1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1li1ul1li1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1li1ul1li1).Attributes.Length);
            Assert.AreEqual("li", dochtml1body1li1ul1li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1li1ul1li1.NodeType);

            var dochtml1body1li1ul1li1Text0 = dochtml1body1li1ul1li1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1li1ul1li1Text0.NodeType);
            Assert.AreEqual("do", dochtml1body1li1ul1li1Text0.TextContent);

            var dochtml1body1li1Text2 = dochtml1body1li1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1li1Text2.NodeType);
            Assert.AreEqual("you", dochtml1body1li1Text2.TextContent);

            var dochtml1Comment2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml1Comment2.NodeType);
            Assert.AreEqual(@"do", dochtml1Comment2.TextContent);
        }

        [Test]
        public void OptionElementsWithSelectContainer()
        {
            var doc = (@"<!DOCTYPE html>A<option>B<optgroup>C<select>D</option>E").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("A", dochtml1body1Text0.TextContent);

            var dochtml1body1option1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1option1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1option1).Attributes.Length);
            Assert.AreEqual("option", dochtml1body1option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1option1.NodeType);

            var dochtml1body1option1Text0 = dochtml1body1option1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1option1Text0.NodeType);
            Assert.AreEqual("B", dochtml1body1option1Text0.TextContent);

            var dochtml1body1optgroup2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1optgroup2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1optgroup2).Attributes.Length);
            Assert.AreEqual("optgroup", dochtml1body1optgroup2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1optgroup2.NodeType);

            var dochtml1body1optgroup2Text0 = dochtml1body1optgroup2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1optgroup2Text0.NodeType);
            Assert.AreEqual("C", dochtml1body1optgroup2Text0.TextContent);

            var dochtml1body1optgroup2select1 = dochtml1body1optgroup2.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1optgroup2select1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1optgroup2select1).Attributes.Length);
            Assert.AreEqual("select", dochtml1body1optgroup2select1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1optgroup2select1.NodeType);

            var dochtml1body1optgroup2select1Text0 = dochtml1body1optgroup2select1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1optgroup2select1Text0.NodeType);
            Assert.AreEqual("DE", dochtml1body1optgroup2select1Text0.TextContent);
        }

        [Test]
        public void SingleLeftAngleBracketEvaluatedAsText()
        {
            var doc = (@"<").ToHtmlDocument();

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("<", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void SingleLeftAngleBracketAndHashEvaluatedAsText()
        {
            var doc = (@"<#").ToHtmlDocument();

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("<#", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void SingleLeftAngleBracketAndSlashEvaluatedAsText()
        {
            var doc = (@"</").ToHtmlDocument();

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("</", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void SingleLeftAngleBracketAndSlashAndHashEvaluatedAsElement()
        {
            var doc = (@"</#").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"#", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void SingleLeftAngleBracketAndSlashAndQuestionMarkEvaluatedAsBogusComment()
        {
            var doc = (@"<?").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void SingleLeftAngleBracketAndQuestionMarkAndHashEvaluatedAsBogusComment()
        {
            var doc = (@"<?#").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?#", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void SingleLeftAngleBracketAndExclamationMarkEvaluatedAsBogusComment()
        {
            var doc = (@"<!").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void SingleLeftAngleBracketAndExclamationMarkAndHashEvaluatedAsBogusComment()
        {
            var doc = (@"<!#").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"#", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaProcessing()
        {
            var doc = (@"<?COMMENT?>").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?COMMENT?", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaExclamationMark()
        {
            var doc = (@"<!COMMENT>").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"COMMENT", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaClosedElement()
        {
            var doc = (@"</ COMMENT >").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@" COMMENT ", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaProcessingWithDashes()
        {
            var doc = (@"<?COM--MENT?>").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?COM--MENT?", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaExclamationMarkWithDashes()
        {
            var doc = (@"<!COM--MENT>").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"COM--MENT", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void BogusCommentViaClosedElementWithDashes()
        {
            var doc = (@"</ COM--MENT >").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@" COM--MENT ", docComment0.TextContent);

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
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void Html5DoctypeWithOpenStyleTagThatContainsText()
        {
            var doc = (@"<!DOCTYPE html><style> EOF").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual(" EOF", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void Html5DoctypeWithScriptTagContainingACommentReadAsText()
        {
            var doc = (@"<!DOCTYPE html><script> <!-- </script> --> </script> EOF").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0script0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0script0).Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);

            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual(" <!-- ", dochtml1head0script0Text0.TextContent);

            var dochtml1head0Text1 = dochtml1head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1head0Text1.NodeType);
            Assert.AreEqual(" ", dochtml1head0Text1.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->  EOF", dochtml1body1Text0.TextContent);
        }

        [Test]
        public void BoldFormattingTakenIntoNewParagraph()
        {
            var doc = (@"<b><p></b>TEST").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1b0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0.NodeType);

            var dochtml0body1p1Text1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text1.NodeType);
            Assert.AreEqual("TEST", dochtml0body1p1Text1.TextContent);
        }

        [Test]
        public void ParagraphsWithIdsAndBoldFormattingPassed()
        {
            var doc = (@"<p id=a><b><p id=b></b>TEST").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1p0).GetAttribute("id"));
            Assert.AreEqual("a", ((Element)dochtml0body1p0).GetAttribute("id"));

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1p1).GetAttribute("id"));
            Assert.AreEqual("b", ((Element)dochtml0body1p1).GetAttribute("id"));

            var dochtml0body1p1Text0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text0.NodeType);
            Assert.AreEqual("TEST", dochtml0body1p1Text0.TextContent);
        }

        [Test]
        public void BoldFormattingsWithIdsTakenIntoAParagraph()
        {
            var doc = (@"<b id=a><p><b id=b></p></b>TEST").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1b0).GetAttribute("id"));
            Assert.AreEqual("a", ((Element)dochtml0body1b0).GetAttribute("id"));

            var dochtml0body1b0p0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1b0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0p0.NodeType);

            var dochtml0body1b0p0b0 = dochtml0body1b0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0p0b0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1b0p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0p0b0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1b0p0b0).GetAttribute("id"));
            Assert.AreEqual("b", ((Element)dochtml0body1b0p0b0).GetAttribute("id"));

            var dochtml0body1b0Text1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text1.NodeType);
            Assert.AreEqual("TEST", dochtml0body1b0Text1.TextContent);
        }

        [Test]
        public void UnderlineFormattingClosedWithParagraph()
        {
            var doc = (@"<!DOCTYPE html><title>U-test</title><body><div><p>Test<u></p></div></body>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0title0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml1head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0title0.NodeType);

            var dochtml1head0title0Text0 = dochtml1head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0title0Text0.NodeType);
            Assert.AreEqual("U-test", dochtml1head0title0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0p0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0p0).Attributes.Length);
            Assert.AreEqual("p", dochtml1body1div0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0p0.NodeType);

            var dochtml1body1div0p0Text0 = dochtml1body1div0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0p0Text0.NodeType);
            Assert.AreEqual("Test", dochtml1body1div0p0Text0.TextContent);

            var dochtml1body1div0p0u1 = dochtml1body1div0p0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div0p0u1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0p0u1).Attributes.Length);
            Assert.AreEqual("u", dochtml1body1div0p0u1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0p0u1.NodeType);
        }

        [Test]
        public void FontFormattingClosedPriorToTable()
        {
            var doc = (@"<!DOCTYPE html><font><table></font></table></font>").ToHtmlDocument();

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

            var dochtml1body1font0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1font0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0).Attributes.Length);
            Assert.AreEqual("font", dochtml1body1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0.NodeType);

            var dochtml1body1font0table0 = dochtml1body1font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1font0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1font0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml1body1font0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1font0table0.NodeType);
        }

        [Test]
        public void FontFormattingTakenIntoNewParagraph()
        {
            var doc = (@"<font><p>hello<b>cruel</font>world").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1font0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1font0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1font0).Attributes.Length);
            Assert.AreEqual("font", dochtml0body1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1font0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1font0).Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var dochtml0body1p1font0Text0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0Text0.NodeType);
            Assert.AreEqual("hello", dochtml0body1p1font0Text0.TextContent);

            var dochtml0body1p1font0b1 = dochtml0body1p1font0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1font0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1font0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1font0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0b1.NodeType);

            var dochtml0body1p1font0b1Text0 = dochtml0body1p1font0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1font0b1Text0.NodeType);
            Assert.AreEqual("cruel", dochtml0body1p1font0b1Text0.TextContent);

            var dochtml0body1p1b1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b1.NodeType);

            var dochtml0body1p1b1Text0 = dochtml0body1p1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1b1Text0.NodeType);
            Assert.AreEqual("world", dochtml0body1p1b1Text0.TextContent);
        }

        [Test]
        public void WronglyClosedItalicFormattingIgnored()
        {
            var doc = (@"<b>Test</i>Test").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("TestTest", dochtml0body1b0Text0.TextContent);
        }

        [Test]
        public void GenerateImpliedEndTagForCiteElementFollowedByDiv()
        {
            var doc = (@"<b>A<cite>B<div>C").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0cite1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b0cite1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0cite1).Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1b0cite1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0cite1.NodeType);

            var dochtml0body1b0cite1Text0 = dochtml0body1b0cite1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0cite1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1b0cite1Text0.TextContent);

            var dochtml0body1b0cite1div1 = dochtml0body1b0cite1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0cite1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0cite1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b0cite1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0cite1div1.NodeType);

            var dochtml0body1b0cite1div1Text0 = dochtml0body1b0cite1div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0cite1div1Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1b0cite1div1Text0.TextContent);
        }

        [Test]
        public void IgnoreClosedCiteTagWhenImpliedEndTagIsGenerated()
        {
            var doc = (@"<b>A<cite>B<div>C</cite>D").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0cite1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1b0cite1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0cite1).Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1b0cite1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0cite1.NodeType);

            var dochtml0body1b0cite1Text0 = dochtml0body1b0cite1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0cite1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1b0cite1Text0.TextContent);

            var dochtml0body1b0cite1div1 = dochtml0body1b0cite1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0cite1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0cite1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1b0cite1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0cite1div1.NodeType);

            var dochtml0body1b0cite1div1Text0 = dochtml0body1b0cite1div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0cite1div1Text0.NodeType);
            Assert.AreEqual("CD", dochtml0body1b0cite1div1Text0.TextContent);
        }

        [Test]
        public void GenerateImpliedEndTagForBoldAndCiteWhenEncounteringDiv()
        {
            var doc = (@"<b>A<cite>B<div>C</b>D").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0Text0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0cite1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0cite1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0cite1).Attributes.Length);
            Assert.AreEqual("cite", dochtml0body1b0cite1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0cite1.NodeType);

            var dochtml0body1b0cite1Text0 = dochtml0body1b0cite1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0cite1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1b0cite1Text0.TextContent);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1b0Text0 = dochtml0body1div1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1b0Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1div1b0Text0.TextContent);

            var dochtml0body1div1Text1 = dochtml0body1div1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1Text1.NodeType);
            Assert.AreEqual("D", dochtml0body1div1Text1.TextContent);
        }

        [Test]
        public void EmptySource()
        {
            var doc = (@"").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void LegacyUppercaseOpeningDivElement()
        {
            var doc = (@"<DIV>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
        }

        [Test]
        public void LegacyUppercaseElementsOpenDivAndText()
        {
            var doc = (@"<DIV> abc").ToHtmlDocument();

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

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc", dochtml0body1div0Text0.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsOpeningBold()
        {
            var doc = (@"<DIV> abc <B>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);
        }

        [Test]
        public void LegacyUppercaseElementsOpenBold()
        {
            var doc = (@"<DIV> abc <B> def").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def", dochtml0body1div0b1Text0.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsOpeningItalics()
        {
            var doc = (@"<DIV> abc <B> def <I>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);
        }

        [Test]
        public void LegacyUppercaseElementsWithNewFormatting()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi", dochtml0body1div0b1i1Text0.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsBoldAndItalicFormatting()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0b1i1p1 = dochtml0body1div0b1i1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0b1i1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0b1i1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1p1.NodeType);
        }

        [Test]
        public void LegacyUppercaseElementsFormattingCopiedToParagraph()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0b1i1p1 = dochtml0body1div0b1i1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0b1i1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1p1.NodeType);

            var dochtml0body1div0b1i1p1Text0 = dochtml0body1div0b1i1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1p1Text0.NodeType);
            Assert.AreEqual(" jkl", dochtml0body1div0b1i1p1Text0.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsWithoutClosingTag()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0i2p0 = dochtml0body1div0i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0i2p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0i2p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2p0.NodeType);

            var dochtml0body1div0i2p0b0 = dochtml0body1div0i2p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0i2p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0i2p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2p0b0.NodeType);

            var dochtml0body1div0i2p0b0Text0 = dochtml0body1div0i2p0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0i2p0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0i2p0b0Text0.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsFormattingClosedIncorrectly()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B> mno").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0i2p0 = dochtml0body1div0i2.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0i2p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0i2p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2p0.NodeType);

            var dochtml0body1div0i2p0b0 = dochtml0body1div0i2p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0i2p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0i2p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2p0b0.NodeType);

            var dochtml0body1div0i2p0b0Text0 = dochtml0body1div0i2p0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0i2p0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0i2p0b0Text0.TextContent);

            var dochtml0body1div0i2p0Text1 = dochtml0body1div0i2p0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0i2p0Text1.NodeType);
            Assert.AreEqual(" mno", dochtml0body1div0i2p0Text1.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsFormattingApplied()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B> mno </I>").ToHtmlDocument();

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
            Assert.AreEqual(4, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0p3 = dochtml0body1div0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1div0p3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3.NodeType);

            var dochtml0body1div0p3i0 = dochtml0body1div0p3.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0p3i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0p3i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0.NodeType);

            var dochtml0body1div0p3i0b0 = dochtml0body1div0p3i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0p3i0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0p3i0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0b0.NodeType);

            var dochtml0body1div0p3i0b0Text0 = dochtml0body1div0p3i0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0p3i0b0Text0.TextContent);

            var dochtml0body1div0p3i0Text1 = dochtml0body1div0p3i0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0Text1.NodeType);
            Assert.AreEqual(" mno ", dochtml0body1div0p3i0Text1.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsFormattingCopied()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B> mno </I> pqr").ToHtmlDocument();

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
            Assert.AreEqual(4, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0p3 = dochtml0body1div0.ChildNodes[3];
            Assert.AreEqual(2, dochtml0body1div0p3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3.NodeType);

            var dochtml0body1div0p3i0 = dochtml0body1div0p3.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0p3i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0p3i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0.NodeType);

            var dochtml0body1div0p3i0b0 = dochtml0body1div0p3i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0p3i0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0p3i0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0b0.NodeType);

            var dochtml0body1div0p3i0b0Text0 = dochtml0body1div0p3i0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0p3i0b0Text0.TextContent);

            var dochtml0body1div0p3i0Text1 = dochtml0body1div0p3i0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0Text1.NodeType);
            Assert.AreEqual(" mno ", dochtml0body1div0p3i0Text1.TextContent);

            var dochtml0body1div0p3Text1 = dochtml0body1div0p3.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3Text1.NodeType);
            Assert.AreEqual(" pqr", dochtml0body1div0p3Text1.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementsWithText()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B> mno </I> pqr </P>").ToHtmlDocument();

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
            Assert.AreEqual(4, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0p3 = dochtml0body1div0.ChildNodes[3];
            Assert.AreEqual(2, dochtml0body1div0p3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3.NodeType);

            var dochtml0body1div0p3i0 = dochtml0body1div0p3.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0p3i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0p3i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0.NodeType);

            var dochtml0body1div0p3i0b0 = dochtml0body1div0p3i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0p3i0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0p3i0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0b0.NodeType);

            var dochtml0body1div0p3i0b0Text0 = dochtml0body1div0p3i0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0p3i0b0Text0.TextContent);

            var dochtml0body1div0p3i0Text1 = dochtml0body1div0p3i0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0Text1.NodeType);
            Assert.AreEqual(" mno ", dochtml0body1div0p3i0Text1.TextContent);

            var dochtml0body1div0p3Text1 = dochtml0body1div0p3.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3Text1.NodeType);
            Assert.AreEqual(" pqr ", dochtml0body1div0p3Text1.TextContent);
        }

        [Test]
        public void LegacyUppercaseElementNamesWithText()
        {
            var doc = (@"<DIV> abc <B> def <I> ghi <P> jkl </B> mno </I> pqr </P> stu").ToHtmlDocument();

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
            Assert.AreEqual(5, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual(" abc ", dochtml0body1div0Text0.TextContent);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1Text0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1Text0.NodeType);
            Assert.AreEqual(" def ", dochtml0body1div0b1Text0.TextContent);

            var dochtml0body1div0b1i1 = dochtml0body1div0b1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0b1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1i1.NodeType);

            var dochtml0body1div0b1i1Text0 = dochtml0body1div0b1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0b1i1Text0.NodeType);
            Assert.AreEqual(" ghi ", dochtml0body1div0b1i1Text0.TextContent);

            var dochtml0body1div0i2 = dochtml0body1div0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1div0i2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0i2).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0i2.NodeType);

            var dochtml0body1div0p3 = dochtml0body1div0.ChildNodes[3];
            Assert.AreEqual(2, dochtml0body1div0p3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3.NodeType);

            var dochtml0body1div0p3i0 = dochtml0body1div0p3.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0p3i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0p3i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0.NodeType);

            var dochtml0body1div0p3i0b0 = dochtml0body1div0p3i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0p3i0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0p3i0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0p3i0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0p3i0b0.NodeType);

            var dochtml0body1div0p3i0b0Text0 = dochtml0body1div0p3i0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0b0Text0.NodeType);
            Assert.AreEqual(" jkl ", dochtml0body1div0p3i0b0Text0.TextContent);

            var dochtml0body1div0p3i0Text1 = dochtml0body1div0p3i0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3i0Text1.NodeType);
            Assert.AreEqual(" mno ", dochtml0body1div0p3i0Text1.TextContent);

            var dochtml0body1div0p3Text1 = dochtml0body1div0p3.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0p3Text1.NodeType);
            Assert.AreEqual(" pqr ", dochtml0body1div0p3Text1.TextContent);

            var dochtml0body1div0Text4 = dochtml0body1div0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text4.NodeType);
            Assert.AreEqual(" stu", dochtml0body1div0Text4.TextContent);
        }

        [Test]
        public void ValidDashesInAttributeName()
        {
            var doc = (@"<test attribute---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------->").ToHtmlDocument();

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

            var dochtml0body1test0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1test0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1test0).Attributes.Length);
            Assert.AreEqual("test", dochtml0body1test0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1test0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1test0).GetAttribute("attribute----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            Assert.AreEqual("", ((Element)dochtml0body1test0).GetAttribute("attribute----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------"));
        }

        [Test]
        public void FosterAnchorTagAndTextInTableElement()
        {
            var doc = (@"<a href=""blah"">aba<table><a href=""foo"">br<tr><td></td></tr>x</table>aoe").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(4, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0).GetAttribute("href"));
            Assert.AreEqual("blah", ((Element)dochtml0body1a0).GetAttribute("href"));

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("aba", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a0a1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0a1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0a1).GetAttribute("href"));
            Assert.AreEqual("foo", ((Element)dochtml0body1a0a1).GetAttribute("href"));

            var dochtml0body1a0a1Text0 = dochtml0body1a0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0a1Text0.NodeType);
            Assert.AreEqual("br", dochtml0body1a0a1Text0.TextContent);

            var dochtml0body1a0a2 = dochtml0body1a0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1a0a2.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0a2).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0a2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0a2.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0a2).GetAttribute("href"));
            Assert.AreEqual("foo", ((Element)dochtml0body1a0a2).GetAttribute("href"));

            var dochtml0body1a0a2Text0 = dochtml0body1a0a2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0a2Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1a0a2Text0.TextContent);

            var dochtml0body1a0table3 = dochtml0body1a0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1a0table3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table3).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table3.NodeType);

            var dochtml0body1a0table3tbody0 = dochtml0body1a0table3.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table3tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table3tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1a0table3tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table3tbody0.NodeType);

            var dochtml0body1a0table3tbody0tr0 = dochtml0body1a0table3tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table3tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table3tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1a0table3tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table3tbody0tr0.NodeType);

            var dochtml0body1a0table3tbody0tr0td0 = dochtml0body1a0table3tbody0tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0table3tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table3tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1a0table3tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table3tbody0tr0td0.NodeType);

            var dochtml0body1a1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a1).GetAttribute("href"));
            Assert.AreEqual("foo", ((Element)dochtml0body1a1).GetAttribute("href"));

            var dochtml0body1a1Text0 = dochtml0body1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a1Text0.NodeType);
            Assert.AreEqual("aoe", dochtml0body1a1Text0.TextContent);
        }

        [Test]
        public void TableWithCorrectAnchorTagAndFosterText()
        {
            var doc = (@"<a href=""blah"">aba<table><tr><td><a href=""foo"">br</td></tr>x</table>aoe").ToHtmlDocument();

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

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0).GetAttribute("href"));
            Assert.AreEqual("blah", ((Element)dochtml0body1a0).GetAttribute("href"));

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("abax", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a0table1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1.NodeType);

            var dochtml0body1a0table1tbody0 = dochtml0body1a0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1a0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0.NodeType);

            var dochtml0body1a0table1tbody0tr0 = dochtml0body1a0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1a0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0 = dochtml0body1a0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1a0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a0 = dochtml0body1a0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0td0a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0table1tbody0tr0td0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0table1tbody0tr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0table1tbody0tr0td0a0).GetAttribute("href"));
            Assert.AreEqual("foo", ((Element)dochtml0body1a0table1tbody0tr0td0a0).GetAttribute("href"));

            var dochtml0body1a0table1tbody0tr0td0a0Text0 = dochtml0body1a0table1tbody0tr0td0a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0table1tbody0tr0td0a0Text0.NodeType);
            Assert.AreEqual("br", dochtml0body1a0table1tbody0tr0td0a0Text0.TextContent);

            var dochtml0body1a0Text2 = dochtml0body1a0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text2.NodeType);
            Assert.AreEqual("aoe", dochtml0body1a0Text2.TextContent);
        }

        [Test]
        public void TableFosterParentingOfAnchorElements()
        {
            var doc = (@"<table><a href=""blah"">aba<tr><td><a href=""foo"">br</td></tr>x</table>aoe").ToHtmlDocument();

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
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0).GetAttribute("href"));
            Assert.AreEqual("blah", ((Element)dochtml0body1a0).GetAttribute("href"));

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("aba", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a1).GetAttribute("href"));
            Assert.AreEqual("blah", ((Element)dochtml0body1a1).GetAttribute("href"));

            var dochtml0body1a1Text0 = dochtml0body1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1a1Text0.TextContent);

            var dochtml0body1table2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table2).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2.NodeType);

            var dochtml0body1table2tbody0 = dochtml0body1table2.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table2tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table2tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table2tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0.NodeType);

            var dochtml0body1table2tbody0tr0 = dochtml0body1table2tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table2tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table2tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table2tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0tr0.NodeType);

            var dochtml0body1table2tbody0tr0td0 = dochtml0body1table2tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table2tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table2tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table2tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0tr0td0.NodeType);

            var dochtml0body1table2tbody0tr0td0a0 = dochtml0body1table2tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table2tbody0tr0td0a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1table2tbody0tr0td0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1table2tbody0tr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2tbody0tr0td0a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1table2tbody0tr0td0a0).GetAttribute("href"));
            Assert.AreEqual("foo", ((Element)dochtml0body1table2tbody0tr0td0a0).GetAttribute("href"));

            var dochtml0body1table2tbody0tr0td0a0Text0 = dochtml0body1table2tbody0tr0td0a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table2tbody0tr0td0a0Text0.NodeType);
            Assert.AreEqual("br", dochtml0body1table2tbody0tr0td0a0Text0.TextContent);

            var dochtml0body1a3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1a3.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a3).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a3.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a3).GetAttribute("href"));
            Assert.AreEqual("blah", ((Element)dochtml0body1a3).GetAttribute("href"));

            var dochtml0body1a3Text0 = dochtml0body1a3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a3Text0.NodeType);
            Assert.AreEqual("aoe", dochtml0body1a3Text0.TextContent);
        }

        [Test]
        public void ObsoleteMarqueeElementWithContent()
        {
            var doc = (@"<a href=a>aa<marquee>aa<a href=b>bb</marquee>aa").ToHtmlDocument();

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

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0).GetAttribute("href"));
            Assert.AreEqual("a", ((Element)dochtml0body1a0).GetAttribute("href"));

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("aa", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a0marquee1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1a0marquee1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0marquee1).Attributes.Length);
            Assert.AreEqual("marquee", dochtml0body1a0marquee1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0marquee1.NodeType);

            var dochtml0body1a0marquee1Text0 = dochtml0body1a0marquee1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0marquee1Text0.NodeType);
            Assert.AreEqual("aa", dochtml0body1a0marquee1Text0.TextContent);

            var dochtml0body1a0marquee1a1 = dochtml0body1a0marquee1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0marquee1a1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1a0marquee1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0marquee1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0marquee1a1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1a0marquee1a1).GetAttribute("href"));
            Assert.AreEqual("b", ((Element)dochtml0body1a0marquee1a1).GetAttribute("href"));

            var dochtml0body1a0marquee1a1Text0 = dochtml0body1a0marquee1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0marquee1a1Text0.NodeType);
            Assert.AreEqual("bb", dochtml0body1a0marquee1a1Text0.TextContent);

            var dochtml0body1a0Text2 = dochtml0body1a0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text2.NodeType);
            Assert.AreEqual("aa", dochtml0body1a0Text2.TextContent);
        }

        [Test]
        public void GenerateImpliedEndForWbrElement()
        {
            var doc = (@"<wbr><strike><code></strike><code><strike></code>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1wbr0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1wbr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1wbr0).Attributes.Length);
            Assert.AreEqual("wbr", dochtml0body1wbr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1wbr0.NodeType);

            var dochtml0body1strike1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1strike1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1strike1).Attributes.Length);
            Assert.AreEqual("strike", dochtml0body1strike1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1strike1.NodeType);

            var dochtml0body1strike1code0 = dochtml0body1strike1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1strike1code0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1strike1code0).Attributes.Length);
            Assert.AreEqual("code", dochtml0body1strike1code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1strike1code0.NodeType);

            var dochtml0body1code2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1code2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1code2).Attributes.Length);
            Assert.AreEqual("code", dochtml0body1code2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1code2.NodeType);

            var dochtml0body1code2code0 = dochtml0body1code2.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1code2code0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1code2code0).Attributes.Length);
            Assert.AreEqual("code", dochtml0body1code2code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1code2code0.NodeType);

            var dochtml0body1code2code0strike0 = dochtml0body1code2code0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1code2code0strike0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1code2code0strike0).Attributes.Length);
            Assert.AreEqual("strike", dochtml0body1code2code0strike0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1code2code0strike0.NodeType);
        }

        [Test]
        public void StandardDoctypeWithCustomElementAndText()
        {
            var doc = (@"<!DOCTYPE html><spacer>foo").ToHtmlDocument();

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

            var dochtml1body1spacer0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1spacer0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1spacer0).Attributes.Length);
            Assert.AreEqual("spacer", dochtml1body1spacer0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1spacer0.NodeType);

            var dochtml1body1spacer0Text0 = dochtml1body1spacer0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1spacer0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1spacer0Text0.TextContent);
        }

        [Test]
        public void TitleSwitchToRawtextVerboseMeta()
        {
            var doc = (@"<title><meta></title><link><title><meta></title>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(3, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<meta>", dochtml0head0title0Text0.TextContent);

            var dochtml0head0link1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0head0link1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0link1).Attributes.Length);
            Assert.AreEqual("link", dochtml0head0link1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0link1.NodeType);

            var dochtml0head0title2 = dochtml0head0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0head0title2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title2).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title2.NodeType);

            var dochtml0head0title2Text0 = dochtml0head0title2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title2Text0.NodeType);
            Assert.AreEqual("<meta>", dochtml0head0title2Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void StyleSwitchToRawtextSkipComment()
        {
            var doc = (@"<style><!--</style><meta><script>--><link></script>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(3, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0style0Text0.TextContent);

            var dochtml0head0meta1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0head0meta1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0meta1).Attributes.Length);
            Assert.AreEqual("meta", dochtml0head0meta1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0meta1.NodeType);

            var dochtml0head0script2 = dochtml0head0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0head0script2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script2).Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script2.NodeType);

            var dochtml0head0script2Text0 = dochtml0head0script2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script2Text0.NodeType);
            Assert.AreEqual("--><link>", dochtml0head0script2Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void LinkElementShiftedBackToHead()
        {
            var doc = (@"<head><meta></head><link>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0meta0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0meta0).Attributes.Length);
            Assert.AreEqual("meta", dochtml0head0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0meta0.NodeType);

            var dochtml0head0link1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0head0link1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0link1).Attributes.Length);
            Assert.AreEqual("link", dochtml0head0link1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0link1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TableWithShiftedRowsAndCells()
        {
            var doc = (@"<table><tr><tr><td><td><span><th><span>X</table>").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr1 = dochtml0body1table0tbody0.ChildNodes[1];
            Assert.AreEqual(3, dochtml0body1table0tbody0tr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1.NodeType);

            var dochtml0body1table0tbody0tr1td0 = dochtml0body1table0tbody0tr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr1td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1td0.NodeType);

            var dochtml0body1table0tbody0tr1td1 = dochtml0body1table0tbody0tr1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr1td1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1td1).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr1td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1td1.NodeType);

            var dochtml0body1table0tbody0tr1td1span0 = dochtml0body1table0tbody0tr1td1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1td1span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1td1span0).Attributes.Length);
            Assert.AreEqual("span", dochtml0body1table0tbody0tr1td1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1td1span0.NodeType);

            var dochtml0body1table0tbody0tr1th2 = dochtml0body1table0tbody0tr1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr1th2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1th2).Attributes.Length);
            Assert.AreEqual("th", dochtml0body1table0tbody0tr1th2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1th2.NodeType);

            var dochtml0body1table0tbody0tr1th2span0 = dochtml0body1table0tbody0tr1th2.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr1th2span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody0tr1th2span0).Attributes.Length);
            Assert.AreEqual("span", dochtml0body1table0tbody0tr1th2span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1th2span0.NodeType);

            var dochtml0body1table0tbody0tr1th2span0Text0 = dochtml0body1table0tbody0tr1th2span0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr1th2span0Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1table0tbody0tr1th2span0Text0.TextContent);
        }

        [Test]
        public void BaseElementSelfClosedJustLikeLinkAndMetaButNotTitle()
        {
            var doc = (@"<body><body><base><link><meta><title><p></title><body><p></body>").ToHtmlDocument();

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
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1base0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1base0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1base0).Attributes.Length);
            Assert.AreEqual("base", dochtml0body1base0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1base0.NodeType);

            var dochtml0body1link1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1link1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1link1).Attributes.Length);
            Assert.AreEqual("link", dochtml0body1link1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1link1.NodeType);

            var dochtml0body1meta2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1meta2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1meta2).Attributes.Length);
            Assert.AreEqual("meta", dochtml0body1meta2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1meta2.NodeType);

            var dochtml0body1title3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1title3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1title3).Attributes.Length);
            Assert.AreEqual("title", dochtml0body1title3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1title3.NodeType);

            var dochtml0body1title3Text0 = dochtml0body1title3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1title3Text0.NodeType);
            Assert.AreEqual("<p>", dochtml0body1title3Text0.TextContent);

            var dochtml0body1p4 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(0, dochtml0body1p4.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p4).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p4.NodeType);
        }

        [Test]
        public void ParagraphElementVerboseInTextarea()
        {
            var doc = (@"<textarea><p></textarea>").ToHtmlDocument();

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

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1textarea0).Attributes.Length);
            Assert.AreEqual("textarea", dochtml0body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1textarea0.NodeType);

            var dochtml0body1textarea0Text0 = dochtml0body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1textarea0Text0.NodeType);
            Assert.AreEqual("<p>", dochtml0body1textarea0Text0.TextContent);
        }

        [Test]
        public void CommonImageTagMistakeAcceptedAsImg()
        {
            var doc = (@"<p><image></p>").ToHtmlDocument();

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

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0img0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0img0).Attributes.Length);
            Assert.AreEqual("img", dochtml0body1p0img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0img0.NodeType);
        }

        [Test]
        public void AnchorElementNotCopiedToTable()
        {
            var doc = (@"<a><table><a></table><p><a><div><a>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0a0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0a0.NodeType);

            var dochtml0body1a0table1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1a0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1a0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a0.NodeType);

            var dochtml0body1div2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1div2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div2).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div2.NodeType);

            var dochtml0body1div2a0 = dochtml0body1div2.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div2a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div2a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div2a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div2a0.NodeType);
        }

        [Test]
        public void MetaElementShiftedToHead()
        {
            var doc = (@"<head></p><meta><p>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0meta0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0meta0).Attributes.Length);
            Assert.AreEqual("meta", dochtml0head0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0meta0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);
        }

        [Test]
        public void MetaElementNotShiftedToHead()
        {
            var doc = (@"<head></html><meta><p>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1meta0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1meta0).Attributes.Length);
            Assert.AreEqual("meta", dochtml0body1meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1meta0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);
        }

        [Test]
        public void BoldFormattingNotCopiedToTableCell()
        {
            var doc = (@"<b><table><td><i></table>").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0table0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1b0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0.NodeType);

            var dochtml0body1b0table0tbody0 = dochtml0body1b0table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1b0table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0.NodeType);

            var dochtml0body1b0table0tbody0tr0 = dochtml0body1b0table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1b0table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0 = dochtml0body1b0table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1b0table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0i0 = dochtml0body1b0table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0table0tbody0tr0td0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1b0table0tbody0tr0td0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0i0.NodeType);
        }

        [Test]
        public void BoldFormattingNotCopiedToTableCellDespiteClosing()
        {
            var doc = (@"<b><table><td></b><i></table>").ToHtmlDocument();

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

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0table0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1b0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0.NodeType);

            var dochtml0body1b0table0tbody0 = dochtml0body1b0table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1b0table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0.NodeType);

            var dochtml0body1b0table0tbody0tr0 = dochtml0body1b0table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1b0table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0 = dochtml0body1b0table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1b0table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0.NodeType);

            var dochtml0body1b0table0tbody0tr0td0i0 = dochtml0body1b0table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0table0tbody0tr0td0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0table0tbody0tr0td0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1b0table0tbody0tr0td0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0table0tbody0tr0td0i0.NodeType);
        }

        [Test]
        public void AnotherHeadingOpenedWithinHeading()
        {
            var doc = (@"<h1><h2>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1h10 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1h10.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10).Attributes.Length);
            Assert.AreEqual("h1", dochtml0body1h10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10.NodeType);

            var dochtml0body1h21 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1h21.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h21).Attributes.Length);
            Assert.AreEqual("h2", dochtml0body1h21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h21.NodeType);
        }

        [Test]
        public void AnchorElementReOpenedInNewParagraph()
        {
            var doc = (@"<a><p><a></a></p></a>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1a0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a0.NodeType);

            var dochtml0body1p1a1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a1.NodeType);
        }

        [Test]
        public void BoldFormattingReOpenedInButtonElement()
        {
            var doc = (@"<b><button></b></button></b>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1b0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1button1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1button1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1).Attributes.Length);
            Assert.AreEqual("button", dochtml0body1button1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1.NodeType);

            var dochtml0body1button1b0 = dochtml0body1button1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1button1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1button1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1b0.NodeType);
        }

        [Test]
        public void UseObsoleteMarqueeElementInNewDiv()
        {
            var doc = (@"<p><b><div><marquee></p></b></div>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1b0marquee0 = dochtml0body1div1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1b0marquee0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0marquee0).Attributes.Length);
            Assert.AreEqual("marquee", dochtml0body1div1b0marquee0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0marquee0.NodeType);

            var dochtml0body1div1b0marquee0p0 = dochtml0body1div1b0marquee0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1b0marquee0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1b0marquee0p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div1b0marquee0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0marquee0p0.NodeType);
        }

        [Test]
        public void ScriptAndTitleElementsToHead()
        {
            var doc = (@"<script></script></div><title></title><p><p>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0script0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script0).Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);

            var dochtml0head0title1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0head0title1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title1).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title1.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);
        }

        [Test]
        public void HorizontalLineGeneratesImpliedEndTagForParagraph()
        {
            var doc = (@"<p><hr></p>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1hr1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1hr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1hr1).Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1hr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1hr1.NodeType);

            var dochtml0body1p2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1p2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p2).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p2.NodeType);
        }

        [Test]
        public void BoldFormattingElementInSelectContainer()
        {
            var doc = (@"<select><b><option><select><option></b></select>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1select0).Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1select0option0).Attributes.Length);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1option1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1option1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option1).Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1.NodeType);
        }

        [Test]
        public void MissingClosingHeadTag()
        {
            var doc = (@"<html><head><title></title><body></body></html>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void NestedTableWithAnchorElements()
        {
            var doc = (@"<a><table><td><a><table></table><a></tr><a></table><a>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0a0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0a0.NodeType);

            var dochtml0body1a0table1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1.NodeType);

            var dochtml0body1a0table1tbody0 = dochtml0body1a0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1a0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0.NodeType);

            var dochtml0body1a0table1tbody0tr0 = dochtml0body1a0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1a0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0 = dochtml0body1a0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1a0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1a0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a0 = dochtml0body1a0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0table1tbody0tr0td0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0table1tbody0tr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a0table0 = dochtml0body1a0table1tbody0tr0td0a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0table1tbody0tr0td0a0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a0table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1a0table1tbody0tr0td0a0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a0table0.NodeType);

            var dochtml0body1a0table1tbody0tr0td0a1 = dochtml0body1a0table1tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1a0table1tbody0tr0td0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0table1tbody0tr0td0a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0table1tbody0tr0td0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0table1tbody0tr0td0a1.NodeType);

            var dochtml0body1a1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1.NodeType);
        }

        [Test]
        public void MixingNewDivElementsInAnUnsortedList()
        {
            var doc = (@"<ul><li></li><div><li></div><li><li><div><li><address><li><b><em></b><li></ul>").ToHtmlDocument();

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

            var dochtml0body1ul0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(7, dochtml0body1ul0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0).Attributes.Length);
            Assert.AreEqual("ul", dochtml0body1ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0.NodeType);

            var dochtml0body1ul0li0 = dochtml0body1ul0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li0).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0.NodeType);

            var dochtml0body1ul0div1 = dochtml0body1ul0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ul0div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ul0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0div1.NodeType);

            var dochtml0body1ul0div1li0 = dochtml0body1ul0div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1ul0div1li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0div1li0).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0div1li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0div1li0.NodeType);

            var dochtml0body1ul0li2 = dochtml0body1ul0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1ul0li2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li2).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li2.NodeType);

            var dochtml0body1ul0li3 = dochtml0body1ul0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1ul0li3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li3).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li3.NodeType);

            var dochtml0body1ul0li3div0 = dochtml0body1ul0li3.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1ul0li3div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li3div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ul0li3div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li3div0.NodeType);

            var dochtml0body1ul0li4 = dochtml0body1ul0.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1ul0li4.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li4).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li4.NodeType);

            var dochtml0body1ul0li4address0 = dochtml0body1ul0li4.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1ul0li4address0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li4address0).Attributes.Length);
            Assert.AreEqual("address", dochtml0body1ul0li4address0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li4address0.NodeType);

            var dochtml0body1ul0li5 = dochtml0body1ul0.ChildNodes[5];
            Assert.AreEqual(1, dochtml0body1ul0li5.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li5).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li5.NodeType);

            var dochtml0body1ul0li5b0 = dochtml0body1ul0li5.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li5b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li5b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1ul0li5b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li5b0.NodeType);

            var dochtml0body1ul0li5b0em0 = dochtml0body1ul0li5b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1ul0li5b0em0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li5b0em0).Attributes.Length);
            Assert.AreEqual("em", dochtml0body1ul0li5b0em0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li5b0em0.NodeType);

            var dochtml0body1ul0li6 = dochtml0body1ul0.ChildNodes[6];
            Assert.AreEqual(0, dochtml0body1ul0li6.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li6).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li6.NodeType);
        }

        [Test]
        public void NestedUnsortedListElements()
        {
            var doc = (@"<ul><li><ul></li><li>a</li></ul></li></ul>").ToHtmlDocument();

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

            var dochtml0body1ul0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0).Attributes.Length);
            Assert.AreEqual("ul", dochtml0body1ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0.NodeType);

            var dochtml0body1ul0li0 = dochtml0body1ul0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li0).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0.NodeType);

            var dochtml0body1ul0li0ul0 = dochtml0body1ul0li0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li0ul0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li0ul0).Attributes.Length);
            Assert.AreEqual("ul", dochtml0body1ul0li0ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0ul0.NodeType);

            var dochtml0body1ul0li0ul0li0 = dochtml0body1ul0li0ul0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li0ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ul0li0ul0li0).Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li0ul0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0ul0li0.NodeType);

            var dochtml0body1ul0li0ul0li0Text0 = dochtml0body1ul0li0ul0li0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li0ul0li0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1ul0li0ul0li0Text0.TextContent);
        }

        [Test]
        public void FramesetWithFrameAndNoFramesCombination()
        {
            var doc = (@"<frameset><frame><frameset><frame></frameset><noframes></noframes></frameset>").ToHtmlDocument();

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

            var dochtml0frameset1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(3, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1).Attributes.Length);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);

            var dochtml0frameset1frame0 = dochtml0frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frame0).Attributes.Length);
            Assert.AreEqual("frame", dochtml0frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frame0.NodeType);

            var dochtml0frameset1frameset1 = dochtml0frameset1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0frameset1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frameset1).Attributes.Length);
            Assert.AreEqual("frameset", dochtml0frameset1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frameset1.NodeType);

            var dochtml0frameset1frameset1frame0 = dochtml0frameset1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0frameset1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frameset1frame0).Attributes.Length);
            Assert.AreEqual("frame", dochtml0frameset1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frameset1frame0.NodeType);

            var dochtml0frameset1noframes2 = dochtml0frameset1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0frameset1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1noframes2).Attributes.Length);
            Assert.AreEqual("noframes", dochtml0frameset1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1noframes2.NodeType);
        }

        [Test]
        public void TableInHeaderElement()
        {
            var doc = (@"<h1><table><td><h3></table><h3></h1>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1h10 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10).Attributes.Length);
            Assert.AreEqual("h1", dochtml0body1h10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10.NodeType);

            var dochtml0body1h10table0 = dochtml0body1h10.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1h10table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10table0.NodeType);

            var dochtml0body1h10table0tbody0 = dochtml0body1h10table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10table0tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1h10table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10table0tbody0.NodeType);

            var dochtml0body1h10table0tbody0tr0 = dochtml0body1h10table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10table0tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1h10table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10table0tbody0tr0.NodeType);

            var dochtml0body1h10table0tbody0tr0td0 = dochtml0body1h10table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1h10table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10table0tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1h10table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10table0tbody0tr0td0.NodeType);

            var dochtml0body1h10table0tbody0tr0td0h30 = dochtml0body1h10table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1h10table0tbody0tr0td0h30.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h10table0tbody0tr0td0h30).Attributes.Length);
            Assert.AreEqual("h3", dochtml0body1h10table0tbody0tr0td0h30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h10table0tbody0tr0td0h30.NodeType);

            var dochtml0body1h31 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1h31.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1h31).Attributes.Length);
            Assert.AreEqual("h3", dochtml0body1h31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1h31.NodeType);
        }

        [Test]
        public void TableWithColgroupThatContainsColsAndOtherColgroups()
        {
            var doc = (@"<table><colgroup><col><colgroup><col><col><col><colgroup><col><col><thead><tr><td></table>").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(4, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup0).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0col0 = dochtml0body1table0colgroup0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup0col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup0col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0col0.NodeType);

            var dochtml0body1table0colgroup1 = dochtml0body1table0.ChildNodes[1];
            Assert.AreEqual(3, dochtml0body1table0colgroup1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup1).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup1.NodeType);

            var dochtml0body1table0colgroup1col0 = dochtml0body1table0colgroup1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup1col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup1col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup1col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup1col0.NodeType);

            var dochtml0body1table0colgroup1col1 = dochtml0body1table0colgroup1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table0colgroup1col1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup1col1).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup1col1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup1col1.NodeType);

            var dochtml0body1table0colgroup1col2 = dochtml0body1table0colgroup1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1table0colgroup1col2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup1col2).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup1col2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup1col2.NodeType);

            var dochtml0body1table0colgroup2 = dochtml0body1table0.ChildNodes[2];
            Assert.AreEqual(2, dochtml0body1table0colgroup2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2.NodeType);

            var dochtml0body1table0colgroup2col0 = dochtml0body1table0colgroup2.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup2col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup2col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2col0.NodeType);

            var dochtml0body1table0colgroup2col1 = dochtml0body1table0colgroup2.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table0colgroup2col1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2col1).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup2col1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2col1.NodeType);

            var dochtml0body1table0thead3 = dochtml0body1table0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1table0thead3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0thead3).Attributes.Length);
            Assert.AreEqual("thead", dochtml0body1table0thead3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead3.NodeType);

            var dochtml0body1table0thead3tr0 = dochtml0body1table0thead3.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0thead3tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0thead3tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0thead3tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead3tr0.NodeType);

            var dochtml0body1table0thead3tr0td0 = dochtml0body1table0thead3tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0thead3tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0thead3tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0thead3tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead3tr0td0.NodeType);
        }

        [Test]
        public void TableWithMultipleColElements()
        {
            var doc = (@"<table><col><tbody><col><tr><col><td><col></table><col>").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(7, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup0).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0col0 = dochtml0body1table0colgroup0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup0col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup0col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0col0.NodeType);

            var dochtml0body1table0tbody1 = dochtml0body1table0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table0tbody1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody1).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody1.NodeType);

            var dochtml0body1table0colgroup2 = dochtml0body1table0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1table0colgroup2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2.NodeType);

            var dochtml0body1table0colgroup2col0 = dochtml0body1table0colgroup2.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup2col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup2col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2col0.NodeType);

            var dochtml0body1table0tbody3 = dochtml0body1table0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1table0tbody3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody3).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody3.NodeType);

            var dochtml0body1table0tbody3tr0 = dochtml0body1table0tbody3.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody3tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody3tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody3tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody3tr0.NodeType);

            var dochtml0body1table0colgroup4 = dochtml0body1table0.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1table0colgroup4.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup4).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup4.NodeType);

            var dochtml0body1table0colgroup4col0 = dochtml0body1table0colgroup4.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup4col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup4col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup4col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup4col0.NodeType);

            var dochtml0body1table0tbody5 = dochtml0body1table0.ChildNodes[5];
            Assert.AreEqual(1, dochtml0body1table0tbody5.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5.NodeType);

            var dochtml0body1table0tbody5tr0 = dochtml0body1table0tbody5.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody5tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody5tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5tr0.NodeType);

            var dochtml0body1table0tbody5tr0td0 = dochtml0body1table0tbody5tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody5tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody5tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5tr0td0.NodeType);

            var dochtml0body1table0colgroup6 = dochtml0body1table0.ChildNodes[6];
            Assert.AreEqual(1, dochtml0body1table0colgroup6.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup6).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup6.NodeType);

            var dochtml0body1table0colgroup6col0 = dochtml0body1table0colgroup6.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup6col0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup6col0).Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup6col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup6col0.NodeType);
        }

        [Test]
        public void ColgroupInTableAndTableSectionElements()
        {
            var doc = (@"<table><colgroup><tbody><colgroup><tr><colgroup><td><colgroup></table><colgroup>").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(7, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup0).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0tbody1 = dochtml0body1table0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1table0tbody1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody1).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody1.NodeType);

            var dochtml0body1table0colgroup2 = dochtml0body1table0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1table0colgroup2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup2).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup2.NodeType);

            var dochtml0body1table0tbody3 = dochtml0body1table0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0body1table0tbody3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody3).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody3.NodeType);

            var dochtml0body1table0tbody3tr0 = dochtml0body1table0tbody3.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody3tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody3tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody3tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody3tr0.NodeType);

            var dochtml0body1table0colgroup4 = dochtml0body1table0.ChildNodes[4];
            Assert.AreEqual(0, dochtml0body1table0colgroup4.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup4).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup4.NodeType);

            var dochtml0body1table0tbody5 = dochtml0body1table0.ChildNodes[5];
            Assert.AreEqual(1, dochtml0body1table0tbody5.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5.NodeType);

            var dochtml0body1table0tbody5tr0 = dochtml0body1table0tbody5.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody5tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody5tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5tr0.NodeType);

            var dochtml0body1table0tbody5tr0td0 = dochtml0body1table0tbody5tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table0tbody5tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0tbody5tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody5tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody5tr0td0.NodeType);

            var dochtml0body1table0colgroup6 = dochtml0body1table0.ChildNodes[6];
            Assert.AreEqual(0, dochtml0body1table0colgroup6.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table0colgroup6).Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup6.NodeType);
        }

        [Test]
        public void FormattingElementsClosedInsideBodyWithoutOpening()
        {
            var doc = (@"</strong></b></em></i></u></strike></s></blink></tt></pre></big></small></font></select></h1></h2></h3></h4></h5></h6></body></br></a></img></title></span></style></script></table></th></td></tr></frame></area></link></param></hr></input></col></base></meta></basefont></bgsound></embed></spacer></p></dd></dt></caption></colgroup></tbody></tfoot></thead></address></blockquote></center></dir></div></dl></fieldset></listing></menu></ol></ul></li></nobr></wbr></form></button></marquee></object></html></frameset></head></iframe></image></isindex></noembed></noframes></noscript></optgroup></option></plaintext></textarea>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1br0).Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);
        }

        [Test]
        public void FormattingElementsClosedInsideTableWithoutOpening()
        {
            var doc = (@"<table><tr></strong></b></em></i></u></strike></s></blink></tt></pre></big></small></font></select></h1></h2></h3></h4></h5></h6></body></br></a></img></title></span></style></script></table></th></td></tr></frame></area></link></param></hr></input></col></base></meta></basefont></bgsound></embed></spacer></p></dd></dt></caption></colgroup></tbody></tfoot></thead></address></blockquote></center></dir></div></dl></fieldset></listing></menu></ol></ul></li></nobr></wbr></form></button></marquee></object></html></frameset></head></iframe></image></isindex></noembed></noframes></noscript></optgroup></option></plaintext></textarea>").ToHtmlDocument();

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1br0).Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table1).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table1tbody0).Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table1tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1p2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1p2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p2).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p2.NodeType);
        }

        [Test]
        public void JustAFramesetElement()
        {
            var doc = (@"<frameset>").ToHtmlDocument();

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

            var dochtml0frameset1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1).Attributes.Length);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);
        }
    }
}