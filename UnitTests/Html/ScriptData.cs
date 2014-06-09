using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/scriptdata01.dat
    /// </summary>
    [TestClass]
    public class ScriptDataTests
    {
        [TestMethod]
        public void ScriptWithQuotedHelloText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'Hello'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'Hello'", dochtml0body1script1Text0.TextContent);


            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithNoContent()
        {
            var doc = DocumentBuilder.Html(@"FOO<script></script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithNoContentAndSpaceInClosingTag()
        {
            var doc = DocumentBuilder.Html(@"FOO<script></script >BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [TestMethod]
        public void ScriptWithNoContentAndSelfClosingClosingTag()
        {
            var doc = DocumentBuilder.Html(@"FOO<script></script/>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [TestMethod]
        public void ScriptWithNoContentAndSpacedSelfClosingClosingTag()
        {
            var doc = DocumentBuilder.Html(@"FOO<script></script/ >BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);


        }

        [TestMethod]
        public void ScriptWithAttributeAndWrongClosingTag()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain""></scriptx>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("</scriptx>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeInClosingTag()
        {
            var doc = DocumentBuilder.Html(@"FOO<script></script foo="" > "" dd>BAR");
            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(0, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);


        }

        [TestMethod]
        public void ScriptWithQuotedLtInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedLtEmInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedLtEmDashInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedCommentStartInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!--'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!--'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedCommentStartAndDashInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!---'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!---'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedEmptyCommentInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-->'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedEmptyShortCommentInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-->'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedPotatoCommentInText()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-- potato'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- potato'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithQuotedScriptEndInComment()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-- <sCrIpt'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributesAndQuotedScriptEnd()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt>'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt>'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributesAndQuotedScriptStart()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> -'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributesAndQuotedScriptStartMoreDashes()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> --'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> --'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithValidCommentAndStartInQuotes()
        {
            var doc = DocumentBuilder.Html(@"FOO<script>'<!-- <sCrIpt> -->'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -->'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeAndToleratedStartInCommentAndQuotes()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> --!>'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> --!>'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeAndInvalidCommentAndQuotes()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt> -- >'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt> -- >'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotes()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt '</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt '</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesTrailingSolidus()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt/'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(2, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt/'</script>BAR", dochtml0body1script1Text0.TextContent);
        }

        [TestMethod]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesTrailingBackslash()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt\'</script>BAR");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual(@"'<!-- <sCrIpt\'", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAR", dochtml0body1Text2.TextContent);

        }

        [TestMethod]
        public void ScriptWithAttributeAndInvalidStartOfCommentAndQuotesConsistentlyClosed()
        {
            var doc = DocumentBuilder.Html(@"FOO<script type=""text/plain"">'<!-- <sCrIpt/'</script>BAR</script>QUX");

            var dochtml0 = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml0.Childs.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.Childs[0] as Element;
            Assert.AreEqual(0, dochtml0head0.Childs.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.Childs[1] as Element;
            Assert.AreEqual(3, dochtml0body1.Childs.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1script1 = dochtml0body1.Childs[1] as Element;
            Assert.AreEqual(1, dochtml0body1script1.Childs.Length);
            Assert.AreEqual(1, dochtml0body1script1.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script1.NodeType);
            Assert.AreEqual("text/plain", dochtml0body1script1.Attributes["type"].Value);

            var dochtml0body1script1Text0 = dochtml0body1script1.Childs[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script1Text0.NodeType);
            Assert.AreEqual("'<!-- <sCrIpt/'</script>BAR", dochtml0body1script1Text0.TextContent);

            var dochtml0body1Text2 = dochtml0body1.Childs[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("QUX", dochtml0body1Text2.TextContent);
        }
    }
}
