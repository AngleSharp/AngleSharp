using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/scriptdata01.dat
    /// </summary>
    [TestFixture]
    public class ScriptDataTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ScriptWithQuotedHelloText()
        {
            var doc = Html(@"FOO<script>'Hello'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'Hello'", dochtml0body1script1Text0.TextContent);


            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithNoContent()
        {
            var doc = Html(@"FOO<script></script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithNoContentAndSpaceInClosingTag()
        {
            var doc = Html(@"FOO<script></script >BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ScriptWithNoContentAndSelfClosingClosingTag()
        {
            var doc = Html(@"FOO<script></script/>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ScriptWithNoContentAndSpacedSelfClosingClosingTag()
        {
            var doc = Html(@"FOO<script></script/ >BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);


        }

        [Test]
        public void ScriptWithAttributeAndWrongClosingTag()
        {
            var doc = Html(@"FOO<script type=""text/plain""></scriptx>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("</scriptx>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributeInClosingTag()
        {
            var doc = Html(@"FOO<script></script foo="" > "" dd>BAR");
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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);


        }

        [Test]
        public void ScriptWithQuotedLtInText()
        {
            var doc = Html(@"FOO<script>'<'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedLtEmInText()
        {
            var doc = Html(@"FOO<script>'<!'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedLtEmDashInText()
        {
            var doc = Html(@"FOO<script>'<!-'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedCommentStartInText()
        {
            var doc = Html(@"FOO<script>'<!--'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!--'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedCommentStartAndDashInText()
        {
            var doc = Html(@"FOO<script>'<!---'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!---'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedEmptyCommentInText()
        {
            var doc = Html(@"FOO<script>'<!-->'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedEmptyShortCommentInText()
        {
            var doc = Html(@"FOO<script>'<!-->'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedPotatoCommentInText()
        {
            var doc = Html(@"FOO<script>'<!-- potato'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- potato'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithQuotedScriptEndInComment()
        {
            var doc = Html(@"FOO<script>'<!-- <sCrIpt'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithAttributesAndQuotedScriptEnd()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt>'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt>'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributesAndQuotedScriptStart()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> -'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributesAndQuotedScriptStartMoreDashes()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> --'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> --'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithValidCommentAndStartInQuotes()
        {
            var doc = Html(@"FOO<script>'<!-- <sCrIpt> -->'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ScriptWithAttributeAndToleratedStartInCommentAndQuotes()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> --!>'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> --!>'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributeAndInvalidCommentAndQuotes()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> -- >'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -- >'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotes()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt '</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt '</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesTrailingSolidus()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt/'</script>BAR");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt/'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [Test]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesTrailingBackslash()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt\'</script>BAR");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual(@"'<!-- <sCrIpt\'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesConsistentlyClosed()
        {
            var doc = Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt/'</script>BAR</script>QUX");

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
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.GetAttribute("type"));

            var dochtml0body1script1Text0 = dochtml0body1script1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt/'</script>BAR", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("QUX", dochtml0body1Text2.TextContent);
        }
    }
}
