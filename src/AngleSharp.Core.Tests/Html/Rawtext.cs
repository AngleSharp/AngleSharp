namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests5.dat
    /// </summary>
    [TestFixture]
    public class RawtextTests
    {
        [Test]
        public void IllegalCommentStartInStyleElement()
        {
            var doc = (@"<style> <!-- </style>x").ToHtmlDocument();

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

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!-- ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInStyleElement()
        {
            var doc = (@"<style> <!-- </style> --> </style>x").ToHtmlDocument();

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

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!-- ", dochtml0head0style0Text0.TextContent);

            var dochtml0head0Text1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0head0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0head0Text1.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void IllegalCommentInStyleTag()
        {
            var doc = (@"<style> <!--> </style>x").ToHtmlDocument();

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

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!--> ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void CommentInStyleElement()
        {
            var doc = (@"<style> <!---> </style>x").ToHtmlDocument();

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

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!---> ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void CommentInIframeElement()
        {
            var doc = (@"<iframe> <!---> </iframe>x").ToHtmlDocument();

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

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1iframe0).Attributes.Length);
            Assert.AreEqual("iframe", dochtml0body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1iframe0.NodeType);

            var dochtml0body1iframe0Text0 = dochtml0body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1iframe0Text0.NodeType);
            Assert.AreEqual(" <!---> ", dochtml0body1iframe0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("x", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void StartOfCommentInIframeElement()
        {
            var doc = (@"<iframe> <!--- </iframe>->x</iframe> --> </iframe>x").ToHtmlDocument();

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

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1iframe0).Attributes.Length);
            Assert.AreEqual("iframe", dochtml0body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1iframe0.NodeType);

            var dochtml0body1iframe0Text0 = dochtml0body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1iframe0Text0.NodeType);
            Assert.AreEqual(" <!--- ", dochtml0body1iframe0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("->x --> x", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void StartOfCommentInScriptElement()
        {
            var doc = (@"<script> <!-- </script> --> </script>x").ToHtmlDocument();

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
            Assert.AreEqual(" <!-- ", dochtml0head0script0Text0.TextContent);

            var dochtml0head0Text1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0head0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0head0Text1.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInTitleElement()
        {
            var doc = (@"<title> <!-- </title> --> </title>x").ToHtmlDocument();

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

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual(" <!-- ", dochtml0head0title0Text0.TextContent);

            var dochtml0head0Text1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0head0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0head0Text1.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInTextareaElement()
        {
            var doc = (@"<textarea> <!--- </textarea>->x</textarea> --> </textarea>x").ToHtmlDocument();

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

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1textarea0).Attributes.Length);
            Assert.AreEqual("textarea", dochtml0body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1textarea0.NodeType);

            var dochtml0body1textarea0Text0 = dochtml0body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1textarea0Text0.NodeType);
            Assert.AreEqual(" <!--- ", dochtml0body1textarea0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("->x --> x", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void RawtextHalfCommentInStyleElement()
        {
            var doc = (@"<style> <!</-- </style>x").ToHtmlDocument();

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

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!</-- ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void XmpInParagraphElement()
        {
            var doc = (@"<p><xmp></xmp>").ToHtmlDocument();

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
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1xmp1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1xmp1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1xmp1).Attributes.Length);
            Assert.AreEqual("xmp", dochtml0body1xmp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1xmp1.NodeType);
        }

        [Test]
        public void RawtextInXmpTag()
        {
            var doc = (@"<xmp> <!-- > --> </xmp>").ToHtmlDocument();

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

            var dochtml0body1xmp0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1xmp0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1xmp0).Attributes.Length);
            Assert.AreEqual("xmp", dochtml0body1xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1xmp0.NodeType);

            var dochtml0body1xmp0Text0 = dochtml0body1xmp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1xmp0Text0.NodeType);
            Assert.AreEqual(" <!-- > --> ", dochtml0body1xmp0Text0.TextContent);
        }

        [Test]
        public void EntityInTitleTag()
        {
            var doc = (@"<title>&amp;</title>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("&", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void CommentAndEntityInTitleText()
        {
            var doc = (@"<title><!--&amp;--></title>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<!--&-->", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TitleTriggersRawtextMode()
        {
            var doc = (@"<title><!--</title>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Length);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void NoScriptTriggersRawtextMode()
        {
            var config = Configuration.Default.WithScripting();
            var doc = (@"<noscript><!--</noscript>--></noscript>").ToHtmlDocument(config);

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

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0noscript0).Attributes.Length);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Text0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0noscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void NoScriptElementWithComment()
        {
            var doc = (@"<noscript><!--</noscript>--></noscript>").ToHtmlDocument();

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

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0noscript0).Attributes.Length);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Comment0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0head0noscript0Comment0.NodeType);
            Assert.AreEqual(@"</noscript>", dochtml0head0noscript0Comment0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
    }
}