using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/domjs-unsafe.dat
    /// </summary>
    [TestClass]
    public class UserJsUnsafeTests
    {
        [TestMethod]
        public void Html5LibSvgCdata()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[foo
bar]]>");
            var html = doc.ChildNodes[0];
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Length);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var htmlhead = html.ChildNodes[0];
            Assert.AreEqual(0, htmlhead.ChildNodes.Length);
            Assert.AreEqual(0, htmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlhead.NodeType);

            var htmlbody = html.ChildNodes[1];
            Assert.AreEqual(1, htmlbody.ChildNodes.Length);
            Assert.AreEqual(0, htmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlbody.NodeType);

            var htmlbodysvg = htmlbody.ChildNodes[0];
            Assert.IsTrue(htmlbodysvg.IsInSvg);
            Assert.AreEqual(Namespaces.Svg, htmlbodysvg.NamespaceURI);
            Assert.AreEqual(1, htmlbodysvg.ChildNodes.Length);
            Assert.AreEqual(0, htmlbodysvg.Attributes.Length);
            Assert.AreEqual(NodeType.Element, htmlbodysvg.NodeType);

            var text = htmlbodysvg.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("foo\nbar", text.TextContent);
        }

        [TestMethod]
        public void Html5LibScriptDataCommentStarted()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!--foo" + Specification.NULL.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0];
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--foo" + Specification.REPLACEMENT.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataCommentFinishing()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!-- foo--" + Specification.NULL.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0];
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo--" + Specification.REPLACEMENT.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataEnding()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!-- foo-<</script>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0];
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo-<", text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataParagraph()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!--<p></script>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0];
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--<p>", text.TextContent);
                        
            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibDoctypeInHeadImplicit()
        {
            var doc = DocumentBuilder.Html(@"<html><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibDoctypeInBodyImplicit()
        {
            var doc = DocumentBuilder.Html(@"<html><head></head><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

        }
    }
}
