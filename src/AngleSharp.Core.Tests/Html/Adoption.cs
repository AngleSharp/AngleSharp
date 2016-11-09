namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/adoption01.dat,
    /// tree-construction/adoption02.dat
    /// </summary>
    [TestFixture]
    public class AdoptionTests
    {
        [Test]
        public void AdoptAnchorInOpenParagraph()
        {
            var doc = (@"<a><p></a></p>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1a0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1p1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1a0.NodeType);
        }

        [Test]
        public void AdoptAnchorWithContentInOpenParagraph()
        {
            var doc = (@"<a>1<p>2</a>3</p>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p1.ChildNodes.Length);
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
            Assert.AreEqual("2", dochtml0body1p1a0Text0.TextContent);

            var dochtml0body1p1Text1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text1.NodeType);
            Assert.AreEqual("3", dochtml0body1p1Text1.TextContent);
        }

        [Test]
        public void AdoptAnchorWithContentInOpenButton()
        {
            var doc = (@"<a>1<button>2</a>3</button>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1button1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1button1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1).Attributes.Length);
            Assert.AreEqual("button", dochtml0body1button1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1.NodeType);

            var dochtml0body1button1a0 = dochtml0body1button1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1button1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1button1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1button1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button1a0.NodeType);

            var dochtml0body1button1a0Text0 = dochtml0body1button1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1button1a0Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1button1a0Text0.TextContent);

            var dochtml0body1button1Text1 = dochtml0body1button1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1button1Text1.NodeType);
            Assert.AreEqual("3", dochtml0body1button1Text1.TextContent);
        }

        [Test]
        public void AdoptBoldWithContentInOpenAnchor()
        {
            var doc = (@"<a>1<b>2</a>3</b>").ToHtmlDocument();

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

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a0b1 = dochtml0body1a0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b1.NodeType);

            var dochtml0body1a0b1Text0 = dochtml0body1a0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0b1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1a0b1Text0.TextContent);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1Text0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1b1Text0.TextContent);
        }

        [Test]
        public void AdoptAnchorWithContentInOpenDiv()
        {
            var doc = (@"<a>1<div>2<div>3</a>4</div>5</div>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1a0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1a0.NodeType);

            var dochtml0body1div1a0Text0 = dochtml0body1div1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1a0Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1div1a0Text0.TextContent);

            var dochtml0body1div1div1 = dochtml0body1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1div1.NodeType);

            var dochtml0body1div1div1a0 = dochtml0body1div1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1div1a0.NodeType);

            var dochtml0body1div1div1a0Text0 = dochtml0body1div1div1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1div1a0Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1div1div1a0Text0.TextContent);

            var dochtml0body1div1div1Text1 = dochtml0body1div1div1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1div1Text1.NodeType);
            Assert.AreEqual("4", dochtml0body1div1div1Text1.TextContent);

            var dochtml0body1div1Text2 = dochtml0body1div1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1Text2.NodeType);
            Assert.AreEqual("5", dochtml0body1div1Text2.TextContent);
        }

        [Test]
        public void AdoptAnchorWithContentInOpenParagraphWithContentInTable()
        {
            var doc = (@"<table><a>1<p>2</a>3</p>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p1.ChildNodes.Length);
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
            Assert.AreEqual("2", dochtml0body1p1a0Text0.TextContent);

            var dochtml0body1p1Text1 = dochtml0body1p1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text1.NodeType);
            Assert.AreEqual("3", dochtml0body1p1Text1.TextContent);

            var dochtml0body1table2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table2).Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2.NodeType);
        }

        [Test]
        public void AdoptBoldAndAnchorInOpenParagraph()
        {
            var doc = (@"<b><b><a><p></a>").ToHtmlDocument();

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

            var dochtml0body1b0b0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0.NodeType);

            var dochtml0body1b0b0a0 = dochtml0body1b0b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0b0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b0b0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0a0.NodeType);

            var dochtml0body1b0b0p1 = dochtml0body1b0b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0b0p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1b0b0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0p1.NodeType);

            var dochtml0body1b0b0p1a0 = dochtml0body1b0b0p1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0b0p1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0p1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b0b0p1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0p1a0.NodeType);
        }

        [Test]
        public void AdoptBoldInAnchorAndAnchorWithBoldInOpenParagraph()
        {
            var doc = (@"<b><a><b><p></a>").ToHtmlDocument();

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

            var dochtml0body1b0a0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0a0.NodeType);

            var dochtml0body1b0a0b0 = dochtml0body1b0a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0a0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0a0b0.NodeType);

            var dochtml0body1b0b1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b1.NodeType);

            var dochtml0body1b0b1p0 = dochtml0body1b0b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0b1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1b0b1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b1p0.NodeType);

            var dochtml0body1b0b1p0a0 = dochtml0body1b0b1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b0b1p0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b1p0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b0b1p0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b1p0a0.NodeType);

        }

        [Test]
        public void AdoptAnchorAndBoldInOpenParagraph()
        {
            var doc = (@"<a><b><b><p></a>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0b0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0.NodeType);

            var dochtml0body1a0b0b0 = dochtml0body1a0b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1a0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0b0b0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1b0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1b0.NodeType);

            var dochtml0body1b1b0p0 = dochtml0body1b1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1b0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1b0p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1b1b0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1b0p0.NodeType);

            var dochtml0body1b1b0p0a0 = dochtml0body1b1b0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1b1b0p0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1b0p0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1b1b0p0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1b0p0a0.NodeType);
        }

        [Test]
        public void AdoptStrikeWithContentCopyAttributes()
        {
            var doc = (@"<p>1<s id=""A"">2<b id=""B"">3</p>4</s>5</b>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0Text0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1p0Text0.TextContent);

            var dochtml0body1p0s1 = dochtml0body1p0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1p0s1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1p0s1).Attributes.Length);
            Assert.AreEqual("s", dochtml0body1p0s1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0s1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1p0s1).GetAttribute("id"));
            Assert.AreEqual("A", ((Element)dochtml0body1p0s1).GetAttribute("id"));

            var dochtml0body1p0s1Text0 = dochtml0body1p0s1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0s1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1p0s1Text0.TextContent);

            var dochtml0body1p0s1b1 = dochtml0body1p0s1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p0s1b1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1p0s1b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0s1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0s1b1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1p0s1b1).GetAttribute("id"));
            Assert.AreEqual("B", ((Element)dochtml0body1p0s1b1).GetAttribute("id"));

            var dochtml0body1p0s1b1Text0 = dochtml0body1p0s1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0s1b1Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1p0s1b1Text0.TextContent);

            var dochtml0body1s1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1s1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1s1).Attributes.Length);
            Assert.AreEqual("s", dochtml0body1s1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1s1.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1s1).GetAttribute("id"));
            Assert.AreEqual("A", ((Element)dochtml0body1s1).GetAttribute("id"));

            var dochtml0body1s1b0 = dochtml0body1s1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1s1b0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1s1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1s1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1s1b0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1s1b0).GetAttribute("id"));
            Assert.AreEqual("B", ((Element)dochtml0body1s1b0).GetAttribute("id"));

            var dochtml0body1s1b0Text0 = dochtml0body1s1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1s1b0Text0.NodeType);
            Assert.AreEqual("4", dochtml0body1s1b0Text0.TextContent);

            var dochtml0body1b2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1b2.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1b2).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b2.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1b2).GetAttribute("id"));
            Assert.AreEqual("B", ((Element)dochtml0body1b2).GetAttribute("id"));

            var dochtml0body1b2Text0 = dochtml0body1b2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b2Text0.NodeType);
            Assert.AreEqual("5", dochtml0body1b2Text0.TextContent);
        }

        [Test]
        public void AdoptAnchorInTableWithContent()
        {
            var doc = (@"<table><a>1<td>2</td>3</table>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1a0Text0.TextContent);

            var dochtml0body1a1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a1.NodeType);

            var dochtml0body1a1Text0 = dochtml0body1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a1Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1a1Text0.TextContent);

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

            var dochtml0body1table2tbody0tr0td0Text0 = dochtml0body1table2tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table2tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1table2tbody0tr0td0Text0.TextContent);

        }

        [Test]
        public void AdoptContentInTable()
        {
            var doc = (@"<table>A<td>B</td>C</table>").ToHtmlDocument();

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("AC", dochtml0body1Text0.TextContent);

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
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table1tbody0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0tr0td0 = dochtml0body1table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1table1tbody0tr0td0).Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0td0.NodeType);

            var dochtml0body1table1tbody0tr0td0Text0 = dochtml0body1table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table1tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1table1tbody0tr0td0Text0.TextContent);

        }

        [Test]
        public void AdoptAnchorInForeignSvgElementWithRowAndInput()
        {
            var doc = (@"<a><svg><tr><input></a>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0svg0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0svg0).Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1a0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0svg0.NodeType);

            var dochtml0body1a0svg0tr0 = dochtml0body1a0svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0svg0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0svg0tr0).Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1a0svg0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0svg0tr0.NodeType);

            var dochtml0body1a0svg0tr0input0 = dochtml0body1a0svg0tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0svg0tr0input0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1a0svg0tr0input0).Attributes.Length);
            Assert.AreEqual("input", dochtml0body1a0svg0tr0input0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0svg0tr0input0.NodeType);
        }

        [Test]
        public void AdoptAnchorAndBoldInOpenDivElements()
        {
            var doc = (@"<div><a><b><div><div><div><div><div><div><div><div><div><div></a>").ToHtmlDocument();

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

            var dochtml0body1div0a0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0.NodeType);

            var dochtml0body1div0a0b0 = dochtml0body1div0a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0b0.NodeType);

            var dochtml0body1div0b1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1.NodeType);

            var dochtml0body1div0b1div0 = dochtml0body1div0b1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0b1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0.NodeType);

            var dochtml0body1div0b1div0a0 = dochtml0body1div0b1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0a0.NodeType);

            var dochtml0body1div0b1div0div1 = dochtml0body1div0b1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1.NodeType);

            var dochtml0body1div0b1div0div1a0 = dochtml0body1div0b1div0div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1 = dochtml0body1div0b1div0div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1a0 = dochtml0body1div0b1div0div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1 = dochtml0body1div0b1div0div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1div1a0 = dochtml0body1div0b1div0div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1 = dochtml0body1div0b1div0div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1a0 = dochtml0body1div0b1div0div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1 = dochtml0body1div0b1div0div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1a0 = dochtml0body1div0b1div0div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1 = dochtml0body1div0b1div0div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div0b1div0div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1a0 = dochtml0body1div0b1div0div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1div1 = dochtml0body1div0b1div0div1div1div1div1div1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0b1div0div1div1div1div1div1div1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1div1div1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1div1.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1div1a0 = dochtml0body1div0b1div0div1div1div1div1div1div1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0b1div0div1div1div1div1div1div1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0 = dochtml0body1div0b1div0div1div1div1div1div1div1div1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0.NodeType);

            var dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0div0 = dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b1div0div1div1div1div1div1div1div1a0div0div0.NodeType);
        }

        [Test]
        public void AdoptAnchorBoldUnderlineItalicCodeInOpenDiv()
        {
            var doc = (@"<div><a><b><u><i><code><div></a>").ToHtmlDocument();

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

            var dochtml0body1div0a0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0.NodeType);

            var dochtml0body1div0a0b0 = dochtml0body1div0a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0b0.NodeType);

            var dochtml0body1div0a0b0u0 = dochtml0body1div0a0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0a0b0u0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0b0u0).Attributes.Length);
            Assert.AreEqual("u", dochtml0body1div0a0b0u0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0b0u0.NodeType);

            var dochtml0body1div0a0b0u0i0 = dochtml0body1div0a0b0u0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0a0b0u0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0b0u0i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0a0b0u0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0b0u0i0.NodeType);

            var dochtml0body1div0a0b0u0i0code0 = dochtml0body1div0a0b0u0i0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0a0b0u0i0code0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0a0b0u0i0code0).Attributes.Length);
            Assert.AreEqual("code", dochtml0body1div0a0b0u0i0code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0a0b0u0i0code0.NodeType);

            var dochtml0body1div0u1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div0u1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0u1).Attributes.Length);
            Assert.AreEqual("u", dochtml0body1div0u1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0u1.NodeType);

            var dochtml0body1div0u1i0 = dochtml0body1div0u1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0u1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0u1i0).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1div0u1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0u1i0.NodeType);

            var dochtml0body1div0u1i0code0 = dochtml0body1div0u1i0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0u1i0code0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0u1i0code0).Attributes.Length);
            Assert.AreEqual("code", dochtml0body1div0u1i0code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0u1i0code0.NodeType);

            var dochtml0body1div0u1i0code0div0 = dochtml0body1div0u1i0code0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0u1i0code0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0u1i0code0div0).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0u1i0code0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0u1i0code0div0.NodeType);

            var dochtml0body1div0u1i0code0div0a0 = dochtml0body1div0u1i0code0div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0u1i0code0div0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div0u1i0code0div0a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div0u1i0code0div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0u1i0code0div0a0.NodeType);
        }

        [Test]
        public void AdoptBoldWithContent()
        {
            var doc = (@"<b><b><b><b>x</b></b></b></b>y").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0.NodeType);

            var dochtml0body1b0b0 = dochtml0body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0.NodeType);

            var dochtml0body1b0b0b0 = dochtml0body1b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0b0.NodeType);

            var dochtml0body1b0b0b0b0 = dochtml0body1b0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0b0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1b0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0b0b0b0.NodeType);

            var dochtml0body1b0b0b0b0Text0 = dochtml0body1b0b0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0b0b0b0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1b0b0b0b0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("y", dochtml0body1Text1.TextContent);

        }

        [Test]
        public void AdoptBoldInOpenParagraphWithContent()
        {
            var doc = (@"<p><b><b><b><b><p>x").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);

            var dochtml0body1p0b0b0 = dochtml0body1p0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0.NodeType);

            var dochtml0body1p0b0b0b0 = dochtml0body1p0b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0.NodeType);

            var dochtml0body1p0b0b0b0b0 = dochtml0body1p0b0b0b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0b0b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0b0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p0b0b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0b0b0b0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1b0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0.NodeType);

            var dochtml0body1p1b0b0 = dochtml0body1p1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0.NodeType);

            var dochtml0body1p1b0b0b0 = dochtml0body1p1b0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p1b0b0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p1b0b0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1p1b0b0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1b0b0b0.NodeType);

            var dochtml0body1p1b0b0b0Text0 = dochtml0body1p1b0b0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1b0b0b0Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1p1b0b0b0Text0.TextContent);

        }

        [Test]
        public void AdoptBoldAndItalicWithContentInOpenParagraph()
        {
            var doc = (@"<b>1<i>2<p>3</b>4").ToHtmlDocument();

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
            Assert.AreEqual("1", dochtml0body1b0Text0.TextContent);

            var dochtml0body1b0i1 = dochtml0body1b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b0i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b0i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1b0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b0i1.NodeType);

            var dochtml0body1b0i1Text0 = dochtml0body1b0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b0i1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1b0i1Text0.TextContent);

            var dochtml0body1i1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1i1).Attributes.Length);
            Assert.AreEqual("i", dochtml0body1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1i1.NodeType);

            var dochtml0body1i1p0 = dochtml0body1i1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1i1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1i1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1i1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1i1p0.NodeType);

            var dochtml0body1i1p0b0 = dochtml0body1i1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1i1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1i1p0b0).Attributes.Length);
            Assert.AreEqual("b", dochtml0body1i1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1i1p0b0.NodeType);

            var dochtml0body1i1p0b0Text0 = dochtml0body1i1p0b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1i1p0b0Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1i1p0b0Text0.TextContent);

            var dochtml0body1i1p0Text1 = dochtml0body1i1p0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1i1p0Text1.NodeType);
            Assert.AreEqual("4", dochtml0body1i1p0Text1.TextContent);

        }

        [Test]
        public void AdoptAnchorInOpenDivWithStyleAndAddress()
        {
            var doc = (@"<a><div><style></style><address><a>").ToHtmlDocument();

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

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1a0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1a0.NodeType);

            var dochtml0body1div1a0style0 = dochtml0body1div1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1a0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1a0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0body1div1a0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1a0style0.NodeType);

            var dochtml0body1div1address1 = dochtml0body1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1div1address1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1address1).Attributes.Length);
            Assert.AreEqual("address", dochtml0body1div1address1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1address1.NodeType);

            var dochtml0body1div1address1a0 = dochtml0body1div1address1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div1address1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1address1a0).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1address1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1address1a0.NodeType);

            var dochtml0body1div1address1a1 = dochtml0body1div1address1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div1address1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1address1a1).Attributes.Length);
            Assert.AreEqual("a", dochtml0body1div1address1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1address1a1.NodeType);

        }
    }
}