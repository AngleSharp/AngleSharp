namespace AngleSharp.Core.Tests
{
    using System;
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
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        static IDocument HtmlWithScripting(String code)
        {
            var scripting = new EnableScripting();
            var config = Configuration.Default.With(scripting);
            return code.ToHtmlDocument(config);
        }

        [Test]
        public void IllegalCommentStartInStyleElement()
        {
            var doc = Html(@"<style> <!-- </style>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!-- ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInStyleElement()
        {
            var doc = Html(@"<style> <!-- </style> --> </style>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
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
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void IllegalCommentInStyleTag()
        {
            var doc = Html(@"<style> <!--> </style>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!--> ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void CommentInStyleElement()
        {
            var doc = Html(@"<style> <!---> </style>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!---> ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void CommentInIframeElement()
        {
            var doc = Html(@"<iframe> <!---> </iframe>x");

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

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1iframe0).Attributes.Count);
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
            var doc = Html(@"<iframe> <!--- </iframe>->x</iframe> --> </iframe>x");

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

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1iframe0).Attributes.Count);
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
            var doc = Html(@"<script> <!-- </script> --> </script>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0script0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script0).Attributes.Count);
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
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInTitleElement()
        {
            var doc = Html(@"<title> <!-- </title> --> </title>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Count);
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
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("--> x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void StartOfCommentInTextareaElement()
        {
            var doc = Html(@"<textarea> <!--- </textarea>->x</textarea> --> </textarea>x");

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

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1textarea0).Attributes.Count);
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
            var doc = Html(@"<style> <!</-- </style>x");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual(" <!</-- ", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void XmpInParagraphElement()
        {
            var doc = Html(@"<p><xmp></xmp>");

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
            Assert.AreEqual(0, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1xmp1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1xmp1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1xmp1).Attributes.Count);
            Assert.AreEqual("xmp", dochtml0body1xmp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1xmp1.NodeType);
        }

        [Test]
        public void RawtextInXmpTag()
        {
            var doc = Html(@"<xmp> <!-- > --> </xmp>");

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1xmp0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1xmp0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1xmp0).Attributes.Count);
            Assert.AreEqual("xmp", dochtml0body1xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1xmp0.NodeType);

            var dochtml0body1xmp0Text0 = dochtml0body1xmp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1xmp0Text0.NodeType);
            Assert.AreEqual(" <!-- > --> ", dochtml0body1xmp0Text0.TextContent);
        }

        [Test]
        public void EntityInTitleTag()
        {
            var doc = Html(@"<title>&amp;</title>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("&", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void CommentAndEntityInTitleText()
        {
            var doc = Html(@"<title><!--&amp;--></title>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<!--&-->", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TitleTriggersRawtextMode()
        {
            var doc = Html(@"<title><!--</title>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void NoScriptTriggersRawtextMode()
        {
            var doc = HtmlWithScripting(@"<noscript><!--</noscript>--></noscript>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0noscript0).Attributes.Count);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Text0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0noscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void NoScriptElementWithComment()
        {
            var doc = Html(@"<noscript><!--</noscript>--></noscript>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0noscript0).Attributes.Count);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Comment0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0head0noscript0Comment0.NodeType);
            Assert.AreEqual(@"</noscript>", dochtml0head0noscript0Comment0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
    }
}